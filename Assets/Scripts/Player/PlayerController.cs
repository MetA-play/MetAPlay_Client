
using Define;
using Google.Protobuf.Protocol;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

/// <summary>
/// 2022.12.05 / LJ
/// 플레이어 조작 관련 스크립트
/// </summary>
public class PlayerController : NetworkingObject
{
    private PlayerStateManager playerStateManager;

    [Header("Player Movement Stat")]
    [Range(0f, 100f)] [SerializeField] private float speed;
    CharacterController controller;
    [SerializeField] private bool jump; // 점프 중이라면 true
    [SerializeField] private LayerMask ground;
    [SerializeField] [Range(0f, 10f)] private float jumpHeight;
    [SerializeField] [Range(0f, 10f)] private float jumpTimeout;
    private float jumpTimer;

    private Rigidbody rb;


    private float movementX;
    private float movementY;

    [Header("Player Rotation")]
    [SerializeField] private Camera cam;
    [SerializeField] private float targetRotation = 0f;
    [SerializeField] private float rotationVelocity;
    private float rotationTime = 0.12f;
    

    [Header("Player Gravity")]
    [SerializeField] [Range(-20f, 20f)] private float gravity = -9.81f;
    [SerializeField] private float verticalVelocity;


    [Header("Player Animation")]
    [SerializeField] private Animator anim;


    [Header("AUdioClip")]
    [SerializeField] private AudioClip jumpClip;

    int prev_inputFlag;
    public int inputFlag;
    public float rotY;
    public bool isSyncronizing;

    public bool isSit = false;

    public TMP_Text nickName_Text;
    private void Awake()
    {
        if (cam == null)
            cam = Camera.main;
        anim = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        playerStateManager = GetComponent<PlayerStateManager>();
    }

    public override void Start()
    {
        base.Start();
        nickName_Text.text = GetComponent<PlayerInfo>().UserName;
    }

    void Update()
    {
        if (isMine)
        {
            InputFunc();
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (isSit) // 의자에 앉아 있다면
                {
                    Stand();
                }
                onJump();
            }

        }
        if (!isMine)
        {
            SyncPos();
        }

