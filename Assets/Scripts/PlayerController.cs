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
    private Rigidbody rb;

    [Header("Player Movement Stat")]
    [Range(0f, 100f)]
    [SerializeField]
    private float speed;

    private float movementX;
    private float movementY;

    [Header("Player Rotation")]
    private Camera cam;
    [SerializeField]private float targetRotation = 0f;
    [SerializeField] private float rotationVelocity;
    private float rotationTime = 0.12f;

    private void Awake()
    {
        cam = Camera.main;
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }
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
    /// 2022.12.07 / LJ
    /// 플레이어 이동 구현
    /// </summary>
    void Movement()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY).normalized;


        if (movement != Vector3.zero)
        {
            Debug.Log(1);
            targetRotation = Mathf.Atan2(movementX, movementY) * Mathf.Rad2Deg + cam.transform.eulerAngles.y;
            float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref rotationVelocity, rotationTime);

            transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
        }


        rb.velocity = movement * speed * Time.deltaTime;
    }
}
