using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Lean.Touch;

public class WirePuzzle : MonoBehaviour
{

    GameObject redButton, greenButton, blueButton, orangeButton;

    public DragBlueWire[] wireDraScript;

    SpriteRenderer correctAns, wrongAns;

    [HideInInspector]
    Sprite normalSprite, normalFlashSprite;
    //[HideInInspector]
    public bool correctWireRed, correctWireGreen, correctWireBlue, correctWireYellow;

    GameManagerScript gameManagerScript;
    float maxTime = 8f, currentTime;

    [HideInInspector]
    public List<Sprite> testPlsWorkMore = new List<Sprite>();

    //Lives
    public int counter = 0;
    Image livesImage;
    public Sprite[] possibleImages;

    Sprite redButtonSprite, blueButtonSprite, greenButtonSprite, orangeButtonSprite;

    [HideInInspector]
    public AudioClip wonPuzzle, lostPuzzle;
    AudioSource aSource;
    bool auidoPlayed;

    //The fun part hwg dudududud
    [HideInInspector]
    public enum flashStages { stage1, stage2, stage3 }
    [HideInInspector]
    public flashStages pickedFlashStage;
    [HideInInspector]
    public enum FlashOrder { RGBO, RRBG, RGRB, OBGR, OROR, OOGG, GRBOGR, BGGR }
    [HideInInspector]
    public FlashOrder flashOrderSelected;
    [HideInInspector]
    public enum flashOrderStage2 { ROBG, OROB, OOOO, RBGR, RRBGR, BOG, RRRB, GGGGBR }
    [HideInInspector]
    public flashOrderStage2 flashOrderStage2Picked;
    [HideInInspector]
    public enum flashOrderStage3 { GRBGRR, BGRGOR, BBB, GRGB, OBOR, ORRB, GGGG, RBGO }
    [HideInInspector]
    public flashOrderStage3 flashOrderStage3Picked;

    //bool so once play once
    bool playOnce1, playOnce2, playOncw3;

    //Different Wires
    [HideInInspector]
    public GameObject redWire, greenWire, blueWire, yellowWire;

    //bOOL FOR lerp
    bool lerpOnceLerp;

    //Alarm Flahs
    [HideInInspector]
    public GameObject alarmFlash;
    [HideInInspector]
    public bool flashOnce;

    //Stage Flash
    [HideInInspector]
    public GameObject stageFlash;
    [HideInInspector]
    public bool stageFlashOnce;

    //Only add one
    bool addOne;

    //PressButtonToChangeStage
    public bool buttonPressStage;

    public bool changeNow;

    void Finding()
    {
        aSource = GameObject.FindGameObjectWithTag("WirePuzzle").GetComponent<AudioSource>();
        correctAns = GameObject.FindGameObjectWithTag("Passed").GetComponent<SpriteRenderer>();
        wrongAns = GameObject.FindGameObjectWithTag("Lost").GetComponent<SpriteRenderer>();
        redButton = GameObject.FindGameObjectWithTag("RedButton");
        greenButton = GameObject.FindGameObjectWithTag("GreenButton");
        blueButton = GameObject.FindGameObjectWithTag("BlueButton");
        orangeButton = GameObject.FindGameObjectWithTag("YellowButton");
        gameManagerScript = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManagerScript>();
        livesImage = GameObject.Find("Circle").GetComponent<Image>();
    }

    // Use this for initialization
    void Start()
    {
        Finding();
        pickedFlashStage = flashStages.stage1;
        flashOrderSelected = (FlashOrder)Random.Range(0,8);
        print(flashOrderSelected);
        currentTime = maxTime;
    }

    void UpdateLives()
    {
        if (counter == 1)
        {
            livesImage.sprite = possibleImages[1];
            StartCoroutine(AlarmFlash());
        }

        if (counter == 2)
        {
            livesImage.sprite = possibleImages[2];
            StartCoroutine(AlarmFlash());
        }

        if (counter == 3)
        {
            livesImage.sprite = possibleImages[3];
            StartCoroutine(AlarmFlash());
        }
    }

    void LightOff()
    {
        redButton.GetComponent<SpriteRenderer>().sprite = testPlsWorkMore[5];
        greenButton.GetComponent<SpriteRenderer>().sprite = testPlsWorkMore[3];
        blueButton.GetComponent<SpriteRenderer>().sprite = testPlsWorkMore[1];
        orangeButton.GetComponent<SpriteRenderer>().sprite = testPlsWorkMore[7];
    }

