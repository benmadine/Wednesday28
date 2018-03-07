using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyCardManager : MonoBehaviour {

    int index;
    GameObject[] keyCardList;

    public AudioClip buttonSwap;
    AudioSource aSource;

    // Use this for initialization
    void Start ()
    {
        aSource = GameObject.FindGameObjectWithTag("KeyCardPuzzle").GetComponent<AudioSource>();
        keyCardList = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            keyCardList[i] = transform.GetChild(i).gameObject;
        }
        foreach (GameObject keyCard in keyCardList)
        {
            keyCard.SetActive(false);
        }
        if (keyCardList[index])
        {
            keyCardList[index].SetActive(true);
            keyCardList[index].transform.localPosition = new Vector2(-69, -41);
        }
    }

    public void LeftArrow()
    {
        aSource.PlayOneShot(buttonSwap, 0.5f);
        keyCardList[index].SetActive(false);
        index--;
        if (index < 0)
        {
            index = keyCardList.Length - 1;
        }
         keyCardList[index].SetActive(true);
         keyCardList[index].transform.localPosition = new Vector2(-69, -41);
    }

    public void RightArrow()
    {
        aSource.PlayOneShot(buttonSwap, 0.5f);
        //Right
        keyCardList[index].SetActive(false);
        index++;
        if (index == keyCardList.Length)
        {
            index = 0;
        }
        keyCardList[index].SetActive(true);
        keyCardList[index].transform.localPosition = new Vector2(-69, -41);
    }
}