using Google.Protobuf.Protocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : NetworkingObject
{
    public int RotY;
    public float Speed;

    private void Update()
    {
        Vector3 nextRot = new Vector3(0, RotY, 0);
        Vector3 currentRot = transform.localEulerAngles;
        transform.localEulerAngles = Vector3.MoveTowards(currentRot, nextRot, Speed);
    }

    public override void UpdateTransform(TransformInfo transformInfo)
    {
        RotY = (int)transformInfo.Rot.Y;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.TryGetComponent(out PlayerController pc))
            {
                if (pc.isMine)
                {
                    C_PlayerDead deadPacket = new C_PlayerDead();
                    NetworkManager.Instance.Send(deadPacket);
                }
            }
        }
    }
}
