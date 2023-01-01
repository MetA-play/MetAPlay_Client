using Google.Protobuf.Protocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.TryGetComponent(out NetworkingObject obj))
            {
                if (obj.isMine)
                {
                    TransformInfo spawnPoint = new TransformInfo
                    {
                        Pos = new Vector() { X = transform.position.x, Y = transform.position.y, Z = transform.position.z }
                    };

                    C_SetSpawnPoint setSpawnPointPacket = new C_SetSpawnPoint();
                    setSpawnPointPacket.SpawnPoint = spawnPoint;
                    NetworkManager.Instance.Send(setSpawnPointPacket);
                }
            }
        }
    }
}
