using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;


public class CurrentRoom : MonoBehaviourPunCallbacks, IInRoomCallbacks
{
    public Canvas CurrentRoomCanvas;
    public Canvas CreateRoomCanvas;
    
    public void OnClick_Leave()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        CreateRoomCanvas.gameObject.SetActive(true);
        CurrentRoomCanvas.gameObject.SetActive(false);
    }

}
