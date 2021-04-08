using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class Player_portal : MonoBehaviour
{
    public GameObject Player;
    
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

            
            LoadRandomRoom();
            
        }

    }

    //Tp
    public void LoadRandomRoom()
    {
        int nbLvl = Random.Range(0,2); //Je génère une scène aléatoire
        Scene scene = (Scene)nbLvl; 
        string lvl = scene.ToString(); //Je prend son string
        PhotonNetwork.LoadLevel(lvl);
        Debug.Log("Room Loaded" + lvl );
    }
}
