using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControlerScenario2 : Photon.MonoBehaviour
{

    public static GameObject localPlayerInstance;

    Vector2 currentVelocity;

    public float speedAmp = 5;

    public Rigidbody2D playerBody;

    private Transform thisTrans;

    public GameObject playerUiPrefab;

    public PlayerJoyStick playerJoyStick;

    GameObject thiefCollector;

    private void Awake()
    {
        if (photonView.isMine)
        {
            GameControlerScenario2.localPlayerInstance = this.gameObject;
        }
    }

    // Use this for initialization
    void Start()
    {
        thisTrans = this.transform;
        playerBody = this.gameObject.GetComponent<Rigidbody2D>();

        CameraWork camWork = this.gameObject.GetComponent<CameraWork>();


        if (camWork != null)
        {
            if (photonView.isMine)
            {
                camWork.OnStartFollow();
            }
        }

        if (playerUiPrefab != null)
        {
            if (photonView.isMine)
            {
                GameObject uiGo = Instantiate(playerUiPrefab) as GameObject;
                playerJoyStick = GameObject.FindGameObjectWithTag("PlayerJoyStick").GetComponentInChildren<PlayerJoyStick>();
                uiGo.GetComponent<Transform>().SetParent(GameObject.Find("Canvas").GetComponent<Transform>(), false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.isMine == false && PhotonNetwork.connected == true)
        {
            return;
        }
    }

    private void FixedUpdate()
    {
        if (photonView.isMine)
        {
            MovementControls();
        }

    }

    void MovementControls()
    {
        float inputHor = Input.GetAxis("Horizontal");
        float inputVer = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(inputHor, inputVer);

       // float angle = Mathf.Atan2(localPlayerInstance.transform.position.y, localPlayerInstance.transform.position.x) * Mathf.Rad2Deg;
       
        if (playerJoyStick.inputDirection != Vector2.zero)
        {
            movement = playerJoyStick.inputDirection;
         //   localPlayerInstance.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }



        playerBody.velocity = movement * speedAmp;
        currentVelocity = movement * speedAmp;
    }
}
