using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyCardTimer : MonoBehaviour
{

    public Text timerText;
    public float maxTime = 60f, currentTime;
    float Timeslol;
    SpriteRenderer looseBanner;
    GameManagerScript gameManagerScript;
    bool timerPassed;
    RandomPuzzlePicker randomPuzzleScript;
    string time;

    //add one
    bool addOne;

    // Use this for initialization
    void Start ()
    {
        gameManagerScript = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManagerScript>();
        timerText = GameObject.FindGameObjectWithTag("TimerText").GetComponent<Text>();
        looseBanner = GameObject.FindGameObjectWithTag("Lost").GetComponent<SpriteRenderer>();
        randomPuzzleScript = GameObject.FindGameObjectWithTag("GameManager").GetComponent<RandomPuzzlePicker>();
        currentTime = maxTime;
        Timeslol = 00;
    }
	
	// Update is called once per frame
	void Update ()
    {
        TimerCountdown();
    }

    void TimerCountdown()
    {
        if (timerPassed == false)
        {

            currentTime -= Time.deltaTime;
            Timeslol += Time.deltaTime;
            float rounded = (float)Mathf.Floor(Timeslol);
            if (currentTime <= 60)
            { timerText.text = "08:0" + rounded.ToString(); }
            if (currentTime <= 50)
                timerText.text = "08:" + rounded.ToString() ;
            if (currentTime <= 40)
                timerText.text = "08:" + rounded.ToString() ;
            if (currentTime <= 30)
                timerText.text = "08:" + rounded.ToString() ; 
            if (currentTime <= 20)
                timerText.text = "08:" + rounded.ToString() ;
            if (currentTime <= 10)
                timerText.text = "08:" + rounded.ToString() ;
        }
       
        if (currentTime <= 0f || Timeslol >= 60f)
        {
            timerText.text = "09:00";
            timerPassed = true;
            looseBanner.enabled = true;
            StartCoroutine(WaitToLerp());
        }
    }

    IEnumerator WaitToLerp()
    {
        yield return new WaitForSeconds(2);
        gameManagerScript.MovingCamera(0);
        Destroy(gameObject, 2);
        if (!addOne)
        {
            gameManagerScript.counterToLerp++;
            addOne = true;
        }
    }
}
