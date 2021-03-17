using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Photon.Pun;


public class GameSetup : MonoBehaviour
{
    public static GameSetup GS;

    public Transform[] spawnPoints;

    private void OnEnable()
    {
        if (GameSetup.GS == null)
        {
            GameSetup.GS = this;
        }
    }
}