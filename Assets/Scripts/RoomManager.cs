using Google.Protobuf.Protocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
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
}
