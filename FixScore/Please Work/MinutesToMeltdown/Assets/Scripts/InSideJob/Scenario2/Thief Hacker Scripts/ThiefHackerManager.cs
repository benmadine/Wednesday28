using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThiefHackerManager : Photon.MonoBehaviour, IPunObservable
{
    #region Stun Drone Variables
    private float circleRadiusDrone = 4f;
    public LayerMask droneMask;

    private float timeLeft = 20;

    GuardDroneManager guardDroneManager;

    public GameObject stunDroneButton;
    #endregion

    #region OpenDoor Variables
    private float circleRadiusDoor = 1f;
    public LayerMask doorMask;

    public GameObject openSecDoorButton;
    #endregion

    public bool stunActivated;
    public GameControlerScenario2 playerMovementManager;

    public float playerRadius = 2;
    public float view = 360;

    public LayerMask targetmask;
    public LayerMask obstacleMask;

    private List<Transform> fogInArea = new List<Transform>();

    private void Start()
    {
        if(photonView.isMine)
        {
            StartCoroutine(WaitToFind());
            StartCoroutine("WaitTarget");
        }
    }

    private void Update()
    {
        if (guardDroneManager == null)
        {
            guardDroneManager = GameObject.FindGameObjectWithTag("GuardDrone").GetComponent<GuardDroneManager>();
        }
        
        if(stunActivated)
        {
            StartCoroutine(Stunned());
        }
        FogStatus();
        timeLeft -= Time.deltaTime;
    }

    public Vector3 PlayersAngle(float angle, bool globalAngle)
    {
        if (!globalAngle)
        {
            angle -= transform.eulerAngles.z;
        }
        return new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad), Mathf.Cos(angle * Mathf.Deg2Rad), 0);
    }

    private void FindFog()
    {
        fogInArea.Clear();
        //Adds targets in view radius to an array
        Collider2D[] fogInRadius = Physics2D.OverlapCircleAll(transform.position, playerRadius, targetmask);
        //For every targetsInViewRadius it checks if they are inside the field of view
        for (int i = 0; i < fogInRadius.Length; i++)
        {
            Transform target = fogInRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.up, dirToTarget) < view / 2)
            {
                float dstToTarget = Vector3.Distance(transform.position, target.position);
                //If line draw from object to target is not interrupted by wall, add target to list of visible 
                //targets
                if (!Physics2D.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask))
                {
                    fogInArea.Add(target);
                }
            }
        }
    }

    private void FogStatus()
    {
        foreach (Transform fogTrans in fogInArea)
        {
            fogTrans.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            if (Vector2.Distance(this.transform.position, fogTrans.position) > 4) //Change that to increase FOV radius
            {
                fogTrans.gameObject.GetComponent<SpriteRenderer>().enabled = true;
            }
        }
    }

    public void DeactivateDoors()
    {
        Collider2D[] doorsInArea = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y), circleRadiusDoor, doorMask);
        for (int i = 0; i < doorsInArea.Length; i++)
        {
            GameObject doors = doorsInArea[i].gameObject;
            PhotonNetwork.Destroy(doors);
        }
    }

    public void StunDrone()
    {
        if(timeLeft <= 0)
        {
            Collider2D[] droneInArea = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y), circleRadiusDrone, droneMask);
            for (int i = 0; i < droneInArea.Length; i++)
            {
                GameObject drone = droneInArea[i].gameObject;
                GetComponent<PhotonView>().RPC("RPCStunDrone", PhotonTargets.All);
                timeLeft = 20f;
            }
        }
    }

    [PunRPC]
    public void RPCStunDrone()
    {
        guardDroneManager.stunActivated = true;
    }

    private IEnumerator WaitToFind()
    {
        WaitForSeconds wait = new WaitForSeconds(1f);
        yield return wait;
        openSecDoorButton = GameObject.Find("BreakSecurityDoorButton");
        Button secDoor = openSecDoorButton.GetComponent<Button>();
        secDoor.onClick.AddListener(() => { DeactivateDoors(); });

        stunDroneButton = GameObject.Find("StunDroneButton");
        Button stunBut = stunDroneButton.GetComponent<Button>();
        stunBut.onClick.AddListener(() => { StunDrone(); });
    }

    private IEnumerator Stunned()
    {
        WaitForSeconds wait = new WaitForSeconds(3f);
        playerMovementManager.speedAmp = 0;
        yield return wait;
        playerMovementManager.speedAmp = 5;
        stunActivated = false;
    }

    private void FogRemove()
    {
       
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, circleRadiusDoor);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(this.transform.position, circleRadiusDrone);
    }

    private IEnumerator WaitTarget()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);
        while (true)
        {
            yield return wait;
            FindFog();
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {

    }
}

#region Old Code When Deactivating alarm, keeping just incase :)

//public bool alarmDeactivated;
//if(timeLeft <= 0)
//{
//    Collider2D[] doorsInArea = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y), circleRadius, doorMask);
//    for (int i = 0; i < doorsInArea.Length; i++)
//    {
//        GameObject alarms = doorsInArea[i].gameObject;
//        if(photonView.isMine)
//        {
//            alarmDeactivated = true;
//            timeLeft = 20;
//        }
//    }           
//}
#endregion