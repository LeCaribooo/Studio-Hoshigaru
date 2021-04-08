using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class RoomListing : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private Transform content;
    [SerializeField]
    private Rooms rooms;

    public Canvas CurrentRoomCanvas;
    public Canvas CreateRoomCanvas;

    public List<Rooms> listing = new List<Rooms>();

    public override void OnJoinedRoom()
    {
        CurrentRoomCanvas.gameObject.SetActive(true);
        CreateRoomCanvas.gameObject.SetActive(false);
    }


    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (RoomInfo info in roomList)
        {
            if (info.RemovedFromList)
            {
                int index = listing.FindIndex(x => x.RoomInfo.Name == info.Name);
                if (index != -1)
                {
                    Destroy(listing[index].gameObject);
                    listing.RemoveAt(index);
                }
            }
            else
            {
                Rooms actualRoom = Instantiate(rooms, content);
                if (actualRoom != null)
                {
                    actualRoom.SetRoomInfo(info);
                    listing.Add(actualRoom);
                }
            }
        }
    }
}
