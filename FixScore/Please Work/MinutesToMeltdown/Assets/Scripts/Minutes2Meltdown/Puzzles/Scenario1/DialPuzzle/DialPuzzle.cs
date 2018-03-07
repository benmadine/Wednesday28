using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialPuzzle : MonoBehaviour
{
    //Lives
    [HideInInspector]
    public int LivesCounter = 0;
    Image livesImage;
    public Sprite[] possibleImages;

    //Buttons
    GameObject button1, button2, button3, button4;
    Text Button1Text, button2Text, button3Text, button4Text;

    SpriteRenderer button1Image, button2Image, button3Image, button4Image;

    //Combinations
    [HideInInspector]
    public enum Combinations { WXYZ, WYZX, YWZX, ZWYX, WYXZ, WZYX, XYWZ, YZWX, YXWZ, ZYWX, ZYXW, XZWY, YZXW, WZXY, ZWXY, ZXYW, YXZW, WXZY, XYZW, NUM26, NUM28, NUM30}
    [HideInInspector]
    public Combinations pickedCombination;
    [HideInInspector]
    public int buttonCount = 0;

    //Combination Images
    Sprite[] combinationImages;

    //Getting Scripts
    GameManagerScript gameManagerScript;
    DialArrow dialArrowScript;

    //Button Checks
    [HideInInspector]
    public bool buttonPass, dialPass;
    [HideInInspector]
    public bool button1Pressed = false, button2Pressed = false, button3Pressed = false, button4Pressed = false;
    bool oneHasBeen, twoHasBeen, threeHasBeen, fourHasBeen;

    //Win loose text
    SpriteRenderer correctAns, wrongAns;

    //Audio Stuff
    AudioSource aSource;
    [HideInInspector]
    public AudioClip wonPuzzle, lostPuzzle, wrongButton, correctButton;
    bool audioPlayed;

    //Stages
    public bool stage1Buttons, stage2Buttons, stage3Buttons, stage4Buttons;

    //CorrectOrder
    bool button1Order, button2Order, button3Order, button4Order;

    //ButtonCounter
    [HideInInspector]
    public int buttonCounter = 0;

    //Run each part once VEYR MESSY
    bool onlyOnce = false, onlyOnce2 = false, onlyOnce3 = false, onlyOnce4 = false;
    bool StageOnce1 = false, StageOnce2 = false, StageOnce3 = false;

    //Array Combinations
    int[] stage1Array, stage2Array, stage3Array, stage4Array;

    //bOOL FOR lerp
    bool lerpOnceLerp;

    //Alarm Flahs
    [HideInInspector]
    public GameObject alarmFlash;
    bool flashOnce;

    //Stage Flash
    [HideInInspector]
    public GameObject stageFlash;
    [HideInInspector]
    public bool stageFlashOnce;
    bool doonce;

    //Tutorial stuff
    public GameObject tutorialPannel;

    // Use this for initialization
    void Start ()
    {
        stage1Buttons = true;
        Finding();
        SortImageCombination();
        pickedCombination = (Combinations)Random.Range(0, 30);
        print(pickedCombination);
        doonce = true;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (gameManagerScript.tutorial)
        {
            tutorialPannel.SetActive(true);
        }
        UpdateLives();
        RandomCombinations();    
        if(LivesCounter >=3 )
        {
            if(!audioPlayed)
            {
                aSource.PlayOneShot(lostPuzzle, 0.5f);
                audioPlayed = true;
            }           
            wrongAns.enabled = true;
            StartCoroutine(WaitToLerp());
        }

        if (stage2Buttons == true && StageOnce1 == false && dialArrowScript.stage2)
        {
            stageFlashOnce = false;
            StartCoroutine(StageFlash());
            pickedCombination = (Combinations)Random.Range(0, 30);
            StageOnce1 = true;
        }

        if (stage3Buttons == true && StageOnce2 == false && dialArrowScript.stage3)
        {
            stageFlashOnce = false;
            StartCoroutine(StageFlash());
            pickedCombination = (Combinations)Random.Range(0, 30);
            StageOnce2 = true;
        }
    }

    void SortImageCombination()
    {
        combinationImages = Resources.LoadAll<Sprite>("CombinationImages");
    }

    void UpdateLives()
    {
        if (LivesCounter == 1)
        {
            livesImage.sprite = possibleImages[1];
            StartCoroutine(AlarmFlash());
        }

        if (LivesCounter == 2)
        {
            livesImage.sprite = possibleImages[2];
            StartCoroutine(AlarmFlash());
        }

        if (LivesCounter == 3)
        {
            livesImage.sprite = possibleImages[3];
            StartCoroutine(AlarmFlash());
            if (doonce == true)
            {
                gameManagerScript.counterToLerp++;
                doonce = false;
            }
        }
    }

    void Finding()
    {
        button1Image = GameObject.Find("Button1ImageSpot").GetComponent<SpriteRenderer>();
        button2Image = GameObject.Find("Button2ImageSpot").GetComponent<SpriteRenderer>();
        button3Image = GameObject.Find("Button3ImageSpot").GetComponent<SpriteRenderer>();
        button4Image = GameObject.Find("Button4ImageSpot").GetComponent<SpriteRenderer>();

        aSource = GameObject.FindGameObjectWithTag("DialPuzzle").GetComponent<AudioSource>();
        button1 = GameObject.FindGameObjectWithTag("Button1");
        button2 = GameObject.FindGameObjectWithTag("Button2");
        button3 = GameObject.FindGameObjectWithTag("Button3");
        button4 = GameObject.FindGameObjectWithTag("Button4");

        correctAns = GameObject.FindGameObjectWithTag("Passed").GetComponent<SpriteRenderer>();
        wrongAns = GameObject.FindGameObjectWithTag("Lost").GetComponent<SpriteRenderer>();

        gameManagerScript = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManagerScript>();
        dialArrowScript = GameObject.FindGameObjectWithTag("Arrow").GetComponent<DialArrow>();
        livesImage = GameObject.Find("Circle").GetComponent<Image>();
    }

    void RandomCombinations()
    {
        switch(pickedCombination)
        {
            case Combinations.WXYZ:
                WXYZ();
                if (stage1Buttons)
                    if (button1Pressed)
                        if (button2Pressed)
                            if (button3Pressed)
                                stage2Buttons = true;
                if (stage2Buttons && dialArrowScript.stage2)
                {
                    if(!onlyOnce)
                    {
                        button1Pressed = false; button2Pressed = false; button3Pressed = false; button4Pressed = false;
                        onlyOnce = true;
                    }
                    stage1Buttons = false;
                    if (button1Pressed)
                        if(buttonCounter >= 2)
                            if (button3Pressed)
                                stage3Buttons = true;
                }
                if (stage3Buttons && dialArrowScript.stage3)
                {
                    if (!onlyOnce2)
                    {
                        button1Pressed = false; button2Pressed = false; button3Pressed = false; button4Pressed = false;
                        onlyOnce2 = true;

                    }
                    stage2Buttons = false;
                    if (button1Pressed)
                        if (button2Pressed)
                            if (button3Pressed)
                                if (button4Pressed)
                                    buttonPass = true;
                }
                break;
            case Combinations.WYZX:
                WYZX();
                if (stage1Buttons) //The stage on
                    if (button4Pressed)
                        if (button2Pressed)
                            if (button3Pressed)
                                stage2Buttons = true;
                if (stage2Buttons && dialArrowScript.stage2)
                {
                    if (!onlyOnce)
                    {
                        button1Pressed = false; button2Pressed = false; button3Pressed = false; button4Pressed = false;
                        onlyOnce = true;
                    }
                    stage1Buttons = false;
                    if (button2Pressed)
                        if (buttonCounter >= 2)
                            if (button1Pressed)
                                stage3Buttons = true;
                }
                if (stage3Buttons && dialArrowScript.stage3)
                {
                    if (!onlyOnce2)
                    {
                        button1Pressed = false; button2Pressed = false; button3Pressed = false; button4Pressed = false;
                        onlyOnce2 = true;
                    }
                    stage2Buttons = false;
                    if (button1Pressed)
                        if (button2Pressed)
                            if (button3Pressed)
                                if (button4Pressed)
                                    buttonPass = true;
                }
                break;
            //case Combinations.XWYZ:
            //    XWYZ();
            //    if (stage1Buttons) //Stage1
            //        if (button1Pressed)
            //            if (button4Pressed)
            //                if (button3Pressed)
            //                    stage2Buttons = true;
            //    if (stage2Buttons && dialArrowScript.stage2) //Stage2
            //    {
            //        if (!onlyOnce)
            //        {
            //            button1Pressed = false; button2Pressed = false; button3Pressed = false; button4Pressed = false;
            //            onlyOnce = true;
            //            buttonCounter = 0;
            //        }
            //        stage1Buttons = false;
            //        if (button3Pressed)                         //Requirementas
            //            if (buttonCounter >= 2)
            //                if (button1Pressed)
            //                {
            //                    stage3Buttons = true;
            //                }                             
            //    }
            //    if (stage3Buttons && dialArrowScript.stage3) //Stage 3
            //    {
            //        if (!onlyOnce2)
            //        {
            //            button1Pressed = false; button2Pressed = false; button3Pressed = false; button4Pressed = false;
            //            onlyOnce2 = true;
            //            buttonCounter = 0;
            //        }
            //        stage2Buttons = false;
            //        if (button3Pressed)
            //            if (button2Pressed)                     // Requiremtns
            //                if (button1Pressed)
            //                    if (button4Pressed)
            //                        buttonPass = true;
            //    }
            //    break;
            case Combinations.YWZX:
                YWZX();
                if (stage1Buttons) //Stage1
                    if (button2Pressed)
                        if (button4Pressed)
                            if (button1Pressed)
                                stage2Buttons = true;
                if (stage2Buttons && dialArrowScript.stage2) //Stage2
                {
                    if (!onlyOnce)
                    {
                        button1Pressed = false; button2Pressed = false; button3Pressed = false; button4Pressed = false;
                        onlyOnce = true;
                        buttonCounter = 0;
                    }
                    stage1Buttons = false;
                    if (button2Pressed)                         //Requirementas
                        if (buttonCounter >= 2)
                            if (button4Pressed)
                            {
                                stage3Buttons = true;
                            }
                }
                if (stage3Buttons && dialArrowScript.stage3) //Stage 3
                {
                    if (!onlyOnce2)
                    {
                        button1Pressed = false; button2Pressed = false; button3Pressed = false; button4Pressed = false;
                        onlyOnce2 = true;
                        buttonCounter = 0;
                    }
                    stage2Buttons = false;
                    if (button3Pressed)
                        if (button2Pressed)                     // Requiremtns
                            if (button1Pressed)
                                if (button4Pressed)
                                    buttonPass = true;
                }
                break;
            //case Combinations.YWXZ:    //Copy this one
            //    YWXZ();
            //    if (stage1Buttons) //Stage1
            //        if (button2Pressed)
            //            if (button3Pressed)
            //                if (button1Pressed)
            //                    stage2Buttons = true;
            //    if (stage2Buttons && dialArrowScript.stage2) //Stage2
            //    {
            //        if (!onlyOnce)
            //        {
            //            button1Pressed = false; button2Pressed = false; button3Pressed = false; button4Pressed = false;
            //            onlyOnce = true;
            //            buttonCounter = 0;
            //        }
            //        stage1Buttons = false;
            //        if (button4Pressed)                         //Requirementas
            //            if (buttonCounter >= 2)
            //                if (button3Pressed)
            //                {
            //                    stage3Buttons = true;
            //                }
            //    }
            //    if (stage3Buttons && dialArrowScript.stage3) //Stage 3
            //    {
            //        if (!onlyOnce2)
            //        {
            //            button1Pressed = false; button2Pressed = false; button3Pressed = false; button4Pressed = false;
            //            onlyOnce2 = true;
            //            buttonCounter = 0;
            //        }
            //        stage2Buttons = false;
            //        if (button3Pressed)
            //            if (button2Pressed)                     // Requiremtns
            //                if (button1Pressed)
            //                    if (button4Pressed)
            //                        buttonPass = true;
            //    }
            //    break;
            case Combinations.ZWYX:
                ZWYX();
                if (stage1Buttons) //Stage1
                    if (button4Pressed)
                        if (button3Pressed)
                            if (button1Pressed)
                                stage2Buttons = true;
                if (stage2Buttons && dialArrowScript.stage2) //Stage2
                {
                    if (!onlyOnce)
                    {
                        button1Pressed = false; button2Pressed = false; button3Pressed = false; button4Pressed = false;
                        onlyOnce = true;
                        buttonCounter = 0;
                    }
                    stage1Buttons = false;
                    if (button3Pressed)                         //Requirementas
                        if (buttonCounter >= 2)
                            if (button1Pressed)
                            {
                                stage3Buttons = true;
                            }
                }
                if (stage3Buttons && dialArrowScript.stage3) //Stage 3
                {
                    if (!onlyOnce2)
                    {
                        button1Pressed = false; button2Pressed = false; button3Pressed = false; button4Pressed = false;
                        onlyOnce2 = true;
                        buttonCounter = 0;
                    }
                    stage2Buttons = false;
                    if (button3Pressed)
                        if (button2Pressed)                     // Requiremtns
                            if (button1Pressed)
                                if (button4Pressed)
                                    buttonPass = true;
                }
                break;
            case Combinations.WYXZ:
                WYXZ();
                if (stage1Buttons) //Stage1
                    if (button4Pressed)
                        if (button2Pressed)
                            if (button1Pressed)
                                stage2Buttons = true;
                if (stage2Buttons && dialArrowScript.stage2) //Stage2
                {
                    if (!onlyOnce)
                    {
                        button1Pressed = false; button2Pressed = false; button3Pressed = false; button4Pressed = false;
                        onlyOnce = true;
                        buttonCounter = 0;
                    }
                    stage1Buttons = false;
                    if (button2Pressed)                         //Requirementas
                        if (buttonCounter >= 2)
                            if (button1Pressed)
                            {
                                stage3Buttons = true;
                            }
                }
                if (stage3Buttons && dialArrowScript.stage3) //Stage 3
                {
                    if (!onlyOnce2)
                    {
                        button1Pressed = false; button2Pressed = false; button3Pressed = false; button4Pressed = false;
                        onlyOnce2 = true;
                        buttonCounter = 0;
                    }
                    stage2Buttons = false;
                    if (button3Pressed)
                        if (button2Pressed)                     // Requiremtns
                            if (button1Pressed)
                                if (button4Pressed)
                                    buttonPass = true;
                }
                break;
            case Combinations.WZYX:
                WZYX();
                if (stage1Buttons) //Stage1
                    if (button3Pressed)
                        if (button2Pressed)
                            if (button1Pressed)
                                stage2Buttons = true;
                if (stage2Buttons && dialArrowScript.stage2) //Stage2
                {
                    if (!onlyOnce)
                    {
                        button1Pressed = false; button2Pressed = false; button3Pressed = false; button4Pressed = false;
                        onlyOnce = true;
                        buttonCounter = 0;
                    }
                    stage1Buttons = false;
                    if (button4Pressed)                         //Requirementas
                        if (buttonCounter >= 3)
                        {
                            stage3Buttons = true;
                        }                            
                }
                if (stage3Buttons && dialArrowScript.stage3) //Stage 3
                {
                    if (!onlyOnce2)
                    {
                        button1Pressed = false; button2Pressed = false; button3Pressed = false; button4Pressed = false;
                        onlyOnce2 = true;
                        buttonCounter = 0;
                    }
                    stage2Buttons = false;
                    if (button3Pressed)
                        if (button2Pressed)                     // Requiremtns
                            if (button1Pressed)
                                if (button4Pressed)
                                    buttonPass = true;
                }
                break;
            case Combinations.XYWZ:
                XYWZ();
                if (stage1Buttons) //Stage1
                    if (button4Pressed)
                        if (button3Pressed)
                            if (button1Pressed)
                                stage2Buttons = true;
                if (stage2Buttons && dialArrowScript.stage2) //Stage2
                {
                    if (!onlyOnce)
                    {
                        button1Pressed = false; button2Pressed = false; button3Pressed = false; button4Pressed = false;
                        onlyOnce = true;
                        buttonCounter = 0;
                    }
                    stage1Buttons = false;
                    if (button3Pressed)                         //Requirementas
                        if (buttonCounter >= 2)
                            if (button1Pressed)
                            {
                                stage3Buttons = true;
                            }
                }
                if (stage3Buttons && dialArrowScript.stage3) //Stage 3
                {
                    if (!onlyOnce2)
                    {
                        button1Pressed = false; button2Pressed = false; button3Pressed = false; button4Pressed = false;
                        onlyOnce2 = true;
                        buttonCounter = 0;
                    }
                    stage2Buttons = false;
                    if (button3Pressed)
                        if (button2Pressed)                     // Requiremtns
                            if (button1Pressed)
                                if (button4Pressed)
                                    buttonPass = true;
                }
                break;
            case Combinations.YZWX:
                YZWX();
                if (stage1Buttons) //Stage1
                    if (button1Pressed)
                        if (button3Pressed)
                            if (button2Pressed)
                                stage2Buttons = true;
                if (stage2Buttons && dialArrowScript.stage2) //Stage2
                {
                    if (!onlyOnce)
                    {
                        button1Pressed = false; button2Pressed = false; button3Pressed = false; button4Pressed = false;
                        onlyOnce = true;
                        buttonCounter = 0;
                    }
                    stage1Buttons = false;
                    if (button1Pressed)                         //Requirementas
                        if (buttonCounter >= 2)
                            if (button2Pressed)
                            {
                                stage3Buttons = true;
                            }
                }
                if (stage3Buttons && dialArrowScript.stage3) //Stage 3
                {
                    if (!onlyOnce2)
                    {
                        button1Pressed = false; button2Pressed = false; button3Pressed = false; button4Pressed = false;
                        onlyOnce2 = true;
                        buttonCounter = 0;
                    }
                    stage2Buttons = false;
                    if (button3Pressed)
                        if (button2Pressed)                     // Requiremtns
                            if (button1Pressed)
                                if (button4Pressed)
                                    buttonPass = true;
                }
                break;
            case Combinations.YXWZ:
                YXWZ();
                if (stage1Buttons) //Stage1
                    if (button2Pressed)
                        if (button4Pressed)
                            if (button1Pressed)
                                stage2Buttons = true;
                if (stage2Buttons && dialArrowScript.stage2) //Stage2
                {
                    if (!onlyOnce)
                    {
                        button1Pressed = false; button2Pressed = false; button3Pressed = false; button4Pressed = false;
                        onlyOnce = true;
                        buttonCounter = 0;
                    }
                    stage1Buttons = false;
                    if (button2Pressed)                         //Requirementas
                        if (buttonCounter >= 2)
                            if (button3Pressed)
                            {
                                stage3Buttons = true;
                            }
                }
                if (stage3Buttons && dialArrowScript.stage3) //Stage 3
                {
                    if (!onlyOnce2)
                    {
                        button1Pressed = false; button2Pressed = false; button3Pressed = false; button4Pressed = false;
                        onlyOnce2 = true;
                        buttonCounter = 0;
                    }
                    stage2Buttons = false;
                    if (button3Pressed)
                        if (button2Pressed)                     // Requiremtns
                            if (button1Pressed)
                                if (button4Pressed)
                                    buttonPass = true;
                }
                break;
            case Combinations.ZYWX:
                ZYWX();
                if (stage1Buttons) //Stage1
                    if (button4Pressed)
                        if (button3Pressed)
                            if (button1Pressed)
                                stage2Buttons = true;
                if (stage2Buttons && dialArrowScript.stage2) //Stage2
                {
                    if (!onlyOnce)
                    {
                        button1Pressed = false; button2Pressed = false; button3Pressed = false; button4Pressed = false;
                        onlyOnce = true;
                        buttonCounter = 0;
                    }
                    stage1Buttons = false;
                    if (button1Pressed)                         //Requirementas
                        if (buttonCounter >= 2)
                            if (button2Pressed)
                            {
                                stage3Buttons = true;
                            }
                }
                if (stage3Buttons && dialArrowScript.stage3) //Stage 3
                {
                    if (!onlyOnce2)
                    {
                        button1Pressed = false; button2Pressed = false; button3Pressed = false; button4Pressed = false;
                        onlyOnce2 = true;
                        buttonCounter = 0;
                    }
                    stage2Buttons = false;
                    if (button3Pressed)
                        if (button2Pressed)                     // Requiremtns
                            if (button1Pressed)
                                if (button4Pressed)
                                    buttonPass = true;
                }
                break;
            //case Combinations.ZXWY: //13
            //    ZXWY();
            //    if (stage1Buttons) //Stage1
            //        if (button4Pressed)
            //            if (button3Pressed)
            //                if (button1Pressed)
            //                    stage2Buttons = true;
            //    if (stage2Buttons && dialArrowScript.stage2) //Stage2
            //    {
            //        if (!onlyOnce)
            //        {
            //            button1Pressed = false; button2Pressed = false; button3Pressed = false; button4Pressed = false;
            //            onlyOnce = true;
            //            buttonCounter = 0;
            //        }
            //        stage1Buttons = false;
            //        if (button4Pressed)                         //Requirementas
            //            if (buttonCounter >= 2)
            //                if (button3Pressed)
            //                {
            //                    stage3Buttons = true;
            //                }
            //    }
            //    if (stage3Buttons && dialArrowScript.stage3) //Stage 3
            //    {
            //        if (!onlyOnce2)
            //        {
            //            button1Pressed = false; button2Pressed = false; button3Pressed = false; button4Pressed = false;
            //            onlyOnce2 = true;
            //            buttonCounter = 0;
            //        }
            //        stage2Buttons = false;
            //        if (button3Pressed)
            //            if (button2Pressed)                     // Requiremtns
            //                if (button1Pressed)
            //                    if (button4Pressed)
            //                        buttonPass = true;
            //    }
            //    break;
            case Combinations.ZYXW:
                ZYXW();
                if (stage1Buttons) //Stage1
                    if (button2Pressed)
                        if (button3Pressed)
                            if (button1Pressed)
                                stage2Buttons = true;
                if (stage2Buttons && dialArrowScript.stage2) //Stage2
                {
                    if (!onlyOnce)
                    {
                        button1Pressed = false; button2Pressed = false; button3Pressed = false; button4Pressed = false;
                        onlyOnce = true;
                        buttonCounter = 0;
                    }
                    stage1Buttons = false;
                    if (button3Pressed)                         //Requirementas
                        if (buttonCounter >= 2)
                            if (button1Pressed)
                            {
                                stage3Buttons = true;
                            }
                }
                if (stage3Buttons && dialArrowScript.stage3) //Stage 3
                {
                    if (!onlyOnce2)
                    {
                        button1Pressed = false; button2Pressed = false; button3Pressed = false; button4Pressed = false;
                        onlyOnce2 = true;
                        buttonCounter = 0;
                    }
                    stage2Buttons = false;
                    if (button3Pressed)
                        if (button2Pressed)                     // Requiremtns
                            if (button1Pressed)
                                if (button4Pressed)
                                    buttonPass = true;
                }
                break;
            case Combinations.XZWY:
                XZWY();
                if (stage1Buttons) //Stage1
                    if (button4Pressed)
                        if (button3Pressed)
                            if (button1Pressed)
                                stage2Buttons = true;
                if (stage2Buttons && dialArrowScript.stage2) //Stage2
                {
                    if (!onlyOnce)
                    {
                        button1Pressed = false; button2Pressed = false; button3Pressed = false; button4Pressed = false;
                        onlyOnce = true;
                        buttonCounter = 0;
                    }
                    stage1Buttons = false;
                    if (button2Pressed)                         //Requirementas
                        if (buttonCounter >= 2)
                            if (button1Pressed)
                            {
                                stage3Buttons = true;
                            }
                }
                if (stage3Buttons && dialArrowScript.stage3) //Stage 3
                {
                    if (!onlyOnce2)
                    {
                        button1Pressed = false; button2Pressed = false; button3Pressed = false; button4Pressed = false;
                        onlyOnce2 = true;
                        buttonCounter = 0;
                    }
                    stage2Buttons = false;
                    if (button3Pressed)
                        if (button2Pressed)                     // Requiremtns
                            if (button1Pressed)
                                if (button4Pressed)
                                    buttonPass = true;
                }
                break;
            case Combinations.YZXW:
                YZXW();
                if (stage1Buttons) //Stage1
                    if (button2Pressed)
                        if (button3Pressed)
                            if (button4Pressed)
                                stage2Buttons = true;
                if (stage2Buttons && dialArrowScript.stage2) //Stage2
                {
                    if (!onlyOnce)
                    {
                        button1Pressed = false; button2Pressed = false; button3Pressed = false; button4Pressed = false;
                        onlyOnce = true;
                        buttonCounter = 0;
                    }
                    stage1Buttons = false;
                    if (button2Pressed)                         //Requirementas
                        if (buttonCounter >= 3)
                                stage3Buttons = true;
                }
                if (stage3Buttons && dialArrowScript.stage3) //Stage 3
                {
                    if (!onlyOnce2)
                    {
                        button1Pressed = false; button2Pressed = false; button3Pressed = false; button4Pressed = false;
                        onlyOnce2 = true;
                        buttonCounter = 0;
                    }
                    stage2Buttons = false;
                    if (button3Pressed)
                        if (button2Pressed)                     // Requiremtns
                            if (button1Pressed)
                                if (button4Pressed)
                                    buttonPass = true;
                }
                break;
            case Combinations.WZXY:
                WZXY();
                if (stage1Buttons) //Stage1
                    if (button2Pressed)
                        if (button3Pressed)
                            if (button1Pressed)
                                stage2Buttons = true;
                if (stage2Buttons && dialArrowScript.stage2) //Stage2
                {
                    if (!onlyOnce)
                    {
                        button1Pressed = false; button2Pressed = false; button3Pressed = false; button4Pressed = false;
                        onlyOnce = true;
                        buttonCounter = 0;
                    }
                    stage1Buttons = false;
                    if (button4Pressed)                         //Requirementas
                        if (buttonCounter >= 2)
                            if (button3Pressed)
                            {
                                stage3Buttons = true;
                            }
                }
                if (stage3Buttons && dialArrowScript.stage3) //Stage 3
                {
                    if (!onlyOnce2)
                    {
                        button1Pressed = false; button2Pressed = false; button3Pressed = false; button4Pressed = false;
                        onlyOnce2 = true;
                        buttonCounter = 0;
                    }
                    stage2Buttons = false;
                    if (button3Pressed)
                        if (button2Pressed)                     // Requiremtns
                            if (button1Pressed)
                                if (button4Pressed)
                                    buttonPass = true;
                }
                break;
            //case Combinations.XZYW:
            //    XZYW();
            //    if (stage1Buttons) //Stage1
            //        if (button4Pressed)
            //            if (button3Pressed)
            //                if (button1Pressed)
            //                    stage2Buttons = true;
            //    if (stage2Buttons && dialArrowScript.stage2) //Stage2
            //    {
            //        if (!onlyOnce)
            //        {
            //            button1Pressed = false; button2Pressed = false; button3Pressed = false; button4Pressed = false;
            //            onlyOnce = true;
            //            buttonCounter = 0;
            //        }
            //        stage1Buttons = false;
            //        if (button4Pressed)                         //Requirementas
            //            if (buttonCounter >= 2)
            //                if (button1Pressed)
            //                {
            //                    stage3Buttons = true;
            //                }
            //    }
            //    if (stage3Buttons && dialArrowScript.stage3) //Stage 3
            //    {
            //        if (!onlyOnce2)
            //        {
            //            button1Pressed = false; button2Pressed = false; button3Pressed = false; button4Pressed = false;
            //            onlyOnce2 = true;
            //            buttonCounter = 0;
            //        }
            //        stage2Buttons = false;
            //        if (button3Pressed)
            //            if (button2Pressed)                     // Requiremtns
            //                if (button1Pressed)
            //                    if (button4Pressed)
            //                        buttonPass = true;
            //    }
            //    break;
            case Combinations.ZWXY:
                ZWXY();
                if (stage1Buttons) //Stage1
                    if (button4Pressed)
                        if (button3Pressed)
                            if (button1Pressed)
                                stage2Buttons = true;
                if (stage2Buttons && dialArrowScript.stage2) //Stage2
                {
                    if (!onlyOnce)
                    {
                        button1Pressed = false; button2Pressed = false; button3Pressed = false; button4Pressed = false;
                        onlyOnce = true;
                        buttonCounter = 0;
                    }
                    stage1Buttons = false;
                    if (button3Pressed)                         //Requirementas
                        if (buttonCounter >= 2)
                            if (button1Pressed)
                            {
                                stage3Buttons = true;
                            }
                }
                if (stage3Buttons && dialArrowScript.stage3) //Stage 3
                {
                    if (!onlyOnce2)
                    {
                        button1Pressed = false; button2Pressed = false; button3Pressed = false; button4Pressed = false;
                        onlyOnce2 = true;
                        buttonCounter = 0;
                    }
                    stage2Buttons = false;
                    if (button3Pressed)
                        if (button2Pressed)                     // Requiremtns
                            if (button1Pressed)
                                if (button4Pressed)
                                    buttonPass = true;
                }
                break;
            case Combinations.ZXYW:
                ZXYW();
                if (stage1Buttons) //Stage1
                    if (button2Pressed)
                        if (button3Pressed)
                            if (button1Pressed)
                                stage2Buttons = true;
                if (stage2Buttons && dialArrowScript.stage2) //Stage2
                {
                    if (!onlyOnce)
                    {
                        button1Pressed = false; button2Pressed = false; button3Pressed = false; button4Pressed = false;
                        onlyOnce = true;
                        buttonCounter = 0;
                    }
                    stage1Buttons = false;
                    if (button2Pressed)                         //Requirementas
                        if (buttonCounter >= 2)
                            if (button4Pressed)
                            {
                                stage3Buttons = true;
                            }
                }
                if (stage3Buttons && dialArrowScript.stage3) //Stage 3
                {
                    if (!onlyOnce2)
                    {
                        button1Pressed = false; button2Pressed = false; button3Pressed = false; button4Pressed = false;
                        onlyOnce2 = true;
                        buttonCounter = 0;
                    }
                    stage2Buttons = false;
                    if (button3Pressed)
                        if (button2Pressed)                     // Requiremtns
                            if (button1Pressed)
                                if (button4Pressed)
                                    buttonPass = true;
                }
                break;
            //case Combinations.XWZY:
            //    XWZY();
            //    if (stage1Buttons) //Stage1
            //        if (button2Pressed)
            //            if (button4Pressed)
            //                if (button1Pressed)
            //                    stage2Buttons = true;
            //    if (stage2Buttons && dialArrowScript.stage2) //Stage2
            //    {
            //        if (!onlyOnce)
            //        {
            //            button1Pressed = false; button2Pressed = false; button3Pressed = false; button4Pressed = false;
            //            onlyOnce = true;
            //            buttonCounter = 0;
            //        }
            //        stage1Buttons = false;
            //        if (button4Pressed)                         //Requirementas
            //            if (buttonCounter >= 2)
            //                if (button2Pressed)
            //                {
            //                    stage3Buttons = true;
            //                }
            //    }
            //    if (stage3Buttons && dialArrowScript.stage3) //Stage 3
            //    {
            //        if (!onlyOnce2)
            //        {
            //            button1Pressed = false; button2Pressed = false; button3Pressed = false; button4Pressed = false;
            //            onlyOnce2 = true;
            //            buttonCounter = 0;
            //        }
            //        stage2Buttons = false;
            //        if (button3Pressed)
            //            if (button2Pressed)                     // Requiremtns
            //                if (button1Pressed)
            //                    if (button4Pressed)
            //                        buttonPass = true;
            //    }
            //    break;
            case Combinations.YXZW:
                YXZW();
                if (stage1Buttons) //Stage1
                    if (button2Pressed)
                        if (button3Pressed)
                            if (button1Pressed)
                                stage2Buttons = true;
                if (stage2Buttons && dialArrowScript.stage2) //Stage2
                {
                    if (!onlyOnce)
                    {
                        button1Pressed = false; button2Pressed = false; button3Pressed = false; button4Pressed = false;
                        onlyOnce = true;
                        buttonCounter = 0;
                    }
                    stage1Buttons = false;
                    if (button3Pressed)                         //Requirementas
                        if (buttonCounter >= 2)
                            if (button1Pressed)
                            {
                                stage3Buttons = true;
                            }
                }
                if (stage3Buttons && dialArrowScript.stage3) //Stage 3
                {
                    if (!onlyOnce2)
                    {
                        button1Pressed = false; button2Pressed = false; button3Pressed = false; button4Pressed = false;
                        onlyOnce2 = true;
                        buttonCounter = 0;
                    }
                    stage2Buttons = false;
                    if (button3Pressed)
                        if (button2Pressed)                     // Requiremtns
                            if (button1Pressed)
                                if (button4Pressed)
                                    buttonPass = true;
                }
                break;
            case Combinations.WXZY:
                WXZY();
                if (stage1Buttons) //Stage1
                    if (button2Pressed)
                        if (button3Pressed)
                            if (button4Pressed)
                                stage2Buttons = true;
                if (stage2Buttons && dialArrowScript.stage2) //Stage2
                {
                    if (!onlyOnce)
                    {
                        button1Pressed = false; button2Pressed = false; button3Pressed = false; button4Pressed = false;
                        onlyOnce = true;
                        buttonCounter = 0;
                    }
                    stage1Buttons = false;
                    if (button2Pressed)                         //Requirementas
                        if (buttonCounter >= 2)
                            if (button4Pressed)
                            {
                                stage3Buttons = true;
                            }
                }
                if (stage3Buttons && dialArrowScript.stage3) //Stage 3
                {
                    if (!onlyOnce2)
                    {
                        button1Pressed = false; button2Pressed = false; button3Pressed = false; button4Pressed = false;
                        onlyOnce2 = true;
                        buttonCounter = 0;
                    }
                    stage2Buttons = false;
                    if (button3Pressed)
                        if (button2Pressed)                     // Requiremtns
                            if (button1Pressed)
                                if (button4Pressed)
                                    buttonPass = true;
                }
                break;
            case Combinations.XYZW:
                XYZW();                                                                                                                        
                if (stage1Buttons) //Stage1
                    if (button2Pressed)
                        if (button3Pressed)
                            if (button1Pressed)
                                stage2Buttons = true;
                if (stage2Buttons && dialArrowScript.stage2) //Stage2
                {
                    if (!onlyOnce)
                    {
                        button1Pressed = false; button2Pressed = false; button3Pressed = false; button4Pressed = false;
                        onlyOnce = true;
                        buttonCounter = 0;
                    }
                    stage1Buttons = false;
                    if (button1Pressed)                         //Requirementas
                        if (buttonCounter >= 3)
                                stage3Buttons = true;
                }
                if (stage3Buttons && dialArrowScript.stage3) //Stage 3
                {
                    if (!onlyOnce2)
                    {
                        button1Pressed = false; button2Pressed = false; button3Pressed = false; button4Pressed = false;
                        onlyOnce2 = true;
                        buttonCounter = 0;
                    }
                    stage2Buttons = false;
                    if (button3Pressed)
                        if (button2Pressed)                     // Requiremtns
                            if (button1Pressed)
                                if (button4Pressed)
                                    buttonPass = true;
                }
                break;
            //case Combinations.NUM25:
            //    Num25();
            //    if (stage1Buttons) //Stage1
            //        if (button4Pressed)
            //            if (button3Pressed)
            //                if (button2Pressed)
            //                    stage2Buttons = true;
            //    if (stage2Buttons && dialArrowScript.stage2) //Stage2
            //    {
            //        if (!onlyOnce)
            //        {
            //            button1Pressed = false; button2Pressed = false; button3Pressed = false; button4Pressed = false;
            //            onlyOnce = true;
            //            buttonCounter = 0;
            //        }
            //        stage1Buttons = false;
            //        if (button3Pressed)                         //Requirementas
            //            if (buttonCounter >= 2)
            //                if (button1Pressed)
            //                {
            //                    stage3Buttons = true;
            //                }
            //    }
            //    if (stage3Buttons && dialArrowScript.stage3) //Stage 3
            //    {
            //        if (!onlyOnce2)
            //        {
            //            button1Pressed = false; button2Pressed = false; button3Pressed = false; button4Pressed = false;
            //            onlyOnce2 = true;
            //            buttonCounter = 0;
            //        }
            //        stage2Buttons = false;
            //        if (button3Pressed)
            //            if (button2Pressed)                     // Requiremtns
            //                if (button1Pressed)
            //                    if (button4Pressed)
            //                        buttonPass = true;
            //    }
            //    break;
            case Combinations.NUM26:
                Num26();
                if (stage1Buttons) //Stage1
                    if (button2Pressed)
                        if (button4Pressed)
                            if (button1Pressed)
                                stage2Buttons = true;
                if (stage2Buttons && dialArrowScript.stage2) //Stage2
                {
                    if (!onlyOnce)
                    {
                        button1Pressed = false; button2Pressed = false; button3Pressed = false; button4Pressed = false;
                        onlyOnce = true;
                        buttonCounter = 0;
                    }
                    stage1Buttons = false;
                    if (button2Pressed)                         //Requirementas
                        if (buttonCounter >= 3)
                                stage3Buttons = true;
                }
                if (stage3Buttons && dialArrowScript.stage3) //Stage 3
                {
                    if (!onlyOnce2)
                    {
                        button1Pressed = false; button2Pressed = false; button3Pressed = false; button4Pressed = false;
                        onlyOnce2 = true;
                        buttonCounter = 0;
                    }
                    stage2Buttons = false;
                    if (button3Pressed)
                        if (button2Pressed)                     // Requiremtns
                            if (button1Pressed)
                                if (button4Pressed)
                                    buttonPass = true;
                }
                break;
            //case Combinations.NUM27:
            //    Num27();
            //    if (stage1Buttons) //Stage1
            //        if (button2Pressed)
            //            if (button3Pressed)
            //                if (button1Pressed)
            //                    stage2Buttons = true;
            //    if (stage2Buttons && dialArrowScript.stage2) //Stage2
            //    {
            //        if (!onlyOnce)
            //        {
            //            button1Pressed = false; button2Pressed = false; button3Pressed = false; button4Pressed = false;
            //            onlyOnce = true;
            //            buttonCounter = 0;
            //        }
            //        stage1Buttons = false;
            //        if (button1Pressed)                         //Requirementas
            //            if (buttonCounter >= 2)
            //                if (button3Pressed)
            //                {
            //                    stage3Buttons = true;
            //                }
            //    }
            //    if (stage3Buttons && dialArrowScript.stage3) //Stage 3
            //    {
            //        if (!onlyOnce2)
            //        {
            //            button1Pressed = false; button2Pressed = false; button3Pressed = false; button4Pressed = false;
            //            onlyOnce2 = true;
            //            buttonCounter = 0;
            //        }
            //        stage2Buttons = false;
            //        if (button3Pressed)
            //            if (button2Pressed)                     // Requiremtns
            //                if (button1Pressed)
            //                    if (button4Pressed)
            //                        buttonPass = true;
            //    }
            //    break;
            case Combinations.NUM28:
                Num28();
                if (stage1Buttons) //Stage1
                    if (button1Pressed)
                        if (button4Pressed)
                            if (button2Pressed)
                                stage2Buttons = true;
                if (stage2Buttons && dialArrowScript.stage2) //Stage2
                {
                    if (!onlyOnce)
                    {
                        button1Pressed = false; button2Pressed = false; button3Pressed = false; button4Pressed = false;
                        onlyOnce = true;
                        buttonCounter = 0;
                    }
                    stage1Buttons = false;
                    if (button2Pressed)                         //Requirementas
                        if (buttonCounter >= 2)
                            if (button4Pressed)
                            {
                                stage3Buttons = true;
                            }
                }
                if (stage3Buttons && dialArrowScript.stage3) //Stage 3
                {
                    if (!onlyOnce2)
                    {
                        button1Pressed = false; button2Pressed = false; button3Pressed = false; button4Pressed = false;
                        onlyOnce2 = true;
                        buttonCounter = 0;
                    }
                    stage2Buttons = false;
                    if (button3Pressed)
                        if (button2Pressed)                     // Requiremtns
                            if (button1Pressed)
                                if (button4Pressed)
                                    buttonPass = true;
                }
                break;
            //case Combinations.NUM29:
            //    Num29();
            //    if (stage1Buttons) //Stage1
            //        if (button4Pressed)
            //            if (button3Pressed)
            //                if (button1Pressed)
            //                    stage2Buttons = true;
            //    if (stage2Buttons && dialArrowScript.stage2) //Stage2
            //    {
            //        if (!onlyOnce)
            //        {
            //            button1Pressed = false; button2Pressed = false; button3Pressed = false; button4Pressed = false;
            //            onlyOnce = true;
            //            buttonCounter = 0;
            //        }
            //        stage1Buttons = false;
            //        if (button2Pressed)                         //Requirementas
            //            if (buttonCounter >= 2)
            //                if (button1Pressed)
            //                {
            //                    stage3Buttons = true;
            //                }
            //    }
            //    if (stage3Buttons && dialArrowScript.stage3) //Stage 3
            //    {
            //        if (!onlyOnce2)
            //        {
            //            button1Pressed = false; button2Pressed = false; button3Pressed = false; button4Pressed = false;
            //            onlyOnce2 = true;
            //            buttonCounter = 0;
            //        }
            //        stage2Buttons = false;
            //        if (button1Pressed)
            //            if (button2Pressed)                     // Requiremtns
            //                if (button2Pressed)
            //                    if (button4Pressed)
            //                        buttonPass = true;
            //    }
            //    break;
            case Combinations.NUM30:
                Num30();
                if (stage1Buttons) //Stage1
                    if (button2Pressed)
                        if (button3Pressed)
                            if (button4Pressed)
                                stage2Buttons = true;
                if (stage2Buttons && dialArrowScript.stage2) //Stage2
                {
                    if (!onlyOnce)
                    {
                        button1Pressed = false; button2Pressed = false; button3Pressed = false; button4Pressed = false;
                        onlyOnce = true;
                        buttonCounter = 0;
                    }
                    stage1Buttons = false;
                    if (button1Pressed)                         //Requirementas
                        if (buttonCounter >= 2)
                            if (button3Pressed)
                            {
                                stage3Buttons = true;
                            }
                }
                if (stage3Buttons && dialArrowScript.stage3) //Stage 3
                {
                    if (!onlyOnce2)
                    {
                        button1Pressed = false; button2Pressed = false; button3Pressed = false; button4Pressed = false;
                        onlyOnce2 = true;
                        buttonCounter = 0;
                    }
                    stage2Buttons = false;
                    if (button3Pressed)
                        if (button2Pressed)                     // Requiremtns
                            if (button1Pressed)
                                if (button4Pressed)
                                    buttonPass = true;
                }
                //if (stage4Buttons && dialArrowScript.stage4) //Stage 4
                //{
                //    if (!onlyOnce3)
                //    {
                //        button1Pressed = false; button2Pressed = false; button3Pressed = false; button4Pressed = false;
                //        onlyOnce3 = true;
                //        buttonCounter = 0;
                //    }
                //    stage3Buttons = false;
                //    if (button3Pressed)                                     //Requirments
                //        if (buttonCounter >= 2)
                //            if (button1Pressed)
                //                if (button2Pressed)
                //                    buttonPass = true;
                //}
                break;
        }
    }

#region Combinations

    void WXYZ()
    {
        button1Image.sprite = combinationImages[17];
        button2Image.sprite = combinationImages[18];
        button3Image.sprite = combinationImages[14];
        button4Image.sprite = combinationImages[16];
   
        if(dialPass == true && buttonPass == true)
        {
            if (!audioPlayed)
            {
                aSource.PlayOneShot(wonPuzzle, 0.5f);
                audioPlayed = true;
            }
            correctAns.enabled = true;
            StartCoroutine(WaitToLerp());
        }
    }

    void WYZX()
    {
        button1Image.sprite = combinationImages[13];
        button2Image.sprite = combinationImages[1];
        button3Image.sprite = combinationImages[4];
        button4Image.sprite = combinationImages[19];

        if (dialPass == true && buttonPass == true)
        {
            if (!audioPlayed)
            {
                aSource.PlayOneShot(wonPuzzle, 0.5f);
                audioPlayed = true;
            }
            correctAns.enabled = true;
            StartCoroutine(WaitToLerp());
        }
    }

    void XWYZ()
    {
        button1Image.sprite = combinationImages[12];
        button2Image.sprite = combinationImages[6];
        button3Image.sprite = combinationImages[10];
        button4Image.sprite = combinationImages[5];

        if (dialPass == true && buttonPass == true)
        {
            if (!audioPlayed)
            {
                aSource.PlayOneShot(wonPuzzle, 0.5f);
                audioPlayed = true;
            }
            correctAns.enabled = true;
            StartCoroutine(WaitToLerp());
        }
    }

    void YWZX()
    {
        button1Image.sprite = combinationImages[20];
        button2Image.sprite = combinationImages[23];
        button3Image.sprite = combinationImages[9];
        button4Image.sprite = combinationImages[14];

        if (dialPass == true && buttonPass == true)
        {
            if (!audioPlayed)
            {
                aSource.PlayOneShot(wonPuzzle, 0.5f);
                audioPlayed = true;
            }
            correctAns.enabled = true;
            StartCoroutine(WaitToLerp());
        }
    }



   //Number 5
    void YWXZ()
    {
        button1Image.sprite = combinationImages[22];
        button2Image.sprite = combinationImages[0];
        button3Image.sprite = combinationImages[21];
        button4Image.sprite = combinationImages[8];

        if (dialPass == true && buttonPass == true)
        {
            if (!audioPlayed)
            {
                aSource.PlayOneShot(wonPuzzle, 0.5f);
                audioPlayed = true;
            }
            correctAns.enabled = true;
            StartCoroutine(WaitToLerp());
        }
    }

    //Number 6
    void ZWYX()
    {
        button1Image.sprite = combinationImages[2];
        button2Image.sprite = combinationImages[3];
        button3Image.sprite = combinationImages[18];
        button4Image.sprite = combinationImages[17];

        if (dialPass == true && buttonPass == true)
        {
            if (!audioPlayed)
            {
                aSource.PlayOneShot(wonPuzzle, 0.5f);
                audioPlayed = true;
            }
            correctAns.enabled = true;
            StartCoroutine(WaitToLerp());
        }
    }

    void WYXZ()
    {
        button1Image.sprite = combinationImages[11];
        button2Image.sprite = combinationImages[5];
        button3Image.sprite = combinationImages[15];
        button4Image.sprite = combinationImages[16];

        if (dialPass == true && buttonPass == true)
        {
            if (!audioPlayed)
            {
                aSource.PlayOneShot(wonPuzzle, 0.5f);
                audioPlayed = true;
            }
            correctAns.enabled = true;
            StartCoroutine(WaitToLerp());
        }
    }

    void WZYX()
    {
        button1Image.sprite = combinationImages[7];
        button2Image.sprite = combinationImages[3];
        button3Image.sprite = combinationImages[17];
        button4Image.sprite = combinationImages[0];

        if (dialPass == true && buttonPass == true)
        {
            if (!audioPlayed)
            {
                aSource.PlayOneShot(wonPuzzle, 0.5f);
                audioPlayed = true;
            }
            correctAns.enabled = true;
            StartCoroutine(WaitToLerp());
        }
    }

    void XYWZ()
    {
        button1Image.sprite = combinationImages[12];
        button2Image.sprite = combinationImages[6];
        button3Image.sprite = combinationImages[18];
        button4Image.sprite = combinationImages[19];

        if (dialPass == true && buttonPass == true)
        {
            if (!audioPlayed)
            {
                aSource.PlayOneShot(wonPuzzle, 0.5f);
                audioPlayed = true;
            }
            correctAns.enabled = true;
            StartCoroutine(WaitToLerp());
        }
    }

    void YZWX()
    {
        button1Image.sprite = combinationImages[1];
        button2Image.sprite = combinationImages[5];
        button3Image.sprite = combinationImages[8];
        button4Image.sprite = combinationImages[6];

        if (dialPass == true && buttonPass == true)
        {
            if (!audioPlayed)
            {
                aSource.PlayOneShot(wonPuzzle, 0.5f);
                audioPlayed = true;
            }
            correctAns.enabled = true;
            StartCoroutine(WaitToLerp());
        }
    }

    void YXWZ()
    {
        button1Image.sprite = combinationImages[20];
        button2Image.sprite = combinationImages[13];
        button3Image.sprite = combinationImages[22];
        button4Image.sprite = combinationImages[23];

        if (dialPass == true && buttonPass == true)
        {
            if (!audioPlayed)
            {
                aSource.PlayOneShot(wonPuzzle, 0.5f);
                audioPlayed = true;
            }
            correctAns.enabled = true;
            StartCoroutine(WaitToLerp());
        }
    }

    void ZYWX()
    {
        button1Image.sprite = combinationImages[9];
        button2Image.sprite = combinationImages[8];
        button3Image.sprite = combinationImages[1];
        button4Image.sprite = combinationImages[3];

        if (dialPass == true && buttonPass == true)
        {
            if (!audioPlayed)
            {
                aSource.PlayOneShot(wonPuzzle, 0.5f);
                audioPlayed = true;
            }
            correctAns.enabled = true;
            StartCoroutine(WaitToLerp());
        }
    }

    void ZXWY()
    {
        button1Image.sprite = combinationImages[19];
        button2Image.sprite = combinationImages[2];
        button3Image.sprite = combinationImages[14];
        button4Image.sprite = combinationImages[4];

        if (dialPass == true && buttonPass == true)
        {
            if (!audioPlayed)
            {
                aSource.PlayOneShot(wonPuzzle, 0.5f);
                audioPlayed = true;
            }
            correctAns.enabled = true;
            StartCoroutine(WaitToLerp());
        }
    }

    //14
    void ZYXW()
    {
        button1Image.sprite = combinationImages[16];
        button2Image.sprite = combinationImages[15];
        button3Image.sprite = combinationImages[21];
        button4Image.sprite = combinationImages[11];

        if (dialPass == true && buttonPass == true)
        {
            if (!audioPlayed)
            {
                aSource.PlayOneShot(wonPuzzle, 0.5f);
                audioPlayed = true;
            }
            correctAns.enabled = true;
            StartCoroutine(WaitToLerp());
        }
    }

    void XZWY()
    {
        button1Image.sprite = combinationImages[6];
        button2Image.sprite = combinationImages[10];
        button3Image.sprite = combinationImages[18];
        button4Image.sprite = combinationImages[17];

        if (dialPass == true && buttonPass == true)
        {
            if (!audioPlayed)
            {
                aSource.PlayOneShot(wonPuzzle, 0.5f);
                audioPlayed = true;
            }
            correctAns.enabled = true;
            StartCoroutine(WaitToLerp());
        }
    }

    void YZXW()
    {
        button1Image.sprite = combinationImages[23];
        button2Image.sprite = combinationImages[2];
        button3Image.sprite = combinationImages[3];
        button4Image.sprite = combinationImages[20];

        if (dialPass == true && buttonPass == true)
        {
            if (!audioPlayed)
            {
                aSource.PlayOneShot(wonPuzzle, 0.5f);
                audioPlayed = true;
            }
            correctAns.enabled = true;
            StartCoroutine(WaitToLerp());
        }
    }

    void WZXY()
    {
        button1Image.sprite = combinationImages[0];
        button2Image.sprite = combinationImages[16];
        button3Image.sprite = combinationImages[9];
        button4Image.sprite = combinationImages[8];

        if (dialPass == true && buttonPass == true)
        {
            if (!audioPlayed)
            {
                aSource.PlayOneShot(wonPuzzle, 0.5f);
                audioPlayed = true;
            }
            correctAns.enabled = true;
            StartCoroutine(WaitToLerp());
        }
    }

    void XZYW()
    {
        button1Image.sprite = combinationImages[15];
        button2Image.sprite = combinationImages[22];
        button3Image.sprite = combinationImages[12];
        button4Image.sprite = combinationImages[13];

        if (dialPass == true && buttonPass == true)
        {
            if (!audioPlayed)
            {
                aSource.PlayOneShot(wonPuzzle, 0.5f);
                audioPlayed = true;
            }
            correctAns.enabled = true;
            StartCoroutine(WaitToLerp());
        }
    }

    void ZWXY()
    {
        button1Image.sprite = combinationImages[3];
        button2Image.sprite = combinationImages[7];
        button3Image.sprite = combinationImages[19];
        button4Image.sprite = combinationImages[5];

        if (dialPass == true && buttonPass == true)
        {
            if (!audioPlayed)
            {
                aSource.PlayOneShot(wonPuzzle, 0.5f);
                audioPlayed = true;
            }
            correctAns.enabled = true;
            StartCoroutine(WaitToLerp());
        }
    }

    void ZXYW()
    {
        button1Image.sprite = combinationImages[11];
        button2Image.sprite = combinationImages[18];
        button3Image.sprite = combinationImages[9];
        button4Image.sprite = combinationImages[21];

        if (dialPass == true && buttonPass == true)
        {
            if (!audioPlayed)
            {
                aSource.PlayOneShot(wonPuzzle, 0.5f);
                audioPlayed = true;
            }
            correctAns.enabled = true;
            StartCoroutine(WaitToLerp());
        }
    }

    void XWZY()
    {
        button1Image.sprite = combinationImages[13];
        button2Image.sprite = combinationImages[17];
        button3Image.sprite = combinationImages[4];
        button4Image.sprite = combinationImages[0];

        if (dialPass == true && buttonPass == true)
        {
            if (!audioPlayed)
            {
                aSource.PlayOneShot(wonPuzzle, 0.5f);
                audioPlayed = true;
            }
            correctAns.enabled = true;
            StartCoroutine(WaitToLerp());
        }
    }

    //22
    void YXZW()
    {
        button1Image.sprite = combinationImages[16];
        button2Image.sprite = combinationImages[2];
        button3Image.sprite = combinationImages[11];
        button4Image.sprite = combinationImages[8];

        if (dialPass == true && buttonPass == true)
        {
            if (!audioPlayed)
            {
                aSource.PlayOneShot(wonPuzzle, 0.5f);
                audioPlayed = true;
            }
            correctAns.enabled = true;
            StartCoroutine(WaitToLerp());
        }
    }

    void WXZY()
    {
        button1Image.sprite = combinationImages[21];
        button2Image.sprite = combinationImages[23];
        button3Image.sprite = combinationImages[14];
        button4Image.sprite = combinationImages[12];

        if (dialPass == true && buttonPass == true)
        {
            if (!audioPlayed)
            {
                aSource.PlayOneShot(wonPuzzle, 0.5f);
                audioPlayed = true;
            }
            correctAns.enabled = true;
            StartCoroutine(WaitToLerp());
        }
    }

    //24
    void XYZW()
    {
        button1Image.sprite = combinationImages[1];
        button2Image.sprite = combinationImages[17];
        button3Image.sprite = combinationImages[11];
        button4Image.sprite = combinationImages[20];

        if (dialPass == true && buttonPass == true)
        {
            if (!audioPlayed)
            {
                aSource.PlayOneShot(wonPuzzle, 0.5f);
                audioPlayed = true;
            }
            correctAns.enabled = true;
            StartCoroutine(WaitToLerp());
        }
    }

    void Num25()
    {
        button1Image.sprite = combinationImages[5];
        button2Image.sprite = combinationImages[6];
        button3Image.sprite = combinationImages[17];
        button4Image.sprite = combinationImages[19];

        if (dialPass == true && buttonPass == true)
        {
            if (!audioPlayed)
            {
                aSource.PlayOneShot(wonPuzzle, 0.5f);
                audioPlayed = true;
            }
            correctAns.enabled = true;
            StartCoroutine(WaitToLerp());
        }
    }

    void Num26()
    {
        button1Image.sprite = combinationImages[15];
        button2Image.sprite = combinationImages[10];
        button3Image.sprite = combinationImages[9];
        button4Image.sprite = combinationImages[7];

        if (dialPass == true && buttonPass == true)
        {
            if (!audioPlayed)
            {
                aSource.PlayOneShot(wonPuzzle, 0.5f);
                audioPlayed = true;
            }
            correctAns.enabled = true;
            StartCoroutine(WaitToLerp());
        }
    }

    void Num27()
    {
        button1Image.sprite = combinationImages[18];
        button2Image.sprite = combinationImages[16];
        button3Image.sprite = combinationImages[2];
        button4Image.sprite = combinationImages[8];

        if (dialPass == true && buttonPass == true)
        {
            if (!audioPlayed)
            {
                aSource.PlayOneShot(wonPuzzle, 0.5f);
                audioPlayed = true;
            }
            correctAns.enabled = true;
            StartCoroutine(WaitToLerp());
        }
    }

    void Num28()
    {
        button1Image.sprite = combinationImages[4];
        button2Image.sprite = combinationImages[13];
        button3Image.sprite = combinationImages[22];
        button4Image.sprite = combinationImages[14];

        if (dialPass == true && buttonPass == true)
        {
            if (!audioPlayed)
            {
                aSource.PlayOneShot(wonPuzzle, 0.5f);
                audioPlayed = true;
            }
            correctAns.enabled = true;
            StartCoroutine(WaitToLerp());
        }
    }

    void Num29()
    {
        button1Image.sprite = combinationImages[14];
        button2Image.sprite = combinationImages[18];
        button3Image.sprite = combinationImages[17];
        button4Image.sprite = combinationImages[21];

        if (dialPass == true && buttonPass == true)
        {
            if (!audioPlayed)
            {
                aSource.PlayOneShot(wonPuzzle, 0.5f);
                audioPlayed = true;
            }
            correctAns.enabled = true;
            StartCoroutine(WaitToLerp());
        }
    }

    void Num30()
    {
        button1Image.sprite = combinationImages[9];
        button2Image.sprite = combinationImages[10];
        button3Image.sprite = combinationImages[3];
        button4Image.sprite = combinationImages[11];

        if (dialPass == true && buttonPass == true)
        {
            if (!audioPlayed)
            {
                aSource.PlayOneShot(wonPuzzle, 0.5f);
                audioPlayed = true;
            }
            correctAns.enabled = true;
            StartCoroutine(WaitToLerp());
        }
    }

#endregion

    public void Button1Clicker()
    {
        button1Pressed = true;
        Button1Clicked();     
    }

    public void Button2Clicker()
    {
        button2Pressed = true;
        Button2Clicked();
    }

    public void Button3Clicker()
    {
        button3Pressed = true;
        Button3Clicked(); 
    }

    public void Button4Clicker()
    {
        button4Pressed = true;
        Button4Clicked(); 
    }

    #region StartHellishMessyAnnoyingCode

    void Button1Clicked()
    {
        if (pickedCombination == Combinations.WXYZ)
        {
            if(stage1Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button1Pressed = true;
            }
            if(stage2Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button1Pressed = true;
                buttonCounter++;
            }
            if(stage3Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button1Pressed = true;
            }
        }

        if (pickedCombination == Combinations.WYZX)
        {
            if (stage1Buttons)
            {
                aSource.PlayOneShot(wrongButton, 0.5f);
                //Handheld.Vibrate();
                LivesCounter++; flashOnce = false;
                
                button1Pressed = true;
            }
            if (stage2Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button1Pressed = true;
            }
            if (stage3Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button1Pressed = true;
            }
        }

        //if (pickedCombination == Combinations.XWYZ)
        //{
        //    if (stage1Buttons)
        //    {
        //        aSource.PlayOneShot(correctButton, 0.5f);
        //        buttonCount++;
        //        oneHasBeen = true;
        //        button1Pressed = true;
        //    }
        //    if (stage2Buttons)
        //    {
        //        aSource.PlayOneShot(correctButton, 0.5f);
        //        buttonCount++;
        //        oneHasBeen = true;
        //        button1Pressed = true;
        //    }
        //    if (stage3Buttons)
        //    {
        //        aSource.PlayOneShot(correctButton, 0.5f);
        //        buttonCount++;
        //        oneHasBeen = true;
        //        button1Pressed = true;
        //    }

        //}

        if (pickedCombination == Combinations.YWZX)
        {
            if (stage1Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button1Pressed = true;
            }
            if (stage2Buttons)
            {
                aSource.PlayOneShot(wrongButton, 0.5f);
                //Handheld.Vibrate();
                LivesCounter++; flashOnce = false;

                button1Pressed = true;
            }
            if (stage3Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button1Pressed = true;
            }
        }

        //if (pickedCombination == Combinations.YWXZ)
        //{
        //    if (stage1Buttons)
        //    {
        //        aSource.PlayOneShot(correctButton, 0.5f);
        //        buttonCount++;
        //        oneHasBeen = true;
        //        button1Pressed = true;
        //    }
        //    if (stage2Buttons)
        //    {
        //        aSource.PlayOneShot(wrongButton, 0.5f);
        //        //Handheld.Vibrate();
        //        LivesCounter++; flashOnce = false;

        //        button1Pressed = true;
        //    }
        //    if (stage3Buttons)
        //    {
        //        aSource.PlayOneShot(correctButton, 0.5f);
        //        buttonCount++;
        //        oneHasBeen = true;
        //        button1Pressed = true;
        //    }
        //}

        if (pickedCombination == Combinations.ZWYX)
        {
            if (stage1Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button1Pressed = true;
            }
            if (stage2Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button1Pressed = true;
            }
            if (stage3Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button1Pressed = true;
            }
        }

        if (pickedCombination == Combinations.WYXZ)
        {
            if (stage1Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button1Pressed = true;
            }
            if (stage2Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button1Pressed = true;
            }
            if (stage3Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button1Pressed = true;
            }
        }

        if (pickedCombination == Combinations.WZYX)
        {
            if (stage1Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button1Pressed = true;
            }
            if (stage2Buttons)
            {
                aSource.PlayOneShot(wrongButton, 0.5f);
                //Handheld.Vibrate();
                LivesCounter++; flashOnce = false;

                button1Pressed = true;
            }
            if (stage3Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button1Pressed = true;
            }
        }

        if (pickedCombination == Combinations.XYWZ)
        {
            if (stage1Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button1Pressed = true;
            }
            if (stage2Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button1Pressed = true;
            }
            if (stage3Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button1Pressed = true;
            }
        }

        if (pickedCombination == Combinations.YZWX)
        {
            if (stage1Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button1Pressed = true;
            }
            if (stage2Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button1Pressed = true;
                buttonCounter++;
            }
            if (stage3Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button1Pressed = true;
            }
        }

        if (pickedCombination == Combinations.YXWZ)
        {
            if (stage1Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button1Pressed = true;
            }
            if (stage2Buttons)
            {
                aSource.PlayOneShot(wrongButton, 0.5f);
                //Handheld.Vibrate();
                LivesCounter++; flashOnce = false;

                button1Pressed = true;
            }
            if (stage3Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button1Pressed = true;
            }
        }

        if (pickedCombination == Combinations.ZYWX)
        {
            if (stage1Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button1Pressed = true;
            }
            if (stage2Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button1Pressed = true;
                buttonCounter++;
            }
            if (stage3Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button1Pressed = true;
            }
        }

        //if (pickedCombination == Combinations.ZXWY)
        //{
        //    if (stage1Buttons)
        //    {
        //        aSource.PlayOneShot(correctButton, 0.5f);
        //        buttonCount++;
        //        oneHasBeen = true;
        //        button1Pressed = true;
        //    }
        //    if (stage2Buttons)
        //    {
        //        aSource.PlayOneShot(wrongButton, 0.5f);
        //        //Handheld.Vibrate();
        //        LivesCounter++; flashOnce = false;

        //        button1Pressed = true;
        //    }
        //    if (stage3Buttons)
        //    {
        //        aSource.PlayOneShot(correctButton, 0.5f);
        //        buttonCount++;
        //        oneHasBeen = true;
        //        button1Pressed = true;
        //    }
        //}

        //Number 13, 14 on sheet starts here

        if (pickedCombination == Combinations.ZYXW)
        {
            if (stage1Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button1Pressed = true;
            }
            if (stage2Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button1Pressed = true;
            }
            if (stage3Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button1Pressed = true;
            }
        }

        if (pickedCombination == Combinations.XZWY)
        {
            if (stage1Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button1Pressed = true;
            }
            if (stage2Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button1Pressed = true;
            }
            if (stage3Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button1Pressed = true;
            }
        }

        if (pickedCombination == Combinations.YZXW)
        {
            if (stage1Buttons)
            {
                aSource.PlayOneShot(wrongButton, 0.5f);
                //Handheld.Vibrate();
                LivesCounter++; flashOnce = false;

                button1Pressed = true;
            }
            if (stage2Buttons)
            {
                aSource.PlayOneShot(wrongButton, 0.5f);
                //Handheld.Vibrate();
                LivesCounter++; flashOnce = false;

                button1Pressed = true;
            }
            if (stage3Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button1Pressed = true;
            }
        }

        if (pickedCombination == Combinations.WZXY)
        {
            if (stage1Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button1Pressed = true;
            }
            if (stage2Buttons)
            {
                aSource.PlayOneShot(wrongButton, 0.5f);
                //Handheld.Vibrate();
                LivesCounter++; flashOnce = false;

                button1Pressed = true;
            }
            if (stage3Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button1Pressed = true;
            }
        }

        //if (pickedCombination == Combinations.XZYW)
        //{
        //    if (stage1Buttons)
        //    {
        //        aSource.PlayOneShot(correctButton, 0.5f);
        //        buttonCount++;
        //        oneHasBeen = true;
        //        button1Pressed = true;
        //    }
        //    if (stage2Buttons)
        //    {
        //        aSource.PlayOneShot(correctButton, 0.5f);
        //        buttonCount++;
        //        oneHasBeen = true;
        //        button1Pressed = true;
        //    }
        //    if (stage3Buttons)
        //    {
        //        aSource.PlayOneShot(correctButton, 0.5f);
        //        buttonCount++;
        //        oneHasBeen = true;
        //        button1Pressed = true;
        //    }
        //}

        if (pickedCombination == Combinations.ZWXY)
        {
            if (stage1Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button1Pressed = true;
            }
            if (stage2Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button1Pressed = true;
            }
            if (stage3Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button1Pressed = true;
            }
        }

        //Number 20 yay (19)

        if (pickedCombination == Combinations.ZXYW)
        {
            if (stage1Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button1Pressed = true;
            }
            if (stage2Buttons)
            {
                aSource.PlayOneShot(wrongButton, 0.5f);
                //Handheld.Vibrate();
                LivesCounter++; flashOnce = false;

                button1Pressed = true;
            }
            if (stage3Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button1Pressed = true;
            }
        }

        //if (pickedCombination == Combinations.XWZY)
        //{
        //    if (stage1Buttons)
        //    {
        //        aSource.PlayOneShot(correctButton, 0.5f);
        //        buttonCount++;
        //        oneHasBeen = true;
        //        button1Pressed = true;
        //    }
        //    if (stage2Buttons)
        //    {
        //        aSource.PlayOneShot(wrongButton, 0.5f);
        //        //Handheld.Vibrate();
        //        LivesCounter++; flashOnce = false;

        //        button1Pressed = true;
        //    }
        //    if (stage3Buttons)
        //    {
        //        aSource.PlayOneShot(correctButton, 0.5f);
        //        buttonCount++;
        //        oneHasBeen = true;
        //        button1Pressed = true;
        //    }
        //}

        if (pickedCombination == Combinations.YXZW)
        {
            if (stage1Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button1Pressed = true;
            }
            if (stage2Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button1Pressed = true;
            }
            if (stage3Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button1Pressed = true;
            }
        }

        if (pickedCombination == Combinations.WXZY)
        {
            if (stage1Buttons)
            {
                aSource.PlayOneShot(wrongButton, 0.5f);
                //Handheld.Vibrate();
                LivesCounter++; flashOnce = false;

                button1Pressed = true;
            }
            if (stage2Buttons)
            {
                aSource.PlayOneShot(wrongButton, 0.5f);
                //Handheld.Vibrate();
                LivesCounter++; flashOnce = false;

                button1Pressed = true;
            }
            if (stage3Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button1Pressed = true;
            }
        }

        if (pickedCombination == Combinations.XYZW)
        {
            if (stage1Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button1Pressed = true;
            }
            if (stage2Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button1Pressed = true;
                buttonCounter++;
            }
            if (stage3Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button1Pressed = true;
            }
        }

        //5 more hwg

        //if (pickedCombination == Combinations.NUM25)
        //{
        //    if (stage1Buttons)
        //    {
        //        aSource.PlayOneShot(wrongButton, 0.5f);
        //        //Handheld.Vibrate();
        //        LivesCounter++; flashOnce = false;

        //        button1Pressed = true;
        //    }
        //    if (stage2Buttons)
        //    {
        //        aSource.PlayOneShot(correctButton, 0.5f);
        //        buttonCount++;
        //        oneHasBeen = true;
        //        button1Pressed = true;
        //    }
        //    if (stage3Buttons)
        //    {
        //        aSource.PlayOneShot(correctButton, 0.5f);
        //        buttonCount++;
        //        oneHasBeen = true;
        //        button1Pressed = true;
        //    }
        //}

        if (pickedCombination == Combinations.NUM26)
        {
            if (stage1Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button1Pressed = true;
            }
            if (stage2Buttons)
            {
                aSource.PlayOneShot(wrongButton, 0.5f);
                //Handheld.Vibrate();
                LivesCounter++; flashOnce = false;

                button1Pressed = true;
            }
            if (stage3Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button1Pressed = true;
            }
        }

        //if (pickedCombination == Combinations.NUM27)
        //{
        //    if (stage1Buttons)
        //    {
        //        aSource.PlayOneShot(correctButton, 0.5f);
        //        buttonCount++;
        //        oneHasBeen = true;
        //        button1Pressed = true;
        //    }
        //    if (stage2Buttons)
        //    {
        //        aSource.PlayOneShot(correctButton, 0.5f);
        //        buttonCount++;
        //        oneHasBeen = true;
        //        button1Pressed = true;
        //    }
        //    if (stage3Buttons)
        //    {
        //        aSource.PlayOneShot(correctButton, 0.5f);
        //        buttonCount++;
        //        oneHasBeen = true;
        //        button1Pressed = true;
        //    }
        //}

        if (pickedCombination == Combinations.NUM28)
        {
            if (stage1Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button1Pressed = true;
            }
            if (stage2Buttons)
            {
                aSource.PlayOneShot(wrongButton, 0.5f);
                //Handheld.Vibrate();
                LivesCounter++; flashOnce = false;

                button1Pressed = true;
            }
            if (stage3Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button1Pressed = true;
            }
        }

        //if (pickedCombination == Combinations.NUM29)
        //{
        //    if (stage1Buttons)
        //    {
        //        aSource.PlayOneShot(correctButton, 0.5f);
        //        buttonCount++;
        //        oneHasBeen = true;
        //        button1Pressed = true;
        //    }
        //    if (stage2Buttons)
        //    {
        //        aSource.PlayOneShot(correctButton, 0.5f);
        //        buttonCount++;
        //        oneHasBeen = true;
        //        button1Pressed = true;
        //    }
        //    if (stage3Buttons)
        //    {
        //        aSource.PlayOneShot(correctButton, 0.5f);
        //        buttonCount++;
        //        oneHasBeen = true;
        //        button1Pressed = true;
        //    }
        //}

        if (pickedCombination == Combinations.NUM30)
        {
            if (stage1Buttons)
            {
                aSource.PlayOneShot(wrongButton, 0.5f);
                //Handheld.Vibrate();
                LivesCounter++; flashOnce = false;

                button1Pressed = true;
            }
            if (stage2Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button1Pressed = true;
            }
            if (stage3Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button1Pressed = true;
            }
        }
    }

    void Button2Clicked()
    {
        if (pickedCombination == Combinations.WXYZ)
        {
            if (stage1Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                button2Pressed = true;
            }
            if (stage2Buttons)
            {
                aSource.PlayOneShot(wrongButton, 0.5f);
                //Handheld.Vibrate();
                LivesCounter++; flashOnce = false;

                button2Pressed = true;
            }
            if(stage3Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                button2Pressed = true;
            }
        }

        if (pickedCombination == Combinations.WYZX)
        {
            if (stage1Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                button2Pressed = true;
            }
            if (stage2Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                button2Pressed = true;
                buttonCounter++;
            }
            if (stage3Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                button2Pressed = true;
            }
        }

        //if (pickedCombination == Combinations.XWYZ)
        //{
        //    if (stage1Buttons)
        //    {
        //        aSource.PlayOneShot(wrongButton, 0.5f);
        //        //Handheld.Vibrate();
        //        LivesCounter++; flashOnce = false;

        //        button2Pressed = true;
        //    }
        //    if (stage2Buttons)
        //    {
        //        aSource.PlayOneShot(wrongButton, 0.5f);
        //        //Handheld.Vibrate();
        //        LivesCounter++; flashOnce = false;

        //        button2Pressed = true;
        //    }
        //    if (stage3Buttons)
        //    {
        //        aSource.PlayOneShot(correctButton, 0.5f);
        //        buttonCount++;
        //        oneHasBeen = true;
        //        button2Pressed = true;
        //    }
        //}

        if (pickedCombination == Combinations.YWZX)
        {
            if (stage1Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button2Pressed = true;
                buttonCounter++;
            }
            if (stage2Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button2Pressed = true;
                buttonCounter++;
            }
            if (stage3Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button2Pressed = true;
            }
        }

        //if (pickedCombination == Combinations.YWXZ)
        //{
        //    if (stage1Buttons)
        //    {
        //        aSource.PlayOneShot(correctButton, 0.5f);
        //        buttonCount++;
        //        oneHasBeen = true;
        //        button2Pressed = true;
        //    }
        //    if (stage2Buttons)
        //    {
        //        aSource.PlayOneShot(wrongButton, 0.5f);
        //        //Handheld.Vibrate();
        //        LivesCounter++; flashOnce = false;

        //        button2Pressed = true;
        //    }
        //    if (stage3Buttons)
        //    {
        //        aSource.PlayOneShot(correctButton, 0.5f);
        //        buttonCount++;
        //        oneHasBeen = true;
        //        button2Pressed = true;
        //    }
        //}

        if (pickedCombination == Combinations.ZWYX)
        {
            if (stage1Buttons)
            {
                aSource.PlayOneShot(wrongButton, 0.5f);
                //Handheld.Vibrate();
                LivesCounter++; flashOnce = false;

                button2Pressed = true;
            }
            if (stage2Buttons)
            {
                aSource.PlayOneShot(wrongButton, 0.5f);
                //Handheld.Vibrate();
                LivesCounter++; flashOnce = false;

                button2Pressed = true;
            }
            if (stage3Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button2Pressed = true;
            }
        }

        if (pickedCombination == Combinations.WYXZ)
        {
            if (stage1Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button2Pressed = true;
            }
            if (stage2Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button2Pressed = true;
                buttonCounter++;
            }
            if (stage3Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button2Pressed = true;
            }
        }

        if (pickedCombination == Combinations.WZYX)
        {
            if (stage1Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button2Pressed = true;
            }
            if (stage2Buttons)
            {
                aSource.PlayOneShot(wrongButton, 0.5f);
                //Handheld.Vibrate();
                LivesCounter++; flashOnce = false;

                button2Pressed = true;
            }
            if (stage3Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button2Pressed = true;
            }
        }

        if (pickedCombination == Combinations.XYWZ)
        {
            if (stage1Buttons)
            {
                aSource.PlayOneShot(wrongButton, 0.5f);
                //Handheld.Vibrate();
                LivesCounter++; flashOnce = false;

                button2Pressed = true;
            }
            if (stage2Buttons)
            {
                aSource.PlayOneShot(wrongButton, 0.5f);
                //Handheld.Vibrate();
                LivesCounter++; flashOnce = false;

                button2Pressed = true;
            }
            if (stage3Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button2Pressed = true;
            }
        }

        if (pickedCombination == Combinations.YZWX)
        {
            if (stage1Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button2Pressed = true;
            }
            if (stage2Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button2Pressed = true;
            }
            if (stage3Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button2Pressed = true;
            }
        }

        if (pickedCombination == Combinations.YXWZ)
        {
            if (stage1Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button2Pressed = true;
            }
            if (stage2Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button2Pressed = true;
            }
            if (stage3Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button2Pressed = true;
            }
        }

        if (pickedCombination == Combinations.ZYWX)
        {
            if (stage1Buttons)
            {
                aSource.PlayOneShot(wrongButton, 0.5f);
                //Handheld.Vibrate();
                LivesCounter++; flashOnce = false;

                button2Pressed = true;
            }
            if (stage2Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button2Pressed = true;
            }
            if (stage3Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button2Pressed = true;
            }
        }

        //if (pickedCombination == Combinations.ZXWY)
        //{
        //    if (stage1Buttons)
        //    {
        //        aSource.PlayOneShot(wrongButton, 0.5f);
        //        //Handheld.Vibrate();
        //        LivesCounter++; flashOnce = false;

        //        button2Pressed = true;
        //    }
        //    if (stage2Buttons)
        //    {
        //        aSource.PlayOneShot(wrongButton, 0.5f);
        //        //Handheld.Vibrate();
        //        LivesCounter++; flashOnce = false;

        //        button2Pressed = true;
        //    }
        //    if (stage3Buttons)
        //    {
        //        aSource.PlayOneShot(correctButton, 0.5f);
        //        buttonCount++;
        //        oneHasBeen = true;
        //        button2Pressed = true;
        //    }
        //}

        if (pickedCombination == Combinations.ZYXW)
        {
            if (stage1Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button2Pressed = true;
            }
            if (stage2Buttons)
            {
                aSource.PlayOneShot(wrongButton, 0.5f);
                //Handheld.Vibrate();
                LivesCounter++; flashOnce = false;

                button2Pressed = true;
            }
            if (stage3Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button2Pressed = true;
            }
        }

        if (pickedCombination == Combinations.XZWY)
        {
            if (stage1Buttons)
            {
                aSource.PlayOneShot(wrongButton, 0.5f);
                //Handheld.Vibrate();
                LivesCounter++; flashOnce = false;

                button2Pressed = true;
            }
            if (stage2Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button2Pressed = true;
                buttonCounter++;
            }
            if (stage3Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button2Pressed = true;
            }
        }

        if (pickedCombination == Combinations.YZXW)
        {
            if (stage1Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button2Pressed = true;
            }
            if (stage2Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button2Pressed = true;
                buttonCounter++;
            }
            if (stage3Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button2Pressed = true;
            }
        }

        if (pickedCombination == Combinations.WZXY)
        {
            if (stage1Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button2Pressed = true;
            }
            if (stage2Buttons)
            {
                aSource.PlayOneShot(wrongButton, 0.5f);
                //Handheld.Vibrate();
                LivesCounter++; flashOnce = false;

                button2Pressed = true;
            }
            if (stage3Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button2Pressed = true;
            }
        }

        //if (pickedCombination == Combinations.XZYW)
        //{
        //    if (stage1Buttons)
        //    {
        //        aSource.PlayOneShot(wrongButton, 0.5f);
        //        //Handheld.Vibrate();
        //        LivesCounter++; flashOnce = false;

        //        button2Pressed = true;
        //    }
        //    if (stage2Buttons)
        //    {
        //        aSource.PlayOneShot(wrongButton, 0.5f);
        //        //Handheld.Vibrate();
        //        LivesCounter++; flashOnce = false;

        //        button2Pressed = true;
        //    }
        //    if (stage3Buttons)
        //    {
        //        aSource.PlayOneShot(correctButton, 0.5f);
        //        buttonCount++;
        //        oneHasBeen = true;
        //        button2Pressed = true;
        //    }
        //}

        if (pickedCombination == Combinations.ZWXY)
        {
            if (stage1Buttons)
            {
                aSource.PlayOneShot(wrongButton, 0.5f);
                //Handheld.Vibrate();
                LivesCounter++; flashOnce = false;

                button2Pressed = true;
            }
            if (stage2Buttons)
            {
                aSource.PlayOneShot(wrongButton, 0.5f);
                //Handheld.Vibrate();
                LivesCounter++; flashOnce = false;

                button2Pressed = true;
            }
            if (stage3Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button2Pressed = true;
            }
        }

        if (pickedCombination == Combinations.ZXYW)
        {
            if (stage1Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button2Pressed = true;
            }
            if (stage2Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button2Pressed = true;
                buttonCounter++;
            }
            if (stage3Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button2Pressed = true;
            }
        }

        //if (pickedCombination == Combinations.XWZY)
        //{
        //    if (stage1Buttons)
        //    {
        //        aSource.PlayOneShot(correctButton, 0.5f);
        //        buttonCount++;
        //        oneHasBeen = true;
        //        button2Pressed = true;
        //    }
        //    if (stage2Buttons)
        //    {
        //        aSource.PlayOneShot(correctButton, 0.5f);
        //        buttonCount++;
        //        oneHasBeen = true;
        //        button2Pressed = true;
        //        buttonCounter++;
        //    }
        //    if (stage3Buttons)
        //    {
        //        aSource.PlayOneShot(correctButton, 0.5f);
        //        buttonCount++;
        //        oneHasBeen = true;
        //        button2Pressed = true;
        //    }
        //}

        if (pickedCombination == Combinations.YXZW)
        {
            if (stage1Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button2Pressed = true;
            }
            if (stage2Buttons)
            {
                aSource.PlayOneShot(wrongButton, 0.5f);
                //Handheld.Vibrate();
                LivesCounter++; flashOnce = false;

                button2Pressed = true;
            }
            if (stage3Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button2Pressed = true;
            }
        }

        if (pickedCombination == Combinations.WXZY)
        {
            if (stage1Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button2Pressed = true;
            }
            if (stage2Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button2Pressed = true;
                buttonCounter++;
            }
            if (stage3Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button2Pressed = true;
            }
        }

        if (pickedCombination == Combinations.XYZW)
        {
            if (stage1Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button2Pressed = true;
            }
            if (stage2Buttons)
            {
                aSource.PlayOneShot(wrongButton, 0.5f);
                //Handheld.Vibrate();
                LivesCounter++; flashOnce = false;

                button2Pressed = true;
            }
            if (stage3Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button2Pressed = true;
            }
        }

        //if (pickedCombination == Combinations.NUM25)
        //{
        //    if (stage1Buttons)
        //    {
        //        aSource.PlayOneShot(correctButton, 0.5f);
        //        buttonCount++;
        //        oneHasBeen = true;
        //        button2Pressed = true;
        //    }
        //    if (stage2Buttons)
        //    {
        //        aSource.PlayOneShot(wrongButton, 0.5f);
        //        //Handheld.Vibrate();
        //        LivesCounter++; flashOnce = false;

        //        button2Pressed = true;
        //    }
        //    if (stage3Buttons)
        //    {
        //        aSource.PlayOneShot(correctButton, 0.5f);
        //        buttonCount++;
        //        oneHasBeen = true;
        //        button2Pressed = true;
        //    }
        //}

        if (pickedCombination == Combinations.NUM26)
        {
            if (stage1Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button2Pressed = true;
            }
            if (stage2Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button2Pressed = true;
                buttonCounter++;
            }
            if (stage3Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button2Pressed = true;
            }
        }

        //if (pickedCombination == Combinations.NUM27)
        //{
        //    if (stage1Buttons)
        //    {
        //        aSource.PlayOneShot(correctButton, 0.5f);
        //        buttonCount++;
        //        oneHasBeen = true;
        //        button2Pressed = true;
        //    }
        //    if (stage2Buttons)
        //    {
        //        aSource.PlayOneShot(wrongButton, 0.5f);
        //        //Handheld.Vibrate();
        //        LivesCounter++; flashOnce = false;

        //        button2Pressed = true;
        //    }
        //    if (stage3Buttons)
        //    {
        //        aSource.PlayOneShot(correctButton, 0.5f);
        //        buttonCount++;
        //        oneHasBeen = true;
        //        button2Pressed = true;
        //    }
        //}

        if (pickedCombination == Combinations.NUM28)
        {
            if (stage1Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button2Pressed = true;
            }
            if (stage2Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button2Pressed = true;
                buttonCounter++;
            }
            if (stage3Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button2Pressed = true;
            }
        }

        //if (pickedCombination == Combinations.NUM29)
        //{
        //    if (stage1Buttons)
        //    {
        //        aSource.PlayOneShot(wrongButton, 0.5f);
        //        //Handheld.Vibrate();
        //        LivesCounter++; flashOnce = false;

        //        button2Pressed = true;
        //    }
        //    if (stage2Buttons)
        //    {
        //        aSource.PlayOneShot(correctButton, 0.5f);
        //        buttonCount++;
        //        oneHasBeen = true;
        //        button2Pressed = true;
        //        buttonCounter++;
        //    }
        //    if (stage3Buttons)
        //    {
        //        aSource.PlayOneShot(correctButton, 0.5f);
        //        buttonCount++;
        //        oneHasBeen = true;
        //        button2Pressed = true;
        //    }
        //}

        if (pickedCombination == Combinations.NUM30)
        {
            if (stage1Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button2Pressed = true;
            }
            if (stage2Buttons)
            {
                aSource.PlayOneShot(wrongButton, 0.5f);
                //Handheld.Vibrate();
                LivesCounter++; flashOnce = false;

                button2Pressed = true;
            }
            if (stage3Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button2Pressed = true;
            }
        }
    }

    void Button3Clicked()
    {
        if (pickedCombination == Combinations.WXYZ)
        {
            if (stage1Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                button3Pressed = true;
            }
            if (stage2Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                button3Pressed = true;
            }
            if(stage3Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                button2Pressed = true;
            }
        }

        if (pickedCombination == Combinations.WYZX)
        {
            if (stage1Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button3Pressed = true;
            }
            if (stage2Buttons)
            {
                aSource.PlayOneShot(wrongButton, 0.5f);
                //Handheld.Vibrate();
                LivesCounter++; flashOnce = false;

                button3Pressed = true;
            }
            if (stage3Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button3Pressed = true;
            }
        }

        //if (pickedCombination == Combinations.XWYZ)
        //{
        //    if (stage1Buttons)
        //    {
        //        aSource.PlayOneShot(correctButton, 0.5f);
        //        buttonCount++;
        //        oneHasBeen = true;
        //        button3Pressed = true;
        //    }
        //    if (stage2Buttons)
        //    {
        //        aSource.PlayOneShot(correctButton, 0.5f);
        //        buttonCount++;
        //        oneHasBeen = true;
        //        button3Pressed = true;
        //        buttonCounter++;
        //    }
        //    if (stage3Buttons)
        //    {
        //        aSource.PlayOneShot(correctButton, 0.5f);
        //        buttonCount++;
        //        oneHasBeen = true;
        //        button3Pressed = true;
        //    }
        //}

        if (pickedCombination == Combinations.YWZX)
        {
            if (stage1Buttons)
            {
                aSource.PlayOneShot(wrongButton, 0.5f);
                //Handheld.Vibrate();
                LivesCounter++; flashOnce = false;

                button3Pressed = true;
            }
            if (stage2Buttons)
            {
                aSource.PlayOneShot(wrongButton, 0.5f);
                //Handheld.Vibrate();
                LivesCounter++; flashOnce = false;

                button3Pressed = true;
            }
            if (stage3Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button3Pressed = true;
            }
        }

        //if (pickedCombination == Combinations.YWXZ)
        //{
        //    if (stage1Buttons)
        //    {
        //        aSource.PlayOneShot(correctButton, 0.5f);
        //        buttonCount++;
        //        oneHasBeen = true;
        //        button3Pressed = true;
        //    }
        //    if (stage2Buttons)
        //    {
        //        aSource.PlayOneShot(correctButton, 0.5f);
        //        buttonCount++;
        //        oneHasBeen = true;
        //        button3Pressed = true;
        //    }
        //    if (stage3Buttons)
        //    {
        //        aSource.PlayOneShot(correctButton, 0.5f);
        //        buttonCount++;
        //        oneHasBeen = true;
        //        button3Pressed = true;
        //    }
        //}

        if (pickedCombination == Combinations.ZWYX)
        {
            if (stage1Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button3Pressed = true;
            }
            if (stage2Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button3Pressed = true;
                buttonCounter++;
            }
            if (stage3Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button3Pressed = true;
            }
        }

        if (pickedCombination == Combinations.WYXZ)
        {
            if (stage1Buttons)
            {
                aSource.PlayOneShot(wrongButton, 0.5f);
                //Handheld.Vibrate();
                LivesCounter++; flashOnce = false;

                button3Pressed = true;
            }
            if (stage2Buttons)
            {
                aSource.PlayOneShot(wrongButton, 0.5f);
                //Handheld.Vibrate();
                LivesCounter++; flashOnce = false;

                button3Pressed = true;
            }
            if (stage3Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button3Pressed = true;
            }
        }

        if (pickedCombination == Combinations.WZYX)
        {
            if (stage1Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button3Pressed = true;
            }
            if (stage2Buttons)
            {
                aSource.PlayOneShot(wrongButton, 0.5f);
                //Handheld.Vibrate();
                LivesCounter++; flashOnce = false;

                button3Pressed = true;
            }
            if (stage3Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button3Pressed = true;
            }
        }

        if (pickedCombination == Combinations.XYWZ)
        {
            if (stage1Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button3Pressed = true;
            }
            if (stage2Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button3Pressed = true;
                buttonCounter++;
            }
            if (stage3Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button3Pressed = true;
            }
        }

        if (pickedCombination == Combinations.YZWX)
        {
            if (stage1Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button3Pressed = true;
            }
            if (stage2Buttons)
            {
                aSource.PlayOneShot(wrongButton, 0.5f);
                //Handheld.Vibrate();
                LivesCounter++; flashOnce = false;

                button3Pressed = true;
            }
            if (stage3Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button3Pressed = true;
            }
        }

        if (pickedCombination == Combinations.YXWZ)
        {
            if (stage1Buttons)
            {
                aSource.PlayOneShot(wrongButton, 0.5f);
                //Handheld.Vibrate();
                LivesCounter++; flashOnce = false;

                button3Pressed = true;
            }
            if (stage2Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button3Pressed = true;
                buttonCounter++;
            }
            if (stage3Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button3Pressed = true;
            }
        }

        if (pickedCombination == Combinations.ZYWX)
        {
            if (stage1Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button3Pressed = true;
            }
            if (stage2Buttons)
            {
                aSource.PlayOneShot(wrongButton, 0.5f);
                //Handheld.Vibrate();
                LivesCounter++; flashOnce = false;

                button3Pressed = true;
            }
            if (stage3Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button3Pressed = true;
            }
        }

        //if (pickedCombination == Combinations.ZXWY)
        //{
        //    if (stage1Buttons)
        //    {
        //        aSource.PlayOneShot(correctButton, 0.5f);
        //        buttonCount++;
        //        oneHasBeen = true;
        //        button3Pressed = true;
        //    }
        //    if (stage2Buttons)
        //    {
        //        aSource.PlayOneShot(correctButton, 0.5f);
        //        buttonCount++;
        //        oneHasBeen = true;
        //        button3Pressed = true;
        //        buttonCounter++;
        //    }
        //    if (stage3Buttons)
        //    {
        //        aSource.PlayOneShot(correctButton, 0.5f);
        //        buttonCount++;
        //        oneHasBeen = true;
        //        button3Pressed = true;
        //    }
        //}

        if (pickedCombination == Combinations.ZYXW)
        {
            if (stage1Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button3Pressed = true;
            }
            if (stage2Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button3Pressed = true;
                buttonCounter++;
            }
            if (stage3Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button3Pressed = true;
            }
        }

        if (pickedCombination == Combinations.XZWY)
        {
            if (stage1Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button3Pressed = true;
            }
            if (stage2Buttons)
            {
                aSource.PlayOneShot(wrongButton, 0.5f);
                //Handheld.Vibrate();
                LivesCounter++; flashOnce = false;

                button3Pressed = true;
            }
            if (stage3Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button3Pressed = true;
            }
        }

        if (pickedCombination == Combinations.YZXW)
        {
            if (stage1Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button3Pressed = true;
            }
            if (stage2Buttons)
            {
                aSource.PlayOneShot(wrongButton, 0.5f);
                //Handheld.Vibrate();
                LivesCounter++; flashOnce = false;

                button3Pressed = true;
            }
            if (stage3Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button3Pressed = true;
            }
        }

        if (pickedCombination == Combinations.WZXY)
        {
            if (stage1Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button3Pressed = true;
            }
            if (stage2Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button3Pressed = true;
            }
            if (stage3Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button3Pressed = true;
            }
        }

        //if (pickedCombination == Combinations.XZYW)
        //{
        //    if (stage1Buttons)
        //    {
        //        aSource.PlayOneShot(correctButton, 0.5f);
        //        buttonCount++;
        //        oneHasBeen = true;
        //        button3Pressed = true;
        //    }
        //    if (stage2Buttons)
        //    {
        //        aSource.PlayOneShot(wrongButton, 0.5f);
        //        //Handheld.Vibrate();
        //        LivesCounter++; flashOnce = false;

        //        button3Pressed = true;
        //    }
        //    if (stage3Buttons)
        //    {
        //        aSource.PlayOneShot(correctButton, 0.5f);
        //        buttonCount++;
        //        oneHasBeen = true;
        //        button3Pressed = true;
        //    }
        //}

        if (pickedCombination == Combinations.ZWXY)
        {
            if (stage1Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button3Pressed = true;
            }
            if (stage2Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button3Pressed = true;
                buttonCounter++;
            }
            if (stage3Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button3Pressed = true;
            }
        }

        if (pickedCombination == Combinations.ZXYW)
        {
            if (stage1Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button3Pressed = true;
            }
            if (stage2Buttons)
            {
                aSource.PlayOneShot(wrongButton, 0.5f);
                //Handheld.Vibrate();
                LivesCounter++; flashOnce = false;

                button3Pressed = true;
            }
            if (stage3Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button3Pressed = true;
            }
        }

        //if (pickedCombination == Combinations.XWZY)
        //{
        //    if (stage1Buttons)
        //    {
        //        aSource.PlayOneShot(wrongButton, 0.5f);
        //        //Handheld.Vibrate();
        //        LivesCounter++; flashOnce = false;

        //        button3Pressed = true;
        //    }
        //    if (stage2Buttons)
        //    {
        //        aSource.PlayOneShot(wrongButton, 0.5f);
        //        //Handheld.Vibrate();
        //        LivesCounter++; flashOnce = false;

        //        button3Pressed = true;
        //    }
        //    if (stage3Buttons)
        //    {
        //        aSource.PlayOneShot(correctButton, 0.5f);
        //        buttonCount++;
        //        oneHasBeen = true;
        //        button3Pressed = true;
        //    }
        //}

        if (pickedCombination == Combinations.YXZW)
        {
            if (stage1Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button3Pressed = true;
            }
            if (stage2Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button3Pressed = true;
                buttonCounter++;
            }
            if (stage3Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button3Pressed = true;
            }
        }

        if (pickedCombination == Combinations.WXZY)
        {
            if (stage1Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button3Pressed = true;
            }
            if (stage2Buttons)
            {
                aSource.PlayOneShot(wrongButton, 0.5f);
                //Handheld.Vibrate();
                LivesCounter++; flashOnce = false;

                button3Pressed = true;
            }
            if (stage3Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button3Pressed = true;
            }
        }

        if (pickedCombination == Combinations.XYZW)
        {
            if (stage1Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button3Pressed = true;
            }
            if (stage2Buttons)
            {
                aSource.PlayOneShot(wrongButton, 0.5f);
                //Handheld.Vibrate();
                LivesCounter++; flashOnce = false;

                button3Pressed = true;
            }
            if (stage3Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button3Pressed = true;
            }
        }

        //if (pickedCombination == Combinations.NUM25)
        //{
        //    if (stage1Buttons)
        //    {
        //        aSource.PlayOneShot(correctButton, 0.5f);
        //        buttonCount++;
        //        oneHasBeen = true;
        //        button3Pressed = true;
        //    }
        //    if (stage2Buttons)
        //    {
        //        aSource.PlayOneShot(correctButton, 0.5f);
        //        buttonCount++;
        //        oneHasBeen = true;
        //        button3Pressed = true;
        //        buttonCounter++;
        //    }
        //    if (stage3Buttons)
        //    {
        //        aSource.PlayOneShot(correctButton, 0.5f);
        //        buttonCount++;
        //        oneHasBeen = true;
        //        button3Pressed = true;
        //    }
        //}

        if (pickedCombination == Combinations.NUM26)
        {
            if (stage1Buttons)
            {
                aSource.PlayOneShot(wrongButton, 0.5f);
                //Handheld.Vibrate();
                LivesCounter++; flashOnce = false;

                button3Pressed = true;
            }
            if (stage2Buttons)
            {
                aSource.PlayOneShot(wrongButton, 0.5f);
                //Handheld.Vibrate();
                LivesCounter++; flashOnce = false;

                button3Pressed = true;
            }
            if (stage3Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button3Pressed = true;
            }
        }

        //if (pickedCombination == Combinations.NUM27)
        //{
        //    if (stage1Buttons)
        //    {
        //        aSource.PlayOneShot(correctButton, 0.5f);
        //        buttonCount++;
        //        oneHasBeen = true;
        //        button3Pressed = true;
        //    }
        //    if (stage2Buttons)
        //    {
        //        aSource.PlayOneShot(correctButton, 0.5f);
        //        buttonCount++;
        //        oneHasBeen = true;
        //        button3Pressed = true;
        //        buttonCounter++;
        //    }
        //    if (stage3Buttons)
        //    {
        //        aSource.PlayOneShot(correctButton, 0.5f);
        //        buttonCount++;
        //        oneHasBeen = true;
        //        button3Pressed = true;
        //    }
        //}

        if (pickedCombination == Combinations.NUM28)
        {
            if (stage1Buttons)
            {
                aSource.PlayOneShot(wrongButton, 0.5f);
                //Handheld.Vibrate();
                LivesCounter++; flashOnce = false;

                button3Pressed = true;
            }
            if (stage2Buttons)
            {
                aSource.PlayOneShot(wrongButton, 0.5f);
                //Handheld.Vibrate();
                LivesCounter++; flashOnce = false;

                button3Pressed = true;
            }
            if (stage3Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button3Pressed = true;
            }
        }

        //if (pickedCombination == Combinations.NUM29)
        //{
        //    if (stage1Buttons)
        //    {
        //        aSource.PlayOneShot(correctButton, 0.5f);
        //        buttonCount++;
        //        oneHasBeen = true;
        //        button3Pressed = true;
        //    }
        //    if (stage2Buttons)
        //    {
        //        aSource.PlayOneShot(wrongButton, 0.5f);
        //        //Handheld.Vibrate();
        //        LivesCounter++; flashOnce = false;

        //        button3Pressed = true;
        //    }
        //    if (stage3Buttons)
        //    {
        //        aSource.PlayOneShot(correctButton, 0.5f);
        //        buttonCount++;
        //        oneHasBeen = true;
        //        button3Pressed = true;
        //    }
        //}

        if (pickedCombination == Combinations.NUM30)
        {
            if (stage1Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button3Pressed = true;
            }
            if (stage2Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button3Pressed = true;
                buttonCounter++;
            }
            if (stage3Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button3Pressed = true;
            }
        }
    }

    void Button4Clicked()
    {
        if (pickedCombination == Combinations.WXYZ)
        {
            if (stage1Buttons)
            {
                aSource.PlayOneShot(wrongButton, 0.5f);
                //Handheld.Vibrate();
                LivesCounter++; flashOnce = false;

                button4Pressed = true;
            }
            if (stage2Buttons)
            {
                aSource.PlayOneShot(wrongButton, 0.5f);
                //Handheld.Vibrate();
                LivesCounter++; flashOnce = false;

                button4Pressed = true;
            }
            if(stage3Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button2Pressed = true;
            }
        }

        if (pickedCombination == Combinations.WYZX)
        {
            if (stage1Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button4Pressed = true;
            }
            if (stage2Buttons)
            {
                aSource.PlayOneShot(wrongButton, 0.5f);
                //Handheld.Vibrate();
                LivesCounter++; flashOnce = false;

                button4Pressed = true;
            }
            if (stage3Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button4Pressed = true;
            }
        }

        //if (pickedCombination == Combinations.XWYZ)
        //{
        //    if (stage1Buttons)
        //    {
        //        aSource.PlayOneShot(correctButton, 0.5f);
        //        buttonCount++;
        //        oneHasBeen = true;
        //        button4Pressed = true;
        //    }
        //    if (stage2Buttons)
        //    {
        //        aSource.PlayOneShot(wrongButton, 0.5f);
        //        //Handheld.Vibrate();
        //        LivesCounter++; flashOnce = false;

        //        button4Pressed = true;
        //    }
        //    if (stage3Buttons)
        //    {
        //        aSource.PlayOneShot(correctButton, 0.5f);
        //        buttonCount++;
        //        oneHasBeen = true;
        //        button4Pressed = true;
        //    }
        //}

        if (pickedCombination == Combinations.YWZX)
        {
            if (stage1Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button4Pressed = true;
            }
            if (stage2Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button4Pressed = true;
            }
            if (stage3Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button4Pressed = true;
            }
        }

        //if (pickedCombination == Combinations.YWXZ)
        //{
        //    if (stage1Buttons)
        //    {
        //        aSource.PlayOneShot(wrongButton, 0.5f);
        //        //Handheld.Vibrate();
        //        LivesCounter++; flashOnce = false;

        //        button4Pressed = true;
        //    }
        //    if (stage2Buttons)
        //    {
        //        aSource.PlayOneShot(correctButton, 0.5f);
        //        buttonCount++;
        //        oneHasBeen = true;
        //        button4Pressed = true;
        //        buttonCounter++;
        //    }
        //    if (stage3Buttons)
        //    {
        //        aSource.PlayOneShot(correctButton, 0.5f);
        //        buttonCount++;
        //        oneHasBeen = true;
        //        button4Pressed = true;
        //    }
        //}

        if (pickedCombination == Combinations.ZWYX)
        {
            if (stage1Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button4Pressed = true;
            }
            if (stage2Buttons)
            {
                aSource.PlayOneShot(wrongButton, 0.5f);
                //Handheld.Vibrate();
                LivesCounter++; flashOnce = false;

                button4Pressed = true;
            }
            if (stage3Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button4Pressed = true;
            }
        }

        if (pickedCombination == Combinations.WYXZ)
        {
            if (stage1Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button4Pressed = true;
            }
            if (stage2Buttons)
            {
                aSource.PlayOneShot(wrongButton, 0.5f);
                //Handheld.Vibrate();
                LivesCounter++; flashOnce = false;

                button4Pressed = true;
            }
            if (stage3Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button4Pressed = true;
            }
        }

        if (pickedCombination == Combinations.WZYX)
        {
            if (stage1Buttons)
            {
                aSource.PlayOneShot(wrongButton, 0.5f);
                //Handheld.Vibrate();
                LivesCounter++; flashOnce = false;

                button4Pressed = true;
            }
            if (stage2Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button4Pressed = true;
                buttonCounter++;
            }
            if (stage3Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button4Pressed = true;
            }
        }

        if (pickedCombination == Combinations.XYWZ)
        {
            if (stage1Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button4Pressed = true;
            }
            if (stage2Buttons)
            {
                aSource.PlayOneShot(wrongButton, 0.5f);
                //Handheld.Vibrate();
                LivesCounter++; flashOnce = false;

                button4Pressed = true;
            }
            if (stage3Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button4Pressed = true;
            }
        }

        if (pickedCombination == Combinations.YZWX)
        {
            if (stage1Buttons)
            {
                aSource.PlayOneShot(wrongButton, 0.5f);
                //Handheld.Vibrate();
                LivesCounter++; flashOnce = false;

                button4Pressed = true;
            }
            if (stage2Buttons)
            {
                aSource.PlayOneShot(wrongButton, 0.5f);
                //Handheld.Vibrate();
                LivesCounter++; flashOnce = false;

                button4Pressed = true;
            }
            if (stage3Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button4Pressed = true;
            }
        }

        if (pickedCombination == Combinations.YXWZ)
        {
            if (stage1Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button4Pressed = true;
            }
            if (stage2Buttons)
            {
                aSource.PlayOneShot(wrongButton, 0.5f);
                //Handheld.Vibrate();
                LivesCounter++; flashOnce = false;

                button4Pressed = true;
            }
            if (stage3Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button4Pressed = true;
            }
        }

        if (pickedCombination == Combinations.ZYWX)
        {
            if (stage1Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button4Pressed = true;
            }
            if (stage2Buttons)
            {
                aSource.PlayOneShot(wrongButton, 0.5f);
                //Handheld.Vibrate();
                LivesCounter++; flashOnce = false;

                button4Pressed = true;
            }
            if (stage3Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button4Pressed = true;
            }
        }

        //if (pickedCombination == Combinations.ZXWY)
        //{
        //    if (stage1Buttons)
        //    {
        //        aSource.PlayOneShot(correctButton, 0.5f);
        //        buttonCount++;
        //        oneHasBeen = true;
        //        button4Pressed = true;
        //    }
        //    if (stage2Buttons)
        //    {
        //        aSource.PlayOneShot(correctButton, 0.5f);
        //        buttonCount++;
        //        oneHasBeen = true;
        //        button4Pressed = true;
        //    }
        //    if (stage3Buttons)
        //    {
        //        aSource.PlayOneShot(correctButton, 0.5f);
        //        buttonCount++;
        //        oneHasBeen = true;
        //        button4Pressed = true;
        //    }
        //}

        if (pickedCombination == Combinations.ZYXW)
        {
            if (stage1Buttons)
            {
                aSource.PlayOneShot(wrongButton, 0.5f);
                //Handheld.Vibrate();
                LivesCounter++; flashOnce = false;

                button4Pressed = true;
            }
            if (stage2Buttons)
            {
                aSource.PlayOneShot(wrongButton, 0.5f);
                //Handheld.Vibrate();
                LivesCounter++; flashOnce = false;

                button4Pressed = true;
            }
            if (stage3Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button4Pressed = true;
            }
        }

        if (pickedCombination == Combinations.XZWY)
        {
            if (stage1Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button4Pressed = true;
            }
            if (stage2Buttons)
            {
                aSource.PlayOneShot(wrongButton, 0.5f);
                //Handheld.Vibrate();
                LivesCounter++; flashOnce = false;

                button4Pressed = true;
            }
            if (stage3Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button4Pressed = true;
            }
        }

        if (pickedCombination == Combinations.YZXW)
        {
            if (stage1Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button4Pressed = true;
            }
            if (stage2Buttons)
            {
                aSource.PlayOneShot(wrongButton, 0.5f);
                //Handheld.Vibrate();
                LivesCounter++; flashOnce = false;

                button4Pressed = true;
            }
            if (stage3Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button4Pressed = true;
            }
        }

        if (pickedCombination == Combinations.WZXY)
        {
            if (stage1Buttons)
            {
                aSource.PlayOneShot(wrongButton, 0.5f);
                //Handheld.Vibrate();
                LivesCounter++; flashOnce = false;

                button4Pressed = true;
            }
            if (stage2Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button4Pressed = true;
                buttonCounter++;
            }
            if (stage3Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button4Pressed = true;
            }
        }

        //if (pickedCombination == Combinations.XZYW)
        //{
        //    if (stage1Buttons)
        //    {
        //        aSource.PlayOneShot(correctButton, 0.5f);
        //        buttonCount++;
        //        oneHasBeen = true;
        //        button4Pressed = true;
        //    }
        //    if (stage2Buttons)
        //    {
        //        aSource.PlayOneShot(correctButton, 0.5f);
        //        buttonCount++;
        //        oneHasBeen = true;
        //        button4Pressed = true;
        //        buttonCounter++;
        //    }
        //    if (stage3Buttons)
        //    {
        //        aSource.PlayOneShot(correctButton, 0.5f);
        //        buttonCount++;
        //        oneHasBeen = true;
        //        button4Pressed = true;
        //    }
        //}

        if (pickedCombination == Combinations.ZWXY)
        {
            if (stage1Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button4Pressed = true;
            }
            if (stage2Buttons)
            {
                aSource.PlayOneShot(wrongButton, 0.5f);
                //Handheld.Vibrate();
                LivesCounter++; flashOnce = false;

                button4Pressed = true;
            }
            if (stage3Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button4Pressed = true;
            }
        }

        if (pickedCombination == Combinations.ZXYW)
        {
            if (stage1Buttons)
            {
                aSource.PlayOneShot(wrongButton, 0.5f);
                //Handheld.Vibrate();
                LivesCounter++; flashOnce = false;

                button4Pressed = true;
            }
            if (stage2Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button4Pressed = true;
            }
            if (stage3Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button4Pressed = true;
            }
        }

        //if (pickedCombination == Combinations.XWZY)
        //{
        //    if (stage1Buttons)
        //    {
        //        aSource.PlayOneShot(correctButton, 0.5f);
        //        buttonCount++;
        //        oneHasBeen = true;
        //        button4Pressed = true;
        //    }
        //    if (stage2Buttons)
        //    {
        //        aSource.PlayOneShot(correctButton, 0.5f);
        //        buttonCount++;
        //        oneHasBeen = true;
        //        button4Pressed = true;
        //    }
        //    if (stage3Buttons)
        //    {
        //        aSource.PlayOneShot(correctButton, 0.5f);
        //        buttonCount++;
        //        oneHasBeen = true;
        //        button4Pressed = true;
        //    }
        //}

        if (pickedCombination == Combinations.YXZW)
        {
            if (stage1Buttons)
            {
                aSource.PlayOneShot(wrongButton, 0.5f);
                //Handheld.Vibrate();
                LivesCounter++; flashOnce = false;

                button4Pressed = true;
            }
            if (stage2Buttons)
            {
                aSource.PlayOneShot(wrongButton, 0.5f);
                //Handheld.Vibrate();
                LivesCounter++; flashOnce = false;

                button4Pressed = true;
            }
            if (stage3Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button4Pressed = true;
            }
        }

        if (pickedCombination == Combinations.WXZY)
        {
            if (stage1Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button4Pressed = true;
            }
            if (stage2Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button4Pressed = true;
            }
            if (stage3Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button4Pressed = true;
            }
        }

        if (pickedCombination == Combinations.XYZW)
        {
            if (stage1Buttons)
            {
                aSource.PlayOneShot(wrongButton, 0.5f);
                //Handheld.Vibrate();
                LivesCounter++; flashOnce = false;

                button4Pressed = true;
            }
            if (stage2Buttons)
            {
                aSource.PlayOneShot(wrongButton, 0.5f);
                //Handheld.Vibrate();
                LivesCounter++; flashOnce = false;

                button4Pressed = true;
            }
            if (stage3Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button4Pressed = true;
            }
        }

        //if (pickedCombination == Combinations.NUM25)
        //{
        //    if (stage1Buttons)
        //    {
        //        aSource.PlayOneShot(correctButton, 0.5f);
        //        buttonCount++;
        //        oneHasBeen = true;
        //        button4Pressed = true;
        //    }
        //    if (stage2Buttons)
        //    {
        //        aSource.PlayOneShot(wrongButton, 0.5f);
        //        //Handheld.Vibrate();
        //        LivesCounter++; flashOnce = false;

        //        button4Pressed = true;
        //    }
        //    if (stage3Buttons)
        //    {
        //        aSource.PlayOneShot(correctButton, 0.5f);
        //        buttonCount++;
        //        oneHasBeen = true;
        //        button4Pressed = true;
        //    }
        //}

        if (pickedCombination == Combinations.NUM26)
        {
            if (stage1Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button4Pressed = true;
            }
            if (stage2Buttons)
            {
                aSource.PlayOneShot(wrongButton, 0.5f);
                //Handheld.Vibrate();
                LivesCounter++; flashOnce = false;

                button4Pressed = true;
            }
            if (stage3Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button4Pressed = true;
            }
        }

        //if (pickedCombination == Combinations.NUM27)
        //{
        //    if (stage1Buttons)
        //    {
        //        aSource.PlayOneShot(wrongButton, 0.5f);
        //        //Handheld.Vibrate();
        //        LivesCounter++; flashOnce = false;

        //        button4Pressed = true;
        //    }
        //    if (stage2Buttons)
        //    {
        //        aSource.PlayOneShot(wrongButton, 0.5f);
        //        //Handheld.Vibrate();
        //        LivesCounter++; flashOnce = false;

        //        button4Pressed = true;
        //    }
        //    if (stage3Buttons)
        //    {
        //        aSource.PlayOneShot(correctButton, 0.5f);
        //        buttonCount++;
        //        oneHasBeen = true;
        //        button4Pressed = true;
        //    }
        //}

        if (pickedCombination == Combinations.NUM28)
        {
            if (stage1Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button4Pressed = true;
            }
            if (stage2Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button4Pressed = true;
            }
            if (stage3Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button4Pressed = true;
            }
        }

        //if (pickedCombination == Combinations.NUM29)
        //{
        //    if (stage1Buttons)
        //    {
        //        aSource.PlayOneShot(correctButton, 0.5f);
        //        buttonCount++;
        //        oneHasBeen = true;
        //        button4Pressed = true;
        //    }
        //    if (stage2Buttons)
        //    {
        //        aSource.PlayOneShot(wrongButton, 0.5f);
        //        //Handheld.Vibrate();
        //        LivesCounter++; flashOnce = false;

        //        button4Pressed = true;
        //    }
        //    if (stage3Buttons)
        //    {
        //        aSource.PlayOneShot(correctButton, 0.5f);
        //        buttonCount++;
        //        oneHasBeen = true;
        //        button4Pressed = true;
        //    }
        //}

        if (pickedCombination == Combinations.NUM30)
        {
            if (stage1Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button4Pressed = true;
            }
            if (stage2Buttons)
            {
                aSource.PlayOneShot(wrongButton, 0.5f);
                //Handheld.Vibrate();
                LivesCounter++; flashOnce = false;

                button4Pressed = true;
            }
            if (stage3Buttons)
            {
                aSource.PlayOneShot(correctButton, 0.5f);
                buttonCount++;
                oneHasBeen = true;
                button4Pressed = true;
            }
            //if (stage4Buttons)
            //{
            //    aSource.PlayOneShot(wrongButton, 0.5f);
            //    //Handheld.Vibrate();
            //    LivesCounter++; flashOnce = false;

            //    button4Pressed = true;
            //}
        }
    }

#endregion

    IEnumerator WaitToLerp()
    {
        gameManagerScript.tutorial = false;
        tutorialPannel.SetActive(false);
        if (correctAns.enabled == true)
            if(!lerpOnceLerp)
            {
                gameManagerScript.fourToWin++;
                gameManagerScript.counterToLerp++;
                lerpOnceLerp = true;
            }
        yield return new WaitForSeconds(2);
        gameManagerScript.MovingCamera(0);
        gameManagerScript.puzzle3 = true;
        Destroy(gameObject, 1);
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
