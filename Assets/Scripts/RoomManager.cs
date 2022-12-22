using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    void Start()
    {
        NetworkManager.Instance.JoinRoom(NetworkManager.Instance.JoinedRoomId);
    }

    void Update()
    {
        
    }
}
