using Google.Protobuf.Protocol;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Room : NetworkingObject
{
    public RoomInfo Info;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Room Collision");
        if (other.gameObject.CompareTag("Player"))
        {
            NetworkManager.Instance.JoinedRoom.Id = Info.Id;
            SceneManager.LoadScene("GameroomScene");
        }
    }


}
