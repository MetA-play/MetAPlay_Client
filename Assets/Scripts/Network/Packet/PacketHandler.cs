using Google.Protobuf;
using Google.Protobuf.Protocol;
using ServerCore;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PacketHandler
{
    /// <summary>
    /// 2022. 12. 22. / Eunseong
    /// 방 생성 요청에 대해 응답이 왔을 때 실행되는 함수
    /// </summary>
    /// <param name="session"> 방 생성 요청을 응답한 세션</param>
    /// <param name="packet"> 받은 메세지</param>
    public static void S_CreateRoomResHandler(PacketSession session, IMessage packet)
    {
        ServerSession SS = session as ServerSession;
        S_CreateRoomRes res = packet as S_CreateRoomRes;
        Room room =  ObjectManager.Instance.FindById(res.ObjectId).GetComponent<Room>();
        room.Info = res.Info;

        NetworkManager.Instance.JoinedRoom = res.Info;
        // TODO 천막 씬으로 이동
        SceneManager.LoadScene("GameroomScene");
        Debug.Log("Room Create");
    }

    /// <summary>
    /// 2022. 12. 20. / Eunseong
    /// 방 참가 요청에 대해 응답이 왔을 때 실행 되는 함수
    /// </summary>
    /// <param name="session"></param>
    /// <param name="packet"></param>
    public static void S_JoinRoomResHandler(PacketSession session, IMessage packet)
    {
        ServerSession SS = session as ServerSession;
        S_JoinRoomRes res = packet as S_JoinRoomRes;

    }
    /// <summary>
    /// 2022. 12. 20. / Eunseong
    /// 다른 Room에 들어갔을 때 서버에서 Enter허가가 오면 실행되는 함수
    /// </summary>
    /// <param name="session"></param>
    /// <param name="packet"></param>
    public static void S_EnterGameHandler(PacketSession session, IMessage packet)
    {   
        ServerSession SS = session as ServerSession;
        S_EnterGame enter = packet as S_EnterGame;

        ObjectManager.Instance.Add(enter.Player, true);
        Debug.Log("Mine Generate");

    }
    public static void S_LeaveGameHandler(PacketSession session, IMessage packet)
    {
        ServerSession SS = session as ServerSession;
    }
    /// <summary>
    /// 2022. 12. 19. / Eunseong
    /// 스폰 패킷이 오면 ObjectManager에서 Type을 구분해 맞는 프리팹 소환
    /// </summary>
    /// <param name="session"></param>
    /// <param name="packet"></param>
    public static void S_SpawnHandler(PacketSession session, IMessage packet)
    {
        ServerSession SS = session as ServerSession;
        S_Spawn spawn = packet as S_Spawn;

        foreach (ObjectInfo obj in spawn.Objects)
        {
            ObjectManager.Instance.Add(obj);
        }
    }
    /// <summary>
    /// 2022. 12. 20. / Eunseong
    /// 서버에서 디스폰할 객체를 패킷으로 전달받으면 실행되는 함수
    /// </summary>
    /// <param name="session"></param>
    /// <param name="packet"></param>
    public static void S_DespawnHandler(PacketSession session, IMessage packet)
    {
        ServerSession SS = session as ServerSession;
        S_Despawn despawn= packet as S_Despawn;

        foreach (int id in despawn.ObjectId)
        {
            ObjectManager.Instance.RemoveById(id);
        }
    }
    public static void S_MoveHandler(PacketSession session, IMessage packet)
    {
        ServerSession SS = session as ServerSession;
        S_Move move = packet as S_Move;

        GameObjectType type = ObjectManager.GetObjectTypeById(move.Id);

        if (type == GameObjectType.Player)
        {
            if (ObjectManager.Instance.MyPlayer.Id == move.Id) return;

            PlayerController player = ObjectManager.Instance.FindById(move.Id)?.GetComponent<PlayerController>();

            if (player == null)
            {
                Debug.Log("player not found");
                return;
            }
            if(move.Transform == null)
            {
                player.inputFlag = move.InputFlag;
            }
            else
            {
                player.transform.position = new Vector3(move.Transform.Pos.X, move.Transform.Pos.Y, move.Transform.Pos.Z);
            }
        }
        else if (type == GameObjectType.None)
        {
            GameObject obj = ObjectManager.Instance.FindById(move.Id);
            obj.GetComponent<NetworkingObject>().UpdateTransform(move.Transform);
            Debug.Log(move.Transform.Rot.Y);
        }
    }
    public static void S_ChatHandler(PacketSession session, IMessage packet)
    {
        ServerSession SS = session as ServerSession;
    }
    public static void S_UpdateGameStateResHandler(PacketSession session, IMessage packet)
    {
        ServerSession SS = session as ServerSession;
        S_UpdateGameStateRes res = packet as S_UpdateGameStateRes;

        Debug.Log("RECV STATE PACKET");
        RoomManager.Instance.State = res.State;

    }   

    public static void S_DeleteFloorBlockHandler(PacketSession session, IMessage packet)
    {
        ServerSession SS = session as ServerSession;

        
    }
}
