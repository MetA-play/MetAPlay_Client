using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChat : MonoBehaviour
{
    [Header("PlayerChatGroup")]
    public Transform bubbleChatGroup;
    public Transform listChatGroup;

    [Header("ChatObject")]
    [SerializeField] private GameObject bubbleChat;
    [SerializeField] private GameObject listChat;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
            Chatting("TEST");
    }

    /// <summary>
    /// 2022.12.26 / LJ
    /// 채팅을 화면에 표시
    /// </summary>
    public void Chatting(string message)
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
    }
}
