using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class Player_portal : MonoBehaviour
{
    public GameObject Player;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
        //A faire -> implémenter l'aléatoire
        PhotonNetwork.LoadLevel(2);
        Debug.Log("Room Loaded");
    }
}