    // Update is called once per frame
    void Update()
    {
        UpdateLives();
        ConditionList();
        if (counter >= 3)
        {
            if (!auidoPlayed)
            {
                aSource.PlayOneShot(lostPuzzle, 0.5f);
                auidoPlayed = true;
            }

            wrongAns.enabled = true;
            StartCoroutine(WaitToLerp());
        }
        if (pickedFlashStage == flashStages.stage1)
        {
            if (correctWireBlue == true && correctWireGreen == true && correctWireRed == true && correctWireYellow == true)
            {
                if(!playOnce1)
                {
                    //buttonPressStage = false;
                    LightOff();
                    stageFlashOnce = false;
                    StartCoroutine(StageFlash());
                    correctWireBlue = false; correctWireGreen = false; correctWireRed = false; correctWireYellow = false;
                    if (!auidoPlayed)
                    {
                        aSource.PlayOneShot(wonPuzzle, 0.5f);
                        auidoPlayed = true;
                    }          
                    changeNow = true;
                }              
            }
            if (changeNow) // here and change now
            {
                pickedFlashStage = flashStages.stage2;
                flashOrderStage2Picked = (flashOrderStage2)Random.Range(0, 8);
                print(flashOrderStage2Picked);
                playOnce1 = true;
                changeNow = false;
            }
        }
        if (pickedFlashStage == flashStages.stage2)
        {
            if (correctWireBlue == true && correctWireGreen == true && correctWireRed == true && correctWireYellow == true)
            {
                if (!playOnce2)
                {
                    buttonPressStage = false;
                    LightOff();
                    stageFlashOnce = false;
                    StartCoroutine(StageFlash());
                    correctWireBlue = false; correctWireGreen = false; correctWireRed = false; correctWireYellow = false;
                    if (!auidoPlayed)
                    {
                        aSource.PlayOneShot(wonPuzzle, 0.5f);
                        auidoPlayed = true;
                    }
                    changeNow = true;
                }
            }
            if (changeNow)
            {
                pickedFlashStage = flashStages.stage3;
                flashOrderStage3Picked = (flashOrderStage3)Random.Range(0, 8);
                print(flashOrderStage3Picked);
                playOnce2 = true;
                changeNow = false;
            }
        }
        if (pickedFlashStage == flashStages.stage3)
        {
            if(!playOncw3)
            {
                stageFlashOnce = false;
                StartCoroutine(StageFlash());
                playOncw3 = true;
            }
            if (correctWireBlue == true && correctWireGreen == true && correctWireRed == true && correctWireYellow == true)
            {
                if (!lerpOnceLerp)
                {
                    gameManagerScript.fourToWin++;
                    lerpOnceLerp = true;
                }
                if (!auidoPlayed)
                {                      
                        aSource.PlayOneShot(wonPuzzle, 0.5f);
                        auidoPlayed = true;
                }
                correctAns.enabled = true;
                StartCoroutine(WaitToLerp());
            }               
        }
    }

    void ConditionList()
    {
        currentTime -= Time.deltaTime;
        if (pickedFlashStage == flashStages.stage1)
        {
            switch (flashOrderSelected)
            {
                case FlashOrder.RGBO:
                    RGBO();
                    break;
                case FlashOrder.RRBG:
                    RRBG();
                    break;
                case FlashOrder.RGRB:
                    RGRB();
                    break;
                case FlashOrder.OBGR:
                    OBGR();
                    break;
                case FlashOrder.OROR:
                    OROR();
                    break;
                case FlashOrder.OOGG:
                    OOGG();
                    break;
                case FlashOrder.GRBOGR:
                    GRBOGR();
                    break;
                case FlashOrder.BGGR:
                    BGGR();
                    break;
            }
        }
        if (pickedFlashStage == flashStages.stage2)
        {
            switch (flashOrderStage2Picked)
            {
                case flashOrderStage2.ROBG:
                    ROBG();
                    break;
                case flashOrderStage2.OROB:
                    OROB();
                    break;
                case flashOrderStage2.OOOO:
                    OOOO();
                    break;
                case flashOrderStage2.RBGR:
                    RBGR();
                    break;
                case flashOrderStage2.RRBGR:
                    RRBGR();
                    break;
                case flashOrderStage2.BOG:
                    BOG();
                    break;
                case flashOrderStage2.RRRB:
                    RRRB();
                    break;
                case flashOrderStage2.GGGGBR:
                    GGGGBR();
                    break;
            }
        }
        if (pickedFlashStage == flashStages.stage3)
        {
            switch (flashOrderStage3Picked)
            {
                case flashOrderStage3.GRBGRR:
                    GRBGRR();
                    break;
                case flashOrderStage3.BGRGOR:
                    BGRGOR();
                    break;
                case flashOrderStage3.BBB:
                    BBB();
                    break;
                case flashOrderStage3.GRGB:
                    GRGB();
                    break;
                case flashOrderStage3.OBOR:
                    OBOR();
                    break;
                case flashOrderStage3.ORRB:
                    ORRB();
                    break;
                case flashOrderStage3.GGGG:
                    GGGG();
                    break;
                case flashOrderStage3.RBGO:
                    RBGO();
                    break;
            }
        }
    }

    public GameObject Flash(GameObject aButton)
    {
        aButton.GetComponent<SpriteRenderer>().sprite = normalFlashSprite;
        return aButton;
    }

    public GameObject FlashBack(GameObject bButton)
    {
        bButton.GetComponent<SpriteRenderer>().sprite = normalSprite;
        return bButton;
    }

    IEnumerator WaitToLerp()
    {
        yield return new WaitForSeconds(2);
        gameManagerScript.MovingCamera(0);
        gameManagerScript.puzzle1 = true;
        Destroy(gameObject, 1);
        if(!addOne)
        {
            gameManagerScript.counterToLerp++;
            addOne = true;
        }
    }

    #region Colours Flash 1 Start
    void RGBO()
    {
        bool repeat = false;
        if (!repeat)
        {
            if (currentTime < 5)
            {
                normalFlashSprite = testPlsWorkMore[4];
                normalSprite = testPlsWorkMore[5];
                redButton = Flash(redButton);
                if (currentTime <= 4)
                {
                    redButton = FlashBack(redButton);
                }
            }
            if (currentTime < 4)
            {
                normalFlashSprite = testPlsWorkMore[2];
                normalSprite = testPlsWorkMore[3];
                greenButton = Flash(greenButton);
                if (currentTime <= 3)
                {
                    greenButton = FlashBack(greenButton);
                }
            }
            if (currentTime < 3)
            {
                normalFlashSprite = testPlsWorkMore[0];
                normalSprite = testPlsWorkMore[1];
                blueButton = Flash(blueButton);
                if (currentTime <= 2)
                {
                    blueButton = FlashBack(blueButton);
                }
            }
            if (currentTime < 2)
            {
                normalFlashSprite = testPlsWorkMore[6];
                normalSprite = testPlsWorkMore[7];
                orangeButton = Flash(orangeButton);
                if (currentTime <= 1)
                {
                    orangeButton = FlashBack(orangeButton);
                    repeat = true;
                    currentTime = 8f;
                }
            }

        }
    }

