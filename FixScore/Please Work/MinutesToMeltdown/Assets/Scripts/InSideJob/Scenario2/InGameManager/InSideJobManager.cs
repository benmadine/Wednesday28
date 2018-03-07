using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InSideJobManager : Photon.MonoBehaviour, IPunObservable
{

    public float thiefScore;
    public Text thiefScoreText;

    private float currentTime = 150f;
    public Text timerText;

    GameObject dontDestroyObject;

    public enum PossibleMaps
    {
        map1,
        map2,
        map3
    }

    public PossibleMaps possibleMapSelector;

	// Use this for initialization
	void Start ()
    {
        if(PhotonNetwork.isMasterClient)
        {
        //possibleMapSelector = (PossibleMaps)Random.Range(0, 3);
        //MapSwitch();
        }
        dontDestroyObject = GameObject.Find("Scenario2ScoreSaver");     
    }
	
	// Update is called once per frame
	void Update ()
    {
        TimerManager();
        GetComponent<PhotonView>().RPC("ScoreManagerMethod", PhotonTargets.All);
        dontDestroyObject.GetComponent<ScoreSaving>().Score = thiefScore;
        EndConditions();

    }

    private void MapSwitch()
    {
        switch (possibleMapSelector)
        {            
            case PossibleMaps.map1:
            PhotonNetwork.Instantiate("Map1", new Vector2(0,0), Quaternion.identity, 0);
            break;
            case PossibleMaps.map2:
            PhotonNetwork.Instantiate("Map2", new Vector2(0,0), Quaternion.identity, 0);
            break;
            case PossibleMaps.map3:
            PhotonNetwork.Instantiate("Map3", new Vector2(0,0), Quaternion.identity, 0);
            break;
        }
    }

    [PunRPC]
    private void ScoreManagerMethod()
    {
        thiefScoreText.text = "Items Collected: " + thiefScore;
    }

    private void TimerManager()
    {
        currentTime -= Time.deltaTime;
        timerText.text = "Time left: " + currentTime.ToString("F2");
    }

    private void EndConditions()
    {
        if(currentTime <= 0)
        {           
            PhotonNetwork.LoadLevel("ScoreReveal");
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        
    }
}
