using Google.Protobuf.Protocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoccerBall : NetworkingObject
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        Debug.Log(other.gameObject.tag);
        if (other.gameObject.CompareTag("Player"))
        {
            C_HitSoccerball hit = new C_HitSoccerball();
            hit.HitterTransform =
                new TransformInfo()
                {
                    Pos = new Vector()
                    {
                        X = other.transform.position.x,
                        Y = other.transform.position.y,
                        Z = other.transform.position.z,
                    }
                };

            hit.ObjectId = Id;

            NetworkManager.Instance.Send(hit);
        }
    }
}
