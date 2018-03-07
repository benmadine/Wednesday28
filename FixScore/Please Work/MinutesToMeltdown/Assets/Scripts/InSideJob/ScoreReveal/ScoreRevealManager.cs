using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreRevealManager : Photon.MonoBehaviour
{

    private float round1Score, round2Score;
    float savingRound1;

    public Text round1Text, round2Text;

    ScoreSaving scoreScript;

    public GameObject startGameButton;

    private void Start()
    {
        scoreScript = GameObject.Find("Scenario2ScoreSaver").GetComponent<ScoreSaving>();
        if (!scoreScript.round)
        {
            round1Score = scoreScript.round1Scores;
            PlayerPrefs.SetFloat("savingRound1", round1Score);
            scoreScript.round = true;
        }
        if (scoreScript.round)
        {
            round2Score = scoreScript.round2Scores;
            
        }
    }

    private void Update()
    {
        round1Text.text = "Round 1: " + PlayerPrefs.GetFloat("savingRound1");

        round2Text.text = "Round 2: " + round2Score;

        if(PhotonNetwork.isMasterClient)
        {
            startGameButton.SetActive(true);
        }
    }

    public void ReplayButton()
    {
        PhotonNetwork.LoadLevel("PlayerScenario2");
    }

    public void Leave()
    {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LoadLevel("Launcher");
    }
}
