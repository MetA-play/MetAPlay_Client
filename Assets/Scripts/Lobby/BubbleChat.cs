using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BubbleChat : MonoBehaviour
{
    [Header("Animation")]
    [SerializeField] private Animator anim;

    [Header("TMP")]
    [SerializeField] private TextMeshProUGUI message;

    void Start()
    {
        Destroy(this.gameObject, 3f);
    }

   void Update()
    {
    }

    /// <summary>
    /// 2022.12.26 / LJ
    /// 채팅 적용
    /// </summary>
    /// <param name="message">글자 수가 30자가 넘는다면 에러 메세지</param>
    public void SetMessage(string message)
    {
        if (message.Length > 30)
        {
            // error message
            return;
        }
        this.message.text = message;
    }

    

    /// <summary>
    /// 2022.12.26 / LJ
    /// 채팅 삭제
    /// </summary>
    public IEnumerator DestroyBubble()
    {
        // anim
        //anim.SetTrigger("Destroy");
        yield return null;
        Destroy(this.gameObject);
    }
}
