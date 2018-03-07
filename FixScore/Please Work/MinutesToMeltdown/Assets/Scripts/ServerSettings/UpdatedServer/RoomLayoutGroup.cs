using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomLayoutGroup : MonoBehaviour
{
    #region Private Variables
    [SerializeField]
    private GameObject _roomListingPrefab;
    private GameObject RoomListingPrefab
    {
        get { return _roomListingPrefab; }
    }

    private List<RoomListing> _roomListingButtons = new List<RoomListing>();
    private List<RoomListing> RoomListingButtons
    {
        get { return _roomListingButtons; }
    }

    #endregion

    #region Private Methods
    /// <summary>
    /// Sets an array of the current rooms in the photon network by accessing the roon list in PhotonNetwork
    /// For ever room uses recieved method (will discuss donw)
    /// </summary>
    private void OnReceivedRoomListUpdate()
    {
        RoomInfo[] rooms = PhotonNetwork.GetRoomList();

        foreach (RoomInfo room in rooms)
        {
            RoomReceived(room);
        }

        RemoveOldRooms();
    }

    private void RoomReceived(RoomInfo room)
    {
        int index = RoomListingButtons.FindIndex(x => x.RoomName == room.Name);

        if (index == -1)
        {
            if (room.IsVisible && room.PlayerCount < room.MaxPlayers)
            {
                GameObject roomListingObj = Instantiate(RoomListingPrefab); //Spawns the room prefab, room prefab is the prefab with room name on it
                roomListingObj.transform.SetParent(transform, false); //Sets the parent of the room prefab

                RoomListing roomListing = roomListingObj.GetComponent<RoomListing>(); 
                RoomListingButtons.Add(roomListing); // Attaches the room listing script to it

                index = (RoomListingButtons.Count - 1); //Sets index to room count -1 cause array start 0
            }
        }

        if (index != -1) //If the room is spawned
        {
            RoomListing roomListing = RoomListingButtons[index];
            roomListing.SetRoomNameText(room.Name); //Sets the name of the room
            roomListing.Updated = true; //Updates the room
        }
    }

    /// <summary>
    /// Removes the rooms that has 0 players
    /// </summary>
    private void RemoveOldRooms()
    {
        List<RoomListing> removeRooms = new List<RoomListing>(); 

        foreach (RoomListing roomListing in RoomListingButtons)
        {
            if (!roomListing.Updated)
                removeRooms.Add(roomListing);
            else
                roomListing.Updated = false;
        }

        foreach (RoomListing roomListing in removeRooms)
        {
            GameObject roomListingObj = roomListing.gameObject;
            RoomListingButtons.Remove(roomListing);
            Destroy(roomListingObj);
        }
    }
    #endregion
}
