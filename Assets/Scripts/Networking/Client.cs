using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Net.Sockets;
using System;
using System.Linq;


namespace Checkers.Network
{
    public class Client : MonoBehaviour
    {

        bool socketReady;
        public string clientName;
        TcpClient socket;
        NetworkStream stream;
        StreamWriter writer;
        StreamReader reader;
        List<GameClient> Players = new List<GameClient>();
        public bool Host;
        public Client Opponent;
        string host;
        int port;

        void Start()
        {
            DontDestroyOnLoad(gameObject);
        }

        void Update()
        {
            if (socketReady)
            {
                if (stream.DataAvailable)
                {
                    string data = reader.ReadLine();
                    if (data != null)
                    {
                        OnIncomingData(data);
                    }
                }
            }
            if (!socket.Connected)
            {
                ConnectToServer(host,port);
            }
            
            
        }

        public bool ConnectToServer(string host, int port)
        {
            if (socketReady) { return false; }

            try
            {
                socket=null;
                socket = new TcpClient(host, port);
                stream = socket.GetStream();
                writer = new StreamWriter(stream);
                reader = new StreamReader(stream);

                socketReady = true;
                this.host=host;
                this.port=port;

            }
            catch (Exception e)
            {
                GameManager.debug("Socket Error : " + e.Message);
            }

            return socketReady;
        }

        void OnIncomingData(string data)
        {
            string[] aData = data.Split('|');

            switch (aData[0])
            {
                case "SWHO":
                    for (int i = 1; i < aData.Length; i++)
                    {
                        UserConnected(aData[i], false);
                    }
                    Send("CWHO|" + clientName);
                    break;

                case "SCNN":
                    UserConnected(aData[1], true);
                    break;


                case "Test":
                    GameManager.debug("Recieved Response From Server");
                    break;


                case "Disconnect":
                    UserDisconnected(aData[1]);
                    break;

                default:
                    break;
            }
        }
        public void Send(string data)
        {
            if (!socketReady)
            {
                return;
            }
            writer.WriteLine(data);
            writer.Flush();
        }

        public void CloseSocket()
        {
            if (!socketReady)
            {
                return;
            }

            writer.Close();
            reader.Close();
            socket.Close();
            socketReady = false;
        }

        void OnIncominngData(ServerClient c, string data)
        {
            OnIncomingData(data);
        }

        public IEnumerator RequestUntil(Func<bool> condition, string msg)
        {
            while (!condition())
            {
                Send(msg);
                yield return new WaitForSeconds(1);
            }
        }

        public IEnumerator RequestWhile(Func<bool> condition, string msg)
        {
            while (condition())
            {
                Send(msg);
                yield return new WaitForSeconds(1);
            }
        }

        void UserConnected(string Name, bool Host)
        {
            if (Name == "" || Players.Any(x => x.name == Name)) { return; }
            GameClient c = new GameClient();
            c.name = Name;
            Players.Add(c);
            if (!GameManager.Instance.Clients.Any(x => x.clientName == Name))
            {
                Client C = Instantiate(GameManager.Instance.clientPrefab).GetComponent<Client>();
                C.clientName = Name;
                C.name = Name;
                GameManager.Instance.Clients.Add(C);
            }
            if (Players.Count == 2)
            {
                GameManager.Instance.StartGame();
            }
        }

        void OnApplicationQuit()
        {
            CloseSocket();
        }

        void OnDisable()
        {
            CloseSocket();
        }
        void OnDestroy()
        {
            CloseSocket();
        }

        void UserDisconnected(string Name){
            Debug.Log(Name+" Disconnected");
        }
        
    }

    public class GameClient
    {
        public string name;
        public bool isHost;
    }
}