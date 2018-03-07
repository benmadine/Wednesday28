using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MathsPuzzle : MonoBehaviour
{
    //Maths Puzzle Part
    int randomMathNumber;
    Text randomNumberText;

    //Buttons have been clicked
    [HideInInspector]
    public bool yellowButton, blueButton, greenButton, redButton, boolCorrectAns, boolWrongAns;

    //Check if puzzle is done
    [HideInInspector]
    public bool puzzleDone;
    
    //Display if correct or wrong ans
    SpriteRenderer correctAns, wrongAns;

    //Reference sceipt
    GameManagerScript gameManagerScript;

    //Audio 
    [HideInInspector]
    public AudioClip wonNoise, lostNoise;
    AudioSource aSource;

    //Stage Bools
    [HideInInspector]
    public bool stage1, stage2, stage3;
    enum Stages { stage1, stage2, stage3 }
    Stages pickedStage;

    //stage2 combobs
    enum stage2Combos { RB = 1, BY = 2, YG = 3, GR = 4 }
    stage2Combos stage2Combonations;

    //Stage3 combos
    enum stage3Combos { RBB = 1, BYG = 2, GRR = 3, YRB = 4}
    stage3Combos stage3Combinations;

    //Counter for Lives
    int counter = 0;

    //If buttons is right button
    [HideInInspector]
    public bool redButtonCorrect, blueButtonCorrect, yellowButtonCorrect, greenButtonCorrect;

    //Only run once pleease
    [HideInInspector]
    public bool runOnce1, runOnce2, runStart;

    //ClickedCounter, how many times clicked
    int clickedCounter = 0;

    //Battery Script
    BatteryPuzzle batteryScript;

    //battery bools
    [HideInInspector]
    public bool switch1, switch2;

    //Int array, options for the math puzzle
    int[] mathAnswers = new int[8];

    //Bool for lerp, to only lerp once
    bool lerpOnceLerp, onceForAudio;

    //Stage Flash
    [HideInInspector]
    public GameObject stageFlash;
    [HideInInspector]
    public bool stageFlashOnce;

    //Movinf to end scene
    bool addOne;

    void MathAns()
    {
        mathAnswers[0] = 1;
        mathAnswers[1] = 2;
        mathAnswers[2] = 3;
        mathAnswers[3] = 4;
        mathAnswers[4] = 10;
        mathAnswers[5] = 12;
        mathAnswers[6] = 13;
        mathAnswers[7] = 14;
    }

    void Finding()
    {
        batteryScript = GameObject.FindGameObjectWithTag("MathsPuzzle").GetComponent<BatteryPuzzle>();
        aSource = GameObject.FindGameObjectWithTag("MathsPuzzle").GetComponent<AudioSource>();
        gameManagerScript = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManagerScript>();
        correctAns = GameObject.FindGameObjectWithTag("Passed").GetComponent<SpriteRenderer>();
        wrongAns = GameObject.FindGameObjectWithTag("Lost").GetComponent<SpriteRenderer>();
        randomNumberText = GameObject.FindGameObjectWithTag("MathPuzzleNumber").GetComponent<Text>();
    }
    // Use this for initialization
    void Start()
    {
        MathAns();
        Finding();        
    }

    // Update is called once per frame
    void Update()
    {
        if (batteryScript.moveOnToPuzzle1)
        {
            if(!runStart)
            {
                StartCoroutine(StageFlash());
                pickedStage = Stages.stage1;
                randomMathNumber = mathAnswers[Random.Range(0, 8)];
                randomNumberText.text = "" + randomMathNumber;
                runStart = true;
                stageFlashOnce = true;
            }
            
        }
        if(batteryScript.moveOnToPuzzle2)
        {
            if (stage2 == true)
            {
                if (!runOnce1)
                {
                    stageFlashOnce = false;
                    StartCoroutine(StageFlash());
                    redButtonCorrect = false; blueButtonCorrect = false; yellowButtonCorrect = false; greenButtonCorrect = false;
                    redButton = false; blueButton = false; greenButton = false; yellowButton = false;

                    clickedCounter = 0;
                    pickedStage = Stages.stage2;

                    stage2Combonations = (stage2Combos)Random.Range(1, 5);
                    print(stage2Combonations);

                    SelectionNumber();

                    runOnce1 = true;
                    
                }
            }
        }
        if(batteryScript.moveOnToPuzzle3)
        {
            if (stage3 == true)
            {
                if (!runOnce2)
                {
                    stageFlashOnce = false;
                    StartCoroutine(StageFlash());
                    redButtonCorrect = false; blueButtonCorrect = false; yellowButtonCorrect = false; greenButtonCorrect = false;
                    redButton = false; blueButton = false; greenButton = false; yellowButton = false;

                    clickedCounter = 0;

                    stage3Combinations = (stage3Combos)Random.Range(1, 5);
                    print(stage3Combinations);

                    pickedStage = Stages.stage3;

                    SelectionNumber();

                    runOnce2 = true;
                }
            }
        }      
        CheckerMeth();
        Results();
    }

    void SelectionNumber()
    {
        if(pickedStage == Stages.stage2)
        {
            if ((int)stage2Combonations == 1) //If the combination is Red Blue
            {
                randomMathNumber = 2;
            }
            if ((int)stage2Combonations == 2) //Combination = blue yellow
            {
                randomMathNumber = 1;
            }
            if ((int)stage2Combonations == 3) //Combination = yellow gtreen
            {
                int[] converter;
                converter = new int[3];
                converter[0] = 3;
                converter[1] = 10;
                converter[2] = 4;
                randomMathNumber = converter[Random.Range(0, 3)];
            }
            if ((int)stage2Combonations == 4) // combination = green red
            {
                int[] converter;
                converter = new int[3];
                converter[1] = 12;
                converter[2] = 13;
                converter[3] = 14;
                randomMathNumber = converter[Random.Range(0, 3)];
            }
            randomNumberText.text = "" + randomMathNumber;
        }

        if(pickedStage == Stages.stage3)
        {
            if ((int)stage3Combinations == 1) //If the combination is Red Blue
            {
                randomMathNumber = 1;
            }
            if ((int)stage3Combinations == 2) //Combination = blue yellow
            {
                int[] converter;
                converter = new int[2];
                converter[0] = 2;
                converter[2] = 13;
                randomMathNumber = converter[Random.Range(0, 2)];
            }
            if ((int)stage3Combinations == 3) //Combination = yellow gtreen
            {
                int[] converter;
                converter = new int[4];
                converter[0] = 3;
                converter[1] = 10;
                converter[2] = 4;
                converter[3] = 14;
                randomMathNumber = converter[Random.Range(0, 4)];
            }
            if ((int)stage3Combinations == 4) // combination = green red
            {
                randomMathNumber = 12;
            }
            randomNumberText.text = "" + randomMathNumber;
        }
    }

    void Results()
    {
        if (boolCorrectAns == true)
        {
            correctAns.enabled = true;
            StartCoroutine(WaitToLerp());         
            puzzleDone = true;
            boolCorrectAns = false;
            if (!lerpOnceLerp)
            {
                aSource.PlayOneShot(wonNoise, 0.5f);
                gameManagerScript.fourToWin++;
                lerpOnceLerp = true;
            }
        }
        if (boolWrongAns == true)
        {
            if(!onceForAudio)
            {
                aSource.PlayOneShot(lostNoise, 0.5f);
                onceForAudio = true;
            }
            wrongAns.enabled = true;
            StartCoroutine(WaitToLerp());
            puzzleDone = true;
            boolWrongAns = false;
        }
    }

    public void RedButtonCombs()
    {
        if(pickedStage == Stages.stage1)
        {
            if (randomMathNumber == 1)
            {
                switch1 = true;
                stage2 = true;
            }
            else
            {
                boolWrongAns = true;
            }
        } 
        
        if(pickedStage == Stages.stage2)
        {
            if(stage2Combonations == stage2Combos.RB)
            {
                if (randomMathNumber == 2)
                {
                    redButton = true;
                }
            }  
            
            if(stage2Combonations == stage2Combos.GR)
            {
                if ((randomMathNumber == 12 || randomMathNumber == 13 || randomMathNumber == 14) /*&& greenButton == true*/)
                {
                    redButton = true;
                }
            }

            if(stage2Combonations == stage2Combos.BY || stage2Combonations == stage2Combos.YG)
            {
                boolWrongAns = true;
            }
        }

        if (pickedStage == Stages.stage3)
        {
            if (stage3Combinations == stage3Combos.RBB)
            {
                if (randomMathNumber == 1)
                {
                    redButton = true;
                }
            }

            if (stage3Combinations == stage3Combos.GRR)
            {
                counter++;
                if ((randomMathNumber == 3 || randomMathNumber == 4 || randomMathNumber == 10 || randomMathNumber == 14))
                {
                    redButton = true;
                }
            }

            if (stage3Combinations == stage3Combos.YRB)
            {
                if ((randomMathNumber == 12) /*&& yellowButton == true*/)
                {
                    redButton = true;
                }
            }

            if (stage3Combinations == stage3Combos.BYG)
            {
                boolWrongAns = true;
            }
        }
    }

    public void BlueButtonCombs()
    {
        if (pickedStage == Stages.stage1)
        {
            if (randomMathNumber == 3 || randomMathNumber == 4 || randomMathNumber == 10)
            {
                switch1 = true;
                stage2 = true;
            }
            else
            {
                boolWrongAns = true;
            }
        }

        if (pickedStage == Stages.stage2)
        {
            if (stage2Combonations == stage2Combos.RB)
            {
                if ((randomMathNumber == 2 || randomMathNumber == 9))
                {
                    blueButton = true;
                }
            }

            if (stage2Combonations == stage2Combos.BY)
            {
                if ((randomMathNumber == 1))
                {
                    blueButton = true;
                }
            }

            if(stage2Combonations == stage2Combos.YG || stage2Combonations == stage2Combos.GR)
            {
                boolWrongAns = true;
            }
        }

        if (pickedStage == Stages.stage3)
        {
            if (stage3Combinations == stage3Combos.RBB)
            {
                clickedCounter++;
                if ((randomMathNumber == 1))
                {
                    blueButton = true;
                }
            }

            if (stage3Combinations == stage3Combos.BYG)
            {
                if (randomMathNumber == 2 || randomMathNumber == 13)
                {
                    blueButton = true;
                }
            }

            if (stage3Combinations == stage3Combos.YRB)
            {
                if ((randomMathNumber == 12))
                {
                    blueButton = true;
                }
            }

            if(stage3Combinations == stage3Combos.GRR)
            {
                boolWrongAns = true;
            }
        }
    }

    public void YellowButtonCombs()
    {
        if(pickedStage == Stages.stage1)
        {
            if (randomMathNumber == 2 || randomMathNumber == 12 || randomMathNumber == 14)
            {
                switch1 = true;
                stage2 = true;
            }
            else
            {
                boolWrongAns = true;
            }
        }

        if (pickedStage == Stages.stage2)
        {
            if (stage2Combonations == stage2Combos.BY)
            {
                if ((randomMathNumber == 1))
                {
                    yellowButton = true;
                }
            }

            if (stage2Combonations == stage2Combos.YG)
            {
                if ((randomMathNumber == 3 || randomMathNumber == 10 || randomMathNumber == 4))
                {
                    yellowButton = true;
                }
            }

            if(stage2Combonations == stage2Combos.GR || stage2Combonations == stage2Combos.RB)
            {
                boolWrongAns = true;
            }
        }

        if (pickedStage == Stages.stage3)
        {
            if (stage3Combinations == stage3Combos.BYG)
            {
                if ((randomMathNumber == 2 || randomMathNumber == 13))
                {
                    yellowButton = true;
                }
            }

            if (stage3Combinations == stage3Combos.YRB)
            {
                if (randomMathNumber == 12)
                {
                    yellowButton = true;
                }
            }

            if(stage3Combinations == stage3Combos.GRR || stage3Combinations == stage3Combos.RBB)
            {
                boolWrongAns = true;
            }
        }
    }        

    public void GreenButtonCombs()
    {
        if (pickedStage == Stages.stage1)
        {
            if (randomMathNumber == 13)
            {
                switch1 = true;
                stage2 = true;
            }
            else
            {
                boolWrongAns = true;
            }
        }

        if (pickedStage == Stages.stage2)
        {
            if (stage2Combonations == stage2Combos.YG)
            {
                if ((randomMathNumber == 3 || randomMathNumber == 10 || randomMathNumber == 4))
                {
                    greenButton = true;
                }
            }

            if (stage2Combonations == stage2Combos.GR)
            {
                if ((randomMathNumber == 12 || randomMathNumber == 13 || randomMathNumber == 14))
                {
                    greenButton = true;
                }
            }

            if(stage2Combonations == stage2Combos.BY || stage2Combonations == stage2Combos.RB)
            {
                boolWrongAns = true;
            }
        }

        if (pickedStage == Stages.stage3)
        {
            if (stage3Combinations == stage3Combos.BYG)
            {
                if ((randomMathNumber == 2 || randomMathNumber == 13))
                {
                    greenButton = true;
                }
            }

            if (stage3Combinations == stage3Combos.GRR)
            {
                if (randomMathNumber == 3 || randomMathNumber == 4 || randomMathNumber == 10 || randomMathNumber == 14)
                {
                    greenButton = true;
                }
            }

            if(stage3Combinations == stage3Combos.RBB || stage3Combinations == stage3Combos.BYG)
            {
                boolWrongAns = true;
            }
        }
    }

    void CheckerMeth()
    {
        if (pickedStage == Stages.stage2)
        {
            if (stage2Combonations == stage2Combos.BY)
            {
                if(blueButton == true && yellowButton == true)
                {
                    switch2 = true;
                    stage3 = true;
                }
            }
            if(stage2Combonations == stage2Combos.GR)
            {
                if(greenButton == true && redButton == true)
                {
                    switch2 = true;
                    stage3 = true;
                }
            }
            if(stage2Combonations == stage2Combos.RB)
            {
                if(redButton == true && blueButton == true)
                {
                    switch2 = true;
                    stage3 = true;
                }
            }
            if(stage2Combonations == stage2Combos.YG)
            {
                if(yellowButton == true && greenButton == true)
                {
                    switch2 = true;
                    stage3 = true;
                }
            }
        }

        if (pickedStage == Stages.stage3)
        {
            if(stage3Combinations == stage3Combos.BYG)
            {
                if (blueButton == true && yellowButton == true && greenButton == true)
                    boolCorrectAns = true;
            }
            if(stage3Combinations == stage3Combos.GRR)
            {
                if (greenButton == true && redButton == true /*&& clickedCounter >= 2*/)
                    boolCorrectAns = true;                
            }
            if(stage3Combinations == stage3Combos.RBB)
            {
                if (redButton == true && blueButton == true /*&& clickedCounter >= 2*/)
                    boolCorrectAns = true;
            }
            if(stage3Combinations == stage3Combos.YRB)
            {
                if (yellowButton == true && blueButton == true && redButton == true)
                    boolCorrectAns = true;
            }
        }
    }

    IEnumerator WaitToLerp()
    {
        yield return new WaitForSeconds(2);
        gameManagerScript.MovingCamera(0);
        gameManagerScript.puzzle2 = true;
        Destroy(gameObject, 1);
        if (!addOne)
        {
            gameManagerScript.counterToLerp++;
            addOne = true;
        }
    }

    public void CallLerp()
    {
        StartCoroutine(WaitToLerp());
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