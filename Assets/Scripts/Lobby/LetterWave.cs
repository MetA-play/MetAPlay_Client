using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterWave : MonoBehaviour
{
    [Header("Letter")]
    [SerializeField] private Transform[] Letters;

    [Header("Delta")]
    [SerializeField]
    private float delta;
    public float Delta { get { return delta; } set
        {
            if (value > 180)
            {
                float val = value - 180;
                delta = val;
            }
        } }

    [Header("Speed")]
    [SerializeField] [Range(0f, 10f)] private float hertz = 2.5f;
    [SerializeField] [Range(0f, 10f)] private float movingSpeed = 2.5f;

    void Update()
    {
        delta += Time.deltaTime * movingSpeed;
        //Debug.Log($":: delta -> {delta}");
        //Debug.Log($":: Sin(delta) -> {Mathf.Sin(delta)}");
        //Debug.Log($":: Cos(delta) -> {Mathf.Cos(delta)}");
        Waving();
    }

    /// <summary>
    /// 2022.12.23 / LJ
    /// 글자 움직임 관련
    /// </summary>
    void Waving()
    {
        for(int i = 0; i < Letters.Length; i++)
        {
            if (i % 2 == 1)
                Letters[i].transform.localPosition = new Vector3(Letters[i].localPosition.x, Mathf.Sin(delta) * hertz, 0f);
            else 
                Letters[i].transform.localPosition = new Vector3(Letters[i].localPosition.x, Mathf.Cos(delta) * hertz, 0f);
                
        }
    }
}
