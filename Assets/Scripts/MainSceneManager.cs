using Google.Protobuf.Protocol;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneManager : MonoBehaviour
{

    static MainSceneManager instance;
    public static MainSceneManager Instance
    {
        get { return instance; }
    } 
    public TMP_InputField nicknameIF;

    public int _headPartIdx;
    public int _bodyPartIdx;
    public int _footPartIdx;
    public Material bodyMet;
    public Material cloakMet;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
            Destroy(gameObject);
    }
    void Start()
    {
    }

    void Update()
    {
        
    }

    public void SetUserInfo()
    {
        C_SetUserinfo set = new C_SetUserinfo();
        set.Info = new UserInfo();
        set.Info.NickName = nicknameIF.text;
        set.Info.HeadPartsIdx = _headPartIdx;
        set.Info.BodyPartsIdx = _bodyPartIdx;
        set.Info.FootPartsIdx= _footPartIdx;
        set.Info.BodyColor = new MetColor() { R = bodyMet.color.r, G = bodyMet.color.g,B = bodyMet.color.b};
        if (cloakMet != null)
        set.Info.CloakColor = new MetColor() { R = cloakMet.color.r, G = cloakMet.color.g,B = cloakMet.color.b};
        
        NetworkManager.Instance.UserInfo = set.Info;

        SceneManager.LoadScene("LobbyTest");
    }

    public void OnSetUserInfo()
    {
        if(nicknameIF.text.Length > 0)
        {
            SetUserInfo();
        }
    }
}
