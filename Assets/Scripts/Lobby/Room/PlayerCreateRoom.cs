using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerCreateRoom : MonoBehaviour
{
    [SerializeField] private GameObject roomPanel;
    [SerializeField] private PlayerCameraView playerCam;

    [Header("Selected Game Button")]
    public RoomButton selectedButton;

    [Header("RoomName InputField")]
    [SerializeField] private TMP_InputField roomNameInputField;

    [Header("GameRooms")]
    [SerializeField] private List<GameObject> gameRooms;

    void Start()
    {
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
        playerCam.cursorLocked = false;
        playerCam.cursorInputForLook = false;
    }

    /// <summary>
    /// 2022.12.30 / LJ
    /// 게임 방 생성 패널 닫기
    /// </summary>
    public void CloseRoomPanel()
    {
        roomPanel.SetActive(false);
        playerCam.cursorInputForLook = true;
        playerCam.cursorLocked = true;
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
        GameObject obj = Instantiate(gameRooms[(int)selectedButton.kind]);
        obj.transform.position = new Vector3(playerCam.transform.position.x, -2.5f, playerCam.transform.position.z);
        obj.GetComponent<GameRoom>().kind = selectedButton.kind;
        CloseRoomPanel();
    }
}
