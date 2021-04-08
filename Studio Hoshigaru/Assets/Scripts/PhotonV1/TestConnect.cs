using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestConnect : MonoBehaviourPunCallbacks
{
    public Text text;
    public Canvas ChooseNameCanvas;
    public Canvas CreateRoomCanvas;

    void Start()
    {
        Debug.Log("Connecting to server ...");

        PhotonNetwork.ConnectUsingSettings();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
            OnClick_ValidButton();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to server");
        PhotonNetwork.JoinLobby();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("Disconnect from server for reason " + cause.ToString());
    }

    public void OnClick_ValidButton()
    {
        PhotonNetwork.NickName = text.text;
        CreateRoomCanvas.gameObject.SetActive(true);
        ChooseNameCanvas.gameObject.SetActive(false);

    }
}
