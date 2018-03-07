using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraWork : MonoBehaviour //This is for the thiefs, dont need to touch
{
    public GameObject target;

    bool isFollowing;

    public bool followOnStart = false;

    Transform camTrans;

    Transform thiefCollector, thiefHacker;


    // Use this for initialization
    void Start()
    {
        if (followOnStart)
        {
            OnStartFollow();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!thiefCollector || !thiefHacker)
        {
            thiefCollector = GameObject.FindGameObjectWithTag("ThiefCollector").GetComponent<Transform>();
            thiefHacker = GameObject.FindGameObjectWithTag("ThiefHacker").GetComponent<Transform>();
        }

        // if (this.gameObject.tag == "ThiefHacker" || this.gameObject.tag == "ThiefCollector")
        // {
        //     if (camTrans != null && isFollowing)
        //     {
        //         ThiefCamFollow();
        //     }
        // }
        // else
        // {
            if (camTrans != null && isFollowing)
            {
                CamFollow();
            }
        //}
    }

    void LateUpdate()
    {
        
    }

    public void OnStartFollow()
    {
        camTrans = Camera.main.transform;
        
        target = this.gameObject;
        isFollowing = true;
    }

    void CamFollow()
    {
        if (target != null)
        {
            camTrans.position = new Vector3(target.transform.position.x, target.transform.position.y, -10);
        }
    }

    private void ThiefCamFollow()
    {
        if(target != null)
        {
            Vector2 targetCentre = (new Vector2(thiefCollector.position.x, thiefCollector.position.y) + new Vector2(thiefHacker.position.x, thiefHacker.position.y)) / 2;
            if (Vector2.Distance(thiefCollector.position, thiefHacker.position) > 5)
            {

            }
            else
            {
                camTrans.position = new Vector3(targetCentre.x, targetCentre.y, -10);
            }
        }
    }
}
