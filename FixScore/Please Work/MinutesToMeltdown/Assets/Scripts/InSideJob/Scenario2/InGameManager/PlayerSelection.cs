using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSelection : Photon.MonoBehaviour, IPunObservable
{
    #region Public Varaiables
    public GameObject canvasObj;

    public GameObject player1Button, player2Button, player3Button, player4Button;

    [Tooltip("Player1 = Thief, Player2 = Thief Hax, Player3 = Guard, Player4 = Guard Drone")]
    public bool player1Activated, player2Activated, player3Activated, player4Activated;

    public GameObject thiefCollectorPrefab, thiefHackerPrefab;
    public GameObject guardPrefab, guardDronePrefab;

    //Map stuff here
    [HideInInspector]
    public bool map1, map2, map3;
    #endregion

    #region Private Variables
    [HideInInspector]   
    public Vector2 thiefCollectorSpawn, thiefHackerSpawn;
    [HideInInspector]
    public Vector2 guardChaserSpawn, guardDroneSpawn;

    //More Map Stuff
    InSideJobManager Map;
    #endregion

    #region Public Methods
    public void ThiefCollectionButton()
    {
        SelectSpawn();
        player1Activated = true;
        RPC_CreatePlayer(thiefCollectorPrefab.name, thiefCollectorSpawn);
        canvasObj.SetActive(false);
    }
    public void ThiefHackerButton()
    {
        SelectSpawn();
        player2Activated = true;
        RPC_CreatePlayer(thiefHackerPrefab.name, thiefHackerSpawn);
        canvasObj.SetActive(false);
    }
    public void GuardChaserButton()
    {
        SelectSpawn();
        player3Activated = true;
        RPC_CreatePlayer(guardPrefab.name, guardChaserSpawn);
        canvasObj.SetActive(false);
    }
    public void GuardDroneButton()
    {
        SelectSpawn();
        player4Activated = true;
        RPC_CreatePlayer(guardDronePrefab.name, guardDroneSpawn);
        canvasObj.SetActive(false);
    }
    #endregion

    #region Private Methods
    private void Awake()
    {

    }

    private void Start()
    {
        Map = gameObject.GetComponent<InSideJobManager>();
    }

    private void Update()
    {

    }

    private void SelectSpawn()
    {
        if (GameObject.FindGameObjectWithTag("Map1"))
        {
            map1 = true;
            thiefCollectorSpawn = new Vector2(-0.75f, -9.75f);
            thiefHackerSpawn = new Vector2(0.6f, -10.94f);

            guardDroneSpawn = new Vector2(0.67f, 9.43f);
            guardChaserSpawn = new Vector2(-0.8f, 8.83f);
        }
        if (GameObject.FindGameObjectWithTag("Map2"))
        {
            map2 = true;
            thiefCollectorSpawn = new Vector2(-10.12f, -4.87f);
            thiefHackerSpawn = new Vector2(0.6f, -2.95f);

            guardDroneSpawn = new Vector2(9.66f, 2.25f);
            guardChaserSpawn = new Vector2(9.66f, 4f);
        }
        if (GameObject.FindGameObjectWithTag("Map3"))
        {
            map3 = true;
            thiefCollectorSpawn = new Vector2(0.9f, -9.43f);
            thiefHackerSpawn = new Vector2(-1.07f, -9.43f);

            guardDroneSpawn = new Vector2(-1.07f, 10.42f);
            guardChaserSpawn = new Vector2(0.9f, 10.42f);
        }
    }

    private void RPC_CreatePlayer(string prefabName, Vector2 spawnVector)
    {
        PhotonNetwork.Instantiate(prefabName, spawnVector, Quaternion.identity, 0);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {

    }
    #endregion
}
