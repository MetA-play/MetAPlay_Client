using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChat : MonoBehaviour
{
    [Header("PlayerChatGroup")]
    public Transform bubbleChatGroup;

    [Header("ChatObject")]
    [SerializeField] private GameObject bubbleChat;

    void Start()
    {
        if (gameObject.GetComponent<NetworkingObject>().isMine)
        {
            ChatManager.instance.SetMe(this);
        }
    }

    void Update()
    {

    }

    /// <summary>
    /// 2022.12.26 / LJ
    /// 채팅을 화면에 표시
    /// </summary>
    public void Chatting(string message,string username)
    {
        // BubbleChat
        if (bubbleChatGroup.childCount >= 3) // FIFO
        {
            StartCoroutine(bubbleChatGroup.GetChild(0).GetComponent<BubbleChat>().DestroyBubble());
        }

        GameObject bubbleObj = Instantiate(bubbleChat, bubbleChatGroup);
        bubbleObj.TryGetComponent<BubbleChat>(out BubbleChat bubble);
        bubble.SetMessage(message);

        // ListChat
        ListChatGroup.instance.addListChat.Invoke(message,username,GetComponent<NetworkingObject>().isMine);
    }
}
