using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Google.Protobuf.Protocol;

public class PlayerCreateRoom : MonoBehaviour
{
    static PlayerCreateRoom instance;
    public static PlayerCreateRoom Instance { get { return instance; } }

    [SerializeField] private GameObject roomPanel;
    [SerializeField] private PlayerCameraView playerCam;
    [SerializeField] private LobbyUIManager uiManager;
    [Header("Selected Game Button")]
    public RoomButton selectedButton;

    [Header("RoomName InputField")]
    [SerializeField] private TMP_InputField roomNameInputField;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    void Start()
    {
        uiManager = GetComponent<LobbyUIManager>();
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach(GameObject player in players)
        {
            if (player.GetComponent<NetworkingObject>().isMine)
                playerCam = player.GetComponent<PlayerCameraView>();
        }
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.C))
        {
            OpenRoomPanel();
        }
    }

    /// <summary>
    /// 2022.12.30 / LJ
    /// 게임 방 생성 패널 열기
    /// </summary>
    void OpenRoomPanel()
    {
        roomPanel.SetActive(true);
        uiManager.CursorLock(false);
    }

    /// <summary>
    /// 2022.12.30 / LJ
    /// 게임 방 생성 패널 닫기
    /// </summary>
    public void CloseRoomPanel()
    {
        roomPanel.SetActive(false);
        uiManager.CursorLock(true);
    }

    /// <summary>
    /// 2022.12.30 / LJ
    /// 게임 방 생성 진행
    /// </summary>
    public void CreateRoom()
    {
        if (selectedButton == null)
        {
            // 선택 된게 없음
            return;
        } 
        if (roomNameInputField.text.Length == 0)
        {
            // 방 이름을 지정하지 않음
            return;
        }
        // Create Object
        RoomSetting setting = new RoomSetting();
        setting.MaxPlayer = 8;
        setting.Name = roomNameInputField.text;
        setting.GameType = (GameType)((int)selectedButton.kind);
        NetworkManager.Instance.CreateRoom(setting);
        /*GameObject obj = Instantiate(gameRooms[(int)selectedButton.kind]);
        obj.transform.position = new Vector3(playerCam.transform.position.x, -2.5f, playerCam.transform.position.z);
        obj.GetComponent<GameRoom>().kind = selectedButton.kind;*/
        CloseRoomPanel();
    }
}
