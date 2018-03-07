//Start of IP3
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace CW.Bedlamk.Minutes2Metldown
{
    public class Launcher : Photon.PunBehaviour
    {
        #region Public Varaibles
        ///<summary>
        ///The PUN loglevel
        /// </summary>
        public PhotonLogLevel loglevel = PhotonLogLevel.Informational;

        ///<summary>
        ///Tha max amount of players per room
        /// </summary>
        [Tooltip("Max players per room")]
        public byte MaxPlayersPerRoom = 4;

        [Tooltip("The UI panel for the user name and play button")]
        public GameObject controlPanel;

        [Tooltip("The UI text that lets the user know if connecting or not")]
        public GameObject progressLabel;

        //The name of the private room the user creates
        string privateRoomName;
        #endregion

        #region Private Variables
        ///<summary>
        ///This games version number, users are seperated if playing on different version
        /// </summary>
        string _gameVersion = "1";

        /// <summary>
        /// Keep track of the current process. SIince the connection is asynchronous and is based on several callbacks from photon,
        /// we need to keep track of this properly to adjust the behaviour when we recieve call backs
        /// </summary>
        bool isConnecting;
        #endregion

        #region Photon.PUNBehaviour Callbacks
        public override void OnConnectedToMaster()
        {
            if(isConnecting) //We dont want to do anything if we are not attempting to get a join a roomn
            {
                Debug.Log("Minutes2Meltdown/Launcher: OnConnectedToMaster was called by PUN");
                PhotonNetwork.JoinOrCreateRoom(privateRoomName, new RoomOptions() { MaxPlayers = MaxPlayersPerRoom }, null); //First try to join a already existing room
            }    
        }

        public override void OnDisconnectedFromPhoton()
        {
            progressLabel.SetActive(false);
            controlPanel.SetActive(true);
            Debug.LogWarning("Minutes2Meltdown/Launcher: OnDisconntedFromPhoton was called by PUN");
        }

        public override void OnPhotonRandomJoinFailed(object[] codeAndMsg)
        {
            print("im here");
            Debug.Log("Minutes2Meltdown/Launcher: No random room avaiable - need to create one");
            PhotonNetwork.JoinOrCreateRoom(privateRoomName, new RoomOptions() { MaxPlayers = MaxPlayersPerRoom }, null);
        }

        public override void OnJoinedRoom()
        {
            if(PhotonNetwork.room.PlayerCount == 1)
            {
                Debug.Log("We load the 'WaitingRoom 1.");
                PhotonNetwork.LoadLevel("WaitingRoom 1");
                Debug.Log("Minutes2Meltdown/Launcher: This client is now in a room");
            }          
        }

        
        #endregion

        #region MonoBehabiour CallBacks
        ///<summary>
        ///MonoBehaviour methods called on gameobject by unity during early initialization
        ///</summary>
        private void Awake()
        {
            
            PhotonNetwork.logLevel = loglevel; //Forcing the loglevel
            //Makes sure we dont join a lobby 
            PhotonNetwork.autoJoinLobby = false;

            //This makes sure we can use PhotonNetwork.loadLevel() and so all clients sync their levels automatically
            PhotonNetwork.automaticallySyncScene = true;
        }

        private void Start()
        {
            progressLabel.SetActive(false);
            controlPanel.SetActive(true);
        }

        #endregion

        #region Public Methods
        ///<summary>
        ///Starts the connection
        ///If already connected, we attempt at joining a random room
        ///If not yet connected, connted this to photon cloud netowrk
        /// </summary>

        public void Connect()
        {
            isConnecting = true; // Keeps track of if we want to join a room or not
            progressLabel.SetActive(true);
            controlPanel.SetActive(false);
            //Check if we are connected
            if (PhotonNetwork.connected)
                PhotonNetwork.JoinRandomRoom(); //Attempt to join a random room
            else
                PhotonNetwork.ConnectUsingSettings(_gameVersion); //Connecting to photon online server
        }
        #endregion

        #region Private Methods
        public void AllocatePrivateServer(string serverName)
        {
            InputField _inputField = GameObject.Find("Server Name").GetComponent<InputField>();
            if(_inputField != null)
            {
                _inputField.text = serverName;
                privateRoomName = serverName;
            }
        }
        #endregion

    }
}

