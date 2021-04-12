using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class Player_portal : MonoBehaviourPun
{
    public GameObject Player;
    public Canvas Vote; 
    
    public enum Scene {
        Level1,
        Level2
    }


    private void OnTriggerStay2D(Collider2D collider)
    {
       if (collider.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.E))
        {
            GameObject[] player = GameObject.FindGameObjectsWithTag("Player");
            foreach (var joueur in player)
            {
                DontDestroyOnLoad(joueur);
            }

            SendNotif();
            //Enlever les //
            //LoadRandomRoom();
            
        }

    }

    //Teleportation
    public void LoadRandomRoom()
    {
        int nbLvl = Random.Range(0,2); //Je génère une scène aléatoire
        Scene scene = (Scene)nbLvl; 
        string lvl = scene.ToString(); //Je prend son string
        PhotonNetwork.LoadLevel(lvl);
        Debug.Log("Room Loaded" + lvl );
    }

    public void SendNotif()
    {
        base.photonView.RPC("SendMessage", RpcTarget.All, "Hello there");
    }
    
    [PunRPC]
    void SendMessage(string message)
    {
        Vote.gameObject.SetActive(true);
        Debug.Log("Message envoyé :" + message);
    }

}
