using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[System.Serializable] public class RoomButtonSelect : UnityEvent<GameKinds> { };

public class RoomButton : MonoBehaviour,IPointerClickHandler
{
    public GameKinds kind = GameKinds.Preparing;

    public RoomButtonSelect selectEvent;

    [SerializeField] private PlayerCreateRoom createRoom;

    [Header("Stat")]
    [SerializeField] private TextMeshProUGUI gameNameText;
    [SerializeField] private TextMeshProUGUI gameDescriptionText;
    [SerializeField] private Image selectedImage;
    public string gameName;
    public string gameDescription;
    public bool isSelected;

    void Start()
    {
        Initialize();
    }

    void Update()
    {
        
    }

    /// <summary>
    /// 2022.12.30 / LJ
    /// 버튼 초기화
    /// </summary>
    void Initialize()
    {
        isSelected = false;
        gameName = gameNameText.text;
        gameDescription = gameDescriptionText.text;
        selectedImage.gameObject.SetActive(false);
    }

    /// <summary>
    /// 2022.12.30 / LJ
    /// 어떤 버튼이 선택 됬을 때 실행
    /// </summary>
    public void AnyButtonSelected(GameKinds gameKind)
    {
        if (kind == gameKind) // 선택된게 버튼이랑 같을때
        {
            isSelected = true;
            selectedImage.gameObject.SetActive(true);
        }
        else
        {
            isSelected = false;
            selectedImage.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// 2022.12.30 / LJ
    /// *마우스로 클릭했을 때 실행되는 이벤트 인터페이스
    /// 마우스로 클릭을 하면 PlayerCreateRoom에 이 스크립트의 정보를 전달 후 모든 버튼들에 있는 AnyButtonSelected함수를 실행
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        createRoom.selectedButton = this;

        Transform parent = transform.parent;

        for (int i = 0; i < parent.childCount; i++)
        {
            parent.GetChild(i).GetComponent<RoomButton>().selectEvent.Invoke(kind);
        }
    }
}
