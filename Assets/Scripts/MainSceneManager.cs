using Google.Protobuf.Protocol;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainSceneManager : MonoBehaviour
{
    public TMP_InputField nicknameIF;
    public int _headPartIdx;
    public int _footPartIdx;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void SetUserInfo()
    {
        C_SetUserinfo set = new C_SetUserinfo();
        set.Info.NickName = nicknameIF.text;
        set.Info.HeadPartsIdx = _headPartIdx;
        set.Info.FootPartsIdx= _footPartIdx;

        NetworkManager.Instance.Send(set);

        NetworkManager.Instance.UserInfo = set.Info;
    }

    public void OnSetUserInfo()
    {
        if(nicknameIF.text.Length > 0)
        {
            SetUserInfo();
        }
    }
}
