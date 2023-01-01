using Google.Protobuf.Protocol;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class ObjectManager : MonoBehaviour
{
    static ObjectManager _instance;
    public static ObjectManager Instance { get { return _instance; } }

    Dictionary<int, GameObject> _objects = new Dictionary<int, GameObject>();

    public PlayerController MyPlayer { get; set; }

    public static GameObjectType GetObjectTypeById(int id)
    {
        int type = (id >> 24) & 0x7F;
        return (GameObjectType)type;
    }


    //public PlayerWithNetwork MyPlayer { get; set; }

    void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(gameObject);

    }


    public void Add(ObjectInfo info, bool myPlayer = false)
    {
        GameObjectType Type = GetObjectTypeById(info.Id);


        if (Type == GameObjectType.Player)
        {
            GameObject prefab = Resources.Load<GameObject>("Prefabs/PlayerTest 1");
            GameObject obj = Instantiate(prefab, new Vector3(info.Transform.Pos.X, info.Transform.Pos.Y, info.Transform.Pos.Z), Quaternion.identity);
            obj.GetComponent<NetworkingObject>().Id = info.Id;

            _objects.Add(info.Id, obj);

            PlayerInfo pInfo = obj.GetComponent<PlayerInfo>();
            pInfo.UserName = info.UserData.NickName;
            pInfo._headPartsIdx = info.UserData.HeadPartsIdx;
            pInfo._bodyPartsIdx = info.UserData.BodyPartsIdx;
            pInfo._footPartsIdx = info.UserData.FootPartsIdx;
            if (info.UserData.BodyColor != null)
                pInfo.BodyColor = new Color(info.UserData.BodyColor.R, info.UserData.BodyColor.G, info.UserData.BodyColor.B);   
            if(info.UserData.CloakColor != null)
                pInfo.CloackColor = new Color(info.UserData.CloakColor.R, info.UserData.CloakColor.G, info.UserData.CloakColor.B);
            if (myPlayer)
            {
                // MyPlayer 대입
                MyPlayer = obj.GetComponent<PlayerController>();
                MyPlayer.isMine = true;
                PlayerCreateRoom.Instance.playerCam = ObjectManager.Instance.MyPlayer.GetComponent<PlayerCameraView>();

                //isMine 활성화
            }
            else
            {
                obj.GetComponentInChildren<Camera>().gameObject.SetActive(false);
                Destroy(obj.GetComponent<PlayerInput>());
                Destroy(obj.GetComponent<PlayerCameraView>());

            }

            
        }
        else if (Type == GameObjectType.Room)
        {
            GameObject prefab = Resources.Load<GameObject>("Prefabs/RoomObject");
            GameObject obj = Instantiate(prefab, new Vector3(info.Transform.Pos.X, info.Transform.Pos.Y, info.Transform.Pos.Z), Quaternion.identity);
            obj.GetComponent<NetworkingObject>().Id = info.Id;
            obj.GetComponent<Room>().Info.Id = (int)info.Transform.Scale.Y;
            _objects.Add(info.Id, obj);

        }
        else if (Type == GameObjectType.SoccerBall)
        {
            GameObject prefab = Resources.Load<GameObject>("Prefabs/SoccerBall");
            GameObject obj = Instantiate(prefab, new Vector3(info.Transform.Pos.X, info.Transform.Pos.Y, info.Transform.Pos.Z), Quaternion.identity);
            obj.GetComponent<NetworkingObject>().Id = info.Id;

            _objects.Add(info.Id, obj);
        }
        else if (Type == GameObjectType.None)
        {
            GameObject prefab = Resources.Load<GameObject>($"Prefabs/{info.PrefabName}");
            GameObject obj = Instantiate(prefab, new Vector3(info.Transform.Pos.X, info.Transform.Pos.Y, info.Transform.Pos.Z), Quaternion.identity);
            obj.GetComponent<NetworkingObject>().Id = info.Id;

            _objects.Add(info.Id, obj);
        }

    }

    public GameObject FindById(int Id)
    {
        GameObject obj = null;

        if (_objects.TryGetValue(Id, out obj))
            return obj;

        return null;
    }

    public void RemoveById(int Id)
    {

        GameObject obj = null;

        if (_objects.Remove(Id, out obj) == true)
        {
            Destroy(obj);
        }
    }

    public int PlayerCount()
    {
        return _objects.Keys.Count((k) => GetObjectTypeById(k) == GameObjectType.Player);
    }
}

  
