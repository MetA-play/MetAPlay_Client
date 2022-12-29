using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using Google.Protobuf.Protocol;

public class ChatManager : MonoBehaviour
{
    [Header("Instance")]
    [HideInInspector] public static ChatManager instance;

    [SerializeField] private GameObject chatInput;

    [SerializeField] bool isactive;

    [SerializeField] PlayerChat Me;


    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    void Start()
    {
        var players = GameObject.FindGameObjectsWithTag("Player");
        foreach (var i in players)
        {
            if (i.GetComponent<NetworkingObject>().isMine)
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
                TrySendMessage();
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
    /// 2022.12.28 / LJ
    /// 채팅 보내기
    /// </summary>
    public void TrySendMessage()
    {
        chatInput.TryGetComponent<TMP_InputField>(out TMP_InputField field);

        if (field.text == "")
            CloseInputField();


        C_Chat chat = new C_Chat();
        chat.Content = field.text;

        NetworkManager.Instance.Send(chat);
        /*else
        {
            // send message
            
        }*/
    }

    public void SendMsg(PlayerChat chat,string nickName,string context)
    {
        chat.Chatting(context, nickName);
        CloseInputField();
    }
    /// <summary>
    /// 2022. 12. 28/ 은성
    /// Me 변수 Set
    /// </summary>
    public void SetMe(PlayerChat chat)
    {
        Me = chat;
    }
}
