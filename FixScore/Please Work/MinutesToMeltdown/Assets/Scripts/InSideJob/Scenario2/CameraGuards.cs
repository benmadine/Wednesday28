using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraGuards : MonoBehaviour
{
    public GameObject target;

    // Use this for initialization
    void Start()
    {

    }

    private void Update()
    {
 
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (target != null)
        {
            transform.position = new Vector3(target.transform.position.x, target.transform.position.y, -10);
        }
    }
    //   Camera guardCamera;
    //   Transform cameraTrans;
    //   bool following = true;

    //// Use this for initialization
    //void Start ()
    //   {
    //       guardCamera = GameObject.Find("GuardCamera").GetComponent<Camera>();
    //   }

    //// Update is called once per frame
    //void Update ()
    //   {
    //       if(following)
    //       {
    //           OnStartFollowing();
    //       }
    //   }

    //   public void OnStartFollowing()
    //   {

    //       if(guardCamera != null)
    //       {
    //           print("pls work");
    //           cameraTrans = guardCamera.transform;
    //           cameraTrans.position = new Vector3(this.transform.position.x, this.transform.position.y, -10);
    //           following = true;
    //       }
    //   }
}
