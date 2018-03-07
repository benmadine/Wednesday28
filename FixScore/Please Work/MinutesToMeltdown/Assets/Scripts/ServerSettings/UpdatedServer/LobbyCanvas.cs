using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyCanvas : MonoBehaviour
{
    #region Private Variables
    /// <summary>
    /// Gets the room layout group in the canvas section
    /// </summary>
    [SerializeField]
    private RoomLayoutGroup _roomLayoutGroup;
    private RoomLayoutGroup RoomLayoutGroup
    {
        get { return _roomLayoutGroup; }
    }
    #endregion

    #region Public Methods
    /// <summary>
    /// When the user clicks the join room button, then call this
    /// Takes in roomName argument which was used in the create room script
    /// Uses photon call, joinRoom to join room
    /// </summary>
    /// <param name="roomName"></param>
    public void OnClickJoinRoom(string roomName)
    {
        if (PhotonNetwork.JoinRoom(roomName))
        {

        }
        else
        {
            print("Join room failed.");
        }
    }

    /// <summary>
    /// Simply quits the game when the button is pressed
    /// </summary>
    public void LeaveButton()
    {
        Application.Quit();
    }

    #endregion
}