    void RRBG()
    {
        bool repeat = false;
        if (!repeat)
        {
            normalFlashSprite = testPlsWorkMore[4];
            normalSprite = testPlsWorkMore[5];
            if(currentTime < 5)
            {
                redButton = Flash(redButton);
                if (currentTime < 4.5)
                {
                    redButton = FlashBack(redButton);
                    if (currentTime < 4)
                    {
                        redButton = Flash(redButton);
                        if (currentTime < 3.5)
                        {
                            redButton = FlashBack(redButton);
                        }
                    }
                }
            }           
            if (currentTime < 3)
            {
                normalFlashSprite = testPlsWorkMore[0];
                normalSprite = testPlsWorkMore[1];
                blueButton = Flash(blueButton);
                if (currentTime <= 2)
                {
                    blueButton = FlashBack(blueButton);
                }
            }
            if (currentTime < 2)
            {
                normalFlashSprite = testPlsWorkMore[2];
                normalSprite = testPlsWorkMore[3];
                greenButton = Flash(greenButton);
                if (currentTime <= 1)
                {
                    greenButton = FlashBack(greenButton);
                    repeat = true;
                    currentTime = 8f;
                }
            }
        }
    }

    void RGRB()
    {
        bool repeat = false;
        if (!repeat)
        {
            if(currentTime < 5)
            {
                normalFlashSprite = testPlsWorkMore[4];
                normalSprite = testPlsWorkMore[5];
                redButton = Flash(redButton);
                if (currentTime <= 4)
                {
                    redButton = FlashBack(redButton);
                }
                if (currentTime < 4)
                {
                    normalFlashSprite = testPlsWorkMore[2];
                    normalSprite = testPlsWorkMore[3];
                    greenButton = Flash(greenButton);
                    if (currentTime <= 3)
                    {
                        greenButton = FlashBack(greenButton);
                    }
                }
                if (currentTime < 3)
                {
                    normalFlashSprite = testPlsWorkMore[4];
                    normalSprite = testPlsWorkMore[5];
                    redButton = Flash(redButton);
                    if (currentTime <= 2)
                    {
                        redButton = FlashBack(redButton);
                    }
                }
                if (currentTime < 2)
                {

                    normalFlashSprite = testPlsWorkMore[0];
                    normalSprite = testPlsWorkMore[1];
                    blueButton = Flash(blueButton);
                    if (currentTime <= 1)
                    {
                        blueButton = FlashBack(blueButton);
                        repeat = true;
                        currentTime = 8f;
                    }
                }
            }                    
        }
    }

    void OBGR()
    {
        bool repeat = false;
        if (!repeat)
        {
            if(currentTime < 5)
            {
                normalFlashSprite = testPlsWorkMore[6];
                normalSprite = testPlsWorkMore[7];
                orangeButton = Flash(orangeButton);
                if (currentTime <= 4)
                {
                    orangeButton = FlashBack(orangeButton);
                }
                if (currentTime < 4)
                {
                    normalFlashSprite = testPlsWorkMore[0];
                    normalSprite = testPlsWorkMore[1];
                    blueButton = Flash(blueButton);
                    if (currentTime <= 3)
                    {
                        blueButton = FlashBack(blueButton);
                    }
                }
                if (currentTime < 3)
                {
                    normalFlashSprite = testPlsWorkMore[2];
                    normalSprite = testPlsWorkMore[3];
                    greenButton = Flash(greenButton);
                    if (currentTime <= 2)
                    {
                        greenButton = FlashBack(greenButton);
                    }
                }
                if (currentTime < 2)
                {
                    normalFlashSprite = testPlsWorkMore[4];
                    normalSprite = testPlsWorkMore[5];
                    redButton = Flash(redButton);
                    if (currentTime <= 1)
                    {
                        redButton = FlashBack(redButton);
                        repeat = true;
                        currentTime = 8f;
                    }
                }
            }                      
        }
    }

    void OROR()
    {
        bool repeat = false;
        if (!repeat)
        {
            if(currentTime < 5)
            {
                normalFlashSprite = testPlsWorkMore[6];
                normalSprite = testPlsWorkMore[7];
                orangeButton = Flash(orangeButton);
                if (currentTime <= 4)
                {
                    orangeButton = FlashBack(orangeButton);
                }
                if (currentTime < 4)
                {
                    normalFlashSprite = testPlsWorkMore[4];
                    normalSprite = testPlsWorkMore[5];
                    redButton = Flash(redButton);
                    if (currentTime <= 3)
                    {
                        redButton = FlashBack(redButton);
                    }
                }
                if (currentTime < 3)
                {
                    normalFlashSprite = testPlsWorkMore[6];
                    normalSprite = testPlsWorkMore[7];
                    orangeButton = Flash(orangeButton);
                    if (currentTime <= 2)
                    {
                        orangeButton = FlashBack(orangeButton);
                    }
                }
                if (currentTime < 2)
                {
                    normalFlashSprite = testPlsWorkMore[4];
                    normalSprite = testPlsWorkMore[5];
                    redButton = Flash(redButton);
                    if (currentTime <= 1)
                    {
                        redButton = FlashBack(redButton);
                        repeat = true;
                        currentTime = 8f;
                    }
                }
            }
        }
    }

