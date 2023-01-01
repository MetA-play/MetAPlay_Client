using Google.Protobuf.Protocol;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Room : NetworkingObject
{
    public RoomInfo Info = new RoomInfo();
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Room Collision");
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.GetComponent<NetworkingObject>().isMine)
            {
                NetworkManager.Instance.JoinedRoom.Id = Info.Id;
                SceneManager.LoadScene("GameroomScene");
            }
        }
    }


}
