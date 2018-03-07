using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyNetwork : MonoBehaviour
{
    #region Private Methods
    /// <summary>
    /// Means that all clients will bve on the same scene as the master,
    /// set to false so they can go around on different scenes
    /// </summary>
    private void Awake()
    {
        PhotonNetwork.automaticallySyncScene = true;
    }

    /// <summary>
    /// Simply connects the game vesion usuing photon cloud if game isnt connected
    /// </summary>
    private void Start()
    {
        if (!PhotonNetwork.connected)
        {
            PhotonNetwork.ConnectUsingSettings("0.0.0");
        }
    }

    /// <summary>
    /// When player has connected to master host
    /// Displays print message
    /// Sets the player name to the client and joins the lobby
    /// </summary>
    private void OnConnectedToMaster()
    {
        print("Connected to master.");

        PhotonNetwork.playerName = PlayerNetwork.Instance.PlayerName;
        PhotonNetwork.JoinLobby(TypedLobby.Default);
    }

    /// <summary>
    /// Now this method is quite cool, when the player joins the lobby back form when he was in room thee lobby canvas will be put in front of the room canvas so he can edit the lobby stuff
    /// </summary>
    private void OnJoinedLobby()
    {
        if (!PhotonNetwork.inRoom)
            MainCanvasManager.Instance.LobbyCanvas.transform.SetAsLastSibling();
    }
    #endregion
}
