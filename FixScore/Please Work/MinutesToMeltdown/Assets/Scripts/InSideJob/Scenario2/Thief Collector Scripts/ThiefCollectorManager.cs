using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThiefCollectorManager : Photon.MonoBehaviour, IPunObservable
{
    float circleRadius = 0.5f;

    public LayerMask barricadeMask;


    public GameObject breakDoorButton;

    GameControlerScenario2 controlerScript;

    public int currentCollected;


    public bool stunActivated;
    public GameControlerScenario2 playerMovementManager;

    public float playerRadius = 2;
    public float view = 360;

    public LayerMask targetmask;
    public LayerMask obstacleMask;

    private List<Transform> fogInArea = new List<Transform>();

    private void Start()
    {
        controlerScript = this.GetComponent<GameControlerScenario2>();

        if (photonView.isMine)
        {
            StartCoroutine(WaitToFind());
            StartCoroutine("WaitTarget");
        }
    }

    private void Update()
    {
        if(currentCollected == 0)
        {
            controlerScript.speedAmp = 5;
        }

        if(stunActivated)
        {
            StartCoroutine(Stunned());
        }
        FogStatus();
    }

    private void BreakOpenDoorButton()
    {
        Collider2D[] doorsInArea = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y), circleRadius, barricadeMask);
        for (int i = 0; i < doorsInArea.Length; i++)
        {
            GameObject doors = doorsInArea[i].gameObject;
            PhotonNetwork.Destroy(doors);
        }
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Loot")
        {
            GetComponent<PhotonView>().RPC("AddingCollected", PhotonTargets.All);
            controlerScript.speedAmp--;
            PhotonNetwork.Destroy(collision.gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(this.transform.position, circleRadius);
    }

    [PunRPC]
    private void AddingCollected() //Had to make this a PUN RPC so that the score would be updated on clients:)
    {
        currentCollected++;
    }

    private IEnumerator WaitToFind()
    {
        WaitForSeconds wait = new WaitForSeconds(1f);
        yield return wait;
        breakDoorButton = GameObject.Find("BreakDoorButton");
        Button breakDoorBut = breakDoorButton.GetComponent<Button>();
        breakDoorBut.onClick.AddListener(() => { BreakOpenDoorButton(); });
    }

    private IEnumerator Stunned()
    {
        WaitForSeconds wait = new WaitForSeconds(3f);
        playerMovementManager.speedAmp = 0;
        yield return wait;
        playerMovementManager.speedAmp = 5;
        stunActivated = false;
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
