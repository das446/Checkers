using Checkers.Network;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScripts : MonoBehaviour {

    public void MainMenu()
    {
        GameObject c = GameObject.Find("Client");
        GameObject s = GameObject.Find("Server");
        GameObject nm = GameObject.Find("NetworkManager");

        //c.GetComponent<Client>().CloseSocket();

        Destroy(c);
        Destroy(s);
        Destroy(nm);

        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        GameObject c = GameObject.Find("Client(Clone)");
        GameObject s = GameObject.Find("Server(Clone)");
        GameObject nm = GameObject.Find("NetworkManager");

        c.GetComponent<Client>().CloseSocket();

        Destroy(c);
        Destroy(s);
        Destroy(nm);

        SceneManager.LoadScene(0);
    }

    public void QuitApp()
    {
        Application.Quit();
    }
}
