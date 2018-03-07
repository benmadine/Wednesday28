using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialArrow : MonoBehaviour {

    DialPuzzle dialPuzzleScript;
    Rigidbody2D rigidBody;
    float arrowRotationValues;
    public bool stage1, stage2, stage3, stage4;

    void Finding()
    {
        rigidBody = this.GetComponent<Rigidbody2D>();
        dialPuzzleScript = GameObject.FindGameObjectWithTag("DialPuzzle").GetComponent<DialPuzzle>();
    }

	// Use this for initialization
	void Start ()
    {
        Finding();
        stage1 = true;
    }

    void OnMouseDrag()
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);


        rigidBody.transform.localEulerAngles = new Vector3(0, 0, Mathf.Atan2
            ((objPosition.y - transform.position.y),
            (objPosition.x - transform.position.x)) * Mathf.Rad2Deg - 90);
        arrowRotationValues = transform.localEulerAngles.z;
    }

#region Condiitons
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(dialPuzzleScript.pickedCombination == DialPuzzle.Combinations.WXYZ)
        {
            if(stage1 && dialPuzzleScript)
            {
                if (collision.gameObject.name == "1")
                {
                    stage2 = true;
                }
            }
            if(stage2 && dialPuzzleScript.stage2Buttons)
            {
                if (collision.gameObject.name == "30")
                {
                    stage3 = true;
                }
            }
            if (stage3 && dialPuzzleScript.stage3Buttons)
            {
                if (collision.gameObject.name == "10")
                {
                    dialPuzzleScript.dialPass = true;
                }
            }
        }
        if (dialPuzzleScript.pickedCombination == DialPuzzle.Combinations.WYZX)
        {
            if (stage1)
            {
                if (collision.gameObject.name == "2")
                {
                    stage2 = true;
                }
            }
            if (stage2 && dialPuzzleScript.stage2Buttons)
            {
                if (collision.gameObject.name == "29")
                {
                    stage3 = true;
                }
            }
            if (stage3 && dialPuzzleScript.stage3Buttons)
            {
                if (collision.gameObject.name == "9")
                {
                    dialPuzzleScript.dialPass = true;
                }
            }
        }
        //if (dialPuzzleScript.pickedCombination == DialPuzzle.Combinations.XWYZ)
        //{
        //    if (stage1)
        //    {
        //        if (collision.gameObject.name == "3")
        //        {
        //            stage2 = true;
        //        }
        //    }
        //    if (stage2 && dialPuzzleScript.stage2Buttons)
        //    {
        //        if (collision.gameObject.name == "28")
        //        {
        //            stage3 = true;
        //        }
        //    }
        //    if (stage3 && dialPuzzleScript.stage3Buttons)
        //    {
        //        if (collision.gameObject.name == "8")
        //        {
        //            dialPuzzleScript.dialPass = true;
        //        }
        //    }
        //}
        if (dialPuzzleScript.pickedCombination == DialPuzzle.Combinations.YWZX)
        {
            if (stage1)
            {
                if (collision.gameObject.name == "4")
                {
                    stage2 = true;
                }
            }
            if (stage2 && dialPuzzleScript.stage2Buttons)
            {
                if (collision.gameObject.name == "27")
                {
                    stage3 = true;
                }
            }
            if (stage3 && dialPuzzleScript.stage3Buttons)
            {
                if (collision.gameObject.name == "7")
                {
                    dialPuzzleScript.dialPass = true;
                }
            }
        }
        //if (dialPuzzleScript.pickedCombination == DialPuzzle.Combinations.YWXZ)
        //{
        //    if (stage1)
        //    {
        //        if (collision.gameObject.name == "5")
        //        {
        //            stage2 = true;
        //        }
        //    }
        //    if (stage2 && dialPuzzleScript.stage2Buttons)
        //    {
        //        if (collision.gameObject.name == "26")
        //        {
        //            stage3 = true;
        //        }
        //    }
        //    if (stage3 && dialPuzzleScript.stage3Buttons)
        //    {
        //        if (collision.gameObject.name == "6")
        //        {
        //            dialPuzzleScript.dialPass = true;
        //        }
        //    }
        //}
        if (dialPuzzleScript.pickedCombination == DialPuzzle.Combinations.ZWYX)
        {
            if (stage1)
            {
                if (collision.gameObject.name == "6")
                {
                    stage2 = true;
                }
            }
            if (stage2 && dialPuzzleScript.stage2Buttons)
            {
                if (collision.gameObject.name == "25")
                {
                    stage3 = true;
                }
            }
            if (stage3 && dialPuzzleScript.stage3Buttons)
            {
                if (collision.gameObject.name == "5")
                {
                    dialPuzzleScript.dialPass = true;
                }
            }
        }
        if (dialPuzzleScript.pickedCombination == DialPuzzle.Combinations.WYXZ)
        {
            if (stage1)
            {
                if (collision.gameObject.name == "7")
                {
                    stage2 = true;
                }
            }
            if (stage2 && dialPuzzleScript.stage2Buttons)
            {
                if (collision.gameObject.name == "24")
                {
                    stage3 = true;
                }
            }
            if (stage3 && dialPuzzleScript.stage3Buttons)
            {
                if (collision.gameObject.name == "4")
                {
                    dialPuzzleScript.dialPass = true;
                }
            }
        }
        if (dialPuzzleScript.pickedCombination == DialPuzzle.Combinations.WYZX)
        {
            if (stage1)
            {
                if (collision.gameObject.name == "8")
                {
                    stage2 = true;
                }
            }
            if (stage2 && dialPuzzleScript.stage2Buttons)
            {
                if (collision.gameObject.name == "23")
                {
                    stage3 = true;
                }
            }
            if (stage3 && dialPuzzleScript.stage3Buttons)
            {
                if (collision.gameObject.name == "3")
                {
                    dialPuzzleScript.dialPass = true;
                }
            }
        }
        if (dialPuzzleScript.pickedCombination == DialPuzzle.Combinations.XYWZ)
        {
            if (stage1)
            {
                if (collision.gameObject.name == "9")
                {
                    stage2 = true;
                }
            }
            if (stage2 && dialPuzzleScript.stage2Buttons)
            {
                if (collision.gameObject.name == "22")
                {
                    stage3 = true;
                }
            }
            if (stage3 && dialPuzzleScript.stage3Buttons)
            {
                if (collision.gameObject.name == "2")
                {
                    dialPuzzleScript.dialPass = true;
                }
            }
        }
        if (dialPuzzleScript.pickedCombination == DialPuzzle.Combinations.YZWX)
        {
            if (stage1)
            {
                if (collision.gameObject.name == "10")
                {
                    stage2 = true;
                }
            }
            if (stage2 && dialPuzzleScript.stage2Buttons)
            {
                if (collision.gameObject.name == "21")
                {
                    stage3 = true;
                }
            }
            if (stage3 && dialPuzzleScript.stage3Buttons)
            {
                if (collision.gameObject.name == "1")
                {
                    dialPuzzleScript.dialPass = true;
                }
            }
        }
        if (dialPuzzleScript.pickedCombination == DialPuzzle.Combinations.YXWZ)
        {
            if (stage1)
            {
                if (collision.gameObject.name == "11")
                {
                    stage2 = true;
                }
            }
            if (stage2 && dialPuzzleScript.stage2Buttons)
            {
                if (collision.gameObject.name == "20")
                {
                    stage3 = true;
                }
            }
            if (stage3 && dialPuzzleScript.stage3Buttons)
            {
                if (collision.gameObject.name == "30")
                {
                    dialPuzzleScript.dialPass = true;
                }
            }
        }
        if (dialPuzzleScript.pickedCombination == DialPuzzle.Combinations.ZYWX)
        {
            if (stage1)
            {
                if (collision.gameObject.name == "12")
                {
                    stage2 = true;
                }
            }
            if (stage2 && dialPuzzleScript.stage2Buttons)
            {
                if (collision.gameObject.name == "19")
                {
                    stage3 = true;
                }
            }
            if (stage3 && dialPuzzleScript.stage3Buttons)
            {
                if (collision.gameObject.name == "29")
                {
                    dialPuzzleScript.dialPass = true;
                }
            }
        }
        //if (dialPuzzleScript.pickedCombination == DialPuzzle.Combinations.ZXWY)
        //{
        //    if (stage1)
        //    {
        //        if (collision.gameObject.name == "13")
        //        {
        //            stage2 = true;
        //        }
        //    }
        //    if (stage2 && dialPuzzleScript.stage2Buttons)
        //    {
        //        if (collision.gameObject.name == "18")
        //        {
        //            stage3 = true;
        //        }
        //    }
        //    if (stage3 && dialPuzzleScript.stage3Buttons)
        //    {
        //        if (collision.gameObject.name == "28")
        //        {
        //            dialPuzzleScript.dialPass = true;
        //        }
        //    }
        //}
        if (dialPuzzleScript.pickedCombination == DialPuzzle.Combinations.ZYXW)
        {
            if (stage1)
            {
                if (collision.gameObject.name == "14")
                {
                    stage2 = true;
                }
            }
            if (stage2 && dialPuzzleScript.stage2Buttons)
            {
                if (collision.gameObject.name == "17")
                {
                    stage3 = true;
                }
            }
            if (stage3 && dialPuzzleScript.stage3Buttons)
            {
                if (collision.gameObject.name == "27")
                {
                    dialPuzzleScript.dialPass = true;
                }
            }
        }
        if (dialPuzzleScript.pickedCombination == DialPuzzle.Combinations.XZWY)
        {
            if (stage1)
            {
                if (collision.gameObject.name == "15")
                {
                    stage2 = true;
                }
            }
            if (stage2 && dialPuzzleScript.stage2Buttons)
            {
                if (collision.gameObject.name == "16")
                {
                    stage3 = true;
                }
            }
            if (stage3 && dialPuzzleScript.stage3Buttons)
            {
                if (collision.gameObject.name == "26")
                {
                    dialPuzzleScript.dialPass = true;
                }
            }
        }
        if (dialPuzzleScript.pickedCombination == DialPuzzle.Combinations.YZXW)
        {
            if (stage1)
            {
                if (collision.gameObject.name == "16")
                {
                    stage2 = true;
                }
            }
            if (stage2 && dialPuzzleScript.stage2Buttons)
            {
                if (collision.gameObject.name == "15")
                {
                    stage3 = true;
                }
            }
            if (stage3 && dialPuzzleScript.stage3Buttons)
            {
                if (collision.gameObject.name == "25")
                {
                    dialPuzzleScript.dialPass = true;
                }
            }
        }
        if (dialPuzzleScript.pickedCombination == DialPuzzle.Combinations.WZXY)
        {
            if (stage1 )
            {
                if (collision.gameObject.name == "17")
                {
                    stage2 = true;
                }
            }
            if (stage2 && dialPuzzleScript.stage2Buttons)
            {
                if (collision.gameObject.name == "14")
                {
                    stage3 = true;
                }
            }
            if (stage3 && dialPuzzleScript.stage3Buttons)
            {
                if (collision.gameObject.name == "24")
                {
                    dialPuzzleScript.dialPass = true;
                }
            }
        }
        //if (dialPuzzleScript.pickedCombination == DialPuzzle.Combinations.XZYW)
        //{
        //    if (stage1)
        //    {
        //        if (collision.gameObject.name == "18")
        //        {
        //            stage2 = true;
        //        }
        //    }
        //    if (stage2 && dialPuzzleScript.stage2Buttons)
        //    {
        //        if (collision.gameObject.name == "13")
        //        {
        //            stage3 = true;
        //        }
        //    }
        //    if (stage3 && dialPuzzleScript.stage3Buttons)
        //    {
        //        if (collision.gameObject.name == "23")
        //        {
        //            dialPuzzleScript.dialPass = true;
        //        }
        //    }
        //}
        if (dialPuzzleScript.pickedCombination == DialPuzzle.Combinations.ZWXY)
        {
            if (stage1)
            {
                if (collision.gameObject.name == "19")
                {
                    stage2 = true;
                }
            }
            if (stage2 && dialPuzzleScript.stage2Buttons)
            {
                if (collision.gameObject.name == "12")
                {
                    stage3 = true;
                }
            }
            if (stage3 && dialPuzzleScript.stage3Buttons)
            {
                if (collision.gameObject.name == "22")
                {
                    dialPuzzleScript.dialPass = true;
                }
            }
        }
        if (dialPuzzleScript.pickedCombination == DialPuzzle.Combinations.ZXYW)
        {
            if (stage1)
            {
                if (collision.gameObject.name == "20")
                {
                    stage2 = true;
                }
            }
            if (stage2 && dialPuzzleScript.stage2Buttons)
            {
                if (collision.gameObject.name == "11")
                {
                    stage3 = true;
                }
            }
            if (stage3 && dialPuzzleScript.stage3Buttons)
            {
                if (collision.gameObject.name == "21")
                {
                    dialPuzzleScript.dialPass = true;
                }
            }
        }
        //if (dialPuzzleScript.pickedCombination == DialPuzzle.Combinations.XWZY)
        //{
        //    if (stage1)
        //    {
        //        if (collision.gameObject.name == "21")
        //        {
        //            stage2 = true;
        //        }
        //    }
        //    if (stage2 && dialPuzzleScript.stage2Buttons)
        //    {
        //        if (collision.gameObject.name == "10")
        //        {
        //            stage3 = true;
        //        }
        //    }
        //    if (stage3 && dialPuzzleScript.stage3Buttons)
        //    {
        //        if (collision.gameObject.name == "20")
        //        {
        //            dialPuzzleScript.dialPass = true;
        //        }
        //    }
        //}
        if (dialPuzzleScript.pickedCombination == DialPuzzle.Combinations.YXZW)
        {
            if (stage1)
            {
                if (collision.gameObject.name == "22")
                {
                    stage2 = true;
                }
            }
            if (stage2 && dialPuzzleScript.stage2Buttons)
            {
                if (collision.gameObject.name == "9")
                {
                    stage3 = true;
                }
            }
            if (stage3 && dialPuzzleScript.stage3Buttons)
            {
                if (collision.gameObject.name == "19")
                {
                    dialPuzzleScript.dialPass = true;
                }
            }
        }
        if (dialPuzzleScript.pickedCombination == DialPuzzle.Combinations.WXZY)
        {
            if (stage1)
            {
                if (collision.gameObject.name == "23")
                {
                    stage2 = true;
                }
            }
            if (stage2 && dialPuzzleScript.stage2Buttons)
            {
                if (collision.gameObject.name == "8")
                {
                    stage3 = true;
                }
            }
            if (stage3 && dialPuzzleScript.stage3Buttons)
            {
                if (collision.gameObject.name == "18")
                {
                    dialPuzzleScript.dialPass = true;
                }
            }
        }
        if (dialPuzzleScript.pickedCombination == DialPuzzle.Combinations.XYZW)
        {
            if (stage1)
            {
                if (collision.gameObject.name == "24")
                {
                    stage2 = true;
                }
            }
            if (stage2 && dialPuzzleScript.stage2Buttons)
            {
                if (collision.gameObject.name == "7")
                {
                    stage3 = true;
                }
            }
            if (stage3 && dialPuzzleScript.stage3Buttons)
            {
                if (collision.gameObject.name == "17")
                {
                    dialPuzzleScript.dialPass = true;
                }
            }
        }
        //if (dialPuzzleScript.pickedCombination == DialPuzzle.Combinations.NUM25)
        //{
        //    if (stage1)
        //    {
        //        if (collision.gameObject.name == "25")
        //        {
        //            stage2 = true;
        //        }
        //    }
        //    if (stage2 && dialPuzzleScript.stage2Buttons)
        //    {
        //        if (collision.gameObject.name == "6")
        //        {
        //            stage3 = true;
        //        }
        //    }
        //    if (stage3 && dialPuzzleScript.stage3Buttons)
        //    {
        //        if (collision.gameObject.name == "16")
        //        {
        //            dialPuzzleScript.dialPass = true;
        //        }
        //    }
        //}
        if (dialPuzzleScript.pickedCombination == DialPuzzle.Combinations.NUM26)
        {
            if (stage1)
            {
                if (collision.gameObject.name == "26")
                {
                    stage2 = true;
                }
            }
            if (stage2 && dialPuzzleScript.stage2Buttons)
            {
                if (collision.gameObject.name == "5")
                {
                    stage3 = true;
                }
            }
            if (stage3 && dialPuzzleScript.stage3Buttons)
            {
                if (collision.gameObject.name == "15")
                {
                    dialPuzzleScript.dialPass = true;
                }
            }
        }
        //if (dialPuzzleScript.pickedCombination == DialPuzzle.Combinations.NUM27)
        //{
        //    if (stage1)
        //    {
        //        if (collision.gameObject.name == "27")
        //        {
        //            stage2 = true;
        //        }
        //    }
        //    if (stage2 && dialPuzzleScript.stage2Buttons)
        //    {
        //        if (collision.gameObject.name == "4")
        //        {
        //            stage3 = true;
        //        }
        //    }
        //    if (stage3 && dialPuzzleScript.stage3Buttons)
        //    {
        //        if (collision.gameObject.name == "14")
        //        {
        //            dialPuzzleScript.dialPass = true;
        //        }
        //    }
        //}
        if (dialPuzzleScript.pickedCombination == DialPuzzle.Combinations.NUM28)
        {
            if (stage1)
            {
                if (collision.gameObject.name == "28")
                {
                    stage2 = true;
                }
            }
            if (stage2 && dialPuzzleScript.stage2Buttons)
            {
                if (collision.gameObject.name == "3")
                {
                    stage3 = true;
                }
            }
            if (stage3 && dialPuzzleScript.stage3Buttons)
            {
                if (collision.gameObject.name == "13")
                {
                    dialPuzzleScript.dialPass = true;
                }
            }
        }
        //if (dialPuzzleScript.pickedCombination == DialPuzzle.Combinations.NUM29)
        //{
        //    if (stage1)
        //    {
        //        if (collision.gameObject.name == "29")
        //        {
        //            stage2 = true;
        //        }
        //    }
        //    if (stage2 && dialPuzzleScript.stage2Buttons)
        //    {
        //        if (collision.gameObject.name == "2")
        //        {
        //            stage3 = true;
        //        }
        //    }
        //    if (stage3 && dialPuzzleScript.stage3Buttons)
        //    {
        //        if (collision.gameObject.name == "12")
        //        {
        //            dialPuzzleScript.dialPass = true;
        //        }
        //    }
        //}
        if (dialPuzzleScript.pickedCombination == DialPuzzle.Combinations.NUM30)
        {
            if (stage1)
            {
                if (collision.gameObject.name == "30")
                {
                    stage2 = true;
                }
            }
            if (stage2 && dialPuzzleScript.stage2Buttons)
            {
                if (collision.gameObject.name == "1")
                {
                    stage3 = true;
                }
            }
            if (stage3 && dialPuzzleScript.stage3Buttons)
            {
                if (collision.gameObject.name == "11")
                {
                    dialPuzzleScript.dialPass = true;
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (dialPuzzleScript.pickedCombination == DialPuzzle.Combinations.WXYZ)
        {
            if (stage3)
            {
                if (collision.gameObject.name != "10")
                {
 
                    dialPuzzleScript.dialPass = false;
                }
            }
        }
        if (dialPuzzleScript.pickedCombination == DialPuzzle.Combinations.WYZX)
        {
            if (stage3)
            {
                if (collision.gameObject.name != "9")
                {
 
                    dialPuzzleScript.dialPass = false;
                }
            }
        }
        //if (dialPuzzleScript.pickedCombination == DialPuzzle.Combinations.XWYZ)
        //{
        //    if (stage3)
        //    {
        //        if (collision.gameObject.name != "8")
        //        {
 
        //            dialPuzzleScript.dialPass = false;
        //        }
        //    }
        //}
        if (dialPuzzleScript.pickedCombination == DialPuzzle.Combinations.YWZX)
        {
            if (stage3)
            {
                if (collision.gameObject.name != "7")
                {
 
                    dialPuzzleScript.dialPass = false;
                }
            }
        }
        //if (dialPuzzleScript.pickedCombination == DialPuzzle.Combinations.YWXZ)
        //{
        //    if (stage3)
        //    {
        //        if (collision.gameObject.name != "6")
        //        {
 
        //            dialPuzzleScript.dialPass = false;
        //        }
        //    }
        //}
        if (dialPuzzleScript.pickedCombination == DialPuzzle.Combinations.ZWYX)
        {
            if (stage3)
            {
                if (collision.gameObject.name != "5")
                {
 
                    dialPuzzleScript.dialPass = false;
                }
            }
        }
        if (dialPuzzleScript.pickedCombination == DialPuzzle.Combinations.WYXZ)
        {
            if (stage3)
            {
                if (collision.gameObject.name != "4")
                {
 
                    dialPuzzleScript.dialPass = false;
                }
            }
        }
        if (dialPuzzleScript.pickedCombination == DialPuzzle.Combinations.WYZX)
        {
            if (stage3)
            {
                if (collision.gameObject.name != "3")
                {
 
                    dialPuzzleScript.dialPass = false;
                }
            }
        }
        if (dialPuzzleScript.pickedCombination == DialPuzzle.Combinations.XYWZ)
        {
            if (stage3)
            {
                if (collision.gameObject.name != "2")
                {
 
                    dialPuzzleScript.dialPass = false;
                }
            }
        }
        if (dialPuzzleScript.pickedCombination != DialPuzzle.Combinations.YZWX)
        {
            if (stage3)
            {
                if (collision.gameObject.name != "1")
                {
 
                    dialPuzzleScript.dialPass = false;
                }
            }
        }
        if (dialPuzzleScript.pickedCombination == DialPuzzle.Combinations.YXWZ)
        {
            if (stage3)
            {
                if (collision.gameObject.name != "30")
                {
 
                    dialPuzzleScript.dialPass = false;
                }
            }
        }
        if (dialPuzzleScript.pickedCombination == DialPuzzle.Combinations.ZYWX)
        {
            if (stage3)
            {
                if (collision.gameObject.name != "29")
                {
 
                    dialPuzzleScript.dialPass = false;
                }
            }
        }
        //if (dialPuzzleScript.pickedCombination == DialPuzzle.Combinations.ZXWY)
        //{
        //    if (stage3)
        //    {
        //        if (collision.gameObject.name != "28")
        //        {
 
        //            dialPuzzleScript.dialPass = false;
        //        }
        //    }
        //}
        if (dialPuzzleScript.pickedCombination != DialPuzzle.Combinations.ZYXW)
        {
            if (stage3)
            {
                if (collision.gameObject.name != "27")
                {
 
                    dialPuzzleScript.dialPass = false;
                }
            }
        }
        if (dialPuzzleScript.pickedCombination == DialPuzzle.Combinations.XZWY)
        {
            if (stage3)
            {
                if (collision.gameObject.name != "26")
                {
 
                    dialPuzzleScript.dialPass = false;
                }
            }
        }
        if (dialPuzzleScript.pickedCombination == DialPuzzle.Combinations.YZXW)
        {
            if (stage3)
            {
                if (collision.gameObject.name != "25")
                {
 
                    dialPuzzleScript.dialPass = false;
                }
            }
        }
        if (dialPuzzleScript.pickedCombination == DialPuzzle.Combinations.WZXY)
        {
            if (stage3)
            {
                if (collision.gameObject.name != "24")
                {
 
                    dialPuzzleScript.dialPass = false;
                }
            }
        }
        //if (dialPuzzleScript.pickedCombination == DialPuzzle.Combinations.XZYW)
        //{
        //    if (stage3)
        //    {
        //        if (collision.gameObject.name != "23")
        //        {
 
        //            dialPuzzleScript.dialPass = false;
        //        }
        //    }
        //}
        if (dialPuzzleScript.pickedCombination == DialPuzzle.Combinations.ZWXY)
        {
            if (stage3)
            {
                if (collision.gameObject.name != "22")
                {
 
                    dialPuzzleScript.dialPass = false;
                }
            }
        }
        if (dialPuzzleScript.pickedCombination == DialPuzzle.Combinations.ZXYW)
        {
            if (stage3)
            {
                if (collision.gameObject.name != "21")
                {
 
                    dialPuzzleScript.dialPass = false;
                }
            }
        }
        //if (dialPuzzleScript.pickedCombination == DialPuzzle.Combinations.XWZY)
        //{
        //    if (stage3)
        //    {
        //        if (collision.gameObject.name != "20")
        //        {
 
        //            dialPuzzleScript.dialPass = false;
        //        }
        //    }
        //}
        if (dialPuzzleScript.pickedCombination == DialPuzzle.Combinations.YXZW)
        {
            if (stage3)
            {
                if (collision.gameObject.name != "19")
                {
 
                    dialPuzzleScript.dialPass = false;
                }
            }
        }
        if (dialPuzzleScript.pickedCombination == DialPuzzle.Combinations.WXZY)
        {
            if (stage3)
            {
                if (collision.gameObject.name != "18")
                {
 
                    dialPuzzleScript.dialPass = false;
                }
            }
        }
        if (dialPuzzleScript.pickedCombination == DialPuzzle.Combinations.XYZW)
        {
            if (stage3)
            {
                if (collision.gameObject.name != "17")
                {
 
                    dialPuzzleScript.dialPass = false;
                }
            }
        }

        //if(dialPuzzleScript.pickedCombination == DialPuzzle.Combinations.NUM25)
        //{
        //    if (stage3)
        //    {
        //        if (collision.gameObject.name != "16")
        //        {
 
        //            dialPuzzleScript.dialPass = false;
        //        }
        //    }
        //}

        if (dialPuzzleScript.pickedCombination == DialPuzzle.Combinations.NUM26)
        {
            if (stage3)
            {
                if (collision.gameObject.name != "15")
                {
 
                    dialPuzzleScript.dialPass = false;
                }
            }
        }

        //if (dialPuzzleScript.pickedCombination == DialPuzzle.Combinations.NUM27)
        //{
        //    if (stage3)
        //    {
        //        if (collision.gameObject.name != "14")
        //        {
 
        //            dialPuzzleScript.dialPass = false;
        //        }
        //    }
        //}

        if (dialPuzzleScript.pickedCombination == DialPuzzle.Combinations.NUM28)
        {
            if (stage3)
            {
                if (collision.gameObject.name != "13")
                {
 
                    dialPuzzleScript.dialPass = false;
                }
            }
        }

        //if (dialPuzzleScript.pickedCombination == DialPuzzle.Combinations.NUM29)
        //{
        //    if (stage3)
        //    {
        //        if (collision.gameObject.name != "12")
        //        {
 
        //            dialPuzzleScript.dialPass = false;
        //        }
        //    }
        //}

        if (dialPuzzleScript.pickedCombination == DialPuzzle.Combinations.NUM30)
        {
            if (stage3)
            {
                if (collision.gameObject.name != "11")
                {
 
                    dialPuzzleScript.dialPass = false;
                }
            }
        }
    }
    #endregion
}
