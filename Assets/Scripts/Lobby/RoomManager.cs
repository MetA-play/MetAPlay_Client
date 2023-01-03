using Google.Protobuf.Protocol;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomManager : MonoBehaviour
{
    public GameObject WaitingRoom;
    public GameObject GameMap;

    static RoomManager instance;
    public static RoomManager Instance { get { return instance; } }
    public RoomSetting Setting;

    GameState state;
    public GameState State { get { return state; } set
        {
            switch (value)
            {
                case GameState.Waiting:
                    break;
                case GameState.Playing:
                    // 게임에 따른 맵 소환
                    OnGameStart();
                    break;
                case GameState.Ending:
                    // 결과 추가
                    break;
                default:
                    break;
            }


            state = value;
        } 
    }

    private TMP_Text winnerUI;
    private bool isStarted = false;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        NetworkManager.Instance.JoinRoom(NetworkManager.Instance.JoinedRoom.Id);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && !isStarted && Setting != null)
        {
            isStarted = true;
            GameStart();
        }
    }

    public void OnRoomInfoUpdate()
    {
        Setting = NetworkManager.Instance.JoinedRoom.Setting;

        if (GameMap == null)
        {
            GameMap = Instantiate(Resources.Load<GameObject>($"Prefabs/Game/{Setting.GameType}"));
            GameMap.SetActive(false);
        }
    }

    public void GameStart()
    {
        // 게임 시작 패킷 전송
        C_UpdateGameStateReq gameStatePacket = new C_UpdateGameStateReq();
        gameStatePacket.State = GameState.Playing;
        NetworkManager.Instance.Send(gameStatePacket);
    }

    public void OnGameStart()
    {
        WaitingRoom.SetActive(false);
        GameMap.SetActive(true);
        winnerUI = GameObject.Find("Winner Text").GetComponent<TMP_Text>();
        winnerUI.gameObject.SetActive(false);
    }

    public void OnGameEnd(string winner)
    {
        winnerUI.text = $"{winner} 승리!";
        winnerUI.gameObject.SetActive(true);
        StartCoroutine(GoLobby());
    }

    private IEnumerator GoLobby()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("Lobby");
    }
}
