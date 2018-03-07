using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreSaving : MonoBehaviour
{
    public float Score;


    public float round1Scores;

    public float round2Scores;

    public bool round;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!round)
        {
            round1Scores = Score;
        }
        if (round)
        {
            round2Scores = Score;
        }
    }
}
