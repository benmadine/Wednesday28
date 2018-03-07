using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragMiddleWire : MonoBehaviour
{

    Rigidbody2D rigidBody;
    public GameObject test;
    DragBlueWire dragBlueWireRed;
    DragBlueWire dragBlueWireBlue;
    DragBlueWire dragBlueWireGreen;
    DragBlueWire dragBlueWireYellow;

    // Use this for initialization
    void Start ()
    {
        dragBlueWireRed = GameObject.FindGameObjectWithTag("RedWireEnd").GetComponent<DragBlueWire>();
        dragBlueWireBlue = GameObject.FindGameObjectWithTag("BlueWireEnd").GetComponent<DragBlueWire>();
        dragBlueWireGreen = GameObject.FindGameObjectWithTag("GreenWireEnd").GetComponent<DragBlueWire>();
        dragBlueWireYellow = GameObject.FindGameObjectWithTag("YellowWireEnd").GetComponent<DragBlueWire>();
        rigidBody = this.GetComponent<Rigidbody2D>();
    }

    void OnMouseDrag()
    {
        Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        rigidBody.MovePosition(objPosition);
        TestMethod(test);
    }

    void TestMethod(GameObject wireGripped)
    {
        if (wireGripped.tag == "RedWireEnd")
        {
            wireGripped = GameObject.FindGameObjectWithTag("RedWireEnd");
            if (dragBlueWireRed.counterPlugged == true)
            {
                wireGripped.transform.position = this.transform.position;
                dragBlueWireRed.counterPlugged = false;
            }
        }

        if (wireGripped.tag == "BlueWireEnd")
        {
            wireGripped = GameObject.FindGameObjectWithTag("BlueWireEnd");
            if (dragBlueWireBlue.counterPlugged == true)
            {
                wireGripped.transform.position = this.transform.position;
                dragBlueWireBlue.counterPlugged = false;
            }
        }

        if (wireGripped.tag == "GreenWireEnd")
        {
            wireGripped = GameObject.FindGameObjectWithTag("GreenWireEnd");
            if (dragBlueWireGreen.counterPlugged == true)
            {
                wireGripped.transform.position = this.transform.position;
                dragBlueWireGreen.counterPlugged = false;
            }
        }

        if (wireGripped.tag == "YellowWireEnd")
        {
            wireGripped = GameObject.FindGameObjectWithTag("YellowWireEnd");
            if (dragBlueWireYellow.counterPlugged == true)
            {
                wireGripped.transform.position = this.transform.position;
                dragBlueWireYellow.counterPlugged = false;
            }
        }
    }
}