    void OOGG()
    {
        bool repeat = false;
        if (!repeat)
        {
            if(currentTime < 5)
            {
                normalFlashSprite = testPlsWorkMore[6];
                normalSprite = testPlsWorkMore[7];
                orangeButton = Flash(orangeButton);
                if (currentTime <= 4.5)
                {
                    orangeButton = FlashBack(orangeButton);
                    if (currentTime <= 4)
                    {
                        orangeButton = Flash(orangeButton);
                        if (currentTime <= 3.5)
                        {
                            orangeButton = FlashBack(orangeButton);
                        }
                    }
                }
                if (currentTime < 3)
                {
                    normalFlashSprite = testPlsWorkMore[2];
                    normalSprite = testPlsWorkMore[3];
                    greenButton = Flash(greenButton);
                    if (currentTime <= 2.5)
                    {
                        greenButton = FlashBack(greenButton);
                        if (currentTime <= 2)
                        {
                            greenButton = Flash(greenButton);
                            if (currentTime <= 1.5)
                            {
                                greenButton = FlashBack(greenButton);
                                repeat = true;
                                currentTime = 8f;
                            }
                        }
                    }
                }
            }         
        }
    }

    void GRBOGR()
    {
        bool repeat = false;
        if (!repeat)
        {
            if(currentTime < 5)
            {
                normalFlashSprite = testPlsWorkMore[2];
                normalSprite = testPlsWorkMore[3];

                greenButton = Flash(greenButton);
                if (currentTime <= 4)
                {
                    greenButton = FlashBack(greenButton);
                }
                if (currentTime < 4)
                {
                    normalFlashSprite = testPlsWorkMore[4];
                    normalSprite = testPlsWorkMore[5];
                    redButton = Flash(redButton);
                    if (currentTime <= 3)
                    {
                        redButton = FlashBack(redButton);
                    }
                }
                if (currentTime < 3)
                {
                    normalFlashSprite = testPlsWorkMore[0];
                    normalSprite = testPlsWorkMore[1];
                    blueButton = Flash(blueButton);
                    if (currentTime <= 2)
                    {
                        blueButton = FlashBack(blueButton);
                    }
                }
                if (currentTime < 2)
                {
                    normalFlashSprite = testPlsWorkMore[6];
                    normalSprite = testPlsWorkMore[7];
                    orangeButton = Flash(orangeButton);
                    if (currentTime <= 1)
                    {
                        orangeButton = FlashBack(orangeButton);
                    }
                }
                if (currentTime < 1)
                {
                    normalFlashSprite = testPlsWorkMore[2];
                    normalSprite = testPlsWorkMore[3];
                    greenButton = Flash(greenButton);
                    if (currentTime <= 0)
                    {
                        greenButton = FlashBack(greenButton);
                    }
                }
                if (currentTime < 0)
                {
                    normalFlashSprite = testPlsWorkMore[4];
                    normalSprite = testPlsWorkMore[5];
                    redButton = Flash(redButton);
                    if (currentTime <= -1)
                    {
                        redButton = FlashBack(redButton);
                        repeat = true;
                        currentTime = 8f;
                    }
                }
            }          
        }
    }

    void BGGR()
    {
        bool repeat = false;
        if (!repeat)
        {
            if(currentTime < 5)
            {
                normalFlashSprite = testPlsWorkMore[0];
                normalSprite = testPlsWorkMore[1];
                blueButton = Flash(blueButton);
                if (currentTime <= 4)
                {
                    blueButton = FlashBack(blueButton);
                }
                if (currentTime < 4)
                {
                    normalFlashSprite = testPlsWorkMore[2];
                    normalSprite = testPlsWorkMore[3];
                    greenButton = Flash(greenButton);
                    if (currentTime <= 3.5)
                    {
                        greenButton = FlashBack(greenButton);
                        if (currentTime <= 3)
                        {
                            greenButton = Flash(greenButton);
                            if (currentTime <= 2.5)
                            {
                                greenButton = FlashBack(greenButton);
                            }
                        }
                    }
                }
                if (currentTime < 2)
                {
                    normalFlashSprite = testPlsWorkMore[4];
                    normalSprite = testPlsWorkMore[5];
                    redButton = Flash(redButton);
                    if (currentTime <= 1)
                    {
                        redButton = FlashBack(redButton);
                        repeat = true;
                        currentTime = 8f;
                    }
                }
            }           
        }
    }
    #endregion

    #region Colour Flash 2 Start

    void ROBG()
    {
        bool repeat = false;
        if (!repeat)
        {
            if (currentTime < 5)
            {
                normalFlashSprite = testPlsWorkMore[4];
                normalSprite = testPlsWorkMore[5];
                redButton = Flash(redButton);
                if (currentTime <= 4)
                {
                    redButton = FlashBack(redButton);
                }
            }
            if (currentTime < 4)
            {
                normalFlashSprite = testPlsWorkMore[6];
                normalSprite = testPlsWorkMore[7];
                orangeButton = Flash(orangeButton);
                if (currentTime <= 3)
                {
                    orangeButton = FlashBack(orangeButton);
                }
            }
            if (currentTime < 3)
            {
                normalFlashSprite = testPlsWorkMore[0];
                normalSprite = testPlsWorkMore[1];
                blueButton = Flash(blueButton);
                if (currentTime <= 2)
                {
                    blueButton = FlashBack(blueButton);
                }
            }
            if (currentTime < 2)
            {
                normalFlashSprite = testPlsWorkMore[2];
                normalSprite = testPlsWorkMore[3];
                greenButton = Flash(greenButton);
                if (currentTime <= 1)
                {
                    greenButton = FlashBack(greenButton);
                    repeat = true;
                    currentTime = 8f;
                }
            }
        }
    }

