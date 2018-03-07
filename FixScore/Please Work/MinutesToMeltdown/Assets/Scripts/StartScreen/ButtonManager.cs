using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : Photon.MonoBehaviour
{
    public GameObject startGameButton;
    enum ScenarioSelector{Scenario1, Scenario2}
    ScenarioSelector randomPick;
    AudioSource buttonClick;

    private void Awake()
    {
        
    }

    private void Start()
    {
        buttonClick = GameObject.Find("ButtonManagement").GetComponent<AudioSource>();
        RandomScenario();
    }

    private void Update()
    {
        if (PhotonNetwork.isMasterClient)
        {
            startGameButton.SetActive(true);
        }
    }

    public void PlayerStart()
    {
        if(PhotonNetwork.playerList.Length > 0)
        {
            string playerStart = "";

            switch (randomPick)
            {
                case ScenarioSelector.Scenario1:
                    PhotonNetwork.automaticallySyncScene = true;
                    playerStart = "PlayerScenario2";
                    break;
                case ScenarioSelector.Scenario2:
                    PhotonNetwork.automaticallySyncScene = true;
                    playerStart = "PlayerScenario2";
                    break;
            }
            StartCoroutine(SoundWait(playerStart));
        }
        else
        {
            return;
        }
    }

    public void PlayerInstructions()
    {
        buttonClick.Play();
        StartCoroutine(SoundWait("PlayerInstructions"));
    }

    public void Options()
    {
        buttonClick.Play();
        StartCoroutine(SoundWait("Options"));
    }

    public void QuitGame()
    {
        buttonClick.Play();
        Application.Quit();
    }

    void RandomScenario()
    {
        randomPick = (ScenarioSelector)Random.Range(0, 2);
    }

    IEnumerator SoundWait(string sceneToLoad)
    {
        buttonClick.Play();
        yield return new WaitForSeconds(1);
        PhotonNetwork.LoadLevel(sceneToLoad);
    }
}
