using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.IO;
using Random = UnityEngine.Random;

public class PhotonPlayer : MonoBehaviourPun
{
    private PhotonView PV;
    private GameObject myAvatar;
    

    public int getPlayer()
    {
        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
            if (PhotonNetwork.PlayerList[i].IsLocal)
                return i;
        }
        return 0;
    }


    private void Start()
    { 
        PV = GetComponent<PhotonView>();
        int spawnPicker = getPlayer();
        if (PV.IsMine)
        {
            myAvatar = PhotonNetwork.Instantiate(Path.Combine("Prefab","Player", "PlayerAvatar"),
                GameSetup.GS.spawnPoints[spawnPicker].position, GameSetup.GS.spawnPoints[spawnPicker].rotation, 0);
        }
    }
}