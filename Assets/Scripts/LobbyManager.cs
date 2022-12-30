using Google.Protobuf.Protocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyManager : MonoBehaviour
{

    static LobbyManager instance;
    public static LobbyManager Instance { get { return instance; } }


    private void Awake()
    {
        if(instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        C_SetUserinfo set = new C_SetUserinfo();
        set.Info = NetworkManager.Instance.UserInfo;
        NetworkManager.Instance.Send(set);
    }


    /// <summary>
    /// 2022. 12. 19. / Eunseong
    /// 방 생성 버튼 이벤트 위한 함수
    /// </summary>
    /// 
    [ContextMenu("CreateRoom")]
    public void OnCreateRoom()
    {
        NetworkManager.Instance.CreateRoom(new RoomSetting() { GameType = GameType.AvoidLog, MaxPlayer = 5, Name = "Test" });
    }

    [ContextMenu("JoinRoom")]
    public void OnJoinRoom()
    {
        NetworkManager.Instance.JoinRoom(1);
    }

    [ContextMenu("TEST MOVE")]
    public void TestsMove()
    {
        C_PlayerDead d = new C_PlayerDead();

        NetworkManager.Instance.Send(d);
    }
}
