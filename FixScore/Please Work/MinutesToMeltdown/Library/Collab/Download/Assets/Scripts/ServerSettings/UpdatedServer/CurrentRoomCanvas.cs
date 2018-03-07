using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentRoomCanvas : MonoBehaviour
{
    public void OnClickStartSync()
    {
        PhotonNetwork.LoadLevel(1);
    }
}
