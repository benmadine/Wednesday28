using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateRoom : MonoBehaviour {

    #region Public Variables

    #endregion

    #region Private Variables
    /// <summary>
    /// The room name used here, getter is used to return reoom name and use it 
    /// </summary>
    [SerializeField]
    private Text _roomName;
    private Text RoomName
    {
        get { return _roomName; }
    }
    #endregion

    #region Private Methods
    /// <summary>
    /// If a room created is fauled then display this message, this method will auto call using photon if failed
    /// </summary>
    /// <param name="codeAndMessage"></param>
    private void OnPhotonCreateRoomFailed(object[] codeAndMessage)
    {
        print("create room failed: " + codeAndMessage[1]);
    }

    #endregion

    #region Public Methods
    /// <summary>
    /// When the button is clicked to create a room.
    /// Sets the room options - max players, if the room is avaible to join and if the room is visible to other players on the network.
    /// If created room, use the parameters above and disaply message
    /// </summary>
    public void OnClick_CreateRoom()
    {
        RoomOptions roomOptions = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = 4 };

        if (PhotonNetwork.CreateRoom(RoomName.text, roomOptions, TypedLobby.Default))
        {
            print("create room successfully sent.");
        }
        else
        {
            print("create room failed to send");
        }
    }
    #endregion
}
