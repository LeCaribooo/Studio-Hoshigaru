using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PhotonLobby : MonoBehaviourPunCallbacks
{
    public static PhotonLobby lobby;


    public GameObject joinGameButton;
    public GameObject cancelButton;

    private void Awake()
    {
        lobby = this; 
    }
    
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Player has connected to the Photon master server");
        PhotonNetwork.AutomaticallySyncScene = true;
        joinGameButton.SetActive(true);
    }

    public void OnJoinGameButtonClicked()
    {
        Debug.Log("Join game was click");
        joinGameButton.SetActive(false);
        cancelButton.SetActive(true);
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Tried to join a random game but failed. There must be no open games available");
        CreateRoom();
    }

    void CreateRoom()
    {
        Debug.Log("Trying to create a new Room");
        int randomRoomName = Random.Range(0, 10000);
        RoomOptions roomOps = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = 4};
        PhotonNetwork.CreateRoom("Room " + randomRoomName, roomOps);
        Debug.Log("Created room number : " + randomRoomName);
    }
    

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Tried to create a new room but failed, there must already be a room with the same name");
        CreateRoom();
    }

    public void OnCanceledButtonClicked()
    {
        Debug.Log("Cancel Button click");
        cancelButton.SetActive(false);
        joinGameButton.SetActive(true);
        PhotonNetwork.LeaveRoom();
    }

}
