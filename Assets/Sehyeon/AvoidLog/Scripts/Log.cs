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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.TryGetComponent(out Rigidbody rb))
            {
                Debug.Log("Player Collision");
                rb.AddForce(rb.velocity * 100);
            }
        }
    }
}
