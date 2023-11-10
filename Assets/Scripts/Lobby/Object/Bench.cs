using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bench : LobbyObject
{
    public bool isSit;

    void Start()
    {
    }

    void Update()
    {
        
    }


    public void Sit(Transform target)
    {
        Debug.Log("sit");
        if (isSit)
            return;
        isSit = true;
        // Player Sit
        target.position = transform.position;
        target.GetComponent<PlayerController>().Sit(transform);
    }
}
