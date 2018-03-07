using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    enum ScenarioSelector{Scenario1, Scenario2, Scenario3}
    ScenarioSelector randomPick;
    AudioSource buttonClick;

    private void Start()
    {
        buttonClick = GameObject.Find("ButtonManagement").GetComponent<AudioSource>();
        RandomScenario();
    }

    public void PlayerStart()
    {
        string playerStart = "";        
        
        switch (randomPick)
        {
            case ScenarioSelector.Scenario1:
                playerStart = "PlayerScenario2";
                break;
            case ScenarioSelector.Scenario2:
                playerStart = "PlayerScenario2";
                break;
            case ScenarioSelector.Scenario3:
                playerStart = "PlayerScenario2";
                break;
        }
        StartCoroutine(SoundWait(playerStart));
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
        randomPick = (ScenarioSelector)Random.Range(0, 3);
    }

    IEnumerator SoundWait(string sceneToLoad)
    {
        buttonClick.Play();
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(sceneToLoad);
    }
}