    void OROB()
    {
        bool repeat = false;
        if (!repeat)
        {
            if (currentTime < 5)
            {
                normalFlashSprite = testPlsWorkMore[6];
                normalSprite = testPlsWorkMore[7];
                orangeButton = Flash(orangeButton);
                if (currentTime <= 4)
                {
                    orangeButton = FlashBack(orangeButton);
                }
            }
            if (currentTime < 4)
            {
                normalFlashSprite = testPlsWorkMore[4];
                normalSprite = testPlsWorkMore[5];
                redButton = Flash(redButton);
                if (currentTime <= 3)
                {
                    redButton = FlashBack(redButton);
                }
            }
            if (currentTime < 3)
            {
                normalFlashSprite = testPlsWorkMore[6];
                normalSprite = testPlsWorkMore[7];
                orangeButton = Flash(orangeButton);
                if (currentTime <= 2)
                {
                    orangeButton = FlashBack(orangeButton);
                }
            }
            if (currentTime < 2)
            {
                normalFlashSprite = testPlsWorkMore[0];
                normalSprite = testPlsWorkMore[1];
                blueButton = Flash(blueButton);
                if (currentTime <= 1)
                {
                    blueButton = FlashBack(blueButton);
                    repeat = true;
                    currentTime = 8f;
                }
            }
        }
    }

    void OOOO()
    {
        bool repeat = false;
        if (!repeat)
        {
            if (currentTime < 5)
            {
                normalFlashSprite = testPlsWorkMore[6];
                normalSprite = testPlsWorkMore[7];
                orangeButton = Flash(orangeButton);
                if (currentTime <= 4.5)
                {
                    orangeButton = FlashBack(orangeButton);
                }
            }
            if (currentTime < 4)
            {
                normalFlashSprite = testPlsWorkMore[6];
                normalSprite = testPlsWorkMore[7];
                orangeButton = Flash(orangeButton);
                if (currentTime <= 3.5)
                {
                    orangeButton = FlashBack(orangeButton);
                }
            }
            if (currentTime < 3)
            {
                normalFlashSprite = testPlsWorkMore[6];
                normalSprite = testPlsWorkMore[7];
                orangeButton = Flash(orangeButton);
                if (currentTime <= 2.5)
                {
                    orangeButton = FlashBack(orangeButton);
                }
            }
            if (currentTime < 2)
            {
                normalFlashSprite = testPlsWorkMore[6];
                normalSprite = testPlsWorkMore[7];
                orangeButton = Flash(orangeButton);
                if (currentTime <= 1.5)
                {
                    orangeButton = FlashBack(orangeButton);
                    repeat = true;
                    currentTime = 8f;
                }
            }
        }
    }

    void RBGR()
    {
        bool repeat = false;
        if (!repeat)
        {
            if (currentTime < 5)
            {
                normalFlashSprite = testPlsWorkMore[4];
                normalSprite = testPlsWorkMore[5];
                redButton = Flash(redButton);
                if (currentTime <= 4)
                {
                    redButton = FlashBack(redButton);
                }
            }
            if (currentTime < 4)
            {
                normalFlashSprite = testPlsWorkMore[0];
                normalSprite = testPlsWorkMore[1];
                blueButton = Flash(blueButton);
                if (currentTime <= 3)
                {
                    blueButton = FlashBack(blueButton);
                }
            }
            if (currentTime < 3)
            {
                normalFlashSprite = testPlsWorkMore[2];
                normalSprite = testPlsWorkMore[3];
                greenButton = Flash(greenButton);
                if (currentTime <= 2)
                {
                    greenButton = FlashBack(greenButton);
                }
            }
            if (currentTime < 2)
            {
                normalFlashSprite = testPlsWorkMore[4];
                normalSprite = testPlsWorkMore[5];
                redButton = Flash(redButton);
                if (currentTime <= 1)
                {
                    redButton = FlashBack(redButton);
                    repeat = true;
                    currentTime = 8f;
                }
            }
        }
    }

    void RRBGR()
    {
        bool repeat = false;
        if (!repeat)
        {
            if (currentTime < 5)
            {
                normalFlashSprite = testPlsWorkMore[4];
                normalSprite = testPlsWorkMore[5];
                redButton = Flash(redButton);
                if (currentTime <= 4.5)
                {
                    redButton = FlashBack(redButton);
                }
            }
            if (currentTime < 4)
            {
                normalFlashSprite = testPlsWorkMore[4];
                normalSprite = testPlsWorkMore[5];
                redButton = Flash(redButton);
                if (currentTime <= 3.5)
                {
                    redButton = FlashBack(redButton);
                }
            }
            if (currentTime < 3)
            {
                normalFlashSprite = testPlsWorkMore[0];
                normalSprite = testPlsWorkMore[1];
                blueButton = Flash(blueButton);
                if (currentTime <= 2)
                {
                    blueButton = FlashBack(blueButton);
                }
            }
            if (currentTime < 2)
            {
                normalFlashSprite = testPlsWorkMore[2];
                normalSprite = testPlsWorkMore[3];
                greenButton = Flash(greenButton);
                if (currentTime <= 1)
                {
                    greenButton = FlashBack(greenButton);
                }
            }
            if (currentTime < 1)
            {
                normalFlashSprite = testPlsWorkMore[4];
                normalSprite = testPlsWorkMore[5];
                redButton = Flash(redButton);
                if (currentTime <= 0)
                {
                    redButton = FlashBack(redButton);
                    repeat = true;
                    currentTime = 8f;
                }
            }
        }
    }

