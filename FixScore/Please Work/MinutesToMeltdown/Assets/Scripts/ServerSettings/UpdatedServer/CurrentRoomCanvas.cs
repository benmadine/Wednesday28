using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentRoomCanvas : Photon.MonoBehaviour
{

    public GameObject startGameButton;
    /// <summary>
    /// Simple method, when the user clicks the start button then load the scene "Start Level"
    /// </summary>
    private void Update()
    {
        if (PhotonNetwork.isMasterClient)
        {
            startGameButton.SetActive(true);
        }
    }

    public void OnClickStartSync()
    {     
        PhotonNetwork.LoadLevel("StartScene");
    }
}
