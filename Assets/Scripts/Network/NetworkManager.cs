using ServerCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using Google.Protobuf;
using Google.Protobuf.Protocol;

public class NetworkManager : MonoBehaviour
{

    static NetworkManager _instance;
    public static NetworkManager Instance { get { return _instance; } }
    ServerSession _session = new ServerSession();


    public UserInfo UserInfo { get; set; }
    public RoomInfo JoinedRoom { get; set; } = new RoomInfo();

    private void Start()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
        Init();
    }

    public void Send(IMessage packet)
    {
        _session.Send(packet);
    }

    public void Init()
    {
        // DNS (Domain Name System)
        /*string host = Dns.GetHostName();
        IPHostEntry ipHost = Dns.GetHostEntry(host);
        IPAddress ipAddr = IPAddress.Parse("10.82.17.113");
        IPEndPoint endPoint = new IPEndPoint(ipAddr, 7777);*/

        string host = Dns.GetHostName();
        IPHostEntry ipHost = Dns.GetHostEntry(host);
        IPAddress ipAddr = IPAddress.Parse("192.168.214.234");
        IPEndPoint endPoint = new IPEndPoint(ipAddr, 7777);

        Connector connector = new Connector();

        connector.Connect(endPoint,
            () => { return _session; },
            1);
    }

    public void Update()
    {
        List<PacketMessage> list = PacketQueue.Instance.PopAll();
        foreach (PacketMessage packet in list)
        {
            Action<PacketSession, IMessage> handler = PacketManager.Instance.GetPacketHandler(packet.Id);
            if (handler != null)
                handler.Invoke(_session, packet.Message);
        }
    }

    /// <summary>
    /// 2022. 12. 19.  / Eunseong
    /// 방 생성 요청을 서버에게 보내는 함수
    /// </summary>
    /// <param name="setting"></param>

    public void CreateRoom(RoomSetting setting)
    {
        C_CreateRoomReq req = new C_CreateRoomReq();
        req.Setting = setting;
        _session.Send(req);
    }


    /// <summary>
    /// 2022. 12. 19. / Eunseong
    /// 방 참가 요청을 서버에게 보내는 함수
    /// </summary>
    /// <param name="id"></param>
    public void JoinRoom(int Id)
    {
        C_JoinRoomReq req = new C_JoinRoomReq();
        req.RoomId = Id;
        _session.Send(req);
    }


}
