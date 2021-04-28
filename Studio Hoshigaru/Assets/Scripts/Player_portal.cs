using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class Player_portal : MonoBehaviourPun
{
    public GameObject Player;
    public GameObject block;
    public Canvas Vote;
    public Canvas Level;
    public Canvas DecompteCanvas;
    public Button LevelVote;
    public Text text;
    public Text Count;
    public Text DecompteTxt;
    private bool HasClickE = false;
    private bool ready = false;
    public Button Ready;
    [SerializeField] Text readyUpText;
    private bool WantToTeleport = false;

    private List<bool> playerReady = new List<bool>();

    [SerializeField]
    private string[] scene = new string[2];

    //Timer
    float time = 21f;    

    GameObject getMinePlayer()
    {
        GameObject[] joueur = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < joueur.Length; i++)
        {
            if (joueur[i].GetPhotonView().IsMine)
            {
                return joueur[i];
            }
        }
        //Pas censé arrivé.
        Debug.Log("Tu n'existes pas...");
        return null;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player" &&  collision.gameObject == getMinePlayer())
        {
            Level.gameObject.SetActive(true);
            if (HasClickE)
            {
                LevelVote.gameObject.SetActive(false);
            }
        }
        

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && collision.gameObject == getMinePlayer())
        {
            Level.gameObject.SetActive(false);
        }
    }

    public void Click_ToVote()
    {
        Level.gameObject.SetActive(false);
        GameObject[] player = GameObject.FindGameObjectsWithTag("Player");
        foreach (var joueur in player)
        {                
            DontDestroyOnLoad(joueur);
        }
        SendNotif();

        VoteCanvas();
        
    }
    private void Update()
    {
        if (HasClickE)
        {
            time -= Time.deltaTime;
            int sec = (int)time;
            text.text = "00 : " + sec.ToString();
            Count.text = playerReady.Count.ToString() + " / " + PhotonNetwork.PlayerList.Length.ToString();
            WantToTeleport = playerReady.Count == PhotonNetwork.PlayerList.Length;
            if (time <= 0f  || WantToTeleport) 
            {
                LevelVote.gameObject.SetActive(false);
                Vote.gameObject.SetActive(false);
                time = 21f;
                if (WantToTeleport)
                {
                    StartCoroutine(Decompte());
                }
                else
                {
                    LevelVote.gameObject.SetActive(true);
                }
                HasClickE = false;
                
            }
        }
    }


    //Teleportation
    public void LoadRandomRoom()
    {
        int nbLvl = Random.Range(0,2); //Je génère une scène aléatoire
        string sceneload = scene[nbLvl];
        PhotonNetwork.LoadLevel(sceneload);
        Debug.Log("Room Loaded" );
    }

    public void SendNotif()
    {
        base.photonView.RPC("SendMessage", RpcTarget.All, "Hello there");
    }

    public void VoteCanvas()
    {
        base.photonView.RPC("VoteRPC", RpcTarget.All);
    }

    public void OnClick_ReadyUp()
    {
        SetReadyUp(!ready);
    }
    private void SetReadyUp(bool state)
    {
        ready = state;
        if (ready)
        {
            readyUpText.text = "Ready !";
            Ready.interactable = false;
            base.photonView.RPC("PlayerReady", RpcTarget.All, ready);
        }
        else
            readyUpText.text = "Not ready";
    }

    IEnumerator Decompte()
    {
        DecompteCanvas.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        DecompteTxt.text = "2";
        yield return new WaitForSeconds(1f);
        DecompteTxt.text = "1";
        yield return new WaitForSeconds(1f);
        DecompteTxt.text = "Iku !";
        yield return new WaitForSeconds(1.2f);
        DecompteCanvas.gameObject.SetActive(false);
        LoadRandomRoom();
    }
    
    [PunRPC]
    //Envoie la notif à tout le monde.
    void SendMessage(string message)
    {
        Vote.gameObject.SetActive(true);
        int nb = PhotonNetwork.PlayerList.Length;
        Debug.Log("Message envoyé :" + message);
        Debug.Log("Nombre de joueur :" + nb);
    }

    [PunRPC]
    //Attend le vote de tout le monde.
    void VoteRPC()
    {
        HasClickE = true;
    }

    [PunRPC]
    void PlayerReady(bool state)
    {
        Debug.Log("Player ready ");
        playerReady.Add(state);
    }

}
