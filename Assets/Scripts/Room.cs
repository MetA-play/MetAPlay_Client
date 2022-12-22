using Google.Protobuf.Protocol;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Room : NetworkingObject
{
    public int roomId;
    public RoomSetting Setting;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Room Collision");
        if (other.gameObject.CompareTag("Player"))
        {
            NetworkManager.Instance.JoinedRoomId = roomId;
            SceneManager.LoadScene("GameroomScene");
        }
    }


}
