using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KetCardCounter : MonoBehaviour
{
    SpriteRenderer wrongAns;
    public bool gameDestroy;
    GameManagerScript gameManagerScript;
    GameObject puzzleD;
    public bool monday, tuesday, wednesday, thursday, friday;
    Text dayText;
    int dayCount;

    AudioSource aSource;
    public AudioClip wonPuzzle, lostPuzzle;
    bool audioPlayed;

    //Lives Stuff
    public int wrongCounter = 0;
    Image livesImage;
    public Sprite[] possibleImages;

    //Alarm Flahs
    [HideInInspector]
    public GameObject alarmFlash;
    [HideInInspector]
    public bool flashOnce;

    //LightSwitch
    bool addOne;

    // Use this for initialization
    void Start ()
    {
        
        aSource = GameObject.FindGameObjectWithTag("KeyCardPuzzle").GetComponent<AudioSource>();

        puzzleD = GameObject.FindGameObjectWithTag("KeyCardPuzzle");

        gameManagerScript = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManagerScript>();

        wrongAns = GameObject.FindGameObjectWithTag("Lost").GetComponent<SpriteRenderer>();

        livesImage = GameObject.Find("Circle").GetComponent<Image>();

        Days();
    }
	
	// Update is called once per frame
	void Update ()
    {
        UpdateLives();
        if (wrongCounter >= 3)
        {
            if(!audioPlayed)
            {
                aSource.PlayOneShot(lostPuzzle, 0.5f);
                audioPlayed = true;
            }
            wrongAns.enabled = true;
            StartCoroutine(WaitToLerp());
        }
    }

    void UpdateLives()
    {
        if (wrongCounter == 1)
        {
            livesImage.sprite = possibleImages[1];
            StartCoroutine(AlarmFlash());
        }

        if (wrongCounter == 2)
        {
            livesImage.sprite = possibleImages[2];
            StartCoroutine(AlarmFlash());
        }

        if (wrongCounter == 3)
        {
            livesImage.sprite = possibleImages[3];
            StartCoroutine(AlarmFlash());
        }
    }

    void Days()
    {
        dayText = GameObject.Find("DayText").GetComponent<Text>();
        dayCount = (int)Random.Range(0, 6);
        if (dayCount == 0)
        {
            monday = true;
            dayText.text = "Monday";
        }
        if (dayCount == 1)
        {
            tuesday = true;
            dayText.text = "Tuesday";
        }
        if (dayCount == 2)
        {
            wednesday = true;
            dayText.text = "Wednesday";
        }
        if (dayCount == 3)
        {
            thursday = true;
            dayText.text = "Thursday";
        }
        if (dayCount == 4)
        {
            friday = true;
            dayText.text = "Friday";
        }
    }

    IEnumerator WaitToLerp()
    {
        gameDestroy = true;
        yield return new WaitForSeconds(2);
        gameManagerScript.MovingCamera(0);
        gameManagerScript.puzzle4 = true;
        Destroy(puzzleD,1);
        if (!addOne)
        {
            gameManagerScript.counterToLerp++;
            addOne = true;
        }
    }

    IEnumerator AlarmFlash()
    {
        if (!flashOnce)
        {
            alarmFlash.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            alarmFlash.SetActive(false);
            flashOnce = true;
        }
    }
}
