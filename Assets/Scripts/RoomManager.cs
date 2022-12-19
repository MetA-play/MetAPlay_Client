using Google.Protobuf.Protocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{

    static RoomManager instance;
    public static RoomManager Instance { get { return instance; } }


    private void Awake()
    {
        if(instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }


    /// <summary>
    /// 2022. 12. 19.  / Eunseong
    /// 방 생성 요청을 서버에게 보내는 함수
    /// </summary>
    /// <param name="setting"></param>
    void CreateRoom(RoomSetting setting)
    {
        C_CreateroomReq req=  new C_CreateroomReq();
        req.Setting = setting;

        NetworkManager.Instance.Send(req);
    }

    /// <summary>
    /// 2022. 12. 19. / Eunseong
    /// 방 생성 버튼 이벤트 위한 함수
    /// </summary>
    public void OnCreateRoom()
    {
        CreateRoom(new RoomSetting() { GameType = GameType.AvoidLog, MaxPlayer = 5, Name = "Test" });
    }

    /// <summary>
    /// 2022. 12. 19. / Eunseong
    /// 방 참가 요청을 서버에게 보내는 함수
    /// </summary>
    /// <param name="id"></param>
    public void JoinRoom(int id)
    {
        C_JoinroomReq req = new C_JoinroomReq();
        req.RoomId = id;
        Debug.Log("JoinRoom Func");
        NetworkManager.Instance.Send(req);
    }
}
