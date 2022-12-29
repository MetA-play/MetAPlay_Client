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

    

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void SetCustom(int head, int body, int leg,Color cloak, Color color)
    {
        if (head != 0)
            Instantiate(Heads[head], HeadCustom);
        if (body != 0)
        { 
            GameObject Cloak = Instantiate(Bodys[body], BodyCustom);
            if (body == 1)
                Cloak.GetComponent<Material>().color = cloak;
        }
        if (leg != 0)
            Instantiate(Legs[leg], LegCustom);
        // PlayerColor
    }
}
