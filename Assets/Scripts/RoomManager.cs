using Google.Protobuf.Protocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{

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
                    Debug.Log("Game Start");
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
        Setting = NetworkManager.Instance.JoinedRoom.Setting;
    }

    void Update()
    {
        
    }


    /// <summary>
    /// 2022. 12. 22. / Eunseong
    /// 게임 시작 함수
    /// </summary>
    void StartGame()
    {
        C_UpdateGameStateReq req = new C_UpdateGameStateReq();
        req.State = GameState.Playing;

        NetworkManager.Instance.Send(req);
    }


    /// <summary>
    /// 2022. 12. 22 / Eunseong
    /// 버튼이벤트를 위한 함수
    /// </summary>
    public void OnStartGame()
    {
        StartGame();
    }
}
