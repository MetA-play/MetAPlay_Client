using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Google.Protobuf.Protocol;

public class GameScene : MonoBehaviour
{
    private void Start()
    {
        C_JoinRoomReq req = new C_JoinRoomReq() { RoomId = 1 };


    }
}
