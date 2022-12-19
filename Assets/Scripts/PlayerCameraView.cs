using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 2022.12.19 / LJ
/// 플레이어 카메라 관련 스크립트
/// </summary>
public class PlayerCameraView : MonoBehaviour
{
    [Header("Camera")]
    [SerializeField] private Camera PlayerCam;

    [Header("Player")]
    [SerializeField] private Transform Player;

    [Header("Rotation")]
    private float x_Rotation;
    private float y_Rotation;

    [Header("RotationSpeed")]
    [SerializeField] [Range(0f, 10f)] private float x_RotationSpeed = 1f;
    [SerializeField] [Range(0f, 10f)] private float y_RotationSpeed = 1f;
    void Start()
    {
        Init();
    }

    void Update()
    {
        MouseHandler();
    }

    /// <summary>
    /// 2022.12.19 / LJ
    /// 카메라 및 플레이어 가져오기
    /// </summary>
    void Init()
    {
        PlayerCam = Camera.main;
        Player = transform;
    }

    /// <summary>
    /// 2022.12.19 / LJ
    /// 마우스 움직임 감지 및 카메라와 플레이어에 값 적용;
    /// </summary>
    void MouseHandler()
    {
        float mouseX = Input.GetAxis("Mouse X") * x_RotationSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * y_RotationSpeed;

        y_Rotation += mouseX;
        x_Rotation -= mouseY;

        Player.eulerAngles = new Vector3(0, y_Rotation, 0.0f);
        PlayerCam.transform.eulerAngles = new Vector3(x_Rotation, y_Rotation, 0.0f);
    }
}
