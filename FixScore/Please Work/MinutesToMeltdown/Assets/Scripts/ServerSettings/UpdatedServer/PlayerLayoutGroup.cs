using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLayoutGroup : MonoBehaviour
{
    #region Private Varaibles
    /// <summary>
    /// Player lsiting prefab is simply the prefab which shows the player that has entered the room
    /// Player lsiting prefab, sets up a getter
    /// </summary>
    [SerializeField]
    private GameObject _playerListingPrefab;
    private GameObject PlayerListingPrefab
    {
        get { return _playerListingPrefab; }
    }

    /// <summary>
    /// Sets up list for the players in the room,
    /// then a getter to accesses that 
    /// </summary>
    private List<PlayerListing> _playerListings = new List<PlayerListing>();
    private List<PlayerListing> PlayerListings
    {
        get { return _playerListings; }
    }
    #endregion

    #region Private Methods
    /// <summary>
    /// When the player joins the room
    /// </summary>
    private void OnJoinedRoom()
    {
        foreach (Transform child in transform) //This deletes player name so they dont dupe
        {
            Destroy(child.gameObject);
        }

        MainCanvasManager.Instance.CurrentRoomCanvas.transform.SetAsLastSibling(); // Moves the Current Room below the lobby in the editor so can be seen

        PhotonPlayer[] photonPlayers = PhotonNetwork.playerList; //Gets the list of players
        for (int i = 0; i < photonPlayers.Length; i++) //Then for the players in the room add that to the joined room list
        {
            PlayerJoinedRoom(photonPlayers[i]);
        }
    }

    /// <summary>
    /// When a player Connects
    /// </summary>
    /// <param name="photonPlayer"></param>
    private void OnPhotonPlayerConnected(PhotonPlayer photonPlayer)
    {
        PlayerJoinedRoom(photonPlayer);
    }

    /// <summary>
    /// When a player disconnects
    /// </summary>
    /// <param name="photonPlayer"></param>
    private void OnPhotonPlayerDisconnected(PhotonPlayer photonPlayer)
    {
        PlayerLeftRoom(photonPlayer);
    }

    /// <summary>
    /// When a player joins the room
    /// </summary>
    /// <param name="photonPlayer"></param>
    private void PlayerJoinedRoom(PhotonPlayer photonPlayer)
    {
        if (photonPlayer == null) //If there is no player, should never happen but just incase
        {
            return;
        }

        PlayerLeftRoom(photonPlayer);

        GameObject playerListingObject = Instantiate(PlayerListingPrefab); //Instantaiting the prefab, player list that shows the player name
        playerListingObject.transform.SetParent(transform, false); //Sets the parent of the prefab to 'PlayerLayoutGroup'

        PlayerListing playerListing = playerListingObject.GetComponent<PlayerListing>(); //Gets the script component
        playerListing.ApplyPhotonPlayer(photonPlayer); 

        PlayerListings.Add(playerListing); //Add the script to each prefab in player listings
    }

    /// <summary>
    /// When the player leaves the rooom
    /// </summary>
    /// <param name="photonPlayer"></param>
    private void PlayerLeftRoom(PhotonPlayer photonPlayer)
    {
        int index = PlayerListings.FindIndex(x => x.PhotonPlayer == photonPlayer); //The player can be -1 cause then there would be no player
        if (index != -1)
        {
            Destroy(PlayerListings[index].gameObject);
            PlayerListings.RemoveAt(index); //Just basically removes the player from the list
        }
    }
    #endregion

    #region Public Methods
    /// <summary>
    /// Just when the player clicks to leave the room this method is called - takes them back to the lobby area
    /// </summary>
    public void OnClickLeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }
    #endregion


}
