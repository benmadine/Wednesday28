using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerNetwork : MonoBehaviour
{
    #region Private Variables
    private PhotonView PhotonView; //Access the photon view compoents

    private int PlayersInGame = 0; //Sets the current players in game, allows us to keep track

    private Scene currentScene; //Then the current scnene and scene name used for the ysncing stuff
    private string sceneName;
    #endregion

    #region Public Variables
    /// <summary>
    /// a non changing instance
    /// Getters for player name
    /// </summary>
    public static PlayerNetwork Instance;
    public string PlayerName { get; private set; }
    #endregion

    #region Private Methods
    /// <summary>
    /// Sets instance to this
    /// Gets the photon view
    /// Sets a player name with a random number after (Wil change to input field later)
    /// </summary>
    private void Awake()
    {
        Instance = this;
        PhotonView = GetComponent<PhotonView>();
        PlayerName = "Player#" + Random.Range(1000, 9999);
    }

    /// <summary>
    /// Simply sets the current scene to this one 
    /// Sets the scene name string to this current one
    /// </summary>
    private void Start()
    {
        currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;
    }

    /// <summary>
    /// When the masters loads the game, 
    /// using RPC calls as they will seen to all players on network
    /// </summary>
    private void MasterLoadGame()
    {
        PhotonView.RPC("RPC_LoadedGameScene", PhotonTargets.MasterClient);
        PhotonView.RPC("RPC_LoadedGameOthers", PhotonTargets.Others);
    }

    /// <summary>
    /// Just calls the loaded game method and checks if all players are in the game
    /// </summary>
    private void NonMasterLoadedGame()
    {
        PhotonView.RPC("RPC_LoadedGameScene", PhotonTargets.MasterClient);
    }

    /// <summary>
    /// Using PunRPC so can be viewed scross network, loads the level, 1 (Start Scene) if the master is there and sync is true
    /// </summary>
    [PunRPC]
    private void RPC_LoadGameOthers()
    {
        PhotonNetwork.LoadLevel(1);
    }

    [PunRPC]
    private void RPC_LoadedGameScene()
    {
        PlayersInGame++;
        if (PlayersInGame == PhotonNetwork.playerList.Length)
        {
            print("All players in");
        }
    }

    /// <summary>
    /// When Connected to master and checks if the scnene name is equal to master, if not then sycn false, 
    /// </summary>
    private void OnConnectedToMaster()
    {
        if (sceneName != "Launcher")
        {
            PhotonNetwork.automaticallySyncScene = false;
        }
    }
    #endregion
}
