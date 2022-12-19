using Google.Protobuf.Protocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : NetworkingObject
{
    public int roomId;


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "PlayerDemo")
        {
            RoomManager.Instance.JoinRoom(roomId);
        }   
    }

}
