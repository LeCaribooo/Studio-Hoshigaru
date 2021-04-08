using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rooms : MonoBehaviour
{
    [SerializeField]
    private Text text;

    public RoomInfo RoomInfo;

    public void SetRoomInfo(RoomInfo roomInfo)
    {
        RoomInfo = roomInfo;
        text.text = roomInfo.Name + ",\t" + roomInfo.PlayerCount + "/" + roomInfo.MaxPlayers;
    }

    public void OnClick_Button()
    {
        PhotonNetwork.JoinRoom(RoomInfo.Name);
    }
}
