using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class CreateRoom : MonoBehaviourPunCallbacks
{

    [SerializeField]
    private Text roomName;

    public Canvas CurrentRoomCanvas;
    public Canvas CreateRoomCanvas;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
            OnClick_CreateRoom();
    }
    public void OnClick_CreateRoom()
    {
        if (!PhotonNetwork.IsConnected)
            return;
        RoomOptions options = new RoomOptions();
        options.MaxPlayers = 4;
        PhotonNetwork.JoinOrCreateRoom(roomName.text, options, TypedLobby.Default); 
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("Created room successfully");
        //CurrentRoomCanvas.sortingOrder += 1;
        //CreateRoomCanvas.sortingOrder -= 1;
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Room creation failed: " + message);
    }
}
