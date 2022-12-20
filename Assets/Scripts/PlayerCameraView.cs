using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
/// <summary>
/// 2022.12.19 / LJ
/// 플레이어 카메라 관련 스크립트
/// </summary>
public class PlayerCameraView : MonoBehaviour
{
    [SerializeField] Vector2 look;

    const float threshold = 0.01f;

    private bool IsCurrentDeviceMouse
    {
        get
        {
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
                return _playerInput.currentControlScheme == "KeyboardMouse";
#else
            return false;
#endif
        }
    }

    [SerializeField] float targetYaw;
    float targetPitch;

    [Header("Camera")]
    [SerializeField] private Transform playerCamTarget;
    [SerializeField] private Camera playerCam;
    public bool LockCameraPosition = false;
    public float BottomClamp = -30.0f;
    public float TopClamp = 70.0f;
    public float CameraAngleOverride = 0.0f;


    [Header("Player")]
    [SerializeField] private Transform Player;

    [Header("Rotation")]
    private float x_Rotation;
    private float y_Rotation;

    [Header("RotationSpeed")]
    [SerializeField] [Range(0f, 10f)] private float x_RotationSpeed = 1f;
    [SerializeField] [Range(0f, 10f)] private float y_RotationSpeed = 1f;

    [Header("Mouse Cursor Settings")]
    public bool cursorLocked = true;
    public bool cursorInputForLook = true;

    void Start()
    {
        Init();
    }

    void Update()
    {
        MouseHandler();
    }

    private void LateUpdate()
    {
    }

    /// <summary>
    /// 2022.12.19 / LJ
    /// 카메라 및 플레이어 가져오기
    /// </summary>
    void Init()
    {
        playerCam = Camera.main;
        Player = transform;
        targetYaw = playerCamTarget.transform.rotation.eulerAngles.y;
    }

    /// <summary>
    /// 2022.12.20 / LJ
    /// 마우스 움직임 감지 및 카메라와 플레이어에 값 적용;
    /// </summary>
    void MouseHandler()
    {
        //float mouseX = Input.GetAxis("Mouse X") * x_RotationSpeed;
        //float mouseY = Input.GetAxis("Mouse Y") * y_RotationSpeed;

        //y_Rotation += mouseX;
        //x_Rotation -= mouseY;

        //Player.transform.rotation = Quaternion.Euler(0, y_Rotation, 0.0f);
        //playerCam.transform.rotation = Quaternion.Euler(x_Rotation, y_Rotation, 0.0f);

        if (look.sqrMagnitude >= threshold && !LockCameraPosition)
        {
            float deltaTimeMultiplier = IsCurrentDeviceMouse ? 1.0f : Time.deltaTime;

            targetYaw += look.x * deltaTimeMultiplier;
            targetPitch += look.y * deltaTimeMultiplier;
        }
        targetYaw = ClampAngle(targetYaw, float.MinValue, float.MaxValue);
        targetPitch = ClampAngle(targetPitch, BottomClamp, TopClamp);

        playerCamTarget.transform.rotation = Quaternion.Euler(targetPitch + CameraAngleOverride, targetYaw, 0.0f);

    }

    /// <summary>
    /// 2022.12.20 / LJ
    /// 마우스 움직임 감지
    /// </summary>
    void OnLook(InputValue value)
    {
        if (cursorInputForLook)
        {
            look = value.Get<Vector2>();
        }
    }

    private void OnApplicationFocus(bool focus)
    {
        SetCursorState(cursorLocked);
    }

    /// <summary>
    /// 2022.12.20 / LJ
    /// 마우스 커서 상태 설정
    /// </summary>
    void SetCursorState(bool newState)
    {
        Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
    }

    /// <summary>
    /// 2022.12.20 / LJ
    /// 앵글 범위 설정
    /// </summary>
    /// <returns></returns>
    private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
    {
        if (lfAngle < -360f) lfAngle += 360f;
        if (lfAngle > 360f) lfAngle -= 360f;
        return Mathf.Clamp(lfAngle, lfMin, lfMax);
    }
}
