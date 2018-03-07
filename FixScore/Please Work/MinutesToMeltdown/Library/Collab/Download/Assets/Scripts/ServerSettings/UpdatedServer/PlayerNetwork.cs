using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerNetwork : MonoBehaviour
{
    public static PlayerNetwork Instance;
    public string PlayerName { get; private set; }

    private PhotonView PhotonView;

    private int PlayersInGame = 0;

    private void Awake()
    {
        Instance = this;
        PhotonView = GetComponent<PhotonView>();
        PlayerName = "Player#" + Random.Range(1000, 9999);
    }

    
}
