using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Customization;
using UnityEngine.Events;
using TMPro;

public class LegSelect : MonoBehaviour
{
    [Header("Current")]
    [SerializeField] private Leg current;

    [SerializeField] private CustomSelectButton button;

    [Header("Objects")]
    [SerializeField] private List<GameObject> legs;

    [Header("PartParent")]
    [SerializeField] private Transform LegPart;

    [SerializeField] private TextMeshProUGUI partName;

    public UnityEvent Change;

    void Start()
    {
        Change.AddListener(ChangeObject);
    }

    void Update()
    {
        current = button.leg;
    }

    /// <summary>
    /// 2022.12.28 / LJ
    /// 미리보기 오브젝트 변경
    /// </summary>
    public void ChangeObject()
    {
        for (int i = 0; i < LegPart.childCount; i++)
        {
            Destroy(LegPart.GetChild(i).gameObject);
        }
        if (current != Leg.선택안함)
        {
            Instantiate(legs[(int)current - 1], LegPart);
        }
        partName.text = current.ToString();
    
        MainSceneManager.Instance._footPartIdx = ((int)current > 0) ? (int)current - 1 : (int)current; ;
    }

    
}
