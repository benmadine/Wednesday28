using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    /// <summary>
    /// Simple method, dont destroy on load this object, keeps it going through all the scenes
    /// </summary>
    private void Awake()
    {
        DontDestroyOnLoad(this);

        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
    }
}
