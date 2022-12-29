using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCustomization : MonoBehaviour
{
    [Header("Player Layer")]
    [SerializeField] private int LayerPlayer;

    [Header("Player Color Picker")]
    [SerializeField] private GameObject playerColorPicker;

    [Header("Body Color Picker")]
    [SerializeField] private GameObject bodyColorPicker;

    void Start()
    {
        LayerPlayer = LayerMask.NameToLayer("Player");
    }

    void Update()
    {
        MouseClickCheck();
    }

    /// <summary>
    /// 2022.12.28 / LJ
    /// 플레이어를 클릭하면 색상변경 UI 띄우기
    /// </summary>
    void MouseClickCheck()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray,out hit))
            {
                if (hit.transform.gameObject.layer == LayerPlayer)
                {
                    //action
                    if (playerColorPicker.activeSelf)
                    {
                        GetComponent<ColorPreview>().previewGraphic.color = playerColorPicker.GetComponent<ColorPicker>().color;
                        playerColorPicker.SetActive(false);
                    }
                    else
                    {
                        playerColorPicker.GetComponent<ColorPicker>().color = GetComponent<ColorPreview>().previewGraphic.color;
                        playerColorPicker.SetActive(true);
                    }
                }
            }
        }
    }
}
