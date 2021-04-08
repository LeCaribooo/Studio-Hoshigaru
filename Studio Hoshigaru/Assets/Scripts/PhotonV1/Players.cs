using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Players : MonoBehaviour
{
    [SerializeField]
    private Text text;
    public bool Ready = false;
    public Player _player { get; private set; }

    public void SetPlayerInfo(Player player)
    {
        _player = player;
        text.text = player.NickName;
    }
}
