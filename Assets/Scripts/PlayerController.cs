using Define;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// 2022.12.05 / LJ
/// 플레이어 조작 관련 스크립트
/// </summary>
public class PlayerController : MonoBehaviour
{
    private PlayerStateManager playerStateManager;

    [Header("Player Movement Stat")]
    [Range(0f, 100f)] [SerializeField] private float speed;
    CharacterController controller;
    [SerializeField] private bool jump;
    [SerializeField] private LayerMask ground;
    [SerializeField] [Range(0f, 10f)] private float jumpHeight;
    [SerializeField] [Range(0f, 10f)] private float jumpTimeout;
    private float jumpTimer;

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


    private void Awake()
    {
        if (cam == null)
            cam = Camera.main;
        controller = GetComponent<CharacterController>();
        playerStateManager = GetComponent<PlayerStateManager>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) onJump();
        if (IsCheckGrounded()) jump = false;

    }

    private void FixedUpdate()
    {
        Movement();
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
    /// 2022.12.20 / LJ
    /// 플레이어 이동 구현
    /// </summary>
    void Movement()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY).normalized;

        Vector3 targetDirection = Vector3.zero;

        if (movement != Vector3.zero)
        {
            targetRotation = Mathf.Atan2(movementX, movementY) * Mathf.Rad2Deg + cam.transform.eulerAngles.y;
            float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref rotationVelocity, rotationTime);

            transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
        }

        if ((!(movementX == 0 && movementY == 0)))
        {
            // StateManager
            playerStateManager.State = PlayerState.Move;

            targetDirection = Quaternion.Euler(0.0f, targetRotation, 0.0f) * Vector3.forward;
            //rb.velocity = movement * speed * Time.deltaTime;
        }
        else
        {
            //StateManager
            if (playerStateManager.State != PlayerState.AFK)
                playerStateManager.State = PlayerState.Idle;
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
        if (IsCheckGrounded() && !jump) // 점프가 가능 하다면
        {
            verticalVelocity = 0f;
            JumpAndGravity();
        }
        else return;
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
    }

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
}
