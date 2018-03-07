using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CW.Bedlamk.Minutes2Metldown
{
    public class GameManager : Photon.PunBehaviour
    {
        #region Photon Messages
        ///<summary>
        ///Called when the local player left the room
        /// </summary>
        public override void OnLeftRoom()
        {
            SceneManager.LoadScene("Launcher");
        }

        public override void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
        {
            Debug.Log("OnPhotonPlayerConnected() " + newPlayer.NickName);

            if(PhotonNetwork.isMasterClient)
            {
                Debug.Log("OnPhotonPlayerConnected isMasterClient " + PhotonNetwork.isMasterClient);

                LoadArena();
            }
        }

        private void Update()
        {
            TwoPlayersTransferScene();
        }

        public override void OnPhotonPlayerDisconnected(PhotonPlayer otherPlayer)
        {
            Debug.Log("OnPhotonPlayerDisconnected() " + otherPlayer.NickName);

            if(PhotonNetwork.isMasterClient)
            {
                Debug.Log("OnPhontonPlayerDisconnected isMasterClient " + PhotonNetwork.isMasterClient);

                LoadArena();
            }
        }
        #endregion

        #region Private Methods
        void LoadArena()
        {
            if(!PhotonNetwork.isMasterClient)
            {
                Debug.LogError("PhotonNetwork: Trying to load a level but we are not the master client");
            }
            Debug.Log("PhotonNetwork: loading level : " + PhotonNetwork.room.PlayerCount);
            PhotonNetwork.LoadLevel("WaitingRoom " + PhotonNetwork.room.PlayerCount);
        }

        void TwoPlayersTransferScene()
        {
            if (PhotonNetwork.room.playerCount == 2) //Change that to test when only have 2 devices
            {
                StartCoroutine(TransferToMainGame());
            }
        }
        #endregion

        #region Public Methods
        public void LeaveRoom()
        {
            PhotonNetwork.LeaveRoom();
        }
        #endregion

        #region Ienumeratores
        IEnumerator TransferToMainGame()
        {
            WaitForSeconds wait = new WaitForSeconds(2);
            yield return wait;
            PhotonNetwork.LoadLevel("StartScene"); //Using PhotonNetwork.LoadLevel() so that the level syncs between all clients
        }
        #endregion
    }
}

