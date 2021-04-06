using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class PlayersListing : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private Transform content;
    [SerializeField]
    public Players PlayerPrefab;
    [SerializeField]
    private Text _readyUpText;

    private bool _ready = false;

    private List<Players> listing = new List<Players>();

    private void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public void Awake()
    {
        GetCurrentRoomPlayers();
    }

    public override void OnEnable()
    {
        base.OnEnable();
        SetReadyUp(false);
    }

    private void GetCurrentRoomPlayers()
    {
        foreach(KeyValuePair<int, Player> playerInfo in PhotonNetwork.CurrentRoom.Players)
        {
            AddPlayerListing(playerInfo.Value);
        }
    }

    private void AddPlayerListing(Player player)
    {
        Players actualPlayer = Instantiate(PlayerPrefab, content);
        if (actualPlayer != null)
        {
            actualPlayer.SetPlayerInfo(player);
            listing.Add(actualPlayer);
        }
    }

    private void RemovePlayerListing(Player player)
    {
        int index = listing.FindIndex(x => x._player == player);
        if (index != -1)
        {
            Destroy(listing[index].gameObject);
            listing.RemoveAt(index);
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        AddPlayerListing(newPlayer);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log("il est parti");
        RemovePlayerListing(otherPlayer);
    } 

    private void SetReadyUp(bool state)
    {
        _ready = state;
        if (_ready)
            _readyUpText.text = "Ready";
        else
            _readyUpText.text = "Not ready";
    }


    public void OnClick_StartGame()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            for (int i = 0; i < listing.Count; i++)
            {
                if(listing[i]._player != PhotonNetwork.LocalPlayer)
                {
                    if (!listing[i].Ready)
                        return;
                }
            }

            PhotonNetwork.CurrentRoom.IsOpen = false;
            PhotonNetwork.CurrentRoom.IsVisible = false;
            PhotonNetwork.LoadLevel(1);
        }
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        PhotonNetwork.LeaveRoom();
    }

    public void OnClick_ReadyUp()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            SetReadyUp(!_ready);
            base.photonView.RPC("RPC_ChangeReadyState", RpcTarget.MasterClient, PhotonNetwork.LocalPlayer, _ready);
        }
    }

    [PunRPC]
    private void RPC_ChangeReadyState(Player player, bool ready)
    {
        int index = listing.FindIndex(x => x._player == player);
        if (index != -1)
            listing[index].Ready = ready;
        
    }

}
