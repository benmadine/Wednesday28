using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BatteryPuzzle : MonoBehaviour
{
    //Maths puzzle script
    MathsPuzzle mathPuzzleScript;

    //list of batteries
    Sprite[] differentBatteries;

    //Stage1, 2, 3
    public enum batteryStages { stage1, stage2, stage3}
    public batteryStages pickedBatteryStage;

    //Stages Combos
    enum stage1BatCom { stg1, stg2, stg3, stg4, stg5}
    stage1BatCom stage1Goal;
    enum stage2BatCom { stg1, stg2, stg3, stg4, stg5 }
    stage2BatCom stage2Goal;
    enum stage3BatCom { stg1, stg2, stg3, stg4, stg5 }
    stage3BatCom stage3Goal;

    //List of BatteryPos
    SpriteRenderer battery1, battery2, battery3, battery4, battery5;

    //Answer
    int answer;
    int current = 240;
    Text answerText;

    //bool, moveOnTo1 is the first maths puzzle, 2nd is econd maths puzzle and 3rd is 3rd maths
    public bool moveOnToPuzzle1 = false, moveOnToPuzzle2 = false, moveOnToPuzzle3 = false;

    //run once
    bool runOnce1, runOnce2;

    public SpriteRenderer door;

    bool confirmVoltage;

    //Lives
    public int counter = 0;
    Image livesImage;
    public Sprite[] possibleImages;

    //Audio
    public AudioClip wrongNoise, correctNoise, buttonPress;
    AudioSource aSource;

    //Alarm Flahs
    public GameObject alarmFlash;
    bool flashOnce;

    void ImageHolders()
    {
        battery1 = GameObject.Find("BatterySlot1").GetComponent<SpriteRenderer>();
        battery2 = GameObject.Find("BatterySlot2").GetComponent<SpriteRenderer>();
        battery3 = GameObject.Find("BatterySlot3").GetComponent<SpriteRenderer>();
        battery4 = GameObject.Find("BatterySlot4").GetComponent<SpriteRenderer>();
        battery5 = GameObject.Find("BatterySlot5").GetComponent<SpriteRenderer>();
    }

    // Use this for initialization
    void Start ()
    {
        ImageHolders();

        aSource = GameObject.FindGameObjectWithTag("MathsPuzzle").GetComponent<AudioSource>();

        mathPuzzleScript = GameObject.FindGameObjectWithTag("MathsPuzzle").GetComponent<MathsPuzzle>();

        answerText = GameObject.Find("AnswerText").GetComponent<Text>();
        differentBatteries = Resources.LoadAll<Sprite>("Batteries");

        livesImage = GameObject.Find("Circle").GetComponent<Image>();

        pickedBatteryStage = batteryStages.stage1;
        stage1Goal = (stage1BatCom)Random.Range(0, 5);
    }
	
	// Update is called once per frame
	void Update ()
    {
        UpdateLives();
        if(mathPuzzleScript.switch1)  // 2nd battery puzzle
        {      
            if (!runOnce1)
            {
                door.enabled = false;
                pickedBatteryStage = batteryStages.stage2;
                stage2Goal = (stage2BatCom)Random.Range(0, 5);
                runOnce1 = true;
            }
            Stage2();
        }
        if(mathPuzzleScript.switch2)
        {
            if (!runOnce2)
            {
                door.enabled = false;
                mathPuzzleScript.switch1 = false;
                pickedBatteryStage = batteryStages.stage3;
                stage3Goal = (stage3BatCom)Random.Range(0, 5);
                runOnce2 = true;
            }
            Stage3();
        }
        if (!mathPuzzleScript.runStart)
        {
            door.enabled = false;
            Stage1();
        }
        if (counter >= 3)
            mathPuzzleScript.CallLerp();
            
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
            mathPuzzleScript.boolWrongAns = true;
        }
    }

    void Stage1()
    {
        if (stage1Goal == stage1BatCom.stg1)
        {
            battery1.sprite = differentBatteries[1];
            battery2.sprite = differentBatteries[1];
            battery3.sprite = differentBatteries[1];
            answer = 80;
            if (current == answer && confirmVoltage && confirmVoltage)
            {
                current = 240;
                moveOnToPuzzle1 = true;
                door.enabled = true;
            }
        }
        if (stage1Goal == stage1BatCom.stg2)
        {
            battery1.sprite = differentBatteries[1];
            battery2.sprite = differentBatteries[0];
            battery3.sprite = differentBatteries[0];
            answer = 80;
            if (current == answer && confirmVoltage && confirmVoltage)
            {
                current = 240;
                moveOnToPuzzle1 = true;
                door.enabled = true;
            }
        }
        if (stage1Goal == stage1BatCom.stg3)
        {
            battery1.sprite = differentBatteries[2];
            battery2.sprite = differentBatteries[1];
            battery3.sprite = differentBatteries[0];
            answer = 120;
            if (current == answer && confirmVoltage)
            {
                current = 240;
                moveOnToPuzzle1 = true;
                door.enabled = true;
            }
        }
        if (stage1Goal == stage1BatCom.stg4)
        {
            battery1.sprite = differentBatteries[2];
            battery2.sprite = differentBatteries[2];
            battery3.sprite = differentBatteries[1];
            answer = 120;
            if (current == answer && confirmVoltage)
            {
                current = 240;
                moveOnToPuzzle1 = true;
                door.enabled = true;
            }
        }
        if (stage1Goal == stage1BatCom.stg5)
        {
            battery1.sprite = differentBatteries[0];
            battery2.sprite = differentBatteries[0];
            battery3.sprite = differentBatteries[0];
            answer = 180;
            if (current == answer && confirmVoltage)
            {
                current = 240;
                moveOnToPuzzle1 = true;
                door.enabled = true;
            }
        }
        
    }
    
    void Stage2()
    {
        if(stage2Goal == stage2BatCom.stg1)
        {
            battery1.sprite = differentBatteries[0];
            battery2.sprite = differentBatteries[0];
            battery3.sprite = differentBatteries[0];
            battery4.sprite = differentBatteries[0];
            answer = 100;
            if (current == answer && confirmVoltage)
            {
                current = 240;
                moveOnToPuzzle2 = true;
                door.enabled = true;
            }
        }
        if(stage2Goal == stage2BatCom.stg2)
        {
            battery1.sprite = differentBatteries[1];
            battery2.sprite = differentBatteries[1];
            battery3.sprite = differentBatteries[0];
            battery4.sprite = differentBatteries[0];
            answer = 100;
            if (current == answer && confirmVoltage)
            {
                current = 240;
                moveOnToPuzzle2 = true;
                door.enabled = true;
            }
        }
        if(stage2Goal == stage2BatCom.stg3)
        {
            battery1.sprite = differentBatteries[1];
            battery2.sprite = differentBatteries[1];
            battery3.sprite = differentBatteries[1];
            battery4.sprite = differentBatteries[0];
            answer = 140;
            if (current == answer && confirmVoltage)
            {
                current = 240;
                moveOnToPuzzle2 = true;
                door.enabled = true;
            }
        }
        if(stage2Goal == stage2BatCom.stg4)
        {
            battery1.sprite = differentBatteries[0];
            battery2.sprite = differentBatteries[0];
            battery3.sprite = differentBatteries[1];
            battery4.sprite = differentBatteries[2];
            answer = 140;
            if (current == answer && confirmVoltage)
            {
                current = 240;
                moveOnToPuzzle2 = true;
                door.enabled = true;
            }
        }
        if(stage2Goal == stage2BatCom.stg5)
        {
            battery1.sprite = differentBatteries[2];
            battery2.sprite = differentBatteries[2];
            battery3.sprite = differentBatteries[0];
            battery4.sprite = differentBatteries[0];
            answer = 180;
            if (current == answer && confirmVoltage)
            {
                current = 240;
                moveOnToPuzzle2 = true;
                door.enabled = true;
            }
        }
    }

    void Stage3()
    {
        if(stage3Goal == stage3BatCom.stg1)
        {
            battery1.sprite = differentBatteries[0];
            battery2.sprite = differentBatteries[0];
            battery3.sprite = differentBatteries[0];
            battery4.sprite = differentBatteries[0];
            battery5.sprite = differentBatteries[0];
            answer = 160;
            if (current == answer && confirmVoltage)
            {
                current = 240;
                moveOnToPuzzle3 = true;
                door.enabled = true;
            }
        }
        if(stage3Goal == stage3BatCom.stg2)
        {
            battery1.sprite = differentBatteries[2];
            battery2.sprite = differentBatteries[2];
            battery3.sprite = differentBatteries[0];
            battery4.sprite = differentBatteries[0];
            battery5.sprite = differentBatteries[1];
            answer = 160;
            if (current == answer && confirmVoltage)
            {
                current = 240;
                moveOnToPuzzle3 = true;
                door.enabled = true;
            }
        }
        if(stage3Goal == stage3BatCom.stg3)
        {
            battery1.sprite = differentBatteries[0];
            battery2.sprite = differentBatteries[0];
            battery3.sprite = differentBatteries[1];
            battery4.sprite = differentBatteries[1];
            battery5.sprite = differentBatteries[2];
            answer = 200;
            if (current == answer && confirmVoltage)
            {
                current = 240;
                moveOnToPuzzle3 = true;
                door.enabled = true;
            }
        }
        if(stage3Goal == stage3BatCom.stg4)
        {
            battery1.sprite = differentBatteries[2];
            battery2.sprite = differentBatteries[2];
            battery3.sprite = differentBatteries[2];
            battery4.sprite = differentBatteries[1];
            battery5.sprite = differentBatteries[0];
            answer = 200;
            if (current == answer && confirmVoltage)
            {
                current = 240;
                moveOnToPuzzle3 = true;
                door.enabled = true;
            }
        }
        if(stage3Goal == stage3BatCom.stg5)
        {
            battery1.sprite = differentBatteries[1];
            battery2.sprite = differentBatteries[1];
            battery3.sprite = differentBatteries[1];
            battery4.sprite = differentBatteries[0];
            battery5.sprite = differentBatteries[0];
            answer = 180;
            if (current == answer && confirmVoltage)
            {
                current = 240;
                moveOnToPuzzle3 = true;
                door.enabled = true;
            }
        }
      }
    
    public void PlusButton()
    {
        current += 10;
        answerText.text = current + "V";
        aSource.PlayOneShot(buttonPress, 0.5f);
        confirmVoltage = false;
    }

    public void MinusButton()
    {
        current -= 10;
        if (current < 0)
            current = 0;
        answerText.text = current + "V";
        aSource.PlayOneShot(buttonPress, 0.5f);
        confirmVoltage = false;

    }

    public void ConfirmButton()
    {
        if (current != answer)
        {
            counter++;
            aSource.PlayOneShot(wrongNoise, 0.5f);
            Handheld.Vibrate();
            flashOnce = false;
        }
        else
        {
            aSource.PlayOneShot(correctNoise, 0.5f);
            confirmVoltage = true;
        }

    }

    IEnumerator AlarmFlash()
    {
        if(!flashOnce)
        {
            alarmFlash.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            alarmFlash.SetActive(false);
            flashOnce = true;
        }
    }
}
