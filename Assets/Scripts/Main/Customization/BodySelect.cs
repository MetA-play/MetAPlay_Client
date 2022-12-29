using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Customization;
using UnityEngine.Events;
using TMPro;

public class BodySelect : MonoBehaviour
{
    [Header("Current")]
    [SerializeField] private Body current;

    [SerializeField] private CustomSelectButton button;

    [Header("Objects")]
    [SerializeField] private List<GameObject> bodys;

    [Header("PartParent")]
    [SerializeField] private Transform BodyPart;

    [SerializeField] private TextMeshProUGUI partName;

    [Header("ColorPicker")]
    [SerializeField] private GameObject colorPicker;

    [Header("PlayerColorPicker")]
    [SerializeField] private GameObject playerColorPicker;

    public UnityEvent Change;

    void Start()
    {
        Change.AddListener(ChangeObject);
    }

    void Update()
    {
        current = button.body;
    }

    /// <summary>
    /// 2022.12.28 / LJ
    /// 미리보기 오브젝트 변경
    /// </summary>
    public void ChangeObject()
    {
        for (int i = 0; i < BodyPart.childCount; i++)
        {
            Destroy(BodyPart.GetChild(i).gameObject);
        }
        colorPicker.SetActive(false);
        if (current != Body.선택안함)
        {
            GameObject Obj = Instantiate(bodys[(int)current - 1], BodyPart);
            if (current == Body.망토)
            {
                playerColorPicker.SetActive(false);
                colorPicker.SetActive(true);
                Obj.TryGetComponent<ColorPreview>(out ColorPreview color);
                color.colorPicker = colorPicker.GetComponent<ColorPicker>();
                colorPicker.GetComponent<ColorPicker>().color = color.previewGraphic.color;
            }
        }
        partName.text = current.ToString();
    }
}