    void BOG()
    {
        bool repeat = false;
        if (!repeat)
        {
            if (currentTime < 5)
            {
                normalFlashSprite = testPlsWorkMore[0];
                normalSprite = testPlsWorkMore[1];
                blueButton = Flash(blueButton);
                if (currentTime <= 4)
                {
                    blueButton = FlashBack(blueButton);
                }
            }
            if (currentTime < 4)
            {
                normalFlashSprite = testPlsWorkMore[6];
                normalSprite = testPlsWorkMore[7];
                orangeButton = Flash(orangeButton);
                if (currentTime <= 3)
                {
                    orangeButton = FlashBack(orangeButton);
                }
            }
            if (currentTime < 3)
            {
                normalFlashSprite = testPlsWorkMore[2];
                normalSprite = testPlsWorkMore[3];
                greenButton = Flash(greenButton);
                if (currentTime <= 2)
                {
                    greenButton = FlashBack(greenButton);
                    repeat = true;
                    currentTime = 8f;
                }
            }
        }
    }

    void RRRB()
    {
        bool repeat = false;
        if (!repeat)
        {
            if (currentTime < 5)
            {
                normalFlashSprite = testPlsWorkMore[4];
                normalSprite = testPlsWorkMore[5];
                redButton = Flash(redButton);
                if (currentTime <= 4.5)
                {
                    redButton = FlashBack(redButton);
                }
            }
            if (currentTime < 4)
            {
                normalFlashSprite = testPlsWorkMore[4];
                normalSprite = testPlsWorkMore[5];
                redButton = Flash(redButton);
                if (currentTime <= 3.5)
                {
                    redButton = FlashBack(redButton);
                }
            }
            if (currentTime < 3)
            {
                normalFlashSprite = testPlsWorkMore[4];
                normalSprite = testPlsWorkMore[5];
                redButton = Flash(redButton);
                if (currentTime <= 2.5)
                {
                    redButton = FlashBack(redButton);
                }
            }
            if (currentTime < 2)
            {
                normalFlashSprite = testPlsWorkMore[0];
                normalSprite = testPlsWorkMore[1];
                blueButton = Flash(blueButton);
                if (currentTime <= 1)
                {
                    blueButton = FlashBack(blueButton);
                    repeat = true;
                    currentTime = 8f;
                }
            }
        }
    }

    void GGGGBR()
    {
        bool repeat = false;
        if (!repeat)
        {
            if (currentTime < 5)
            {
                normalFlashSprite = testPlsWorkMore[2];
                normalSprite = testPlsWorkMore[3];
                greenButton = Flash(greenButton);
                if (currentTime <= 4.5)
                {
                    greenButton = FlashBack(greenButton);
                }
            }
            if (currentTime < 4)
            {
                normalFlashSprite = testPlsWorkMore[2];
                normalSprite = testPlsWorkMore[3];
                greenButton = Flash(greenButton);
                if (currentTime <= 3.5)
                {
                    greenButton = FlashBack(greenButton);
                }
            }
            if (currentTime < 3)
            {
                normalFlashSprite = testPlsWorkMore[2];
                normalSprite = testPlsWorkMore[3];
                greenButton = Flash(greenButton);
                if (currentTime <= 2.5)
                {
                    greenButton = FlashBack(greenButton);
                }
            }
            if (currentTime < 2)
            {
                normalFlashSprite = testPlsWorkMore[2];
                normalSprite = testPlsWorkMore[3];
                greenButton = Flash(greenButton);
                if (currentTime <= 1.5)
                {
                    greenButton = FlashBack(greenButton);
                }
            }
            if (currentTime < 1)
            {
                normalFlashSprite = testPlsWorkMore[0];
                normalSprite = testPlsWorkMore[1];
                blueButton = Flash(blueButton);
                if (currentTime <= 0)
                {
                    blueButton = FlashBack(blueButton);
                }
            }
            if (currentTime < -1)
            {
                normalFlashSprite = testPlsWorkMore[4];
                normalSprite = testPlsWorkMore[5];
                redButton = Flash(redButton);
                if (currentTime <= -2)
                {
                    redButton = FlashBack(redButton);
                    repeat = true;
                    currentTime = 8f;
                }
            }
        }
    }
    #endregion

    #region Colour Flash 3 Start
    void GRBGRR()
    {
        bool repeat = false;
        if (!repeat)
        {
            if (currentTime < 5)
            {
                normalFlashSprite = testPlsWorkMore[2];
                normalSprite = testPlsWorkMore[3];
                greenButton = Flash(greenButton);
                if (currentTime <= 4)
                {
                    greenButton = FlashBack(greenButton);
                }
            }
            if (currentTime < 4)
            {
                normalFlashSprite = testPlsWorkMore[4];
                normalSprite = testPlsWorkMore[5];
                redButton = Flash(redButton);
                if (currentTime <= 3)
                {
                    redButton = FlashBack(redButton);
                }
            }
            if (currentTime < 3)
            {
                normalFlashSprite = testPlsWorkMore[0];
                normalSprite = testPlsWorkMore[1];
                blueButton = Flash(blueButton);
                if (currentTime <= 2)
                {
                    blueButton = FlashBack(blueButton);
                }
            }
            if (currentTime < 2)
            {
                normalFlashSprite = testPlsWorkMore[2];
                normalSprite = testPlsWorkMore[3];
                greenButton = Flash(greenButton);
                if (currentTime <= 1)
                {
                    greenButton = FlashBack(greenButton);
                }
            }
            if (currentTime < 1)
            {
                normalFlashSprite = testPlsWorkMore[4];
                normalSprite = testPlsWorkMore[5];
                redButton = Flash(redButton);
                if (currentTime <= 0.5)
                {
                    redButton = FlashBack(redButton);
                }
            }
            if (currentTime < 0)
            {
                normalFlashSprite = testPlsWorkMore[4];
                normalSprite = testPlsWorkMore[5];
                redButton = Flash(redButton);
                if (currentTime <= -0.5)
                {
                    redButton = FlashBack(redButton);
                    repeat = true;
                    currentTime = 8f;
                }
            }
        }
    }

