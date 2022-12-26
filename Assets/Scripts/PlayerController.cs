using Google.Protobuf.Protocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

/// <summary>
/// 2022.12.05 / LJ
/// 플레이어 조작 관련 스크립트
/// </summary>
public class PlayerController : NetworkingObject
{
    private Rigidbody rb;

    [Header("Player Movement Stat")]
    [Range(0f, 100f)]
    [SerializeField]
    private float speed;

    private float movementX;
    private float movementY;

    // [Horizontal(4)][Vertical(4)][Jump(4)]
    public int prev_inputFlag = 0;
    public int inputFlag = 0;
    private void Awake()
    {
        /**/
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }
    }

    public override void Start()
    {
        base.Start();
        Debug.Log(new Vector2(1, 1).normalized.x + ", " + new Vector2(1, 1).normalized.y);
    }

    void Update()
    {

        if (isMine)
        {
            InputFunc();
        }
        if (!isMine)
        {
            SyncPos();
        }
    }

    private void FixedUpdate()
    {
        if(isMine)
            Movement();
    }

    void InputFunc()
    {

        int XInput = (movementX > 0) ? 1 : (movementX <0) ? 2 : 0;
        int YInput = (movementY > 0) ? 1 : (movementY <0) ? 2 : 0;
        int x = XInput << 27;
        int y = YInput << 23;

        inputFlag = 0;
        inputFlag = inputFlag | x;
        inputFlag = inputFlag | y;
        bool isDiff = prev_inputFlag != inputFlag;
        if(isDiff)
            Debug.Log("Difficult:  " + isDiff);

        prev_inputFlag = inputFlag;
        if (isDiff)
        {
            C_Move move = new C_Move();
            move.Transform = null;
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
        Debug .Log(movementX + ", " + movementY);

    }

    /// <summary>
    /// 2022.12.07 / LJ??
    /// 플레이어 이동 구현
    /// </summary>
    void Movement()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY).normalized;
        rb.velocity = movement * speed * Time.deltaTime;
    }

    void SyncPos()
    {

        int x = ((inputFlag >> 27) == 1) ? 1: ((inputFlag >> 27) == 2) ? -1 : 0;
        int y = ((inputFlag >> 23 & 0b1111) == 1) ? 1: ((inputFlag >> 23 & 0b1111) == 2) ? -1 : 0;
        rb.velocity = new Vector3(x,0,y).normalized * speed * Time.deltaTime;
        Debug.Log("Y:  " + (inputFlag >> 23 & 0b1111));
        Debug.Log($"{x} {y}");
    }



}


