using Google.Protobuf;
using ServerCore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacketHandler
{
    public static void S_CreateroomResHandler(PacketSession session, IMessage packet)
    {
        ServerSession SS = session as ServerSession;
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
}
