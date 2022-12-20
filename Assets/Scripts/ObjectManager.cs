using Google.Protobuf.Protocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    static ObjectManager _instance;
    public static ObjectManager Instance { get { return _instance; } }

    Dictionary<int, GameObject> _objects = new Dictionary<int, GameObject>();

    

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
        GameObjectType Type = GetObjectTypeById(info.Id);
        if (Type == GameObjectType.Player)
        {
            GameObject prefab = Resources.Load<GameObject>("Prefabs/PlayerTest");
            GameObject obj = Instantiate(prefab, new Vector3(info.Transform.Pos.X, info.Transform.Pos.Y, info.Transform.Pos.Z), Quaternion.identity);

            _objects.Add(info.Id, obj);     

            if (myPlayer)
            {
                // MyPlayer 대입
                //isMine 활성화
            }
            else
            {
            }

        }
        else if(Type == GameObjectType.Room)
        {
            GameObject prefab = Resources.Load<GameObject>("Prefabs/RoomObject");
            GameObject obj = Instantiate(prefab, new Vector3(info.Transform.Pos.X, info.Transform.Pos.Y, info.Transform.Pos.Z), Quaternion.identity);
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

}
