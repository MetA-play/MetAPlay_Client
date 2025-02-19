using Google.Protobuf.Protocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorBlock : MonoBehaviour
{
    public int FloorIndex;
    public int BlockIndex;
    public float Speed;

    private bool IsDeleted;

    public void OnDelete()
    {
        StartCoroutine(DeleteAnimation());
    }

    private IEnumerator DeleteAnimation()
    {
        Vector3 currPos = transform.position;
        Vector3 destPos = new Vector3(currPos.x, currPos.y - 1f, currPos.z);

        while (transform.position.y > destPos.y + 0.1f)
        {
            transform.position = Vector3.Lerp(currPos, destPos, Speed);
            currPos = transform.position;
            yield return null;
        }

        Destroy(gameObject);
    }

    public void OnCollisionPlayer()
    {
        if (IsDeleted) return;
        IsDeleted = true;

        C_DeleteFloorBlock DFB = new C_DeleteFloorBlock();
        DFB.FloorIndex = FloorIndex;
        DFB.BlockIndex = BlockIndex;
        NetworkManager.Instance.Send(DFB);
    }
}
