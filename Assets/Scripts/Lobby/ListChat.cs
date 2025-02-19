using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ListChat : MonoBehaviour
{
    [Header("TMP")]
    [SerializeField] private TextMeshProUGUI userName_TMP;
    [SerializeField] private TextMeshProUGUI message_TMP;

    void Start()
    {
        Destroy(this.gameObject, 5f);
    }

    void Update()
    {
        
    }

    public void SetMessage(string message,string username,bool ismine)
    {
        message_TMP.text = message;
        userName_TMP.text = $"[ {username} ]";
        if (ismine)
            userName_TMP.color = new Color(0, 255, 0);
        else 
            userName_TMP.color = new Color(255, 0, 0);
    }
}
