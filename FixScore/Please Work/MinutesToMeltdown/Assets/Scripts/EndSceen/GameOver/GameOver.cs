﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void MainMenu()
    {
        SceneManager.LoadScene("StartScene");
    }

    public void QuitMenu()
    {
        Application.Quit();
    }
}
