using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPuzzlePicker : MonoBehaviour
{
    GameManagerScript gameManagerScript;
    GameObject selectedPuzzle, spawnSelected;

    public GameObject[] playablePuzzles;
    List<GameObject> puzzles;
    public int heldInt;


    // Use this for initialization
    void Start ()
    {
        playablePuzzles = Resources.LoadAll<GameObject>("PrefabPuzzles");
        puzzles = new List<GameObject>();
        for(int i = 0; i < playablePuzzles.Length; i++)
        {
            puzzles.Add(playablePuzzles[i]);
        }
        gameManagerScript = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManagerScript>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (gameManagerScript.buttonPressed == true)
        {
            PuzzlePicker();          
            gameManagerScript.buttonPressed = false;
        }
    } 

    void PuzzlePicker()
    {
            heldInt = (int)Random.Range(0, puzzles.Count);
            selectedPuzzle = puzzles[heldInt];
            spawnSelected = Instantiate(selectedPuzzle, new Vector3(0, -460, 0), Quaternion.identity);
            puzzles.RemoveAt(heldInt);
    }
}