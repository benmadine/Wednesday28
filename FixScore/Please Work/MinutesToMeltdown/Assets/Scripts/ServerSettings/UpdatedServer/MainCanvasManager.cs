using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCanvasManager : MonoBehaviour {

    #region Private Variables
    /// <summary>
    /// Simply access the room and lobby canvas objects
    /// </summary>
    [SerializeField]
    private LobbyCanvas _lobbyCanvas;

    [SerializeField]
    private CurrentRoomCanvas _currentRoomCanvas;
    #endregion

    #region Public Variables
    //Sets a non changeable instance of the canvas manager
    public static MainCanvasManager Instance;

    /// <summary>
    /// Public getters for the lobby canvas and room canvas
    /// </summary>
    public LobbyCanvas LobbyCanvas
    {
        get { return _lobbyCanvas; }
    }
    public CurrentRoomCanvas CurrentRoomCanvas
    {
        get { return _currentRoomCanvas; }
    }
    #endregion

    #region Private Methods
    /// <summary>
    /// Sets the instance to this, the scirpt attached to the object
    /// </summary>
    private void Awake()
    {
        Instance = this;
    }
    #endregion
}
