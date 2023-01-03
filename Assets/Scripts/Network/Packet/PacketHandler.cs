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
        NetworkManager.Instance.JoinedRoom = res.Info;
        RoomManager.Instance.OnRoomInfoUpdate();
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
        Debug.Log("EnterGame");
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

        GameScene gs = Object.FindObjectOfType<GameScene>();
        if (gs != null)
            gs.Init();
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
                Debug.Log($"player not found in move   id: {move.Id}");
                return;
            }
        
            // 만약 inputFlag를 사용한다면
            player.inputFlag = move.InputFlag;
            player.rotY = move.Transform.Rot.Y;
        }
        else if (type == GameObjectType.None)
        {
            NetworkingObject obj = ObjectManager.Instance.FindById(move.Id).GetComponent<NetworkingObject>();
            obj.UpdateTransform(move.Transform);
        }
    }
    public static void S_ChatHandler(PacketSession session, IMessage packet)
    {
        ServerSession SS = session as ServerSession;
        S_Chat chat = packet as S_Chat;

        ChatManager.instance.SendMsg(ObjectManager.Instance.FindById(chat.PlayerId).GetComponent<PlayerChat>(),ObjectManager.Instance.FindById(chat.PlayerId).GetComponent<PlayerInfo>().UserName, chat.Content);
    }
    public static void S_UpdateGameStateResHandler(PacketSession session, IMessage packet)
    {
        ServerSession SS = session as ServerSession;
        S_UpdateGameStateRes res = packet as S_UpdateGameStateRes;

        Debug.Log("RECV STATE PACKET");
        RoomManager.Instance.State = res.State;

    }
    public static void S_SyncPosHandler(PacketSession session, IMessage packet)
    {
        Debug.Log("sync");
        ServerSession SS = session as ServerSession;
        S_SyncPos sync = packet as S_SyncPos;

        GameObject obj = ObjectManager.Instance.FindById(sync.Id);

        if(obj == null)
        {
            Debug.Log($"obj is null in syncPos {sync.Id}"); 
            return;
        }

        obj.transform.position = new Vector3(sync.Transform.Pos.X, sync.Transform.Pos.Y, sync.Transform.Pos.Z);

        GameObjectType Type = ObjectManager.GetObjectTypeById(sync.Id);
        if(Type == GameObjectType.Player)
        {
            obj.GetComponent<PlayerController>().isSyncronizing = true;
        }
        
    }

    public static void S_DeleteFloorBlockHandler(PacketSession session, IMessage packet)
    {
        if (FloorBlockController.Instance == null) return;
        S_DeleteFloorBlock DFB = packet as S_DeleteFloorBlock;
        FloorBlockController.Instance.DeleteFloorBlock(DFB.FloorIndex, DFB.BlockIndex);
    }

    public static void S_PlayerDeadHandler(PacketSession session, IMessage packet)
    {
        S_PlayerDead deadPacket = packet as S_PlayerDead;

        GameObject obj = ObjectManager.Instance.FindById(deadPacket.PlayerId);
        if (obj != null)
            obj.SetActive(false);
    }

    public static void S_GameEndHandler(PacketSession session, IMessage packet)
    {
        S_GameEnd endGamePacket = packet as S_GameEnd;
        RoomManager.Instance.State = GameState.Ending;
        GameObject obj = ObjectManager.Instance.FindById(endGamePacket.WinnerId);
        if (obj.TryGetComponent(out PlayerInfo playerInfo))
        {
            string winnerName = playerInfo.UserName;
            RoomManager.Instance.OnGameEnd(winnerName);
        }
    }

    public static void S_JumpHandler(PacketSession session, IMessage packet)
    {
        S_Jump jumpPacket = packet as S_Jump;
        GameObject obj = ObjectManager.Instance.FindById(jumpPacket.PlayerId);
        obj.GetComponent<PlayerController>().onJump();
    }
}
