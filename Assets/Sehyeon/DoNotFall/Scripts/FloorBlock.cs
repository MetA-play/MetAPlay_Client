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
        IsDeleted = true;
        Vector3 currPos = transform.position;
        Vector3 destPos = new Vector3(currPos.x, currPos.y - 1f, currPos.z);

        while (transform.position.y > destPos.y + 0.1f)
        {
            transform.position = Vector3.Lerp(currPos, destPos, Speed);
            currPos = transform.position;
            yield return null;
        }

        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (IsDeleted) return;

        if (other.CompareTag("Player"))
        {
            if (other.TryGetComponent(out NetworkingObject player))
            {
                if (player.Id == ObjectManager.Instance.MyPlayer.Id)
                {
                    C_DeleteFloorBlock DFB = new C_DeleteFloorBlock();
                    DFB.FloorIndex = FloorIndex;
                    DFB.BlockIndex = BlockIndex;
                    NetworkManager.Instance.Send(DFB);
                }
            }
        }
    }
}
