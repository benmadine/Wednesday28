using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InstructionsButtons : MonoBehaviour
{

    public void Instructions1()
    {
        SceneManager.LoadScene("ScenarioInstructions1");
    }

    public void Instructions2()
    {
        SceneManager.LoadScene("ScenarioInstructions2");
    }

    public void Instructions3()
    {
        SceneManager.LoadScene("ScenarioInstructions3");
    }

    public void Instructions4()
    {
        SceneManager.LoadScene("ScenarioInstructions4");
    }

#region Main menu and quit game
    public void BackToMenu()
    {
        SceneManager.LoadScene("StartScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
#endregion
}