        if (IsCheckGrounded()) jump = false;
        else jump = true;
    }

    private void FixedUpdate()
    {
        if(isMine)
            Movement(movementX,movementY, cam.transform.eulerAngles.y);
    }

    /// <summary>
    /// 2022.12.29 / LJ
    /// 플레이어가 의자에 앉음
    /// </summary>
    /// <param name="target">의자 포지션</param>
    public void Sit(Transform target)
    {
        isSit = true;
        transform.position = target.position;
        // anim
    }

    /// <summary>
    /// 2022.12.29 / LJ
    /// 의자에서 일어남
    /// </summary>
    void Stand()
    {
        isSit = false;
        // anim
    }

    /// <summary>
    /// 2022. 12. 26. / 은성
    /// 플레이어의 input 값을 받아 inputFlag에 비트단위로 넣어 input값이 달라졌을 시 서버에게 input값을 보냄
    /// </summary>
    void InputFunc()
    {
        // MOVE
        int XInput = (movementX > 0) ? 1 : (movementX < 0) ? 2 : 0;
        int YInput = (movementY > 0) ? 1 : (movementY < 0) ? 2 : 0;
        int x = XInput << 27;
        int y = YInput << 23;
        int jump;

        inputFlag = 0;
        inputFlag = inputFlag | x;
        inputFlag = inputFlag | y;
        bool isDiff = prev_inputFlag != inputFlag || rotY != cam.transform.eulerAngles.y;
        
        
        // JUMP
        if (Input.GetKeyDown(KeyCode.Space))
        { 
            jump = 1 << 22;
            isDiff = true;
        }
        else
            jump = 0;

        inputFlag = inputFlag | jump;

        prev_inputFlag = inputFlag;
        rotY = cam.transform.eulerAngles.y;
        if (isDiff)
        {
            C_Move move = new C_Move();
            move.Transform = new TransformInfo() { Rot = new Vector() { Y = cam.transform.eulerAngles.y } };
            move.InputFlag = inputFlag;

            NetworkManager.Instance.Send(move);
        }
    }

    /// <summary>
    /// 2022.12.07 / LJ
    /// 플레이어 이동 감지
    /// </summary>
    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    /// <summary>
    /// 2022.12.26 / 은성
    /// 플레이어 이동 구현
    /// 값을 받아와 이동하도록 구현
    /// </summary>
    public void Movement(float x, float z,float rotY)
    {
        if(isSyncronizing)
        {
            isSyncronizing= false;
            return;
        }
        Vector3 movement = new Vector3(x,0,z);
        Vector3 targetDirection = Vector3.zero;

        if (movement != Vector3.zero)
        {
            targetRotation = Mathf.Atan2(x, z) * Mathf.Rad2Deg + rotY;
            float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref rotationVelocity, rotationTime);

            transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
        }

        if ((!(x == 0 && z == 0)))
        {
            // StateManager
            playerStateManager.State = PlayerState.Move;

            // Animation
            anim.SetBool("Move", true);

            targetDirection = Quaternion.Euler(0.0f, targetRotation, 0.0f) * Vector3.forward;
        }
        else
        {
            //StateManager
            if (playerStateManager.State != PlayerState.AFK)
            {
                playerStateManager.State = PlayerState.Idle;
                // Animation
                anim.SetBool("Move", false);
            }
        }

        if (jump) // 중력
        {
            verticalVelocity += gravity * Time.deltaTime;
        }

        controller.Move(targetDirection.normalized * (speed * Time.deltaTime) + new Vector3(0.0f, verticalVelocity, 0.0f) * Time.deltaTime);
    }

    /// <summary>
    /// 2022.12.21 / LJ
    /// 바닥 검사
    /// </summary>
    private bool IsCheckGrounded()
    {
        if (controller.isGrounded)
        {
            return true;
        }
        var ray = new Ray(this.transform.position + Vector3.up * 0.1f, Vector3.down);
        float maxDistance = 0.3f;
        Debug.DrawRay(transform.position + Vector3.up * 0.1f, Vector3.down * maxDistance, Color.yellow);
        return Physics.Raycast(ray, maxDistance, ground);
    }

    /// <summary>
    /// 2022.12.21 / LJ
    /// Space키를 눌렀을 때 실행
    /// </summary>
    void onJump()
    {
        if (!jump) // 점프가 가능 하다면
        {
            if (isMine)
            {
                if (IsCheckGrounded())
                {
                    verticalVelocity = 0f;
                    JumpAndGravity();
                }
            }
            else
            {
                verticalVelocity = 0f;
                JumpAndGravity();
            }
        }
    }

    /// <summary>
    /// 2022.12.21 / LJ
    /// 점프 및 중력 관리
    /// </summary>
    void JumpAndGravity()
    {
        verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
        jumpTimer = 0f;
        JumpTimerOut();
        playerStateManager.State = PlayerState.Move;
        // Animation
        anim.SetTrigger("Jump");
        // AudioClip
        SoundManager.instance.SFXPlay("Jump", jumpClip);
    }

    /// <summary>
    ///  2022.12.21 / LJ
    ///  점프 시간 관리
    /// </summary>
    void JumpTimerOut()
    {
        jumpTimer += Time.deltaTime;
        if (jumpTimer < jumpTimeout)
        {
            Invoke("JumpTimerOut", Time.deltaTime);
            return;
        }
        jump = true;
        return;
    }

    /// <summary>
    /// 2022. 12. 26. / 은성
    /// 다른 클라의 캐릭터들의 포지션을 싱크를 맞추는 함수
    /// </summary>
    void SyncPos()
    {

        int x = ((inputFlag >> 27) == 1) ? 1: ((inputFlag >> 27) == 2) ? -1 : 0;
        int y = ((inputFlag >> 23 & 0b1111) == 1) ? 1: ((inputFlag >> 23 & 0b1111) == 2) ? -1 : 0;
        Movement(x, y, rotY);
        int jump = (inputFlag >> 22 & 0b1);
        if (jump > 0)
            onJump();
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (!isMine) return;

        if (hit.gameObject.TryGetComponent(out FloorBlock floorBlock))
        {
            floorBlock.OnCollisionPlayer();
        }
        else if (hit.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            C_CollideObstacle collidePacket = new C_CollideObstacle();
            NetworkManager.Instance.Send(collidePacket);
        }
        else if (hit.gameObject.layer == LayerMask.NameToLayer("EndLine"))
        {
            C_CollideEndLine collidePacket = new C_CollideEndLine();
            NetworkManager.Instance.Send(collidePacket);
        }
    }
}


