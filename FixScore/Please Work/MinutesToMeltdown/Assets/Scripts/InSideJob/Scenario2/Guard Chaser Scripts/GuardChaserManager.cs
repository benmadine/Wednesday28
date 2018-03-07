using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuardChaserManager : Photon.MonoBehaviour
{
    public GameObject setBarricadeButton;

    public GameObject barricadePrefab;

    public GameObject barricade;

    Vector2 barricadePos;
    float maxDistanceFromPlayer = 2.5f;
    public bool barricadeSet = false;

    public List<GameObject> maxBarricades = new List<GameObject>();

    PlayerSelection spawnSorter;

    InSideJobManager scoreManager;

    /// <summary>
    ///New fog code, using the same as the alarm line casting technique :)
    /// </summary>
    /// 
    public float playerRadius = 2;
    public float view = 360;

    public LayerMask targetmask;
    public LayerMask obstacleMask;

    private List<Transform> fogInArea = new List<Transform>();

    private void Start()
    {
        spawnSorter = GameObject.Find("GameManager").GetComponent<PlayerSelection>();
        scoreManager = GameObject.Find("GameManager").GetComponent<InSideJobManager>();

        if(photonView.isMine)
        {
            StartCoroutine(WaitToFind());
            StartCoroutine("WaitTarget");
        }
    }

    private void Update()
    {
        FogStatus();
    }

    private void PlaceBarricadeButton()
    {
        if(maxBarricades.Count < 5)
        {
            if(barricadeSet)
            {
                barricade = PhotonNetwork.Instantiate(barricadePrefab.name, barricadePos, Quaternion.identity, 0);
                barricadeSet = false;
                maxBarricades.Add(barricade);
            }
        }
        else
        {
            Debug.LogError("Oops, ran out of barricades u silly billy");
            return;
        }
    }

    private void OnMouseDown()
    {
        if(!barricadeSet)
        {
            Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(mousePosition);
            if (Vector2.Distance(this.gameObject.transform.position, touchPos) < maxDistanceFromPlayer)
            {
                barricadePos = touchPos;
                Debug.Log("Barricade Position is: " + barricadePos);
                barricadeSet = true;
            }
            else
            {
                Debug.LogError("Distance is too far away from player");
            }
        }
    }

    [PunRPC]
    private void RoundScoreThief()
    {
        scoreManager.thiefScore = 0;
    }

    [PunRPC]
    private void RoundScoreHacker()
    {
        if (scoreManager.thiefScore == 1)
        {
            scoreManager.thiefScore = 0;
        }
        else
        {
            scoreManager.thiefScore = scoreManager.thiefScore / 2;
            Mathf.Round(scoreManager.thiefScore);
        }
    }

    private void CollectorHit(GameObject thiefCollector)
    {
        ThiefCollectorManager thiefCollectorScript = thiefCollector.GetComponent<ThiefCollectorManager>();
        if (spawnSorter.map1)
        {
            thiefCollector.transform.position = spawnSorter.thiefCollectorSpawn;
        }
        if (spawnSorter.map2)
        {
            thiefCollector.transform.position = spawnSorter.thiefCollectorSpawn;
        }
        if (spawnSorter.map3)
        {
            thiefCollector.transform.position = spawnSorter.thiefCollectorSpawn;
        }
    }

    private void HackerHit(GameObject thiefHacker)
    {
        ThiefHackerManager thiefHackerScript = thiefHacker.GetComponent<ThiefHackerManager>();
        if (spawnSorter.map1)
        {
            thiefHacker.transform.position = spawnSorter.thiefHackerSpawn;
        }
        if (spawnSorter.map2)
        {
            thiefHacker.transform.position = spawnSorter.thiefHackerSpawn;
        }
        if (spawnSorter.map3)
        {
            thiefHacker.transform.position = spawnSorter.thiefHackerSpawn;
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
            if(Vector2.Distance(this.transform.position, fogTrans.position) > 4) //Change that to increase FOV radius
            {
                fogTrans.gameObject.GetComponent<SpriteRenderer>().enabled = true;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ThiefCollector") //All points gone
        {
            CollectorHit(collision.gameObject);
            GetComponent<PhotonView>().RPC("RoundScoreThief", PhotonTargets.All);
        }
        if (collision.gameObject.tag == "ThiefHacker") //Half points gone
        {
            HackerHit(collision.gameObject);
            GetComponent<PhotonView>().RPC("RoundScoreHacker", PhotonTargets.All);
        }
    }

    private IEnumerator WaitToFind()
    {
        WaitForSeconds wait = new WaitForSeconds(1f);
        yield return wait;
        setBarricadeButton = GameObject.Find("SpawnBarricadeButton");
        Button stunThiefBut = setBarricadeButton.GetComponent<Button>();
        stunThiefBut.onClick.AddListener(() => { PlaceBarricadeButton(); });
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
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, maxDistanceFromPlayer);
    }
}
