using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable] public class Interaction : UnityEvent<Transform> { };

public abstract class LobbyObject : MonoBehaviour
{
    public Interaction interaction;

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
