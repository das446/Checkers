using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

namespace Checkers.Network {
    public class Server : MonoBehaviour {

        public int port = 6321;

        List<ServerClient> clients;
        List<ServerClient> disconnectList;

        TcpListener server;
        bool ServerStarted;
        string Player1Name;

        string CurrentPlayer;
        public bool[, ] Monsters;
        bool MonstersMade;

        public void Init() {
            DontDestroyOnLoad(gameObject);
            Player1Name = PlayerPrefs.GetString("Player1Name");
            CurrentPlayer = Player1Name;
            clients = new List<ServerClient>();
            disconnectList = new List<ServerClient>();

            try {
                server = new TcpListener(IPAddress.Any, port);
                server.Start();

                StartListening();
                ServerStarted = true;
            } catch (Exception e) {
                NetworkManager.debug("Socket Error: " + e.Message);
            }
        }

        void Update() {
            if (!ServerStarted) {
                return;
            }

            if(clients.Count==2&&NetworkManager.Instance.gameStarted==false){
              NetworkManager.Instance.StartGame();  
            }

            for (int i = 0; i < clients.Count; i++) {
                ServerClient c = clients[i];
                if (!IsConnected(c.tcp)) {
                    c.tcp.Close();
                    disconnectList.Add(c);
                    continue;
                } else {
                    NetworkStream s = c.tcp.GetStream();
                    if (s.DataAvailable) {
                        StreamReader reader = new StreamReader(s, true);
                        string data = reader.ReadLine();
                        if (!String.IsNullOrEmpty(data)) {
                            OnIncominngData(c, data);
                        }
                    }
                }
            }

            for (int i = 0; i < disconnectList.Count - 1; i++) {
                Broadcast("Disconnect|" + disconnectList[i], clients);
                clients.Remove(disconnectList[i]);
                disconnectList.RemoveAt(i);
            }
        }

        void StartListening() {
            server.BeginAcceptTcpClient(AcceptTcpClient, server);
        }
        void AcceptTcpClient(IAsyncResult ar) {
            TcpListener listener = (TcpListener) ar.AsyncState;
            string allUsers = "";
            foreach (ServerClient SC in clients) {
                allUsers += SC.ClientName + '|';

            }
            ServerClient sc = new ServerClient(listener.EndAcceptTcpClient(ar));
            clients.Add(sc);
            clients[0].ClientName = Player1Name;
            StartListening();

            Broadcast("SWHO|" + clients[0].ClientName, clients[clients.Count - 1]);
            

            if(NetworkManager.Instance.gameStarted==false && clients.Count==2){
                NetworkManager.Instance.StartGame();
                NetworkManager.debug("Start Game");
            }
        }

        bool IsConnected(TcpClient c) {
            try {
                if (c != null && c.Client != null && c.Client.Connected) {
                    if (c.Client.Poll(0, SelectMode.SelectRead)) {
                        return !(c.Client.Receive(new byte[1], SocketFlags.Peek) == 0);
                    }
                    return true;
                } else { return false; }
            } catch {
                return false;
            }
        }

        void Broadcast(string data, List<ServerClient> cl) {
             NetworkManager.debug("Server Broadcast| "+data);
            foreach (ServerClient sc in cl) {
                try {
                    StreamWriter writer = new StreamWriter(sc.tcp.GetStream());
                    writer.WriteLine(data);
                    writer.Flush();
                } catch (Exception e) {
                    NetworkManager.debug("Write Error : " + e.Message);
                }
            }
        }

        void Broadcast(string data, ServerClient c) {
            List<ServerClient> sc = new List<ServerClient> { c };
            Broadcast(data, sc);
        }

        void OnIncominngData(ServerClient c, string data) {

            string[] aData = data.Split('|');

            switch (aData[0]) {
                case "CWHO":
                    c.ClientName = aData[1];
                    Broadcast("SCNN|" + c.ClientName, clients);
                    break;

                case "Current?":
                    Broadcast("Current|" + CurrentPlayer, clients);
                    break;

                case "Test":
                    Broadcast("Test|" + aData[1], byName(aData[1]));
                    break;
                case "Select":
                    Broadcast("Selected|" + aData[1] + "|" + aData[2], ClientsExcept(aData[1]));
                    break;
                case "SetGhost":
                    Broadcast("SetGhost|" + aData[2] + "|" + aData[3], ClientsExcept(aData[1]));
                    break;
                case "GhostRot":
                    Broadcast("GhostRot", ClientsExcept(aData[1]));
                    break;

                default:
                    break;
            }
        }

        ServerClient byName(string Name) {
            for (int i = 0; i < clients.Count; i++) {
                if (clients[i].ClientName == Name) {
                    return clients[i];
                }
            }
            return null;
        }

        List<ServerClient> ClientsExcept(string c) {
            ServerClient sc = byName(c);
            return clients.Where(x => x != sc).ToList();
        }

        List<ServerClient> ClientsExcept(ServerClient c) {
            return clients.Where(x => x != c).ToList();
        }
    }

    public class ServerClient {
        public string ClientName;
        public TcpClient tcp;

        public ServerClient(TcpClient Tcp, string client) {
            tcp = Tcp;
            ClientName = client;
        }

        public ServerClient(TcpClient Tcp) {
            tcp = Tcp;
        }

    }
}