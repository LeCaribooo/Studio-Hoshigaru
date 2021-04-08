using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class DeadState : MonoBehaviour
{
    int actualDisplay = 0;
    Parallaxing parallaxing;
    public Canvas UI;
    private PhotonView PV;
    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
        if (PV.IsMine)
        {
            parallaxing = GameObject.Find("_GameMaster").GetComponent<Parallaxing>();
            parallaxing.cam = DisplayCameraWhenDead();
            UI.gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (PV.IsMine)
        {
            parallaxing.cam = DisplayCameraWhenDead();
        }
    }


    public Transform DisplayCameraWhenDead()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        if (players.Length == 0)
        {
            return null;
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                players[actualDisplay].GetComponent<PlayerControler>().camera.gameObject.SetActive(false);
                actualDisplay++;
                actualDisplay = actualDisplay % players.Length;
            }
            players[actualDisplay].GetComponent<PlayerControler>().camera.gameObject.SetActive(true);
            return players[actualDisplay].GetComponent<PlayerControler>().camera.transform;
        }
    }
}
