using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraView : MonoBehaviour
{
    [Header("Camera")]
    [SerializeField] private Camera PlayerCam;

    [Header("Player")]
    [SerializeField] private Transform Player;

    [Header("Rotation")]
    [SerializeField] private float x_Rotation;
    [SerializeField] private float y_Rotation;

    void Start()
    {
        Init();
    }

    void Update()
    {
        MouseHandler();
    }

    void Init()
    {
        PlayerCam = Camera.main;
        Player = transform;
    }

    void MouseHandler()
    {
        float mouseX = Input.GetAxis("Mouse X") * 1f;
        float mouseY = Input.GetAxis("Mouse Y") * 1f;

        y_Rotation += mouseX;
        x_Rotation -= mouseY;

        Player.eulerAngles = new Vector3(x_Rotation, y_Rotation, 0.0f);
    }
}
