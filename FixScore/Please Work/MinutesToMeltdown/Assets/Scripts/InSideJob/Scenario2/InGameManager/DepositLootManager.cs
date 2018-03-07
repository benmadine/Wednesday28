using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepositLootManager : Photon.MonoBehaviour, IPunObservable
{
    [SerializeField]
    protected InSideJobManager managerSciprt;

    ThiefCollectorManager thiefCollecterScript;

    // Use this for initialization
    void Start ()
    {

    }
	
	// Update is called once per frame
	void Update ()
    {
        if(managerSciprt == null)
        {
            managerSciprt = GameObject.Find("GameManager").GetComponent<InSideJobManager>();
        }
        if(thiefCollecterScript == null)
        {
            thiefCollecterScript = GameObject.FindGameObjectWithTag("ThiefCollector").GetComponent<ThiefCollectorManager>();
        }     
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(photonView.isMine)
        {
            if (collision.tag == "ThiefCollector")
            {
                GetComponent<PhotonView>().RPC("AddingScore", PhotonTargets.All);               
            }
        }    
    }

    [PunRPC]
    private void AddingScore()
    {
        managerSciprt.thiefScore += thiefCollecterScript.currentCollected;
        thiefCollecterScript.currentCollected = 0;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {

    }
}
