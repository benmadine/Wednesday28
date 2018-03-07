using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour
{
    public Text timerText;
    public float maxTime = 60f ,currentTime;
    SpriteRenderer wrongAns;
    GameManagerScript gameManagerScript;
    bool timerPassed;
    RandomPuzzlePicker randomPuzzleScript;

    void Finding()
    {
        gameManagerScript = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManagerScript>();
        timerText = GameObject.FindGameObjectWithTag("TimerText").GetComponent<Text>();
        wrongAns = GameObject.FindGameObjectWithTag("Lost").GetComponent<SpriteRenderer>();
        randomPuzzleScript = GameObject.FindGameObjectWithTag("GameManager").GetComponent<RandomPuzzlePicker>();
    }

	// Use this for initialization
	void Start ()
    {
        Finding();
        timerText.text = "" + maxTime;
        currentTime = maxTime;
    }
	
	// Update is called once per frame
	void Update ()
    {
        TimerCountdown();
    }

    void TimerCountdown()
    {        
        if(timerPassed == false)
        {
            currentTime -= Time.deltaTime;
        }
        
        timerText.text = "" + currentTime.ToString("F2");       
        if(currentTime <=0)
        {
            timerPassed = true;
            wrongAns.enabled = true;
            StartCoroutine(WaitToLerp());           
        }
    }

    IEnumerator WaitToLerp()
    {
        yield return new WaitForSeconds(2);
        gameManagerScript.MovingCamera(0);
        if(randomPuzzleScript.heldInt == 0)
        {
            gameManagerScript.puzzle2 = true;
        }
        if (randomPuzzleScript.heldInt == 1)
        {
            gameManagerScript.puzzle1 = true;
        }
        if (randomPuzzleScript.heldInt == 2)
        {
            gameManagerScript.puzzle3 = true;
        }
        if (randomPuzzleScript.heldInt == 3)
        {
            gameManagerScript.puzzle4 = true;
        }
        Destroy(gameObject, 2);
    }
}
