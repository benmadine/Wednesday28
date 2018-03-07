using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControlerScenario2 : Photon.MonoBehaviour
{
    #region Public Variables
     
    #endregion

    #region Private Variables
    Vector2 currentVelocity;
    [SerializeField] [Tooltip("How much the player's speed will be amplified by, default is 2")]
    float speedAmp = 2;

    PlayerSelection playerSelectionScript;
    PlayerJoyStick playerJoyStickScript;

    Rigidbody2D rigidBodySpy, rigidBodyGuard;

    private PhotonView photonView;
    #endregion

    #region Public Methods

    #endregion

    #region Private Methods

    private void Awake()
    {
        photonView = GetComponent<PhotonView>();
    }

    private void Start()
    {
        playerSelectionScript = GameObject.Find("RolePickerCanvas").GetComponent<PlayerSelection>();
        playerJoyStickScript = GameObject.Find("ThiefJoyStickBackground").GetComponentInChildren<PlayerJoyStick>();
    }

    private void FixedUpdate()
    {
        if(photonView.isMine)
        {
            ThiefCollector();
            ThiefHacker();
        }
    }

    private void ThiefCollector()
    {
        if(playerSelectionScript.player1Activated) 
        {
            //This player is the spy, running away from the guard and hiding from cameras
            rigidBodySpy = GameObject.Find("Thief(Collector)(Clone)").GetComponent<Rigidbody2D>();
            MovementControls(rigidBodySpy);
        }
    }

    private void ThiefHacker()
    {
        if(playerSelectionScript.player2Activated)
        {
            //This player is the guard who is chasing the spy
            rigidBodyGuard = GameObject.Find("Thief(Hacker)(Clone)").GetComponent<Rigidbody2D>();
            MovementControls(rigidBodyGuard);
        }
    }

    private void MovementControls(Rigidbody2D playerBody)
    {
        ///<summary>
        ///Just use the "player" for adding movement, other methods will sort if Spy or Guard :)
        ///The 'Horizontal' and 'Vertical' is simply for testing on keyboard
        /// </summary>
        float inputHor = Input.GetAxisRaw("Horizontal");
        float inputVer = Input.GetAxisRaw("Vertical");


        Vector2 movement = new Vector2(inputHor, inputVer);
        float angle = Mathf.Atan2(inputVer, inputHor) * Mathf.Rad2Deg;

        //#Critical
        //Used when using the virtual joystick
        if (playerJoyStickScript.inputDirection != Vector2.zero)
        {
            movement = playerJoyStickScript.inputDirection;
        }

        playerBody.velocity = movement * speedAmp;
        currentVelocity = movement * speedAmp;
    } 
    #endregion
}
