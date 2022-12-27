using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable] public class AddListChat : UnityEvent<string, string, bool> { };

public class ListChatGroup : MonoBehaviour
{
    public static ListChatGroup instance;

    public AddListChat addListChat;

    [Header("ChatObject")]
    [SerializeField] private GameObject listChatObj;

    private void Start()
    {
        instance = this;
        addListChat.AddListener(AddChat);
    }

    /// <summary>
    /// 22022.12.27 / LJ
    /// 리스트 채팅에 채팅 추가
    /// </summary>
    public void AddChat(string message, string username, bool ismine)
    {
        Debug.Log("call");
        GameObject chat = Instantiate(listChatObj, transform);
        chat.TryGetComponent<ListChat>(out ListChat list);
        list.SetMessage(message,username,ismine);
    }
}
