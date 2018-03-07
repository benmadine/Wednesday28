using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Touch;
using UnityEngine.SceneManagement;

public class ManualManager : MonoBehaviour {

    Vector2 swipe;
    int index;
    GameObject[] manualList;

    private void OnEnable()
    {
        LeanTouch.OnFingerSwipe += OnFingerSwipe;
    }

    private void OnDisable()
    {
        LeanTouch.OnFingerSwipe -= OnFingerSwipe;
    }

    void Start()
    {
        manualList = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            manualList[i] = transform.GetChild(i).gameObject;
        }
        foreach (GameObject manual in manualList)
        {
            manual.SetActive(false);
        }
        if (manualList[index])
        {
            manualList[index].SetActive(true);
            manualList[index].transform.localPosition = new Vector2(0, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnFingerSwipe(LeanFinger finger)
    {
        swipe = finger.SwipeScreenDelta;
        if (swipe.x > -Mathf.Abs(swipe.y))
        {
            manualList[index].SetActive(false);
            index--;
            if (index < 0)
            {
                index = manualList.Length - 1;
            }
            manualList[index].SetActive(true);
            manualList[index].transform.localPosition = new Vector2(0, 0);
        }
        if (swipe.x < Mathf.Abs(swipe.y))
        {
            //Right
            manualList[index].SetActive(false);
            index++;
            if (index == manualList.Length)
            {
                index = 0;
            }
            manualList[index].SetActive(true);
            manualList[index].transform.localPosition = new Vector2(0, 0);
        }
    }

    public void QuitingTheGame()
    {
        Application.Quit();
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("PlayerInstructions");
    }
}
