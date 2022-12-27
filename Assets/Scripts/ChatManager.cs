using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class ChatManager : MonoBehaviour
{
    [Header("Instance")]
    [HideInInspector] public static ChatManager instance;

    [SerializeField] private GameObject chatInput;

    [SerializeField] bool isactive;

    [SerializeField] PlayerChat Me;

    void Start()
    {
        var players = GameObject.FindGameObjectsWithTag("Player");
        foreach (var i in players)
        {
            if (i.GetComponent<PlayerInfo>().isMine)
                Me = i.GetComponent<PlayerChat>();
        }
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Return))
        {
            if (isactive == false)
                OpenInputField();
            else
                SendMessage();
        }
    }

    /// <summary>
    /// 2022.12.27 / LJ
    /// 채팅 필드 활성화 및 포커싱
    /// </summary>
    void OpenInputField()
    {
        chatInput.SetActive(true);
        EventSystem.current.SetSelectedGameObject(chatInput.gameObject, null);
        chatInput.TryGetComponent<TMP_InputField>(out TMP_InputField field);
        field.OnPointerClick(new PointerEventData(EventSystem.current));
        isactive = true;
    }

    /// <summary>
    /// 2022.12.27 / LJ
    /// 채팅 필드 비활성화 및 채팅 초기화
    /// </summary>
    void CloseInputField()
    {
        chatInput.SetActive(false);
        chatInput.GetComponent<TMP_InputField>().text = null;
        isactive = false;
    }

    /// <summary>
    /// 2022.12.27 / LJ
    /// 채팅 보내기
    /// </summary>
    void SendMessage()
    {
        chatInput.TryGetComponent<TMP_InputField>(out TMP_InputField field);

        if (field.text == "")
            CloseInputField();
        else
        {
            // send message
            Me.Chatting(field.text, Me.GetComponent<PlayerInfo>().UserName);
            CloseInputField();
        }
    }
}
