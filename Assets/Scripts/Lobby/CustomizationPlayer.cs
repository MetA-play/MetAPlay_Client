using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomizationPlayer : MonoBehaviour
{
    [SerializeField] private Transform HeadCustom;
    [SerializeField] private Transform BodyCustom;
    [SerializeField] private Transform LegCustom;

    [SerializeField] List<GameObject> Heads;
    [SerializeField] List<GameObject> Bodys;
    [SerializeField] List<GameObject> Legs;

    MeshRenderer[] MR;  

    void Start()
    {
        MR = gameObject.transform.parent.gameObject.GetComponentsInChildren<MeshRenderer>();
        for(int i = 0; i<MR.Length;i++)
            MR[i].material = Instantiate(MR[i].material);
        PlayerInfo info = GetComponentInParent<PlayerInfo>();
        SetCustom(info._headPartsIdx, info._bodyPartsIdx, info._footPartsIdx, info.CloackColor, info.BodyColor);
    }

    void Update()
    {
        
    }

    public void SetCustom(int head, int body, int leg,Color cloak, Color color)
    {
        for (int i = 0; i < MR.Length; i++)
        {
            if(!MR[i].gameObject.CompareTag("NotColor"))
                MR[i].material.color = color;
        }
        if (head >= 0)
            Instantiate(Heads[head], HeadCustom);
        if (body >= 0)
        { 
            GameObject Cloak = Instantiate(Bodys[body], BodyCustom);
            MeshRenderer R = Cloak.GetComponentInChildren<MeshRenderer>();
            R.material = Instantiate(R.material);
            if (body == 1)
                R.material.color = cloak;
        }
        if (leg >= 0)
            Instantiate(Legs[leg], LegCustom);
    }
}
