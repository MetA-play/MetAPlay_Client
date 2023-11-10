using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragPlayerRotation : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] Transform player;
    Vector3 prevPos = Vector3.zero;
    Vector3 posDelta = Vector3.zero;

    [Header("Rotation Speed")]
    [SerializeField] [Range(0f, 20f)] private float rotationSpeed;

    void Start()
    {
        
    }

    void Update()
    {
        RotatePlayer();
    }

    /// <summary>
    /// 2022.12.28 / LJ
    /// 마우스 입력을 받으면 플레이어 회전
    /// </summary>
    void RotatePlayer()
    {
        if (Input.GetMouseButton(0))
        {
            posDelta = Input.mousePosition - prevPos;
            player.Rotate(player.up, Vector3.Dot(posDelta, -Camera.main.transform.right)* Time.deltaTime * rotationSpeed);
        }
        prevPos = Input.mousePosition;
    }
}
