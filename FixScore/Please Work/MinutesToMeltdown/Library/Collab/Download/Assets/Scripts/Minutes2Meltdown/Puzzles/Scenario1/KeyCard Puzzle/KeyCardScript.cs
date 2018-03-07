using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyCardScript : MonoBehaviour {

    //Script
    public KeyCardTimer timerScript;
    //Card rigigdbody
    Rigidbody2D rigidBody;
    //The correct and lose text
    SpriteRenderer correctAns;
    //The correct keycards
    public enum KeyCards { KeyCard1, KeyCard2, KeyCard3, KeyCard4, KeyCard5, KeyCard6 }
    public KeyCards keyCardselected;
    //Script
    GameManagerScript gameManagerScript;
    //To check if the game should be destroyed
    public bool gameDestroy;
    //This puzzle
    GameObject puzzleD;
    //Keycard main scrpt
    KetCardCounter ketCardCounter;

    //Audio Stuff
    AudioSource aSource;
    public AudioClip correctCard, wrongCard;
    bool audioPlayed;

    Vector2 mousePosition, objPosition;

    //bOOL FOR lerp
    bool lerpOnceLerp;

    //Lights Flash
    public SpriteRenderer lightImageHolder;
    public Sprite[] lightImages;


    void Finding()
    {
        aSource = GameObject.FindGameObjectWithTag("KeyCardPuzzle").GetComponent<AudioSource>();
        ketCardCounter = GameObject.FindGameObjectWithTag("KeyCardPuzzle").GetComponent<KetCardCounter>();
        puzzleD = GameObject.FindGameObjectWithTag("KeyCardPuzzle");
        gameManagerScript = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManagerScript>();
        rigidBody = this.GetComponent<Rigidbody2D>();
        timerScript = GameObject.FindGameObjectWithTag("KeyCardPuzzle").GetComponent<KeyCardTimer>();
        correctAns = GameObject.FindGameObjectWithTag("Passed").GetComponent<SpriteRenderer>();
    }

	// Use this for initialization
	void Start ()
    {
        Finding();     
	}
	
	// Update is called once per frame
	void Update ()
    {
        DaysOfWeek();  
    }

    private void OnMouseDrag()
    {
        Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        rigidBody.MovePosition(objPosition);    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch(keyCardselected)
        {
            case KeyCards.KeyCard1:
                if(gameObject.tag == "KeyCard1")
                {
                    if (collision.tag == "Scanner")
                    {
                        StartCoroutine(ChangeFlash(2));
                        aSource.PlayOneShot(correctCard, 0.5f);
                        correctAns.enabled = true;
                        StartCoroutine(WaitToLerp());                       
                    }
                }
                else
                {
                    if (collision.tag == "Scanner")
                    {
                        StartCoroutine(ChangeFlash(1));
                        aSource.PlayOneShot(wrongCard, 0.5f);
                        Handheld.Vibrate();
                        ketCardCounter.wrongCounter++;
                        ketCardCounter.flashOnce = false;
                        ketCardCounter.flashOnce = false;                       
                    }
                }
                break;
            case KeyCards.KeyCard2:
                if (gameObject.tag == "KeyCard2")
                {
                    if (collision.tag == "Scanner")
                    {
                        aSource.PlayOneShot(correctCard, 0.5f);
                        StartCoroutine(ChangeFlash(2));
                        correctAns.enabled = true;
                        StartCoroutine(WaitToLerp());
                    }
                }
                else
                {
                    if (collision.tag == "Scanner")
                    {
                        StartCoroutine(ChangeFlash(1));
                        aSource.PlayOneShot(wrongCard, 0.5f);
                        Handheld.Vibrate();
                        ketCardCounter.wrongCounter++;
                        ketCardCounter.flashOnce = false;
                    }
                }
                break;
            case KeyCards.KeyCard3:
                if (gameObject.tag == "KeyCard3")
                {
                    if (collision.tag == "Scanner")
                    {
                        aSource.PlayOneShot(correctCard, 0.5f);
                        StartCoroutine(ChangeFlash(2));
                        correctAns.enabled = true;
                        StartCoroutine(WaitToLerp());
                    }
                }
                else
                {
                    if (collision.tag == "Scanner")
                    {
                        StartCoroutine(ChangeFlash(1));
                        aSource.PlayOneShot(wrongCard, 0.5f);
                        Handheld.Vibrate();
                        ketCardCounter.wrongCounter++;
                        ketCardCounter.flashOnce = false;
                    }
                }
                break;
            case KeyCards.KeyCard4:
                if (gameObject.tag == "KeyCard4")
                {
                    if (collision.tag == "Scanner")
                    {
                        aSource.PlayOneShot(correctCard, 0.5f);
                        StartCoroutine(ChangeFlash(2));
                        correctAns.enabled = true;
                        StartCoroutine(WaitToLerp());
                    }
                }
                else
                {
                    if (collision.tag == "Scanner")
                    {
                        StartCoroutine(ChangeFlash(1));
                        aSource.PlayOneShot(wrongCard, 0.5f);
                        Handheld.Vibrate();
                        ketCardCounter.wrongCounter++;
                        ketCardCounter.flashOnce = false;
                    }
                }
                break;
            case KeyCards.KeyCard5:
                if (gameObject.tag == "KeyCard5")
                {
                    if (collision.tag == "Scanner")
                    {
                        aSource.PlayOneShot(correctCard, 0.5f);
                        StartCoroutine(ChangeFlash(2));
                        correctAns.enabled = true;
                        StartCoroutine(WaitToLerp());
                    }
                }
                else
                {
                    if (collision.tag == "Scanner")
                    {
                        StartCoroutine(ChangeFlash(1));
                        aSource.PlayOneShot(wrongCard, 0.5f);
                        Handheld.Vibrate();
                        ketCardCounter.wrongCounter++;
                        ketCardCounter.flashOnce = false;
                    }
                }
                break;
            case KeyCards.KeyCard6:
                if (gameObject.tag == "KeyCard6")
                {
                    if (collision.tag == "Scanner")
                    {
                        aSource.PlayOneShot(correctCard, 0.5f);
                        StartCoroutine(ChangeFlash(2));
                        correctAns.enabled = true;
                        StartCoroutine(WaitToLerp());
                    }
                }
                else
                {
                    if (collision.tag == "Scanner")
                    {
                        StartCoroutine(ChangeFlash(1));
                        aSource.PlayOneShot(wrongCard, 0.5f);
                        Handheld.Vibrate();
                        ketCardCounter.wrongCounter++;
                        ketCardCounter.flashOnce = false;
                    }
                }
                break;
        }
    }

    IEnumerator WaitToLerp()
    {
        if (correctAns.enabled == true)
            if (!lerpOnceLerp)
            {
                gameManagerScript.fourToWin++;
                gameManagerScript.counterToLerp++;
                lerpOnceLerp = true;
            }
        gameDestroy = true;
        yield return new WaitForSeconds(2);
        gameManagerScript.MovingCamera(0);
        gameManagerScript.puzzle4 = true;
        Destroy(puzzleD,1);
    }

    IEnumerator ChangeFlash(int i)
    {
        lightImageHolder.sprite = lightImages[i];
        yield return new WaitForSeconds(0.5f);
        lightImageHolder.sprite = lightImages[0];
    }

    void DaysOfWeek()
    {
        if (ketCardCounter.monday == true)
        {
            if (timerScript.currentTime >= 50)
            {
                keyCardselected = KeyCards.KeyCard1;
            }
            if (timerScript.currentTime >= 40 && timerScript.currentTime <= 50)
            {
                keyCardselected = KeyCards.KeyCard2;
            }
            if (timerScript.currentTime >= 30 && timerScript.currentTime <= 40)
            {
                keyCardselected = KeyCards.KeyCard3;
            }
            if (timerScript.currentTime >= 20 && timerScript.currentTime <= 30)
            {
                keyCardselected = KeyCards.KeyCard4;
            }
            if (timerScript.currentTime >= 10 && timerScript.currentTime <= 20)
            {
                keyCardselected = KeyCards.KeyCard5;
            }
            if (timerScript.currentTime >= 0 && timerScript.currentTime <= 10)
            {
                keyCardselected = KeyCards.KeyCard6;
            }
        }
        if(ketCardCounter.tuesday == true)
        {
            if (timerScript.currentTime >= 50)
            {
                keyCardselected = KeyCards.KeyCard3;
            }
            if (timerScript.currentTime >= 40 && timerScript.currentTime <= 50)
            {
                keyCardselected = KeyCards.KeyCard5;
            }
            if (timerScript.currentTime >= 30 && timerScript.currentTime <= 40)
            {
                keyCardselected = KeyCards.KeyCard1;
            }
            if (timerScript.currentTime >= 20 && timerScript.currentTime <= 30)
            {
                keyCardselected = KeyCards.KeyCard4;
            }
            if (timerScript.currentTime >= 10 && timerScript.currentTime <= 20)
            {
                keyCardselected = KeyCards.KeyCard6;
            }
            if (timerScript.currentTime >= 0 && timerScript.currentTime <= 10)
            {
                keyCardselected = KeyCards.KeyCard2;
            }
        }
        if(ketCardCounter.wednesday == true)
        {
            if (timerScript.currentTime >= 50)
            {
                keyCardselected = KeyCards.KeyCard5;
            }
            if (timerScript.currentTime >= 40 && timerScript.currentTime <= 50)
            {
                keyCardselected = KeyCards.KeyCard4;
            }
            if (timerScript.currentTime >= 30 && timerScript.currentTime <= 40)
            {
                keyCardselected = KeyCards.KeyCard3;
            }
            if (timerScript.currentTime >= 20 && timerScript.currentTime <= 30)
            {
                keyCardselected = KeyCards.KeyCard1;
            }
            if (timerScript.currentTime >= 10 && timerScript.currentTime <= 20)
            {
                keyCardselected = KeyCards.KeyCard6;
            }
            if (timerScript.currentTime >= 0 && timerScript.currentTime <= 10)
            {
                keyCardselected = KeyCards.KeyCard2;
            }
        }
        if(ketCardCounter.thursday == true)
        {
            if (timerScript.currentTime >= 50)
            {
                keyCardselected = KeyCards.KeyCard1;
            }
            if (timerScript.currentTime >= 40 && timerScript.currentTime <= 50)
            {
                keyCardselected = KeyCards.KeyCard3;
            }
            if (timerScript.currentTime >= 30 && timerScript.currentTime <= 40)
            {
                keyCardselected = KeyCards.KeyCard5;
            }
            if (timerScript.currentTime >= 20 && timerScript.currentTime <= 30)
            {
                keyCardselected = KeyCards.KeyCard2;
            }
            if (timerScript.currentTime >= 10 && timerScript.currentTime <= 20)
            {
                keyCardselected = KeyCards.KeyCard4;
            }
            if (timerScript.currentTime >= 0 && timerScript.currentTime <= 10)
            {
                keyCardselected = KeyCards.KeyCard6;
            }
        }
        if(ketCardCounter.friday == true)
        {
            if (timerScript.currentTime >= 50)
            {
                keyCardselected = KeyCards.KeyCard5;
            }
            if (timerScript.currentTime >= 40 && timerScript.currentTime <= 50)
            {
                keyCardselected = KeyCards.KeyCard3;
            }
            if (timerScript.currentTime >= 30 && timerScript.currentTime <= 40)
            {
                keyCardselected = KeyCards.KeyCard1;
            }
            if (timerScript.currentTime >= 20 && timerScript.currentTime <= 30)
            {
                keyCardselected = KeyCards.KeyCard4;
            }
            if (timerScript.currentTime >= 10 && timerScript.currentTime <= 20)
            {
                keyCardselected = KeyCards.KeyCard6;
            }
            if (timerScript.currentTime >= 0 && timerScript.currentTime <= 10)
            {
                keyCardselected = KeyCards.KeyCard2;
            }
        }
    }
}
