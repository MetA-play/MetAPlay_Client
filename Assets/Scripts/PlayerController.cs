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

    private float movementX;
    private float movementY;

    [Header("Player Rotation")]
    [SerializeField] private Camera cam;
    [SerializeField] private float targetRotation = 0f;
    [SerializeField] private float rotationVelocity;
    private float rotationTime = 0.12f;
    private float verticalVelocity;


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


        if (movement != Vector3.zero)
        {
            targetRotation = Mathf.Atan2(movementX, movementY) * Mathf.Rad2Deg + cam.transform.eulerAngles.y;
            float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref rotationVelocity, rotationTime);

            transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
        }

        if (!(movementX == 0 && movementY == 0))
        {
            // StateManager
            playerStateManager.State = PlayerState.Move;

            Vector3 targetDirection = Quaternion.Euler(0.0f, targetRotation, 0.0f) * Vector3.forward;
            //rb.velocity = movement * speed * Time.deltaTime;
            controller.Move(targetDirection.normalized * (speed * Time.deltaTime) + new Vector3(0.0f, verticalVelocity, 0.0f) * Time.deltaTime);
        }
        else
        {
            //StateManager
            if (playerStateManager.State != PlayerState.AFK)
                playerStateManager.State = PlayerState.Idle;
        }
    }
}
