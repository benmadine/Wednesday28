using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuardDroneManager : Photon.MonoBehaviour
{
    #region Arrow Alarms Variables
    GameObject arrowAlarmImage;

    
    public List<AlarmScript> scriptFromAlarm = new List<AlarmScript>();
    public  List<AlarmScript> singleAlarmScipt = new List<AlarmScript>();
    #endregion

    #region Thief Tracker Variables
    public GameObject TrackThiefsButton;
    public GameObject thiefChaserArrow;
    public GameObject thiefHackerArrow;

    public GameObject thiefCollector;
    public GameObject thiefHacker;

    float timeLeft = 5;
    #endregion

    #region Thief Stun Varaibles
    public GameObject stunThiefsButton;
    private float stunCoolDown = 20f;

    private float circleRadiusDrone = 4f;
    public LayerMask thiefMask;

    ThiefHackerManager thiefHackerManager;
    ThiefCollectorManager thiefCollectorManager;
    #endregion

    public bool stunActivated;

    public GameControlerScenario2 playerMovementManager;

    public float playerRadius = 2;
    public float view = 360;

    public LayerMask targetmask;
    public LayerMask obstacleMask;

    private List<Transform> fogInArea = new List<Transform>();

    public bool checkTrack;

    private void Start()
    {        
        if (photonView.isMine)
        {
            StartCoroutine(WaitToFind());
            StartCoroutine("WaitTarget");
        }
    }

    private void Update()
    {
        if(photonView.isMine)
        {
            arrowAlarmImage = GameObject.Find("AlarmArrowImage");
            thiefHackerArrow = GameObject.Find("ThiefHackerImage");
            thiefChaserArrow = GameObject.Find("ThiefCollectorImage");
        }
        FindingAlarms();

        if (thiefHackerManager == null)
        {

             thiefHacker = GameObject.FindGameObjectWithTag("ThiefHacker");
            thiefHackerManager = GameObject.FindGameObjectWithTag("ThiefHacker").GetComponent<ThiefHackerManager>();
        }
        if (thiefCollectorManager == null)
        {
                        thiefCollector = GameObject.FindGameObjectWithTag("ThiefCollector");
            thiefCollectorManager = GameObject.FindGameObjectWithTag("ThiefCollector").GetComponent<ThiefCollectorManager>();
        }


        if (stunActivated)
        {
            StartCoroutine(Stunned());
        }


        // if (GameObject.FindGameObjectWithTag("ThiefCollector") == null)
        // {
        //     thiefCollector = GameObject.FindGameObjectWithTag("ThiefCollector");
        // }
        // if (GameObject.FindGameObjectWithTag("ThiefHacker") == null)
        // {
        //     thiefHacker = GameObject.FindGameObjectWithTag("ThiefHacker");
        // }

        FogStatus();
        timeLeft -= Time.deltaTime;
        stunCoolDown -= Time.deltaTime;
    }

    private void FindingAlarms()
    {
        for (int i = 0; i < scriptFromAlarm.Count; i++)
        {
            if (scriptFromAlarm[i].alarmActivated)
            {
                AlarmActivated();
                singleAlarmScipt[i].hasBeenActivated = false;
            }
        }
    }

    private void StunThiefs()
    {
        if (stunCoolDown <= 0)
        {
            print("Stun Thiefs Collected");
            Collider2D hackerInArea = Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y), circleRadiusDrone, thiefMask);
            if(hackerInArea)
            {
                GameObject hacker = hackerInArea.gameObject;
                GetComponent<PhotonView>().RPC("RPCStunHacker", PhotonTargets.All);
            }

            Collider2D collectorInArea = Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y), circleRadiusDrone, thiefMask);
            if (collectorInArea)
            {
                GameObject collector = collectorInArea.gameObject;
                GetComponent<PhotonView>().RPC("RPCStunCollector", PhotonTargets.All);
            }
            stunCoolDown = 20f;
        }
    }

    [PunRPC]
    public void RPCStunHacker()
    {
        thiefHackerManager.stunActivated = true;
    }

    [PunRPC]
    public void RPCStunCollector()
    {
        thiefCollectorManager.stunActivated = true;
    }

    private void TrackThiefs()
    {
        if (timeLeft <= 0)
        {
            ThiefTracker();
            timeLeft = 5;
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

    private GameObject ThiefTracker()
    {
        checkTrack = true;
        float speed = 10 * Time.deltaTime;

        Vector3 thiefCollectorPosition = thiefCollector.transform.position;
        Vector3 thiefHackerPosition = thiefHacker.transform.position;

        Vector3 collectorArrow = this.transform.position - thiefCollectorPosition;
        Vector3 hackerArrow = this.transform.position - thiefHackerPosition;

        collectorArrow = collectorArrow * -1f;
        hackerArrow = hackerArrow * -1f;

        if (collectorArrow.x > -6.49f && collectorArrow.x < 6.51f &&
               collectorArrow.y > -4.49f && collectorArrow.y < 4.51f)
        {
            thiefChaserArrow.GetComponent<Image>().enabled = false;
        }
        else
        {
            thiefChaserArrow.GetComponent<Image>().enabled = true;
        }
        
        if (hackerArrow.x > -6.49f && hackerArrow.x < 6.51f &&
              hackerArrow.y > -4.49f && hackerArrow.y < 4.51f)
        {
            thiefHackerArrow.GetComponent<Image>().enabled = false;
        }
        else
        {
            thiefHackerArrow.GetComponent<Image>().enabled = true;
        }

        if (collectorArrow.x > 6.5f || collectorArrow.x < -6.5f)
        {
            collectorArrow.x = Mathf.Clamp(collectorArrow.x, -6f, 6f);
        }
        if (collectorArrow.y < -4.5f || collectorArrow.y > 4.5f)
        {
            collectorArrow.y = Mathf.Clamp(collectorArrow.y, -3.5f, 3.5f);
        }

        if (hackerArrow.x > 6.5f || hackerArrow.x < -6.5f)
        {
            hackerArrow.x = Mathf.Clamp(hackerArrow.x, -6f, 6f);
        }
        if (hackerArrow.y < -4.5f || hackerArrow.y > 4.5f)
        {
            hackerArrow.y = Mathf.Clamp(hackerArrow.y, -3.5f, 3.5f);
        }

        thiefChaserArrow.transform.position = this.gameObject.transform.position + collectorArrow;
        thiefHackerArrow.transform.position = this.gameObject.transform.position + hackerArrow;

        Vector3 newDirection = this.gameObject.transform.position - thiefCollectorPosition;
        float angle = Mathf.Atan2(newDirection.y, newDirection.x) * Mathf.Rad2Deg;
        thiefChaserArrow.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        newDirection = this.gameObject.transform.position - thiefHackerPosition;
        angle = Mathf.Atan2(newDirection.y, newDirection.x) * Mathf.Rad2Deg;
        thiefHackerArrow.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        print("end of method");
        StartCoroutine(TrackerDisappear());
        return null;
    }

    private GameObject AlarmActivated()
    {
        float turningSpeed = 10 * Time.deltaTime;

        for (int i = 0; i < scriptFromAlarm.Count; i++)
        {
            Vector3 alarmPos = scriptFromAlarm[i].transform.position;
            Vector3 alarmPointer = this.gameObject.transform.position - alarmPos;

            alarmPointer = alarmPointer * -1f;

            if(alarmPointer.x > -6.49f && alarmPointer.x < 6.51f &&
                alarmPointer.y > -4.49f && alarmPointer.y < 4.51f)
            {
                arrowAlarmImage.SetActive(false);
            }
            else
            {
                arrowAlarmImage.SetActive(true);
            }
            if(alarmPointer.x > 6.5f || alarmPointer.x < -6.5f)
            {
                alarmPointer.x = Mathf.Clamp(alarmPointer.x, -6f, 6f);
            }
            if(alarmPointer.y < -4.5f || alarmPointer.y > 4.5f)
            {
                alarmPointer.y = Mathf.Clamp(alarmPointer.y, -3.5f, 3.5f);
            }

            arrowAlarmImage.transform.position = this.gameObject.transform.position + alarmPointer;

            Vector3 newDirection = this.gameObject.transform.position - alarmPos;
            float angle = Mathf.Atan2(newDirection.y, newDirection.x) * Mathf.Rad2Deg;
            arrowAlarmImage.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            StartCoroutine(ArrowDisappear());
        }   
        return null;
    }

    private IEnumerator ArrowDisappear()
    {
        arrowAlarmImage.GetComponent<Image>().enabled = true;
        WaitForSeconds wait = new WaitForSeconds(1f);
        yield return wait;
        arrowAlarmImage.GetComponent<Image>().enabled = false;
    }

    private IEnumerator TrackerDisappear()
    {
        thiefChaserArrow.GetComponent<Image>().enabled = true;
        thiefHackerArrow.GetComponent<Image>().enabled = true;
        WaitForSeconds wait = new WaitForSeconds(1.5f);
        yield return wait;
        checkTrack = false;
        thiefChaserArrow.GetComponent<Image>().enabled = false;
        thiefHackerArrow.GetComponent<Image>().enabled = false;
    }

    private IEnumerator Stunned()
    {
        WaitForSeconds wait = new WaitForSeconds(3f);
        playerMovementManager.speedAmp = 0;
        yield return wait;
        playerMovementManager.speedAmp = 5;
        stunActivated = false;
    }

    private IEnumerator WaitToFind()
    {
        WaitForSeconds wait = new WaitForSeconds(1f);
        yield return wait;
        stunThiefsButton = GameObject.Find("StunThiefs");
        Button stunThiefBut = stunThiefsButton.GetComponent<Button>();
        stunThiefBut.onClick.AddListener(() => { StunThiefs(); });

        TrackThiefsButton = GameObject.Find("TrackThiefs");
        Button trackBut = TrackThiefsButton.GetComponent<Button>();
        trackBut.onClick.AddListener(() => { TrackThiefs(); });
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

    private void OnDrawGizmos()
    {

    }
}
