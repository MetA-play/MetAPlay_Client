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
        
    }

    public override void UpdateTransform(TransformInfo transformInfo)
    {
        RotY = (int)transformInfo.Rot.Y;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //if (collision.gameObject.CompareTag("Player"))
        //{
        //    if (collision.gameObject.TryGetComponent(out PlayerController pc))
        //    {
        //        C_Move movePacket = new C_Move();
        //        movePacket.State = ObjectState.Stun;
        //        NetworkManager.Instance.Send(movePacket);
        //    }
        //}
    }
}