    void BGRGOR()
    {
        bool repeat = false;
        if (!repeat)
        {
            if (currentTime < 5)
            {
                normalFlashSprite = testPlsWorkMore[0];
                normalSprite = testPlsWorkMore[1];
                blueButton = Flash(blueButton);
                if (currentTime <= 4)
                {
                    blueButton = FlashBack(blueButton);
                }
            }
            if (currentTime < 4)
            {
                normalFlashSprite = testPlsWorkMore[2];
                normalSprite = testPlsWorkMore[3];
                greenButton = Flash(greenButton);
                if (currentTime <= 3)
                {
                    greenButton = FlashBack(greenButton);
                }
            }
            if (currentTime < 3)
            {
                normalFlashSprite = testPlsWorkMore[4];
                normalSprite = testPlsWorkMore[5];
                redButton = Flash(redButton);
                if (currentTime <= 2)
                {
                    redButton = FlashBack(redButton);
                }
            }
            if (currentTime < 2)
            {
                normalFlashSprite = testPlsWorkMore[2];
                normalSprite = testPlsWorkMore[3];
                greenButton = Flash(greenButton);
                if (currentTime <= 1)
                {
                    greenButton = FlashBack(greenButton);
                }
            }
            if (currentTime < 1)
            {
                normalFlashSprite = testPlsWorkMore[6];
                normalSprite = testPlsWorkMore[7];
                orangeButton = Flash(orangeButton);
                if (currentTime <= 0)
                {
                    orangeButton = FlashBack(orangeButton);
                }
            }
            if (currentTime < 0)
            {
                normalFlashSprite = testPlsWorkMore[4];
                normalSprite = testPlsWorkMore[5];
                redButton = Flash(redButton);
                if (currentTime <= -1)
                {
                    redButton = FlashBack(redButton);
                    repeat = true;
                    currentTime = 8f;
                }
            }
        }
    }

    void BBB()
    {
        bool repeat = false;
        if (!repeat)
        {
            if (currentTime < 5)
            {
                normalFlashSprite = testPlsWorkMore[0];
                normalSprite = testPlsWorkMore[1];
                blueButton = Flash(blueButton);
                if (currentTime <= 4.5)
                {
                    blueButton = FlashBack(blueButton);
                }
            }
            if (currentTime < 4)
            {
                normalFlashSprite = testPlsWorkMore[0];
                normalSprite = testPlsWorkMore[1];
                blueButton = Flash(blueButton);
                if (currentTime <= 3.5)
                {
                    blueButton = FlashBack(blueButton);
                }
            }
            if (currentTime < 3)
            {
                normalFlashSprite = testPlsWorkMore[0];
                normalSprite = testPlsWorkMore[1];
                blueButton = Flash(blueButton);
                if (currentTime <= 2.5)
                {
                    blueButton = FlashBack(blueButton);
                    repeat = true;
                    currentTime = 8f;
                }
            }
        }
    }

    void GRGB()
    {
        bool repeat = false;
        if (!repeat)
        {
            if (currentTime < 5)
            {
                normalFlashSprite = testPlsWorkMore[2];
                normalSprite = testPlsWorkMore[3];
                greenButton = Flash(greenButton);
                if (currentTime <= 4)
                {
                    greenButton = FlashBack(greenButton);
                }
            }
            if (currentTime < 4)
            {
                normalFlashSprite = testPlsWorkMore[4];
                normalSprite = testPlsWorkMore[5];
                redButton = Flash(redButton);
                if (currentTime <= 3)
                {
                    redButton = FlashBack(redButton);
                }
            }
            if (currentTime < 3)
            {
                normalFlashSprite = testPlsWorkMore[2];
                normalSprite = testPlsWorkMore[3];
                greenButton = Flash(greenButton);
                if (currentTime <= 2)
                {
                    greenButton = FlashBack(greenButton);
                }
            }
            if (currentTime < 2)
            {
                normalFlashSprite = testPlsWorkMore[0];
                normalSprite = testPlsWorkMore[1];
                blueButton = Flash(blueButton);
                if (currentTime <= 1)
                {
                    blueButton = FlashBack(blueButton);
                    repeat = true;
                    currentTime = 8f;
                }
            }
        }
    }

    void OBOR()
    {
        bool repeat = false;
        if (!repeat)
        {
            if (currentTime < 5)
            {
                normalFlashSprite = testPlsWorkMore[6];
                normalSprite = testPlsWorkMore[7];
                orangeButton = Flash(orangeButton);
                if (currentTime <= 4)
                {
                    orangeButton = FlashBack(orangeButton);
                }
            }
            if (currentTime < 4)
            {
                normalFlashSprite = testPlsWorkMore[0];
                normalSprite = testPlsWorkMore[1];
                blueButton = Flash(blueButton);
                if (currentTime <= 3)
                {
                    blueButton = FlashBack(blueButton);
                }
            }
            if (currentTime < 3)
            {
                normalFlashSprite = testPlsWorkMore[6];
                normalSprite = testPlsWorkMore[7];
                orangeButton = Flash(orangeButton);
                if (currentTime <= 2)
                {
                    orangeButton = FlashBack(orangeButton);
                }
            }
            if (currentTime < 2)
            {
                normalFlashSprite = testPlsWorkMore[4];
                normalSprite = testPlsWorkMore[5];
                redButton = Flash(redButton);
                if (currentTime <= 1)
                {
                    redButton = FlashBack(redButton);
                    repeat = true;
                    currentTime = 8f;
                }
            }
        }
    }

