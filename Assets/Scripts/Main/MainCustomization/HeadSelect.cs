using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Customization;
using UnityEngine.Events;
using TMPro;

public class HeadSelect : MonoBehaviour
{
    [Header("Current")]
    [SerializeField] private Head current;

    [SerializeField] private CustomSelectButton button;

    [Header("Objects")]
    [SerializeField] private List<GameObject> heads;

    [Header("PartParent")]
    [SerializeField] private Transform HeadPart;

    [SerializeField] private TextMeshProUGUI partName;

    public UnityEvent Change;

    void Start()
    {
        ChangeObject();
    }

    void Update()
    {
        current = button.head;
    }

    /// <summary>
    /// 2022.12.28 / LJ
    /// 미리보기 오브젝트 변경
    /// </summary>
    public void ChangeObject()
    {
        for (int i = 0; i < HeadPart.childCount; i++)
        {
            Destroy(HeadPart.GetChild(i).gameObject);
        }
        if (current != Head.선택안함)
        {
            Instantiate(heads[(int)current - 1], HeadPart);
        }
        partName.text = current.ToString();

        MainSceneManager.Instance._headPartIdx = (int)current - 1;
    }
}
