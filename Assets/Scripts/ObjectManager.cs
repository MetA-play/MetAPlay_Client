using Google.Protobuf.Protocol;
using System.Collections;
using System.Collections.Generic;
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

    void Start()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(gameObject);
    
    }


    public void Add(ObjectInfo info, bool myPlayer = false)
    {
        Debug.Log("애드");
        GameObjectType Type = GetObjectTypeById(info.Id);
        if (Type == GameObjectType.Player)
        {
            GameObject prefab = Resources.Load<GameObject>("Prefabs/PlayerDemo");
            GameObject obj = Instantiate(prefab, new Vector3(info.Transform.Pos.X, info.Transform.Pos.Y, info.Transform.Pos.Z), Quaternion.identity);

            _objects.Add(info.Id, obj);
            obj.GetComponent<NetworkingObject>().Id = info.Id;

            PlayerInfo pInfo = obj.GetComponent<PlayerInfo>();
            pInfo.UserName = info.UserData.NickName;
            pInfo._headPartsIdx = info.UserData.HeadPartsIdx;
            pInfo._footPartsIdx = info.UserData.FootPartsIdx;
            if (myPlayer)
            {
                // MyPlayer 대입
                MyPlayer = obj.GetComponent<PlayerController>();
                MyPlayer.isMine = true;
                //isMine 활성화
            }
            else
            {
                obj.GetComponentInChildren<Camera>().gameObject.SetActive(false);
                Destroy(obj.GetComponent<PlayerInput>());
                Destroy(obj.GetComponent<PlayerCameraView>());
            }
        }
        else if(Type == GameObjectType.Room)
        {
            GameObject prefab = Resources.Load<GameObject>("Prefabs/RoomObject");
            GameObject obj = Instantiate(prefab, new Vector3(info.Transform.Pos.X, info.Transform.Pos.Y, info.Transform.Pos.Z), Quaternion.identity);

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
            Destroy(_objects[Id]);
        }
    }

}
