using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyUIManager : MonoBehaviour
{

    public static LobbyUIManager instance;
    [SerializeField] public Transform player;

    [Header("Window")]
    public GameObject menuObject;

    [Header("Slider")]
    [SerializeField] private Slider SFXSlider;
    [SerializeField] private Slider BGMSlider;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    void Start()
    {
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            OpenMenu();
        }
    }


    /// <summary>
    /// 2023.1.1 / LJ
    /// 플레이어 커서 관리
    /// </summary>
    public void CursorLock(bool locking)
    {
        player.TryGetComponent<PlayerCameraView>(out PlayerCameraView cam);
        if (!locking)
        {
            cam.cursorInputForLook = false;
            cam.cursorLocked = false;
        }
        else
        {
                cam.cursorLocked = true;
                cam.cursorInputForLook = true;
            
        }
    }

    /// <summary>
    /// 2023.1.1 / LJ
    /// 메뉴 창 열기
    /// </summary>
    void OpenMenu()
    {
        menuObject.SetActive(true);
        CursorLock(false);
    }

    /// <summary>
    /// 2023.1.1 / LJ
    /// 메뉴 창 닫기
    /// </summary>
    public void CloseMenu()
    {
        menuObject.SetActive(false);
        SoundManager.instance.SetSFXLevel(SFXSlider.value);
        SoundManager.instance.SetBGMLevel(BGMSlider.value);
        CursorLock(true);
    }

    /// <summary>
    /// 2023.1.1 / LJ
    /// 게임 종료
    /// </summary>
    public void ExitGame()
    {
        Application.Quit();
    }
}
