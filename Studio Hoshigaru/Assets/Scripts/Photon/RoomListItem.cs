using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using UnityEngine.UI;

public class RoomListItem : MonoBehaviour
{
    [SerializeField] Text text;

    public RoomInfo info;

    public void SetUp(RoomInfo _info)
    {
        info = _info;
        text.text = _info.Name + "\t" + _info.PlayerCount + "/" + _info.MaxPlayers;
    }

    public void OnClick()
    {
        Launcher.Instance.JoinRoom(info);
    }

}
