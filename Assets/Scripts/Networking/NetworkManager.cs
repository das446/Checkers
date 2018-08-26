using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Checkers.Network {
    public class NetworkManager : MonoBehaviour {
        public static NetworkManager Instance { get; set; }

        public string ClientName;

        public GameObject serverPrefab;
        public GameObject clientPrefab;

        public GameObject mainMenu;
        public GameObject serverMenu;
        public GameObject connectMenu;
        public GameObject ConnectingText;

        public Server server;

        public static int portNumber = 6321;

        public Text DebugText;
        public Text IP;

        public List<Client> Clients;

        public bool gameStarted = false;

        public static int mainThreadId;

        void Start() {
            Instance = this;
            serverMenu.SetActive(false);
            connectMenu.SetActive(false);
            DontDestroyOnLoad(gameObject);
            mainThreadId = System.Threading.Thread.CurrentThread.ManagedThreadId;
            //DontDestroyOnLoad(DebugText.gameObject.transform.parent.gameObject);

        }

        void Update() {
            Event e = Event.current;
            if (e == null) { return; }
            if (!e.isKey) { return; }
            if (e.keyCode == KeyCode.Return && GameObject.Find("HostInput").activeSelf) {
                ConnectButton();
            }
        }

        public void ConnectButton() {
            mainMenu.SetActive(false);
            connectMenu.SetActive(true);
            string lastHost = PlayerPrefs.GetString("LastIP", "127.0.0.1");
            GameObject.Find("IP Address Textbox").GetComponent<InputField>().text = lastHost;

        }
        public void HostButton() {

            try {
                Server s = Instantiate(serverPrefab).GetComponent<Server>();
                s.Init();
                server = s;

                Client c = Instantiate(clientPrefab).GetComponent<Client>();
                Clients.Add(c);
                c.clientName = ClientName;
                c.clientName = "Player1";
                c.Host = true;
                c.ConnectToServer(LocalIPAddress().ToString(), portNumber);
                IP.text = LocalIPAddress().ToString();
            } catch (Exception e) {
                debug(e.Message);
            }

            mainMenu.SetActive(false);
            serverMenu.SetActive(true);
        }

        public void ConnectToServerButton() {
            string hostAdress = GameObject.Find("IP Address Textbox").GetComponent<InputField>().text;
            if (hostAdress == "") {
                hostAdress = "127.0.0.1";
            }

            try {
                PlayerPrefs.SetString("LastIP", hostAdress);
                Client c = Instantiate(clientPrefab).GetComponent<Client>();
                c.clientName = "Player2";
                Clients.Add(c);
                c.ConnectToServer(hostAdress, portNumber);
                //ConnectingText.SetActive(true);
                //connectMenu.SetActive(false);
                StartGame();
            } catch (Exception e) {
                debug(e.Message);
            }
        }
        public void BackButton() {
            mainMenu.SetActive(true);
            serverMenu.SetActive(false);
            connectMenu.SetActive(false);
            ConnectingText.SetActive(false);

            Server s = FindObjectOfType<Server>();
            if (s != null) {
                server = null;
                Destroy(s.gameObject);
            }

            Client c = FindObjectOfType<Client>();
            if (c != null) {
                Clients.Remove(c);
                c.CloseSocket();
                Destroy(c.gameObject);
            }

        }

        public void StartGame() {
            if (gameStarted) { return; }
            UnityEngine.SceneManagement.SceneManager.LoadScene(1);
            gameStarted = true;
            
        }

        public static void debug(string s) {

            if(!IsMainThread){return;}

            if (Instance.DebugText == null) {
                Instance.DebugText = GameObject.Find("Debug")?.GetComponent<Text>();
            }
            Instance.DebugText.text += s + "\n";
            Instance.Invoke("ClearDebug", 20);
        }

        public void ClearDebug() {
            if (Instance.DebugText == null) {
                Instance.DebugText = GameObject.Find("Debug")?.GetComponent<Text>();
            }
            Instance.DebugText.text = "";
        }

        private IPAddress LocalIPAddress() {
            if (!System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable()) {
                return null;
            }

            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());

            return host
                .AddressList
                .FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork);
        }

        public static bool IsMainThread {
            get { return System.Threading.Thread.CurrentThread.ManagedThreadId == mainThreadId; }
        }

    }
}