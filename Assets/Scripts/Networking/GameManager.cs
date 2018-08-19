using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
using System.Net;
using System.Net.Sockets;
using System.Linq;

namespace Checkers.Network
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; set; }

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

        void Start()
        {
            Instance = this;
            serverMenu.SetActive(false);
            connectMenu.SetActive(false);
            DontDestroyOnLoad(gameObject);
            DontDestroyOnLoad(DebugText.gameObject.transform.parent.gameObject);
            ClientName = PlayerPrefs.GetString("Player1Name");
        }

        void Update(){
            Event e = Event.current;
            if(e==null){return;}
            if (!e.isKey) {return;}
            if(e.keyCode==KeyCode.Return&&GameObject.Find("HostInput").activeSelf){
                ConnectButton();
            }
        }

        public void ConnectButton()
        {
            mainMenu.SetActive(false);
            connectMenu.SetActive(true);
            string lastHost=PlayerPrefs.GetString("LastIP","127.0.0.1");
            GameObject.Find("HostInput").GetComponent<InputField>().text=lastHost;

        }
        public void HostButton()
        {

            try
            {
                Server s = Instantiate(serverPrefab).GetComponent<Server>();
                s.Init();
                server = s;

                Client c = Instantiate(clientPrefab).GetComponent<Client>();
                Clients.Add(c);
                c.clientName = ClientName;
                if (c.clientName == "") { c.clientName = "Host"; }
                c.ConnectToServer(LocalIPAddress().ToString(), portNumber);
				IP.text="Waiting for another player...\nYoure IP is "+	LocalIPAddress().ToString();
            }
            catch (Exception e)
            {
                debug(e.Message);
            }

            mainMenu.SetActive(false);
            serverMenu.SetActive(true);
        }

        public void ConnectToServerButton()
        {
            string hostAdress = GameObject.Find("HostInput").GetComponent<InputField>().text;
            if (hostAdress == "")
            {
                hostAdress = "127.0.0.1";
            }

            try
            {
                PlayerPrefs.SetString("LastIP",hostAdress);
                Client c = Instantiate(clientPrefab).GetComponent<Client>();
                c.clientName = ClientName;
                Clients.Add(c);
                c.ConnectToServer(hostAdress, portNumber);
                ConnectingText.SetActive(true);
                connectMenu.SetActive(false);
            }
            catch (Exception e)
            {
                debug(e.Message);
            }
        }
        public void BackButton()
        {
            mainMenu.SetActive(true);
            serverMenu.SetActive(false);
            connectMenu.SetActive(false);
            ConnectingText.SetActive(false);

            Server s = FindObjectOfType<Server>();
            if (s != null)
            {
                server = null;
                Destroy(s.gameObject);
            }

            Client c = FindObjectOfType<Client>();
            if (c != null)
            {
                Clients.Remove(c);
                c.CloseSocket();
                Destroy(c.gameObject);
            }

        }

        public void StartGame()
        {
            SceneManager.LoadScene(3);
        }

        public static void debug(string s)
        {
            Instance.DebugText.text += s + "\n";
			Instance.Invoke("ClearDebug",10);
        }

		public void ClearDebug(){
			Instance.DebugText.text="";
		}


		private IPAddress LocalIPAddress()
        {
            if (!System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
            {
                return null;
            }

            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());

            return host
                .AddressList
                .FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork);
        }



    }
}