    void ORRB()
    {
        bool repeat = false;
        if (!repeat)
        {
            if (currentTime < 5)
            {
                normalFlashSprite = testPlsWorkMore[6];
                normalSprite = testPlsWorkMore[7];
                orangeButton = Flash(orangeButton);
                if (currentTime <= 4)
                {
                    orangeButton = FlashBack(orangeButton);
                }
            }
            if (currentTime < 4)
            {
                normalFlashSprite = testPlsWorkMore[4];
                normalSprite = testPlsWorkMore[5];
                redButton = Flash(redButton);
                if (currentTime <= 3.5)
                {
                    redButton = FlashBack(redButton);
                }
            }
            if (currentTime < 3)
            {
                normalFlashSprite = testPlsWorkMore[4];
                normalSprite = testPlsWorkMore[5];
                redButton = Flash(redButton);
                if (currentTime <= 2.5)
                {
                    redButton = FlashBack(redButton);
                }
            }
            if (currentTime < 2)
            {
                normalFlashSprite = testPlsWorkMore[0];
                normalSprite = testPlsWorkMore[1];
                blueButton = Flash(blueButton);
                if (currentTime <= 1)
                {
                    blueButton = FlashBack(blueButton);
                    repeat = true;
                    currentTime = 8f;
                }
            }
        }
    }

    void GGGG()
    {
        bool repeat = false;
        if (!repeat)
        {
            if (currentTime < 5)
            {
                normalFlashSprite = testPlsWorkMore[2];
                normalSprite = testPlsWorkMore[3];
                greenButton = Flash(greenButton);
                if (currentTime <= 4.5)
                {
                    greenButton = FlashBack(greenButton);
                }
            }
            if (currentTime < 4)
            {
                normalFlashSprite = testPlsWorkMore[2];
                normalSprite = testPlsWorkMore[3];
                greenButton = Flash(greenButton);
                if (currentTime <= 3.5)
                {
                    greenButton = FlashBack(greenButton);
                }
            }
            if (currentTime < 3)
            {
                normalFlashSprite = testPlsWorkMore[2];
                normalSprite = testPlsWorkMore[3];
                greenButton = Flash(greenButton);
                if (currentTime <= 2.5)
                {
                    greenButton = FlashBack(greenButton);
                }
            }
            if (currentTime < 2)
            {
                normalFlashSprite = testPlsWorkMore[2];
                normalSprite = testPlsWorkMore[3];
                greenButton = Flash(greenButton);
                if (currentTime <= 1.5)
                {
                    greenButton = FlashBack(greenButton);
                    repeat = true;
                    currentTime = 8f;
                }
            }
        }
    }

    void RBGO()
    {
        bool repeat = false;
        if (!repeat)
        {
            if (currentTime < 5)
            {
                normalFlashSprite = testPlsWorkMore[4];
                normalSprite = testPlsWorkMore[5];
                redButton = Flash(redButton);
                if (currentTime <= 4)
                {
                    redButton = FlashBack(redButton);
                }
            }
            if (currentTime < 4)
            {
                normalFlashSprite = testPlsWorkMore[0];
                normalSprite = testPlsWorkMore[1];
                blueButton = Flash(blueButton);
                if (currentTime <= 3)
                {
                    blueButton = FlashBack(blueButton);
                }
            }
            if (currentTime < 3)
            {
                normalFlashSprite = testPlsWorkMore[2];
                normalSprite = testPlsWorkMore[3];
                greenButton = Flash(greenButton);
                if (currentTime <= 2)
                {
                    greenButton = FlashBack(greenButton);
                }
            }
            if (currentTime < 2)
            {
                normalFlashSprite = testPlsWorkMore[6];
                normalSprite = testPlsWorkMore[7];
                orangeButton = Flash(orangeButton);
                if (currentTime <= 1)
                {
                    orangeButton = FlashBack(orangeButton);
                    repeat = true;
                    currentTime = 8f;
                }
            }
        }
    }
    #endregion

    public void ClearWires()
    {
        redWire.transform.localPosition = new Vector3(6f, -10.7f, 0);
        wireDraScript[0].plugged = false;
        wireDraScript[0].counterPlugged = false;
        wireDraScript[0].audioPlayed = false;
        correctWireRed = false;

        greenWire.transform.localPosition = new Vector3(6f, -10.7f, 0);
        wireDraScript[1].plugged = false;
        wireDraScript[1].counterPlugged = false;
        wireDraScript[1].audioPlayed = false;
        correctWireBlue = false;

        blueWire.transform.localPosition = new Vector3(8f, -25.7f, 0);
        wireDraScript[2].plugged = false;
        wireDraScript[2].counterPlugged = false;
        wireDraScript[2].audioPlayed = false;
        correctWireGreen = false;

        yellowWire.transform.localPosition = new Vector3(8f, -33.7f, 0);
        wireDraScript[3].plugged = false;
        wireDraScript[3].counterPlugged = false;
        wireDraScript[3].audioPlayed = false;
        correctWireYellow = false;
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

    IEnumerator StageFlash()
    {
        if (!stageFlashOnce)
        {
            stageFlash.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            stageFlash.SetActive(false);
            stageFlashOnce = true;
        }
    }
}
