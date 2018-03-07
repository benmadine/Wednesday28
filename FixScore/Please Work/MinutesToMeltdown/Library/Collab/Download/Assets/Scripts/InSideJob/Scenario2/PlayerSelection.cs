using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSelection : Photon.MonoBehaviour, IPunObservable
{
    #region Public Varaiables
    public GameObject player1Controls, player2Controls, player3Controls, player4Controls;

    [Tooltip("The panel where the players choose their role.")]
    public GameObject playerControlPannel;

    [Tooltip("Player1 = Thief, Player2 = Thief Hax, Player3 = Guard, Player4 = Guard Drone")]
    public bool player1Activated, player2Activated, player3Activated, player4Activated;
    #endregion

    #region Private Variables
    GameObject thiefCollectorPrefab, thiefHackerPrefab;
    GameObject guardPrefab, guardDronePrefab;

    GameObject joyStickControls, joystickParent;

    string thiefCollectorPrefabName, thiefHackerPrefabName;
    string guardPreFabName, guardDronePrefabName;

    private PhotonView photonView;

    private Vector2 thiefCollectorSpawn, thiefHackerSpawn;
    private Vector2 guardChaserSpawn, guardDroneSpawn;
    #endregion

    #region Public Methods
    //Thief Collector
    public void Player1Button() 
    {
        player1Activated = true;
        JoyStickLoading();
        thiefCollectorPrefabName = "Thief(Collector)(Clone)";
        RPC_CreatePlayer(thiefCollectorPrefab, thiefCollectorPrefabName, thiefCollectorSpawn);
        playerControlPannel.SetActive(false);
    }

    //Thief Hacker
    public void Player2Button()
    {
        player2Activated = true;
        JoyStickLoading();
        thiefHackerPrefabName = "Thief(Hacker)(Clone)";
        RPC_CreatePlayer(thiefHackerPrefab, thiefHackerPrefabName, thiefHackerSpawn);
        playerControlPannel.SetActive(false);
    }

    //Guard Chaser
    public void Player3Button()
    {
        player3Activated = true;
        JoyStickLoading();
        guardPreFabName = "Guard(Chaser)(Clone)";
        RPC_CreatePlayer(guardPrefab, guardPreFabName, guardChaserSpawn);
        playerControlPannel.SetActive(false);
    }

    public void Player4Button()
    {
        player4Activated = true;
        player4Controls.SetActive(false);
        JoyStickLoading();
        guardDronePrefabName = "Guard(Drone)(Clone)";
        RPC_CreatePlayer(guardDronePrefab, guardDronePrefabName, guardDroneSpawn);
        playerControlPannel.SetActive(false);
    }
    #endregion

    #region Private Methods
    private void Awake()
    {
        photonView = GetComponent<PhotonView>();
    }

    private void Start()
    {
        #region Player Names

        #endregion

        #region Player Spawns
        thiefCollectorSpawn = new Vector2(-30, -15);
        thiefHackerSpawn = new Vector2(-25, -20);

        guardDroneSpawn = new Vector2(0, 0);
        guardChaserSpawn = new Vector2(5, 5);
        #endregion

    }

    private void Update()
    {
        //if (player1Activated && player2Activated /*&& player3Activated && player4Activated*/)
        //{
        //    playerControlPannel.SetActive(false);

        //    photonView.RPC(" RPC_CreatePlayer", PhotonTargets.All, new object[] { spyPrefab, spyPrefabName });

        //    photonView.RPC(" RPC_CreatePlayer", PhotonTargets.All, new object[] { guardPrefab, guardPreFabName });
        //    //Other chars not confirmed
        //}
    }

    private void JoyStickLoading()
    {
        joystickParent = GameObject.Find("ThiefMovementControlsCanvas");
        joyStickControls = Instantiate(Resources.Load("ThiefJoyStickBackground") as GameObject);
        joyStickControls.name = "ThiefJoyStickBackground";
        joyStickControls.transform.SetParent(joystickParent.transform, false);
    }

    [PunRPC]
    private void RPC_CreatePlayer(GameObject gameObject, string prefabName, Vector2 spawnVector)
    {
        gameObject = PhotonNetwork.Instantiate(prefabName, spawnVector, Quaternion.identity, 0);
        gameObject.name = prefabName;
    }
    #endregion

    #region IPunObserver
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.isWriting)
        {
            stream.SendNext(this.player1Activated);
            stream.SendNext(this.player2Activated);
            stream.SendNext(this.player3Activated);
            stream.SendNext(this.player4Activated);
        }
        else
        {
            this.player1Activated = (bool)stream.ReceiveNext();
            this.player2Activated = (bool)stream.ReceiveNext();
            this.player3Activated = (bool)stream.ReceiveNext();
            this.player4Activated = (bool)stream.ReceiveNext();
        }
    }
    #endregion

}
