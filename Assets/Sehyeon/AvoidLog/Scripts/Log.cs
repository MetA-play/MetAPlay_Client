using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : MonoBehaviour
{
    public bool IsStarted;
    public int RotY;

    private void Update()
    {
        if (!IsStarted) return;
        transform.eulerAngles = new Vector3(0, RotY, 0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.TryGetComponent(out PlayerController pc))
            {
                // 넉백 처리
                
            }
        }
    }
}
