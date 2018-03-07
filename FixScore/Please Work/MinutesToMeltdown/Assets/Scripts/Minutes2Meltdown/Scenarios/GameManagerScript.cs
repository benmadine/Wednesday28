using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    GameObject _camera;
    Button _puzzle1Button, _puzzle2Button, _puzzle3Button, _puzzle4Button;
    public Vector3 cameraPosition;
    float lerpSpeed = 0.1f;
    public bool buttonPressed;
    bool Button1pressed = false;
    bool Button2pressed = false;
    bool Button3pressed = false;

    AudioSource buttonClick, dontDestroyASource;

    //puzzle games
    public bool puzzle1, puzzle2, puzzle3, puzzle4;

    bool playOnce;

    public int counterToLerp;

    //For swapping images, buttons etc
    public Image background;
    public GameObject buttonsDisappear, buttonsAppear;
    public Sprite reactorBackground;

    //WinConditions
    public int fourToWin = 0;

    //tutorials
    public bool tutorial;
    public GameObject tutorialPannel;

    public GameObject tutorialInstructionPannel;


    // Use this for initialization
    private void Start ()
    {
        FindItems();
        _camera.transform.position = new Vector3(0, 0, -10);
    }
	
	// Update is called once per frame
	private void Update ()
    {
        _camera.transform.position = Vector3.Lerp(_camera.transform.position, cameraPosition, lerpSpeed);
        //ReactivateButtons();
        if (counterToLerp >= 3)
        {
            StartCoroutine(PicutreSwap());
            if (fourToWin != 4 && counterToLerp >= 4)
            {
                Debug.Log("Lost Game");
                StartCoroutine(TransferScene());
            }
            else if (counterToLerp >= 4 && fourToWin >= 4)
            {
                Debug.Log("Won Game");
                StartCoroutine(WinGameScreen());
            }
        }
        if(tutorialInstructionPannel)
        {
            if (tutorial)
            {
                TutorialActivated();
            }
        }
    }

    public void MovingCamera(int cameraVector)
    {
        switch(cameraVector)
        {
            default:
            case 0:
                cameraPosition = new Vector3(0, 0, -10f);
                break;
            case 1:
                cameraPosition = new Vector3(0, -460, -10);
                break;
        }
    }

    private void FindItems()
    {
        dontDestroyASource = GameObject.Find("DontDestroyOnLoad").GetComponent<AudioSource>();
        buttonClick = GameObject.Find("GameManager").GetComponent<AudioSource>();
        _camera = GameObject.FindGameObjectWithTag("MainCamera");
        _puzzle1Button = GameObject.FindGameObjectWithTag("Puzzle1Button").GetComponent<Button>();
        _puzzle2Button = GameObject.FindGameObjectWithTag("Puzzle2Button").GetComponent<Button>();
        _puzzle3Button = GameObject.FindGameObjectWithTag("Puzzle3Button").GetComponent<Button>();
    }

    private void ButtonPressed()
    {
        buttonClick.Play();
        MovingCamera(1);        
    }    

    public void TutorialYes()
    {
        tutorial = true;
        Destroy(tutorialPannel);
    }

    public void TutorialNo()
    {
        tutorial = false;
        Destroy(tutorialPannel);
    }

    private void TutorialActivated()
    {
        tutorialInstructionPannel.SetActive(true);
    }

    #region PuzzlePressed
    public void Puzzle1()
    {
        if(!buttonPressed)
        {
            ButtonPressed();
            buttonPressed = true;
            _puzzle1Button.GetComponent<Button>().enabled = false;
            dontDestroyASource.pitch += 0.2f;
            Button1pressed = true;
            RemoveTutorialButtonInstructions();
        }
    }

    public void Puzzle2()
    {
        if (!buttonPressed)
        {
            ButtonPressed();
            buttonPressed = true;
            _puzzle2Button.GetComponent<Button>().enabled = false;
            dontDestroyASource.pitch += 0.2f;
            Button2pressed = true;
            RemoveTutorialButtonInstructions();
        }
    }

    public void Puzzle3()
    {
        if (!buttonPressed)
        {
            ButtonPressed();
            buttonPressed = true;
            _puzzle3Button.GetComponent<Button>().enabled = false;
            dontDestroyASource.pitch += 0.2f;
            Button3pressed = true;
            RemoveTutorialButtonInstructions();
        }
    }

    public void Puzzle4()
    {
        if (!buttonPressed)
        {
            ButtonPressed();
            buttonPressed = true;
            _puzzle4Button.GetComponent<Button>().enabled = false;
            dontDestroyASource.pitch += 0.2f;
        }
    }

    private void RemoveTutorialButtonInstructions()
    {
        Destroy(tutorialInstructionPannel);
    }
    #endregion

    IEnumerator TransferScene()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("GameOverScene");
    }

    IEnumerator PicutreSwap()
    {
        yield return new WaitForSeconds(0.5f);
        buttonsAppear.SetActive(true);
        _puzzle4Button = GameObject.FindGameObjectWithTag("Puzzle4Button").GetComponent<Button>();
        background.sprite = reactorBackground;
        buttonsDisappear.SetActive(false);
    }

    IEnumerator WinGameScreen()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("GameWinScene");
    }
}
