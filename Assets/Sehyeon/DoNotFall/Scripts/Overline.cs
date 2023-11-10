using Google.Protobuf.Protocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Overline : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.TryGetComponent(out NetworkingObject obj))
            {
                if (obj.isMine)
                {
                    C_PlayerDead deadPacket = new C_PlayerDead();
                    NetworkManager.Instance.Send(deadPacket);
                }
            }
        }
    }
}
