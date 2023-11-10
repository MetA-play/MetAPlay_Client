using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCanvas : MonoBehaviour
{
    [SerializeField] private Transform target;

    void Start()
    {
        if (transform.parent.GetComponent<NetworkingObject>().isMine == true) // 지금 이 객체가 사용자일때
        {
            transform.gameObject.SetActive(false);
        }
        target = Camera.main.transform;
    }

    void Update()
    {
        // Look Player (has main camera)
        transform.LookAt(target);
    }



}
