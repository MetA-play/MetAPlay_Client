using Google.Protobuf;
using Google.Protobuf.Protocol;
using ServerCore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacketHandler
{
    /// <summary>
    /// 2022. 12. 19. / Eunseong
    /// 방 생성 요청에 대해 응답이 왔을 때 실행되는 함수
    /// </summary>
    /// <param name="session"> 방 생성 요청을 응답한 세션</param>
    /// <param name="packet"> 받은 메세지</param>
    public static void S_CreateroomResHandler(PacketSession session, IMessage packet)
    {
        ServerSession SS = session as ServerSession;
        S_CreateroomRes res = packet as S_CreateroomRes;
        Room room =  ObjectManager.Instance.FindById(res.ObjectId).GetComponent<Room>();
        room.Id = res.RoomId;

        // 천막 씬으로 이동
        Debug.Log("Room Create");
    }
    public static void S_JoinroomResHandler(PacketSession session, IMessage packet)
    {
        ServerSession SS = session as ServerSession;
    }
    public static void S_EnterGameHandler(PacketSession session, IMessage packet)
    {   
        ServerSession SS = session as ServerSession;
    }
    public static void S_LeaveGameHandler(PacketSession session, IMessage packet)
    {
        ServerSession SS = session as ServerSession;
    }
    public static void S_SpawnHandler(PacketSession session, IMessage packet)
    {
        ServerSession SS = session as ServerSession;
        S_Spawn spawn = packet as S_Spawn;

        foreach (ObjectInfo obj in spawn.Objects)
        {
            ObjectManager.Instance.Add(obj);
        }
    }
    public static void S_DespawnHandler(PacketSession session, IMessage packet)
    {
        ServerSession SS = session as ServerSession;
    }
    public static void S_MoveHandler(PacketSession session, IMessage packet)
    {
        ServerSession SS = session as ServerSession;
    }
    public static void S_ChatHandler(PacketSession session, IMessage packet)
    {
        ServerSession SS = session as ServerSession;
    }
    public static void S_UpdateGameStateResHandler(PacketSession session, IMessage packet)
    {
        ServerSession SS = session as ServerSession;
    }
}
