using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System.Net;
using System;

public class Server : MonoBehaviour
{
    Socket _socket;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void InitialSocket()
    {
        IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, 58001);
        _socket = new Socket(endPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        _socket.Bind(endPoint);
        _socket.Listen(100);
    }
    bool isRunning = false;
    private void listenRecv()
    {
        while (isRunning)
        {
            try
            {
                _socket.BeginAccept(new System.AsyncCallback(AsysAcceptCallBack), _socket);
            }
            catch (System.Exception)
            {

                throw;
            }
        }
    }
    void AsysAcceptCallBack(IAsyncResult ar)
    {
        Socket listener = (Socket)ar.AsyncState;
    }
}
