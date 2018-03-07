using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogueScript : MonoBehaviour {

    Text dialogueText;
    float currentTime, maxTime =20f;

    // Use this for initialization
    void Start()
    {
        dialogueText = GameObject.Find("Text").GetComponent<Text>();
        currentTime = maxTime;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime -= Time.deltaTime;
        TextChange();

    }

    void TextChange()
    {
        if (currentTime < maxTime)
            dialogueText.text = "This is Moby.";

        if (currentTime <= 18)
            dialogueText.CrossFadeAlpha(0.0f, 1f, false);

        if (currentTime <= 15)
        {
            dialogueText.CrossFadeAlpha(2.0f, 1f, false);
            dialogueText.text = "Or more specifically, this is you.";
        }

        if (currentTime <= 13)
        {
            dialogueText.text = "And this is Moby's mind.";
        }

        if (currentTime <= 11)
        {
            dialogueText.text = "Now Moby's mind is full of characters, you could even say they are his 'friends', always arguing about what's best for Moby.";
        }

        if (currentTime <= 6)
        {
            dialogueText.text = "Today however, is Moby's first day at his new job.";
        }

        if (currentTime <= 4)
        {
            dialogueText.text = "And lets just say...";
        }

        if(currentTime <= 2)
        {
            dialogueText.text = "It's not going well.";
        }

        if (currentTime <= 0)
            SceneManager.LoadScene("PlayerScenario1");

    }

    public void SkipButton()
    {
        SceneManager.LoadScene("PlayerScenario1");
    }
}
