using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Lean.Touch;

public class DragBlueWire : MonoBehaviour
{
    WirePuzzle wirePuzzleScript;
    public bool plugged = false, audioPlayed;
    Rigidbody2D rigidBody;
    public bool counterPlugged;

    public AudioClip wireConnect;
    AudioSource aSource;

    public GameObject endOfWire;

    bool playOnce1, playOnce2;

    public Vector2 startingPos, endingPos = new Vector3(0, 0);
    public Vector2 objPosition;

    // Use this for initialization
    void Start()
    {
        endOfWire = this.gameObject;
        aSource = GameObject.FindGameObjectWithTag("WirePuzzle").GetComponent<AudioSource>();
        wirePuzzleScript = GameObject.FindGameObjectWithTag("WirePuzzle").GetComponent<WirePuzzle>();
        rigidBody = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckWires();
        UnFreezeMe();
        rigidBody = GetComponent<Rigidbody2D>();
    }

    void CheckWires()
    {
        if(wirePuzzleScript.pickedFlashStage == WirePuzzle.flashStages.stage3)
        {
            if(wirePuzzleScript.flashOrderStage3Picked == WirePuzzle.flashOrderStage3.GRGB)
            {
                wirePuzzleScript.correctWireYellow = true;
            }
            if (wirePuzzleScript.flashOrderStage3Picked == WirePuzzle.flashOrderStage3.BBB)
            {
                wirePuzzleScript.correctWireYellow = true;
                wirePuzzleScript.correctWireGreen = true;
                wirePuzzleScript.correctWireBlue = true;
            }
            if(wirePuzzleScript.flashOrderStage3Picked == WirePuzzle.flashOrderStage3.GRBGRR)
            {
                wirePuzzleScript.correctWireRed = true;
                wirePuzzleScript.correctWireBlue = true;
                wirePuzzleScript.correctWireGreen = true;
            }
        }

        if (wirePuzzleScript.pickedFlashStage == WirePuzzle.flashStages.stage1)
        {
            if (wirePuzzleScript.flashOrderSelected == WirePuzzle.FlashOrder.GRBOGR)
            {
                wirePuzzleScript.correctWireRed = true;
                wirePuzzleScript.correctWireBlue = true;
                wirePuzzleScript.correctWireGreen = true;
            }
            if (wirePuzzleScript.flashOrderSelected == WirePuzzle.FlashOrder.OOGG)
            {
                wirePuzzleScript.correctWireRed = true;
                wirePuzzleScript.correctWireYellow = true;
            }
        }

        if(wirePuzzleScript.pickedFlashStage == WirePuzzle.flashStages.stage2)
        {
            if(wirePuzzleScript.flashOrderStage2Picked == WirePuzzle.flashOrderStage2.OOOO)
            {
                wirePuzzleScript.correctWireRed = true;
                wirePuzzleScript.correctWireYellow = true;
            }
        }
    }

    void OnMouseDrag()
    {
        if (plugged == false)
        {
            Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            rigidBody.MovePosition(objPosition);

            //fordebug//
            Vector3 objPosition2 = new Vector3(rigidBody.position.x, rigidBody.position.y, 0);
            Vector3 objposition3 = new Vector3(objPosition.x, objPosition.y, 0);
            Debug.DrawLine(objPosition2, objposition3);
            //endofdebug//  
        }

        if (plugged == true)
        {
            rigidBody.constraints = RigidbodyConstraints2D.None;
            plugged = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!audioPlayed)
        {
            aSource.PlayOneShot(wireConnect, 0.5f);
            audioPlayed = true;
        }
        if (wirePuzzleScript.pickedFlashStage == WirePuzzle.flashStages.stage1)
        {
            #region Yellow Wire Win Conditions
            if (this.gameObject.tag == "YellowWireEnd")
            {
                //First Flash
                if (wirePuzzleScript.flashOrderSelected == WirePuzzle.FlashOrder.RGBO)
                {
                    if (collision.gameObject.tag == "SocketEnd4" && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(-17,-25f,0);

                        wirePuzzleScript.correctWireYellow = true;
                        if (!counterPlugged)
                        {
                            counterPlugged = true;
                        }
                    }
                    if ((collision.gameObject.tag == "SocketEnd1" || collision.gameObject.tag == "SocketEnd2" || collision.gameObject.tag == "SocketEnd3") && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(-17, -25f, 0);
                        wirePuzzleScript.correctWireYellow = false;
                        if (!counterPlugged)
                        {
                            Handheld.Vibrate();
                            wirePuzzleScript.counter++;
                            wirePuzzleScript.flashOnce = false;
                            counterPlugged = true;
                        }
                    }
                }
                //Second Flash
                if (wirePuzzleScript.flashOrderSelected == WirePuzzle.FlashOrder.RRBG)
                {
                    if (collision.gameObject.tag == "SocketEnd1" && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(-17, -25f, 0);
                        wirePuzzleScript.correctWireYellow = true;
                        if (!counterPlugged)
                        {
                            counterPlugged = true;
                        }
                    }
                    if ((collision.gameObject.tag == "SocketEnd2" || collision.gameObject.tag == "SocketEnd3" || collision.gameObject.tag == "SocketEnd4") && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(-17, -25f, 0);
                        wirePuzzleScript.correctWireYellow = false;
                        if (!counterPlugged)
                        {
                            Handheld.Vibrate();
                            wirePuzzleScript.counter++;
                            wirePuzzleScript.flashOnce = false;
                            counterPlugged = true;
                        }
                    }
                }
                //Third Flash
                if (wirePuzzleScript.flashOrderSelected == WirePuzzle.FlashOrder.RGRB)
                {
                    if (collision.gameObject.tag == "SocketEnd4" && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(-17, -25f, 0);
                        wirePuzzleScript.correctWireYellow = true;
                        if (!counterPlugged)
                        {
                            counterPlugged = true;
                        }
                    }
                    if ((collision.gameObject.tag == "SocketEnd2" || collision.gameObject.tag == "SocketEnd3" || collision.gameObject.tag == "SocketEnd1") && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(-17, -25f, 0);
                        wirePuzzleScript.correctWireYellow = false;
                        if (!counterPlugged)
                        {
                            Handheld.Vibrate();
                            wirePuzzleScript.counter++;
                            wirePuzzleScript.flashOnce = false;
                            counterPlugged = true;
                        }
                    }
                }
                //Forth Flash
                if (wirePuzzleScript.flashOrderSelected == WirePuzzle.FlashOrder.OBGR)
                {
                    if (collision.gameObject.tag == "SocketEnd1" && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(-17, -25f, 0);
                        wirePuzzleScript.correctWireYellow = true;
                        if (!counterPlugged)
                        {
                            counterPlugged = true;
                        }
                    }
                    if ((collision.gameObject.tag == "SocketEnd2" || collision.gameObject.tag == "SocketEnd3" || collision.gameObject.tag == "SocketEnd4") && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(-17, -25f, 0);
                        wirePuzzleScript.correctWireYellow = false;
                        if (!counterPlugged)
                        {
                            Handheld.Vibrate();
                            wirePuzzleScript.counter++;
                            wirePuzzleScript.flashOnce = false;
                            counterPlugged = true;
                        }
                    }
                }
                //Fifth Flash
                if (wirePuzzleScript.flashOrderSelected == WirePuzzle.FlashOrder.OROR)
                {
                    if (collision.gameObject.tag == "SocketEnd4" && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(-17, -25f, 0);
                        wirePuzzleScript.correctWireYellow = true;
                        if (!counterPlugged)
                        {
                            counterPlugged = true;
                        }
                    }
                    if ((collision.gameObject.tag == "SocketEnd2" || collision.gameObject.tag == "SocketEnd3" || collision.gameObject.tag == "SocketEnd1") && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(-17, -25f, 0);
                        wirePuzzleScript.correctWireYellow = false;
                        if (!counterPlugged)
                        {
                            Handheld.Vibrate();
                            wirePuzzleScript.counter++;
                            wirePuzzleScript.flashOnce = false;
                            counterPlugged = true;
                        }
                    }
                }
                //Sixith Flash
                if (wirePuzzleScript.flashOrderSelected == WirePuzzle.FlashOrder.OOGG)
                {
                    if ((collision.gameObject.tag == "SocketEnd4" || collision.gameObject.tag == "SocketEnd2" || collision.gameObject.tag == "SocketEnd3" || collision.gameObject.tag == "SocketEnd1") && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(-17, -25f, 0);
                        wirePuzzleScript.correctWireYellow = false;
                        if (!counterPlugged)
                        {
                            Handheld.Vibrate();
                            wirePuzzleScript.counter++;
                            wirePuzzleScript.flashOnce = false;
                            counterPlugged = true;
                        }
                    }
                }
                //SeventhFlash
                if (wirePuzzleScript.flashOrderSelected == WirePuzzle.FlashOrder.GRBOGR)
                {
                    if (collision.gameObject.tag == "SocketEnd4" && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(-17, -25f, 0);
                        wirePuzzleScript.correctWireYellow = true;
                        if (!counterPlugged)
                        {
                            counterPlugged = true;
                        }
                    }
                    if ((collision.gameObject.tag == "SocketEnd2" || collision.gameObject.tag == "SocketEnd3" || collision.gameObject.tag == "SocketEnd1") && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(-17, -25f, 0);
                        wirePuzzleScript.correctWireYellow = false;
                        if (!counterPlugged)
                        {
                            Handheld.Vibrate();
                            wirePuzzleScript.counter++;
                            wirePuzzleScript.flashOnce = false;
                            counterPlugged = true;
                        }
                    }
                }
                //Eighth Flash
                if (wirePuzzleScript.flashOrderSelected == WirePuzzle.FlashOrder.BGGR)
                {
                    if (collision.gameObject.tag == "SocketEnd1" && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(-17, -25f, 0);
                        wirePuzzleScript.correctWireYellow = true;
                        if (!counterPlugged)
                        {
                            counterPlugged = true;
                        }
                    }
                    if ((collision.gameObject.tag == "SocketEnd2" || collision.gameObject.tag == "SocketEnd3" || collision.gameObject.tag == "SocketEnd4") && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(-17, -25f, 0);
                        wirePuzzleScript.correctWireYellow = false;
                        if (!counterPlugged)
                        {
                            Handheld.Vibrate();
                            wirePuzzleScript.counter++;
                            wirePuzzleScript.flashOnce = false;
                            counterPlugged = true;
                        }
                    }
                }
            }
            #endregion

            #region Green Wire Win Conditions
            if (this.gameObject.tag == "GreenWireEnd")
            {
                //First Flash
                if (wirePuzzleScript.flashOrderSelected == WirePuzzle.FlashOrder.RGBO)
                {
                    if (collision.gameObject.tag == "SocketEnd2" && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(-17, 5f, 0);
                        wirePuzzleScript.correctWireGreen = true;
                        if (!counterPlugged)
                        {
                            counterPlugged = true;
                        }
                    }
                    if ((collision.gameObject.tag == "SocketEnd1" || collision.gameObject.tag == "SocketEnd3" || collision.gameObject.tag == "SocketEnd4") && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(-17, 5f, 0);
                        wirePuzzleScript.correctWireGreen = false;
                        if (!counterPlugged)
                        {
                            Handheld.Vibrate();
                            wirePuzzleScript.counter++;
                            wirePuzzleScript.flashOnce = false;
                            counterPlugged = true;
                        }
                    }
                }
                //Second Flash
                if (wirePuzzleScript.flashOrderSelected == WirePuzzle.FlashOrder.RRBG)
                {
                    if (collision.gameObject.tag == "SocketEnd3" && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(-17, 5f, 0);
                        wirePuzzleScript.correctWireGreen = true;
                        if (!counterPlugged)
                        {
                            counterPlugged = true;
                        }
                    }
                    if ((collision.gameObject.tag == "SocketEnd2" || collision.gameObject.tag == "SocketEnd1" || collision.gameObject.tag == "SocketEnd4") && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(-17, 5f, 0);
                        wirePuzzleScript.correctWireGreen = false;
                        if (!counterPlugged)
                        {
                            Handheld.Vibrate();
                            wirePuzzleScript.counter++;
                            wirePuzzleScript.flashOnce = false;
                            counterPlugged = true;
                        }
                    }
                }
                //Third Flash
                if (wirePuzzleScript.flashOrderSelected == WirePuzzle.FlashOrder.RGRB)
                {
                    if (collision.gameObject.tag == "SocketEnd3" && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(-17, 5f, 0);
                        wirePuzzleScript.correctWireGreen = true;
                        if (!counterPlugged)
                        {
                            counterPlugged = true;
                        }
                    }
                    if ((collision.gameObject.tag == "SocketEnd2" || collision.gameObject.tag == "SocketEnd1" || collision.gameObject.tag == "SocketEnd4") && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(-17, 5f, 0);
                        wirePuzzleScript.correctWireGreen = false;
                        if (!counterPlugged)
                        {
                            Handheld.Vibrate();
                            wirePuzzleScript.counter++;
                            wirePuzzleScript.flashOnce = false;
                            counterPlugged = true;
                        }
                    }
                }
                //Fourth Flash
                if (wirePuzzleScript.flashOrderSelected == WirePuzzle.FlashOrder.OBGR)
                {
                    if (collision.gameObject.tag == "SocketEnd3" && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(-17, 5f, 0);
                        wirePuzzleScript.correctWireGreen = true;
                        if (!counterPlugged)
                        {
                            counterPlugged = true;
                        }
                    }
                    if ((collision.gameObject.tag == "SocketEnd2" || collision.gameObject.tag == "SocketEnd1" || collision.gameObject.tag == "SocketEnd4") && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(-17, 5f, 0);
                        wirePuzzleScript.correctWireGreen = false;
                        if (!counterPlugged)
                        {
                            Handheld.Vibrate();
                            wirePuzzleScript.counter++;
                            wirePuzzleScript.flashOnce = false;
                            counterPlugged = true;
                        }
                    }
                }
                //Fifth Flash
                if (wirePuzzleScript.flashOrderSelected == WirePuzzle.FlashOrder.OROR)
                {
                    if (collision.gameObject.tag == "SocketEnd4" && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(-17, 5f, 0);
                        wirePuzzleScript.correctWireGreen = true;
                        if (!counterPlugged)
                        {
                            counterPlugged = true;
                        }
                    }
                    if ((collision.gameObject.tag == "SocketEnd2" || collision.gameObject.tag == "SocketEnd1" || collision.gameObject.tag == "SocketEnd3") && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(-17, 5f, 0);
                        wirePuzzleScript.correctWireGreen = false;
                        if (!counterPlugged)
                        {
                            Handheld.Vibrate();
                            wirePuzzleScript.counter++;
                            wirePuzzleScript.flashOnce = false;
                            counterPlugged = true;
                        }
                    }
                }
                //Sixith Flash
                if (wirePuzzleScript.flashOrderSelected == WirePuzzle.FlashOrder.OOGG)
                {
                    if (collision.gameObject.tag == "SocketEnd2" && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(-17, 5f, 0);
                        wirePuzzleScript.correctWireGreen = true;
                        if (!counterPlugged)
                        {
                            counterPlugged = true;
                        }
                    }
                    if ((collision.gameObject.tag == "SocketEnd3" || collision.gameObject.tag == "SocketEnd1" || collision.gameObject.tag == "SocketEnd4") && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(-17, 5f, 0);
                        wirePuzzleScript.correctWireGreen = false;
                        if (!counterPlugged)
                        {
                            Handheld.Vibrate();
                            wirePuzzleScript.counter++;
                            wirePuzzleScript.flashOnce = false;
                            counterPlugged = true;
                        }
                    }
                }
                //Sevinth Flash
                if (wirePuzzleScript.flashOrderSelected == WirePuzzle.FlashOrder.GRBOGR)
                {
                    if ((collision.gameObject.tag == "SocketEnd4" || collision.gameObject.tag == "SocketEnd2" || collision.gameObject.tag == "SocketEnd3" || collision.gameObject.tag == "SocketEnd1") && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(-17, 5f, 0);
                        wirePuzzleScript.correctWireGreen = false;
                        if (!counterPlugged)
                        {
                            Handheld.Vibrate();
                            wirePuzzleScript.counter++;
                            wirePuzzleScript.flashOnce = false;
                            counterPlugged = true;
                        }
                    }
                }
                //Eighth Flash
                if (wirePuzzleScript.flashOrderSelected == WirePuzzle.FlashOrder.BGGR)
                {
                    if (collision.gameObject.tag == "SocketEnd1" && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(-17, 5f, 0);
                        wirePuzzleScript.correctWireGreen = true;
                        if (!counterPlugged)
                        {
                            counterPlugged = true;
                        }
                    }
                    if ((collision.gameObject.tag == "SocketEnd2" || collision.gameObject.tag == "SocketEnd3" || collision.gameObject.tag == "SocketEnd4") && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(-17, 5f, 0);
                        wirePuzzleScript.correctWireGreen = false;
                        if (!counterPlugged)
                        {
                            Handheld.Vibrate();
                            wirePuzzleScript.counter++;
                            wirePuzzleScript.flashOnce = false;
                            counterPlugged = true;
                        }
                    }
                }
            }
            #endregion

            #region Red Wire Win Conditions
            if (this.gameObject.tag == "RedWireEnd")
            {
                //First Flash
                if (wirePuzzleScript.flashOrderSelected == WirePuzzle.FlashOrder.RGBO)
                {
                    if (collision.gameObject.tag == "SocketEnd1" && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(16f, 5f, 0);
                        wirePuzzleScript.correctWireRed = true;
                        if (!counterPlugged)
                        {
                            counterPlugged = true;
                        }
                    }
                    if ((collision.gameObject.tag == "SocketEnd2" || collision.gameObject.tag == "SocketEnd3" || collision.gameObject.tag == "SocketEnd4") && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(16f, 5f, 0);
                        wirePuzzleScript.correctWireRed = false;
                        if (!counterPlugged)
                        {
                            Handheld.Vibrate();
                            wirePuzzleScript.counter++;
                            wirePuzzleScript.flashOnce = false;
                            counterPlugged = true;
                        }
                    }
                }
                //Second Flash
                if (wirePuzzleScript.flashOrderSelected == WirePuzzle.FlashOrder.RRBG)
                {
                    if (collision.gameObject.tag == "SocketEnd2" && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(16f, 5f, 0);
                        wirePuzzleScript.correctWireRed = true;
                        if (!counterPlugged)
                        {
                            counterPlugged = true;
                        }
                    }
                    if ((collision.gameObject.tag == "SocketEnd3" || collision.gameObject.tag == "SocketEnd1" || collision.gameObject.tag == "SocketEnd4") && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(16f, 5f, 0);
                        wirePuzzleScript.correctWireRed = false;
                        if (!counterPlugged)
                        {
                            Handheld.Vibrate();
                            wirePuzzleScript.counter++;
                            wirePuzzleScript.flashOnce = false;
                            counterPlugged = true;
                        }
                    }
                }
                //Third Flash
                if (wirePuzzleScript.flashOrderSelected == WirePuzzle.FlashOrder.RGRB)
                {
                    if (collision.gameObject.tag == "SocketEnd3" && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(16f, 5f, 0);
                        wirePuzzleScript.correctWireRed = true;
                        if (!counterPlugged)
                        {
                            counterPlugged = true;
                        }
                    }
                    if ((collision.gameObject.tag == "SocketEnd2" || collision.gameObject.tag == "SocketEnd1" || collision.gameObject.tag == "SocketEnd4") && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(16f, 5f, 0);
                        wirePuzzleScript.correctWireRed = false;
                        if (!counterPlugged)
                        {
                            Handheld.Vibrate();
                            wirePuzzleScript.counter++;
                            wirePuzzleScript.flashOnce = false;
                            counterPlugged = true;
                        }
                    }
                }
                //Forth Flash
                if (wirePuzzleScript.flashOrderSelected == WirePuzzle.FlashOrder.OBGR)
                {
                    if (collision.gameObject.tag == "SocketEnd4" && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(16f, 5f, 0);
                        wirePuzzleScript.correctWireRed = true;
                        if (!counterPlugged)
                        {
                            counterPlugged = true;
                        }
                    }
                    if ((collision.gameObject.tag == "SocketEnd3" || collision.gameObject.tag == "SocketEnd1" || collision.gameObject.tag == "SocketEnd2") && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(16f, 5f, 0);
                        wirePuzzleScript.correctWireRed = false;
                        if (!counterPlugged)
                        {
                            Handheld.Vibrate();
                            wirePuzzleScript.counter++;
                            wirePuzzleScript.flashOnce = false;
                            counterPlugged = true;
                        }
                    }
                }
                //Fifth Flash
                if (wirePuzzleScript.flashOrderSelected == WirePuzzle.FlashOrder.OROR)
                {
                    if (collision.gameObject.tag == "SocketEnd2" && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(16f, 5f, 0);
                        wirePuzzleScript.correctWireRed = true;
                        if (!counterPlugged)
                        {
                            counterPlugged = true;
                        }
                    }
                    if ((collision.gameObject.tag == "SocketEnd3" || collision.gameObject.tag == "SocketEnd1" || collision.gameObject.tag == "SocketEnd4") && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(16f, 5f, 0);
                        wirePuzzleScript.correctWireRed = false;
                        if (!counterPlugged)
                        {
                            Handheld.Vibrate();
                            wirePuzzleScript.counter++;
                            wirePuzzleScript.flashOnce = false;
                            counterPlugged = true;
                        }
                    }
                }
                //Sixith Flash
                if (wirePuzzleScript.flashOrderSelected == WirePuzzle.FlashOrder.OOGG)
                {
                    if ((collision.gameObject.tag == "SocketEnd4" || collision.gameObject.tag == "SocketEnd2" || collision.gameObject.tag == "SocketEnd3" || collision.gameObject.tag == "SocketEnd1") && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(16f, 5f, 0);
                        wirePuzzleScript.correctWireRed = false;
                        if (!counterPlugged)
                        {
                            Handheld.Vibrate();
                            wirePuzzleScript.counter++;
                            wirePuzzleScript.flashOnce = false;
                            counterPlugged = true;
                        }
                    }
                }
                //Seventh Flash
                if (wirePuzzleScript.flashOrderSelected == WirePuzzle.FlashOrder.GRBOGR)
                {
                    if ((collision.gameObject.tag == "SocketEnd4" || collision.gameObject.tag == "SocketEnd2" || collision.gameObject.tag == "SocketEnd3" || collision.gameObject.tag == "SocketEnd1") && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(16f, 5f, 0);
                        wirePuzzleScript.correctWireRed = false;
                        if (!counterPlugged)
                        {
                            Handheld.Vibrate();
                            wirePuzzleScript.counter++;
                            wirePuzzleScript.flashOnce = false;
                            counterPlugged = true;
                        }
                    }
                }
                //Eight Flash
                if (wirePuzzleScript.flashOrderSelected == WirePuzzle.FlashOrder.BGGR)
                {
                    if (collision.gameObject.tag == "SocketEnd1" && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(16f, 5f, 0);
                        wirePuzzleScript.correctWireRed = true;
                        if (!counterPlugged)
                        {
                            counterPlugged = true;
                        }
                    }
                    if ((collision.gameObject.tag == "SocketEnd3" || collision.gameObject.tag == "SocketEnd2" || collision.gameObject.tag == "SocketEnd4") && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(16f, 5f, 0);
                        wirePuzzleScript.correctWireRed = false;
                        if (!counterPlugged)
                        {
                            Handheld.Vibrate();
                            wirePuzzleScript.counter++;
                            wirePuzzleScript.flashOnce = false;
                            counterPlugged = true;
                        }
                    }
                }
            }

            #endregion

            #region Blue Wire Win Conditions

            if (this.gameObject.tag == "BlueWireEnd")
            {
                //First Flash
                if (wirePuzzleScript.flashOrderSelected == WirePuzzle.FlashOrder.RGBO)
                {
                    if (collision.gameObject.tag == "SocketEnd3" && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(16f, -25f, 0);
                        wirePuzzleScript.correctWireBlue = true;
                        if (!counterPlugged)
                        {
                            counterPlugged = true;
                        }
                    }
                    if ((collision.gameObject.tag == "SocketEnd1" || collision.gameObject.tag == "SocketEnd2" || collision.gameObject.tag == "SocketEnd4") && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(16f, -25f, 0);
                        wirePuzzleScript.correctWireBlue = false;
                        if (!counterPlugged)
                        {
                            Handheld.Vibrate();
                            wirePuzzleScript.counter++;
                            wirePuzzleScript.flashOnce = false;
                            counterPlugged = true;
                        }
                    }
                }
                //Second Flash
                if (wirePuzzleScript.flashOrderSelected == WirePuzzle.FlashOrder.RRBG)
                {
                    if (collision.gameObject.tag == "SocketEnd4" && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(16f, -25f, 0);
                        wirePuzzleScript.correctWireBlue = true;
                        if (!counterPlugged)
                        {
                            counterPlugged = true;
                        }
                    }
                    if ((collision.gameObject.tag == "SocketEnd3" || collision.gameObject.tag == "SocketEnd1" || collision.gameObject.tag == "SocketEnd2") && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(16f, -25f, 0);
                        wirePuzzleScript.correctWireBlue = false;
                        if (!counterPlugged)
                        {
                            Handheld.Vibrate();
                            wirePuzzleScript.counter++;
                            wirePuzzleScript.flashOnce = false;
                            counterPlugged = true;
                        }
                    }
                }
                //Third Flash
                if (wirePuzzleScript.flashOrderSelected == WirePuzzle.FlashOrder.RGRB)
                {
                    if (collision.gameObject.tag == "SocketEnd4" && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(16f, -25f, 0);
                        wirePuzzleScript.correctWireBlue = true;
                        if (!counterPlugged)
                        {
                            counterPlugged = true;
                        }
                    }
                    if ((collision.gameObject.tag == "SocketEnd3" || collision.gameObject.tag == "SocketEnd1" || collision.gameObject.tag == "SocketEnd2") && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(16f, -25f, 0);
                        wirePuzzleScript.correctWireBlue = false;
                        if (!counterPlugged)
                        {
                            Handheld.Vibrate();
                            wirePuzzleScript.counter++;
                            wirePuzzleScript.flashOnce = false;
                            counterPlugged = true;
                        }
                    }
                }
                //Fourth Flash
                if (wirePuzzleScript.flashOrderSelected == WirePuzzle.FlashOrder.OBGR)
                {
                    if (collision.gameObject.tag == "SocketEnd2" && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(16f, -25f, 0);
                        wirePuzzleScript.correctWireBlue = true;
                        if (!counterPlugged)
                        {
                            counterPlugged = true;
                        }
                    }
                    if ((collision.gameObject.tag == "SocketEnd3" || collision.gameObject.tag == "SocketEnd1" || collision.gameObject.tag == "SocketEnd4") && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(16f, -25f, 0);
                        wirePuzzleScript.correctWireBlue = false;
                        if (!counterPlugged)
                        {
                            Handheld.Vibrate();
                            wirePuzzleScript.counter++;
                            wirePuzzleScript.flashOnce = false;
                            counterPlugged = true;
                        }
                    }
                }
                //Fifth Flash
                if (wirePuzzleScript.flashOrderSelected == WirePuzzle.FlashOrder.OROR)
                {
                    if (collision.gameObject.tag == "SocketEnd2" && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(16f, -25f, 0);
                        wirePuzzleScript.correctWireBlue = true;
                        if (!counterPlugged)
                        {
                            counterPlugged = true;
                        }
                    }
                    if ((collision.gameObject.tag == "SocketEnd3" || collision.gameObject.tag == "SocketEnd1" || collision.gameObject.tag == "SocketEnd4") && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(16f, -25f, 0);
                        wirePuzzleScript.correctWireBlue = false;
                        if (!counterPlugged)
                        {
                            Handheld.Vibrate();
                            wirePuzzleScript.counter++;
                            wirePuzzleScript.flashOnce = false;
                            counterPlugged = true;
                        }
                    }
                }
                //Sixith Flash
                if (wirePuzzleScript.flashOrderSelected == WirePuzzle.FlashOrder.OOGG)
                {
                    if (collision.gameObject.tag == "SocketEnd1" && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(16f, -25f, 0);
                        wirePuzzleScript.correctWireBlue = true;
                        if (!counterPlugged)
                        {
                            counterPlugged = true;
                        }
                    }
                    if ((collision.gameObject.tag == "SocketEnd3" || collision.gameObject.tag == "SocketEnd4" || collision.gameObject.tag == "SocketEnd2") && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(16f, -25f, 0);
                        wirePuzzleScript.correctWireBlue = false;
                        if (!counterPlugged)
                        {
                            Handheld.Vibrate();
                            wirePuzzleScript.counter++;
                            wirePuzzleScript.flashOnce = false;
                            counterPlugged = true;
                        }
                    }
                }
                //Seventh Flash
                if (wirePuzzleScript.flashOrderSelected == WirePuzzle.FlashOrder.GRBOGR)
                {
                    if ((collision.gameObject.tag == "SocketEnd4" || collision.gameObject.tag == "SocketEnd2" || collision.gameObject.tag == "SocketEnd3" || collision.gameObject.tag == "SocketEnd1") && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(16f, -25f, 0);
                        wirePuzzleScript.correctWireBlue = false;
                        if (!counterPlugged)
                        {
                            Handheld.Vibrate();
                            wirePuzzleScript.counter++;
                            wirePuzzleScript.flashOnce = false;
                            counterPlugged = true;
                        }
                    }
                }
                //Eight Flash
                if (wirePuzzleScript.flashOrderSelected == WirePuzzle.FlashOrder.BGGR)
                {
                    if (collision.gameObject.tag == "SocketEnd1" && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(16f, -25f, 0);
                        wirePuzzleScript.correctWireBlue = true;
                        if (!counterPlugged)
                        {
                            counterPlugged = true;
                        }
                    }
                    if ((collision.gameObject.tag == "SocketEnd3" || collision.gameObject.tag == "SocketEnd4" || collision.gameObject.tag == "SocketEnd2") && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(16f, -25f, 0);
                        wirePuzzleScript.correctWireBlue = false;
                        if (!counterPlugged)
                        {
                            Handheld.Vibrate();
                            wirePuzzleScript.counter++;
                            wirePuzzleScript.flashOnce = false;
                            counterPlugged = true;
                        }
                    }
                }
            }
            #endregion
        }
        if (wirePuzzleScript.pickedFlashStage == WirePuzzle.flashStages.stage2)
        {
            #region Yellow Wire Win Conditions
            if (this.gameObject.tag == "YellowWireEnd")
            {
                //First Flash
                if (wirePuzzleScript.flashOrderStage2Picked == WirePuzzle.flashOrderStage2.ROBG)
                {
                    if (collision.gameObject.tag == "SocketEnd2" && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(-17, -25f, 0);
                        wirePuzzleScript.correctWireYellow = true;
                        if (!counterPlugged)
                        {
                            counterPlugged = true;
                        }
                    }
                    if ((collision.gameObject.tag == "SocketEnd1" || collision.gameObject.tag == "SocketEnd4" || collision.gameObject.tag == "SocketEnd3") && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(-17, -25f, 0);
                        wirePuzzleScript.correctWireYellow = false;
                        if (!counterPlugged)
                        {
                            Handheld.Vibrate();
                            wirePuzzleScript.counter++;
                            wirePuzzleScript.flashOnce = false;
                            counterPlugged = true;
                        }
                    }
                }
                //Second Flash
                if (wirePuzzleScript.flashOrderStage2Picked == WirePuzzle.flashOrderStage2.OROB)
                {
                    if (collision.gameObject.tag == "SocketEnd2" && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(-17, -25f, 0);
                        wirePuzzleScript.correctWireYellow = true;
                        if (!counterPlugged)
                        {
                            counterPlugged = true;
                        }
                    }
                    if ((collision.gameObject.tag == "SocketEnd1" || collision.gameObject.tag == "SocketEnd3" || collision.gameObject.tag == "SocketEnd4") && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(-17, -25f, 0);
                        wirePuzzleScript.correctWireYellow = false;
                        if (!counterPlugged)
                        {
                            Handheld.Vibrate();
                            wirePuzzleScript.counter++;
                            wirePuzzleScript.flashOnce = false;
                            counterPlugged = true;
                        }
                    }
                }
                //Third Flash
                if (wirePuzzleScript.flashOrderStage2Picked == WirePuzzle.flashOrderStage2.OOOO)
                {
                    if ((collision.gameObject.tag == "SocketEnd2" || collision.gameObject.tag == "SocketEnd3" || collision.gameObject.tag == "SocketEnd1" || collision.gameObject.tag == "SocketEnd4") && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(-17, -25f, 0);
                        wirePuzzleScript.correctWireYellow = false;
                        if (!counterPlugged)
                        {
                            Handheld.Vibrate();
                            wirePuzzleScript.counter++;
                            wirePuzzleScript.flashOnce = false;
                            counterPlugged = true;
                        }
                    }
                }
                //Forth Flash                                                                       HERRERERERERERERERERER
                if (wirePuzzleScript.flashOrderStage2Picked == WirePuzzle.flashOrderStage2.RBGR)
                {
                    if (collision.gameObject.tag == "SocketEnd2" && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(-17, -25f, 0);
                        wirePuzzleScript.correctWireYellow = true;
                        if (!counterPlugged)
                        {
                            counterPlugged = true;
                        }
                    }
                    if ((collision.gameObject.tag == "SocketEnd4" || collision.gameObject.tag == "SocketEnd3" || collision.gameObject.tag == "SocketEnd1") && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(-17, -25f, 0);
                        wirePuzzleScript.correctWireYellow = false;
                        if (!counterPlugged)
                        {
                            Handheld.Vibrate();
                            wirePuzzleScript.counter++;
                            wirePuzzleScript.flashOnce = false;
                            counterPlugged = true;
                        }
                    }
                }
                //Fifth Flash
                if (wirePuzzleScript.flashOrderStage2Picked == WirePuzzle.flashOrderStage2.RRBGR)
                {
                    if (collision.gameObject.tag == "SocketEnd2" && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(-17, -25f, 0);
                        wirePuzzleScript.correctWireYellow = true;
                        if (!counterPlugged)
                        {
                            counterPlugged = true;
                        }
                    }
                    if ((collision.gameObject.tag == "SocketEnd4" || collision.gameObject.tag == "SocketEnd3" || collision.gameObject.tag == "SocketEnd1") && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(-17, -25f, 0);
                        wirePuzzleScript.correctWireYellow = false;
                        if (!counterPlugged)
                        {
                            Handheld.Vibrate();
                            wirePuzzleScript.counter++;
                            wirePuzzleScript.flashOnce = false;
                            counterPlugged = true;
                        }
                    }
                }
                //Sixith Flash
                if (wirePuzzleScript.flashOrderStage2Picked == WirePuzzle.flashOrderStage2.BOG)
                {
                    if (collision.gameObject.tag == "SocketEnd3" && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(-17, -25f, 0);
                        wirePuzzleScript.correctWireYellow = true;
                        if (!counterPlugged)
                        {
                            counterPlugged = true;
                        }
                    }
                    if ((collision.gameObject.tag == "SocketEnd4" || collision.gameObject.tag == "SocketEnd2" || collision.gameObject.tag == "SocketEnd1") && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(-17, -25f, 0);
                        wirePuzzleScript.correctWireYellow = false;
                        if (!counterPlugged)
                        {
                            Handheld.Vibrate();
                            wirePuzzleScript.counter++;
                            wirePuzzleScript.flashOnce = false;
                            counterPlugged = true;
                        }
                    }
                }
                //SeventhFlash
                if (wirePuzzleScript.flashOrderStage2Picked == WirePuzzle.flashOrderStage2.RRRB)
                {
                    if (collision.gameObject.tag == "SocketEnd1" && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(-17, -25f, 0);
                        wirePuzzleScript.correctWireYellow = true;
                        if (!counterPlugged)
                        {
                            counterPlugged = true;
                        }
                    }
                    if ((collision.gameObject.tag == "SocketEnd2" || collision.gameObject.tag == "SocketEnd4" || collision.gameObject.tag == "SocketEnd3") && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(-17, -25f, 0);
                        wirePuzzleScript.correctWireYellow = false;
                        if (!counterPlugged)
                        {
                            Handheld.Vibrate();
                            wirePuzzleScript.counter++;
                            wirePuzzleScript.flashOnce = false;
                            counterPlugged = true;
                        }
                    }
                }
                //Eighth Flash
                if (wirePuzzleScript.flashOrderStage2Picked == WirePuzzle.flashOrderStage2.GGGGBR)
                {
                    if (collision.gameObject.tag == "SocketEnd1" && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(-17, -25f, 0);
                        wirePuzzleScript.correctWireYellow = true;
                        if (!counterPlugged)
                        {
                            counterPlugged = true;
                        }
                    }
                    if ((collision.gameObject.tag == "SocketEnd2" || collision.gameObject.tag == "SocketEnd3" || collision.gameObject.tag == "SocketEnd4") && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(-17, -25f, 0);
                        wirePuzzleScript.correctWireYellow = false;
                        if (!counterPlugged)
                        {
                            Handheld.Vibrate();
                            wirePuzzleScript.counter++;
                            wirePuzzleScript.flashOnce = false;
                            counterPlugged = true;
                        }
                    }
                }
            }
            #endregion

            #region Green Wire Win Conditions
            if (this.gameObject.tag == "GreenWireEnd")
            {
                //First Flash
                if (wirePuzzleScript.flashOrderStage2Picked == WirePuzzle.flashOrderStage2.ROBG)
                {
                    if (collision.gameObject.tag == "SocketEnd4" && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(-17, 5f, 0);
                        wirePuzzleScript.correctWireGreen = true;
                        if (!counterPlugged)
                        {
                            counterPlugged = true;
                        }
                    }
                    if ((collision.gameObject.tag == "SocketEnd1" || collision.gameObject.tag == "SocketEnd3" || collision.gameObject.tag == "SocketEnd2") && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(-17, 5f, 0);
                        wirePuzzleScript.correctWireGreen = false;
                        if (!counterPlugged)
                        {
                            Handheld.Vibrate();
                            wirePuzzleScript.counter++;
                            wirePuzzleScript.flashOnce = false;
                            counterPlugged = true;
                        }
                    }
                }
                //Second Flash
                if (wirePuzzleScript.flashOrderStage2Picked == WirePuzzle.flashOrderStage2.OROB)
                {
                    if (collision.gameObject.tag == "SocketEnd3" && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(-17, 5f, 0);
                        wirePuzzleScript.correctWireGreen = true;
                        if (!counterPlugged)
                        {
                            counterPlugged = true;
                        }
                    }
                    if ((collision.gameObject.tag == "SocketEnd2" || collision.gameObject.tag == "SocketEnd1" || collision.gameObject.tag == "SocketEnd4") && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(-17, 5f, 0);
                        wirePuzzleScript.correctWireGreen = false;
                        if (!counterPlugged)
                        {
                            Handheld.Vibrate();
                            wirePuzzleScript.counter++;
                            wirePuzzleScript.flashOnce = false;
                            counterPlugged = true;
                        }
                    }
                }
                //Third Flash
                if (wirePuzzleScript.flashOrderStage2Picked == WirePuzzle.flashOrderStage2.OOOO)
                {
                    if (collision.gameObject.tag == "SocketEnd4" && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(-17, 5f, 0);
                        wirePuzzleScript.correctWireGreen = true;
                        if (!counterPlugged)
                        {
                            counterPlugged = true;
                        }
                    }
                    if ((collision.gameObject.tag == "SocketEnd2" || collision.gameObject.tag == "SocketEnd1" || collision.gameObject.tag == "SocketEnd3") && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(-17, 5f, 0);
                        wirePuzzleScript.correctWireGreen = false;
                        if (!counterPlugged)
                        {
                            Handheld.Vibrate();
                            wirePuzzleScript.counter++;
                            wirePuzzleScript.flashOnce = false;
                            counterPlugged = true;
                        }
                    }
                }
                //Fourth Flash
                if (wirePuzzleScript.flashOrderStage2Picked == WirePuzzle.flashOrderStage2.RBGR)
                {
                    if (collision.gameObject.tag == "SocketEnd4" && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(-17, 5f, 0);
                        wirePuzzleScript.correctWireGreen = true;
                        if (!counterPlugged)
                        {
                            counterPlugged = true;
                        }
                    }
                    if ((collision.gameObject.tag == "SocketEnd2" || collision.gameObject.tag == "SocketEnd1" || collision.gameObject.tag == "SocketEnd3") && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(-17, 5f, 0);
                        wirePuzzleScript.correctWireGreen = false;
                        if (!counterPlugged)
                        {
                            Handheld.Vibrate();
                            wirePuzzleScript.counter++;
                            wirePuzzleScript.flashOnce = false;
                            counterPlugged = true;
                        }
                    }
                }
                //Fifth Flash                                                               here
                if (wirePuzzleScript.flashOrderStage2Picked == WirePuzzle.flashOrderStage2.RRBGR)
                {
                    if (collision.gameObject.tag == "SocketEnd3" && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(-17, 5f, 0);
                        wirePuzzleScript.correctWireGreen = true;
                        if (!counterPlugged)
                        {
                            counterPlugged = true;
                        }
                    }
                    if ((collision.gameObject.tag == "SocketEnd2" || collision.gameObject.tag == "SocketEnd1" || collision.gameObject.tag == "SocketEnd4") && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(-17, 5f, 0);
                        wirePuzzleScript.correctWireGreen = false;
                        if (!counterPlugged)
                        {
                            Handheld.Vibrate();
                            wirePuzzleScript.counter++;
                            wirePuzzleScript.flashOnce = false;
                            counterPlugged = true;
                        }
                    }
                }
                //Sixith Flash
                if (wirePuzzleScript.flashOrderStage2Picked == WirePuzzle.flashOrderStage2.BOG)
                {
                    if (collision.gameObject.tag == "SocketEnd3" && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(-17, 5f, 0);
                        wirePuzzleScript.correctWireGreen = true;
                        if (!counterPlugged)
                        {
                            counterPlugged = true;
                        }
                    }
                    if ((collision.gameObject.tag == "SocketEnd2" || collision.gameObject.tag == "SocketEnd1" || collision.gameObject.tag == "SocketEnd4") && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(-17, 5f, 0);
                        wirePuzzleScript.correctWireGreen = false;
                        if (!counterPlugged)
                        {
                            Handheld.Vibrate();
                            wirePuzzleScript.counter++;
                            wirePuzzleScript.flashOnce = false;
                            counterPlugged = true;
                        }
                    }
                }
                //Sevinth Flash
                if (wirePuzzleScript.flashOrderStage2Picked == WirePuzzle.flashOrderStage2.RRRB)
                {
                    if (collision.gameObject.tag == "SocketEnd2" && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(-17, 5f, 0);
                        wirePuzzleScript.correctWireGreen = true;
                        if (!counterPlugged)
                        {
                            counterPlugged = true;
                        }
                    }
                    if ((collision.gameObject.tag == "SocketEnd4" || collision.gameObject.tag == "SocketEnd3" || collision.gameObject.tag == "SocketEnd1") && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(-17, 5f, 0);
                        wirePuzzleScript.correctWireGreen = false;
                        if (!counterPlugged)
                        {
                            Handheld.Vibrate();
                            wirePuzzleScript.counter++;
                            wirePuzzleScript.flashOnce = false;
                            counterPlugged = true;
                        }
                    }
                }
                //Eighth Flash
                if (wirePuzzleScript.flashOrderStage2Picked == WirePuzzle.flashOrderStage2.GGGGBR)
                {
                    if (collision.gameObject.tag == "SocketEnd1" && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(-17, 5f, 0);
                        wirePuzzleScript.correctWireGreen = true;
                        if (!counterPlugged)
                        {
                            counterPlugged = true;
                        }
                    }
                    if ((collision.gameObject.tag == "SocketEnd2" || collision.gameObject.tag == "SocketEnd3" || collision.gameObject.tag == "SocketEnd4") && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(-17, 5f, 0);
                        wirePuzzleScript.correctWireGreen = false;
                        if (!counterPlugged)
                        {
                            Handheld.Vibrate();
                            wirePuzzleScript.counter++;
                            wirePuzzleScript.flashOnce = false;
                            counterPlugged = true;
                        }
                    }
                }
            }

            #endregion

            #region Red Wire Win Conditions
            if (this.gameObject.tag == "RedWireEnd")
            {
                //First Flash
                if (wirePuzzleScript.flashOrderStage2Picked == WirePuzzle.flashOrderStage2.ROBG)
                {
                    if (collision.gameObject.tag == "SocketEnd1" && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(16f, 5f, 0);
                        wirePuzzleScript.correctWireRed = true;
                        if (!counterPlugged)
                        {
                            counterPlugged = true;
                        }
                    }
                    if ((collision.gameObject.tag == "SocketEnd2" || collision.gameObject.tag == "SocketEnd3" || collision.gameObject.tag == "SocketEnd4") && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(16f, 5f, 0);
                        wirePuzzleScript.correctWireRed = false;
                        if (!counterPlugged)
                        {
                            Handheld.Vibrate();
                            wirePuzzleScript.counter++;
                            wirePuzzleScript.flashOnce = false;
                            counterPlugged = true;
                        }
                    }
                }
                //Second Flash
                if (wirePuzzleScript.flashOrderStage2Picked == WirePuzzle.flashOrderStage2.OROB)
                {
                    if (collision.gameObject.tag == "SocketEnd2" && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(16f, 5f, 0);
                        wirePuzzleScript.correctWireRed = true;
                        if (!counterPlugged)
                        {
                            counterPlugged = true;
                        }
                    }
                    if ((collision.gameObject.tag == "SocketEnd3" || collision.gameObject.tag == "SocketEnd1" || collision.gameObject.tag == "SocketEnd4") && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(16f, 5f, 0);
                        wirePuzzleScript.correctWireRed = false;
                        if (!counterPlugged)
                        {
                            Handheld.Vibrate();
                            wirePuzzleScript.counter++;
                            wirePuzzleScript.flashOnce = false;
                            counterPlugged = true;
                        }
                    }
                }
                //Third Flash
                if (wirePuzzleScript.flashOrderStage2Picked == WirePuzzle.flashOrderStage2.OOOO)
                {
                    if ((collision.gameObject.tag == "SocketEnd2" || collision.gameObject.tag == "SocketEnd1" || collision.gameObject.tag == "SocketEnd4") && plugged == false || collision.gameObject.tag == "SocketEnd3")
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(16f, 5f, 0);
                        wirePuzzleScript.correctWireRed = false;
                        if (!counterPlugged)
                        {
                            Handheld.Vibrate();
                            wirePuzzleScript.counter++;
                            wirePuzzleScript.flashOnce = false;
                            counterPlugged = true;
                        }
                    }
                }
                //Forth Flash
                if (wirePuzzleScript.flashOrderStage2Picked == WirePuzzle.flashOrderStage2.RBGR)
                {
                    if (collision.gameObject.tag == "SocketEnd1" && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(16f, 5f, 0);
                        wirePuzzleScript.correctWireRed = true;
                        if (!counterPlugged)
                        {
                            counterPlugged = true;
                        }
                    }
                    if ((collision.gameObject.tag == "SocketEnd4" || collision.gameObject.tag == "SocketEnd3" || collision.gameObject.tag == "SocketEnd2") && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(16f, 5f, 0);
                        wirePuzzleScript.correctWireRed = false;
                        if (!counterPlugged)
                        {
                            Handheld.Vibrate();
                            wirePuzzleScript.counter++;
                            wirePuzzleScript.flashOnce = false;
                            counterPlugged = true;
                        }
                    }
                }
                //Fifth Flash
                if (wirePuzzleScript.flashOrderStage2Picked == WirePuzzle.flashOrderStage2.RRBGR)
                {
                    if (collision.gameObject.tag == "SocketEnd2" && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(16f, 5f, 0);
                        wirePuzzleScript.correctWireRed = true;
                        if (!counterPlugged)
                        {
                            counterPlugged = true;
                        }
                    }
                    if ((collision.gameObject.tag == "SocketEnd3" || collision.gameObject.tag == "SocketEnd1" || collision.gameObject.tag == "SocketEnd4") && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(16f, 5f, 0);
                        wirePuzzleScript.correctWireRed = false;
                        if (!counterPlugged)
                        {
                            Handheld.Vibrate();
                            wirePuzzleScript.counter++;
                            wirePuzzleScript.flashOnce = false;
                            counterPlugged = true;
                        }
                    }
                }
                //Sixith Flash
                if (wirePuzzleScript.flashOrderStage2Picked == WirePuzzle.flashOrderStage2.BOG)
                {
                    if (collision.gameObject.tag == "SocketEnd3" && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(16f, 5f, 0);
                        wirePuzzleScript.correctWireRed = true;
                        if (!counterPlugged)
                        {
                            counterPlugged = true;
                        }
                    }
                    if ((collision.gameObject.tag == "SocketEnd4" || collision.gameObject.tag == "SocketEnd2" || collision.gameObject.tag == "SocketEnd1") && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(16f, 5f, 0);
                        wirePuzzleScript.correctWireRed = false;
                        if (!counterPlugged)
                        {
                            Handheld.Vibrate();
                            wirePuzzleScript.counter++;
                            wirePuzzleScript.flashOnce = false;
                            counterPlugged = true;
                        }
                    }
                }
                //Seventh Flash
                if (wirePuzzleScript.flashOrderStage2Picked == WirePuzzle.flashOrderStage2.RRRB) //look here
                {
                    if (collision.gameObject.tag == "SocketEnd4" && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(16f, 5f, 0);
                        wirePuzzleScript.correctWireRed = true;
                        if (!counterPlugged)
                        {
                            counterPlugged = true;
                        }
                    }
                    if ((collision.gameObject.tag == "SocketEnd2" || collision.gameObject.tag == "SocketEnd3" || collision.gameObject.tag == "SocketEnd1") && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(16f, 5f, 0);
                        wirePuzzleScript.correctWireRed = false;
                        if (!counterPlugged)
                        {
                            Handheld.Vibrate();
                            wirePuzzleScript.counter++;
                            wirePuzzleScript.flashOnce = false;
                            counterPlugged = true;
                        }
                    }

                }
                //Eight Flash
                if (wirePuzzleScript.flashOrderStage2Picked == WirePuzzle.flashOrderStage2.GGGGBR)
                {
                    if (collision.gameObject.tag == "SocketEnd3" && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(16f, 5f, 0);
                        wirePuzzleScript.correctWireRed = true;
                        if (!counterPlugged)
                        {
                            counterPlugged = true;
                        }
                    }
                    if ((collision.gameObject.tag == "SocketEnd1" || collision.gameObject.tag == "SocketEnd2" || collision.gameObject.tag == "SocketEnd4") && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(16f, 5f, 0);
                        wirePuzzleScript.correctWireRed = false;
                        if (!counterPlugged)
                        {
                            Handheld.Vibrate();
                            wirePuzzleScript.counter++;
                            wirePuzzleScript.flashOnce = false;
                            counterPlugged = true;
                        }
                    }
                }
            }

            #endregion

            #region Blue Wire Win Conditions

            if (this.gameObject.tag == "BlueWireEnd")
            {
                //First Flash
                if (wirePuzzleScript.flashOrderStage2Picked == WirePuzzle.flashOrderStage2.ROBG)
                {
                    if (collision.gameObject.tag == "SocketEnd3" && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(16f, -25f, 0);
                        wirePuzzleScript.correctWireBlue = true;
                        if (!counterPlugged)
                        {
                            counterPlugged = true;
                        }
                    }
                    if ((collision.gameObject.tag == "SocketEnd1" || collision.gameObject.tag == "SocketEnd2" || collision.gameObject.tag == "SocketEnd4") && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(16f, -25f, 0);
                        wirePuzzleScript.correctWireBlue = false;
                        if (!counterPlugged)
                        {
                            Handheld.Vibrate();
                            wirePuzzleScript.counter++;
                            wirePuzzleScript.flashOnce = false;
                            counterPlugged = true;
                        }
                    }
                }
                //Second Flash
                if (wirePuzzleScript.flashOrderStage2Picked == WirePuzzle.flashOrderStage2.OROB)
                {
                    if (collision.gameObject.tag == "SocketEnd3" && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(16f, -25f, 0);
                        wirePuzzleScript.correctWireBlue = true;
                        if (!counterPlugged)
                        {
                            counterPlugged = true;
                        }
                    }
                    if ((collision.gameObject.tag == "SocketEnd4" || collision.gameObject.tag == "SocketEnd1" || collision.gameObject.tag == "SocketEnd2") && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(16f, -25f, 0);
                        wirePuzzleScript.correctWireBlue = false;
                        if (!counterPlugged)
                        {
                            Handheld.Vibrate();
                            wirePuzzleScript.counter++;
                            wirePuzzleScript.flashOnce = false;
                            counterPlugged = true;
                        }
                    }
                }
                //Third Flash
                if (wirePuzzleScript.flashOrderStage2Picked == WirePuzzle.flashOrderStage2.OOOO)
                {
                    if (collision.gameObject.tag == "SocketEnd3" && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(16f, -25f, 0);
                        wirePuzzleScript.correctWireBlue = true;
                        if (!counterPlugged)
                        {
                            counterPlugged = true;
                        }
                    }
                    if ((collision.gameObject.tag == "SocketEnd4" || collision.gameObject.tag == "SocketEnd1" || collision.gameObject.tag == "SocketEnd2") && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(16f, -25f, 0);
                        wirePuzzleScript.correctWireBlue = false;
                        if (!counterPlugged)
                        {
                            Handheld.Vibrate();
                            wirePuzzleScript.counter++;
                            wirePuzzleScript.flashOnce = false;
                            counterPlugged = true;
                        }
                    }
                }
                //Fourth Flash
                if (wirePuzzleScript.flashOrderStage2Picked == WirePuzzle.flashOrderStage2.RBGR)
                {
                    if (collision.gameObject.tag == "SocketEnd3" && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(16f, -25f, 0);
                        wirePuzzleScript.correctWireBlue = true;
                        if (!counterPlugged)
                        {
                            counterPlugged = true;
                        }
                    }
                    if ((collision.gameObject.tag == "SocketEnd2" || collision.gameObject.tag == "SocketEnd1" || collision.gameObject.tag == "SocketEnd4") && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(16f, -25f, 0);
                        wirePuzzleScript.correctWireBlue = false;
                        if (!counterPlugged)
                        {
                            Handheld.Vibrate();
                            wirePuzzleScript.counter++;
                            wirePuzzleScript.flashOnce = false;
                            counterPlugged = true;
                        }
                    }
                }
                //Fifth Flash
                if (wirePuzzleScript.flashOrderStage2Picked == WirePuzzle.flashOrderStage2.RRBGR)
                {
                    if (collision.gameObject.tag == "SocketEnd4" && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(16f, -25f, 0);
                        wirePuzzleScript.correctWireBlue = true;
                        if (!counterPlugged)
                        {
                            counterPlugged = true;
                        }
                    }
                    if ((collision.gameObject.tag == "SocketEnd3" || collision.gameObject.tag == "SocketEnd1" || collision.gameObject.tag == "SocketEnd2") && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(16f, -25f, 0);
                        wirePuzzleScript.correctWireBlue = false;
                        if (!counterPlugged)
                        {
                            Handheld.Vibrate();
                            wirePuzzleScript.counter++;
                            wirePuzzleScript.flashOnce = false;
                            counterPlugged = true;
                        }
                    }
                }
                //Sixith Flash
                if (wirePuzzleScript.flashOrderStage2Picked == WirePuzzle.flashOrderStage2.BOG)
                {
                    if (collision.gameObject.tag == "SocketEnd3" && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(16f, -25f, 0);
                        wirePuzzleScript.correctWireBlue = true;
                        if (!counterPlugged)
                        {
                            counterPlugged = true;
                        }
                    }
                    if ((collision.gameObject.tag == "SocketEnd4" || collision.gameObject.tag == "SocketEnd2") || collision.gameObject.tag == "SocketEnd1" && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(16f, -25f, 0);
                        wirePuzzleScript.correctWireBlue = false;
                        if (!counterPlugged)
                        {
                            Handheld.Vibrate();
                            wirePuzzleScript.counter++;
                            wirePuzzleScript.flashOnce = false;
                            counterPlugged = true;
                        }
                    }
                }
                //Seventh Flash
                if (wirePuzzleScript.flashOrderStage2Picked == WirePuzzle.flashOrderStage2.RRRB)
                {
                    if (collision.gameObject.tag == "SocketEnd3" && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(16f, -25f, 0);
                        wirePuzzleScript.correctWireBlue = true;
                        if (!counterPlugged)
                        {
                            counterPlugged = true;
                        }
                    }
                    if ((collision.gameObject.tag == "SocketEnd4" || collision.gameObject.tag == "SocketEnd2" || collision.gameObject.tag == "SocketEnd1") && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(16f, -25f, 0);
                        if (!counterPlugged)
                        {
                            Handheld.Vibrate();
                            wirePuzzleScript.counter++;
                            wirePuzzleScript.flashOnce = false;
                            counterPlugged = true;
                        }
                    }
                }
                //Eight Flash
                if (wirePuzzleScript.flashOrderStage2Picked == WirePuzzle.flashOrderStage2.GGGGBR)
                {
                    if (collision.gameObject.tag == "SocketEnd3" && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(16f, -25f, 0);
                        wirePuzzleScript.correctWireBlue = true;
                        if (!counterPlugged)
                        {
                            counterPlugged = true;
                        }
                    }
                    if ((collision.gameObject.tag == "SocketEnd1" || collision.gameObject.tag == "SocketEnd4" || collision.gameObject.tag == "SocketEnd2") && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(16f, -25f, 0);
                        wirePuzzleScript.correctWireBlue = false;
                        if (!counterPlugged)
                        {
                            Handheld.Vibrate();
                            wirePuzzleScript.counter++;
                            wirePuzzleScript.flashOnce = false;
                            counterPlugged = true;
                        }
                    }
                }
            }
            #endregion
        }

        if (wirePuzzleScript.pickedFlashStage == WirePuzzle.flashStages.stage3)
        {
            #region Yellow Wire Win Conditions
            if (this.gameObject.tag == "YellowWireEnd")
            {
                //First Flash
                if (wirePuzzleScript.flashOrderStage3Picked == WirePuzzle.flashOrderStage3.GRBGRR)
                {
                    if (collision.gameObject.tag == "SocketEnd3" && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(-17, -25f, 0);
                        wirePuzzleScript.correctWireYellow = true;
                        if (!counterPlugged)
                        {
                            counterPlugged = true;
                        }
                    }
                    if ((collision.gameObject.tag == "SocketEnd1" || collision.gameObject.tag == "SocketEnd2" || collision.gameObject.tag == "SocketEnd4") && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(-17, -25f, 0);
                        wirePuzzleScript.correctWireYellow = false;
                        if (!counterPlugged)
                        {
                            Handheld.Vibrate();
                            wirePuzzleScript.counter++;
                            wirePuzzleScript.flashOnce = false;
                            counterPlugged = true;
                        }
                    }
                }
                //Second Flash
                if (wirePuzzleScript.flashOrderStage3Picked == WirePuzzle.flashOrderStage3.BGRGOR)
                {
                    if (collision.gameObject.tag == "SocketEnd4" && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(-17, -25f, 0);
                        wirePuzzleScript.correctWireYellow = true;
                        if (!counterPlugged)
                        {
                            counterPlugged = true;
                        }
                    }
                    if ((collision.gameObject.tag == "SocketEnd1" || collision.gameObject.tag == "SocketEnd3" || collision.gameObject.tag == "SocketEnd2") && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(-17, -25f, 0);
                        wirePuzzleScript.correctWireYellow = false;
                        if (!counterPlugged)
                        {
                            Handheld.Vibrate();
                            wirePuzzleScript.counter++;
                            wirePuzzleScript.flashOnce = false;
                            counterPlugged = true;
                        }
                    }
                }
                //Third Flash
                if (wirePuzzleScript.flashOrderStage3Picked == WirePuzzle.flashOrderStage3.BBB)
                {
                    if ((collision.gameObject.tag == "SocketEnd2" || collision.gameObject.tag == "SocketEnd3" || collision.gameObject.tag == "SocketEnd4") && plugged == false || collision.gameObject.tag == "SocketEnd1")
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(-17, -25f, 0);
                        wirePuzzleScript.correctWireYellow = false;
                        if (!counterPlugged)
                        {
                            Handheld.Vibrate();
                            wirePuzzleScript.counter++;
                            wirePuzzleScript.flashOnce = false;
                            counterPlugged = true;
                        }
                    }
                }
                //Forth Flash                                                                       HERRERERERERERERERERER
                if (wirePuzzleScript.flashOrderStage3Picked == WirePuzzle.flashOrderStage3.GRGB)
                {
                    if ((collision.gameObject.tag == "SocketEnd2" || collision.gameObject.tag == "SocketEnd3" || collision.gameObject.tag == "SocketEnd4") && plugged == false || collision.gameObject.tag == "SocketEnd1")
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(-17, -25f, 0);
                        wirePuzzleScript.correctWireYellow = false;
                        if (!counterPlugged)
                        {
                            Handheld.Vibrate();
                            wirePuzzleScript.counter++;
                            wirePuzzleScript.flashOnce = false;
                            counterPlugged = true;
                        }
                    }
                }
                //Fifth Flash
                if (wirePuzzleScript.flashOrderStage3Picked == WirePuzzle.flashOrderStage3.OBOR)
                {
                    if (collision.gameObject.tag == "SocketEnd2" && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(-17, -25f, 0);
                        wirePuzzleScript.correctWireYellow = true;
                        if (!counterPlugged)
                        {
                            counterPlugged = true;
                        }
                    }
                    if ((collision.gameObject.tag == "SocketEnd4" || collision.gameObject.tag == "SocketEnd3" || collision.gameObject.tag == "SocketEnd1") && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(-17, -25f, 0);
                        wirePuzzleScript.correctWireYellow = false;
                        if (!counterPlugged)
                        {
                            Handheld.Vibrate();
                            wirePuzzleScript.counter++;
                            wirePuzzleScript.flashOnce = false;
                            counterPlugged = true;
                        }
                    }
                }
                //Sixith Flash
                if (wirePuzzleScript.flashOrderStage3Picked == WirePuzzle.flashOrderStage3.ORRB)
                {
                    if (collision.gameObject.tag == "SocketEnd4" && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(-17, -25f, 0);
                        wirePuzzleScript.correctWireYellow = true;
                        if (!counterPlugged)
                        {
                            counterPlugged = true;
                        }
                    }
                    if ((collision.gameObject.tag == "SocketEnd1" || collision.gameObject.tag == "SocketEnd2" || collision.gameObject.tag == "SocketEnd3") && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(-17, -25f, 0);
                        wirePuzzleScript.correctWireYellow = false;
                        if (!counterPlugged)
                        {
                            Handheld.Vibrate();
                            wirePuzzleScript.counter++;
                            wirePuzzleScript.flashOnce = false;
                            counterPlugged = true;
                        }
                    }
                }
                //SeventhFlash
                if (wirePuzzleScript.flashOrderStage3Picked == WirePuzzle.flashOrderStage3.GGGG)
                {
                    if (collision.gameObject.tag == "SocketEnd1" && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(-17, -25f, 0);
                        wirePuzzleScript.correctWireYellow = true;
                        if (!counterPlugged)
                        {
                            counterPlugged = true;
                        }
                    }
                    if ((collision.gameObject.tag == "SocketEnd2" || collision.gameObject.tag == "SocketEnd4" || collision.gameObject.tag == "SocketEnd3") && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(-17, -25f, 0);
                        wirePuzzleScript.correctWireYellow = false;
                        if (!counterPlugged)
                        {
                            Handheld.Vibrate();
                            wirePuzzleScript.counter++;
                            wirePuzzleScript.flashOnce = false;
                            counterPlugged = true;
                        }
                    }
                }
                //Eighth Flash
                if (wirePuzzleScript.flashOrderStage3Picked == WirePuzzle.flashOrderStage3.RBGO)
                {
                    if (collision.gameObject.tag == "SocketEnd4" && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(-17, -25f, 0);
                        wirePuzzleScript.correctWireYellow = true;
                        if (!counterPlugged)
                        {
                            counterPlugged = true;
                        }
                    }
                    if ((collision.gameObject.tag == "SocketEnd2" || collision.gameObject.tag == "SocketEnd3" || collision.gameObject.tag == "SocketEnd1") && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(-17, -25f, 0);
                        wirePuzzleScript.correctWireYellow = false;
                        if (!counterPlugged)
                        {
                            Handheld.Vibrate();
                            wirePuzzleScript.counter++;
                            wirePuzzleScript.flashOnce = false;
                            counterPlugged = true;
                        }
                    }
                }
            }
            #endregion

            #region Green Wire Win Conditions
            if (this.gameObject.tag == "GreenWireEnd")
            {
                //First Flash
                if (wirePuzzleScript.flashOrderStage3Picked == WirePuzzle.flashOrderStage3.GRBGRR)
                {
                    if ((collision.gameObject.tag == "SocketEnd1" || collision.gameObject.tag == "SocketEnd3" || collision.gameObject.tag == "SocketEnd2" || collision.gameObject.tag == "SocketEnd4") && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(-17, 5f, 0);
                        wirePuzzleScript.correctWireGreen = false;
                        if (!counterPlugged)
                        {
                            Handheld.Vibrate();
                            wirePuzzleScript.counter++;
                            wirePuzzleScript.flashOnce = false;
                            counterPlugged = true;
                        }
                    }
                }
                //Second Flash
                if (wirePuzzleScript.flashOrderStage3Picked == WirePuzzle.flashOrderStage3.BGRGOR)
                {
                    if (collision.gameObject.tag == "SocketEnd3" && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(-17, 5f, 0);
                        wirePuzzleScript.correctWireGreen = true;
                        if (!counterPlugged)
                        {
                            counterPlugged = true;
                        }
                    }
                    if ((collision.gameObject.tag == "SocketEnd2" || collision.gameObject.tag == "SocketEnd1" || collision.gameObject.tag == "SocketEnd4") && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(-17, 5f, 0);
                        wirePuzzleScript.correctWireGreen = false;
                        if (!counterPlugged)
                        {
                            Handheld.Vibrate();
                            wirePuzzleScript.counter++;
                            wirePuzzleScript.flashOnce = false;
                            counterPlugged = true;
                        }
                    }
                }
                //Third Flash
                if (wirePuzzleScript.flashOrderStage3Picked == WirePuzzle.flashOrderStage3.BBB)
                {
                    if ((collision.gameObject.tag == "SocketEnd2" || collision.gameObject.tag == "SocketEnd1" || collision.gameObject.tag == "SocketEnd3" || collision.gameObject.tag == "SocketEnd4") && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(-17, 5f, 0);
                        wirePuzzleScript.correctWireGreen = false;
                        if (!counterPlugged)
                        {
                            Handheld.Vibrate();
                            wirePuzzleScript.counter++;
                            wirePuzzleScript.flashOnce = false;
                            counterPlugged = true;
                        }
                    }
                }
                //Fourth Flash
                if (wirePuzzleScript.flashOrderStage3Picked == WirePuzzle.flashOrderStage3.GRGB)
                {
                    if (collision.gameObject.tag == "SocketEnd3" && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(-17, 5f, 0);
                        wirePuzzleScript.correctWireGreen = true;
                        if (!counterPlugged)
                        {
                            counterPlugged = true;
                        }
                    }
                    if ((collision.gameObject.tag == "SocketEnd2" || collision.gameObject.tag == "SocketEnd1" || collision.gameObject.tag == "SocketEnd4") && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(-17, 5f, 0);
                        wirePuzzleScript.correctWireGreen = false;
                        if (!counterPlugged)
                        {
                            Handheld.Vibrate();
                            wirePuzzleScript.counter++;
                            wirePuzzleScript.flashOnce = false;
                            counterPlugged = true;
                        }
                    }
                }
                //Fifth Flash                                                               here
                if (wirePuzzleScript.flashOrderStage3Picked == WirePuzzle.flashOrderStage3.OBOR)
                {
                    if (collision.gameObject.tag == "SocketEnd3" && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(-17, 5f, 0);
                        wirePuzzleScript.correctWireGreen = true;
                        if (!counterPlugged)
                        {
                            counterPlugged = true;
                        }
                    }
                    if ((collision.gameObject.tag == "SocketEnd2" || collision.gameObject.tag == "SocketEnd1" || collision.gameObject.tag == "SocketEnd4") && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(-17, 5f, 0);
                        wirePuzzleScript.correctWireGreen = false;
                        if (!counterPlugged)
                        {
                            Handheld.Vibrate();
                            wirePuzzleScript.counter++;
                            wirePuzzleScript.flashOnce = false;
                            counterPlugged = true;
                        }
                    }
                }
                //Sixith Flash
                if (wirePuzzleScript.flashOrderStage3Picked == WirePuzzle.flashOrderStage3.ORRB)
                {
                    if (collision.gameObject.tag == "SocketEnd3" && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(-17, 5f, 0);
                        wirePuzzleScript.correctWireGreen = true;
                        if (!counterPlugged)
                        {
                            counterPlugged = true;
                        }
                    }
                    if ((collision.gameObject.tag == "SocketEnd2" || collision.gameObject.tag == "SocketEnd1" || collision.gameObject.tag == "SocketEnd4") && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(-17, 5f, 0);
                        wirePuzzleScript.correctWireGreen = false;
                        if (!counterPlugged)
                        {
                            Handheld.Vibrate();
                            wirePuzzleScript.counter++;
                            wirePuzzleScript.flashOnce = false;
                            counterPlugged = true;
                        }
                    }
                }
                //Sevinth Flash
                if (wirePuzzleScript.flashOrderStage3Picked == WirePuzzle.flashOrderStage3.GGGG)
                {
                    if (collision.gameObject.tag == "SocketEnd1" && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(-17, 5f, 0);
                        wirePuzzleScript.correctWireGreen = true;
                        if (!counterPlugged)
                        {
                            counterPlugged = true;
                        }
                    }
                    if ((collision.gameObject.tag == "SocketEnd4" || collision.gameObject.tag == "SocketEnd3" || collision.gameObject.tag == "SocketEnd2") && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(-17, 5f, 0);
                        wirePuzzleScript.correctWireGreen = false;
                        if (!counterPlugged)
                        {
                            Handheld.Vibrate();
                            wirePuzzleScript.counter++;
                            wirePuzzleScript.flashOnce = false;
                            counterPlugged = true;
                        }
                    }
                }
                //Eighth Flash
                if (wirePuzzleScript.flashOrderStage3Picked == WirePuzzle.flashOrderStage3.RBGO)
                {
                    if (collision.gameObject.tag == "SocketEnd1" && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(-17, 5f, 0);
                        wirePuzzleScript.correctWireGreen = true;
                        if (!counterPlugged)
                        {
                            counterPlugged = true;
                        }
                    }
                    if ((collision.gameObject.tag == "SocketEnd2" || collision.gameObject.tag == "SocketEnd3" || collision.gameObject.tag == "SocketEnd4") && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(-17, 5f, 0);
                        wirePuzzleScript.correctWireGreen = false;
                        if (!counterPlugged)
                        {
                            Handheld.Vibrate();
                            wirePuzzleScript.counter++;
                            wirePuzzleScript.flashOnce = false;
                            counterPlugged = true;
                        }
                    }
                }
            }

            #endregion

            #region Red Wire Win Conditions
            if (this.gameObject.tag == "RedWireEnd")
            {
                //First Flash
                if (wirePuzzleScript.flashOrderStage3Picked == WirePuzzle.flashOrderStage3.GRBGRR)
                {
                    if ((collision.gameObject.tag == "SocketEnd2" || collision.gameObject.tag == "SocketEnd3" || collision.gameObject.tag == "SocketEnd4" || collision.gameObject.tag == "SocketEnd1") && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(16f, 5f, 0);
                        wirePuzzleScript.correctWireRed = false;
                        if (!counterPlugged)
                        {
                            Handheld.Vibrate();
                            wirePuzzleScript.counter++;
                            wirePuzzleScript.flashOnce = false;
                            counterPlugged = true;
                        }
                    }
                }
                //Second Flash
                if (wirePuzzleScript.flashOrderStage3Picked == WirePuzzle.flashOrderStage3.BGRGOR)
                {
                    if (collision.gameObject.tag == "SocketEnd1" && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(16f, 5f, 0);
                        wirePuzzleScript.correctWireRed = true;
                        if (!counterPlugged)
                        {
                            counterPlugged = true;
                        }
                    }
                    if ((collision.gameObject.tag == "SocketEnd3" || collision.gameObject.tag == "SocketEnd2" || collision.gameObject.tag == "SocketEnd4") && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position;
                        wirePuzzleScript.correctWireRed = false;
                        if (!counterPlugged)
                        {
                            Handheld.Vibrate();
                            wirePuzzleScript.counter++;
                            wirePuzzleScript.flashOnce = false;
                            counterPlugged = true;
                        }
                    }
                }
                //Third Flash
                if (wirePuzzleScript.flashOrderStage3Picked == WirePuzzle.flashOrderStage3.BBB)
                {
                    if (collision.gameObject.tag == "SocketEnd3" && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(16f, 5f, 0);
                        wirePuzzleScript.correctWireRed = true;
                        if (!counterPlugged)
                        {
                            counterPlugged = true;
                        }
                    }
                    if ((collision.gameObject.tag == "SocketEnd2" || collision.gameObject.tag == "SocketEnd1" || collision.gameObject.tag == "SocketEnd4") && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(16f, 5f, 0);
                        wirePuzzleScript.correctWireRed = false;
                        if (!counterPlugged)
                        {
                            Handheld.Vibrate();
                            wirePuzzleScript.counter++;
                            wirePuzzleScript.flashOnce = false;
                            counterPlugged = true;
                        }
                    }
                }
                //Forth Flash
                if (wirePuzzleScript.flashOrderStage3Picked == WirePuzzle.flashOrderStage3.GRGB)
                {
                    if (collision.gameObject.tag == "SocketEnd3" && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(16f, 5f, 0);
                        wirePuzzleScript.correctWireRed = true;
                        if (!counterPlugged)
                        {
                            counterPlugged = true;
                        }
                    }
                    if ((collision.gameObject.tag == "SocketEnd4" || collision.gameObject.tag == "SocketEnd1" || collision.gameObject.tag == "SocketEnd2") && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(16f, 5f, 0);
                        wirePuzzleScript.correctWireRed = false;
                        if (!counterPlugged)
                        {
                            Handheld.Vibrate();
                            wirePuzzleScript.counter++;
                            wirePuzzleScript.flashOnce = false;
                            counterPlugged = true;
                        }
                    }
                }
                //Fifth Flash
                if (wirePuzzleScript.flashOrderStage3Picked == WirePuzzle.flashOrderStage3.OBOR)
                {
                    if (collision.gameObject.tag == "SocketEnd4" && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(16f, 5f, 0);
                        wirePuzzleScript.correctWireRed = true;
                        if (!counterPlugged)
                        {
                            counterPlugged = true;
                        }
                    }
                    if ((collision.gameObject.tag == "SocketEnd3" || collision.gameObject.tag == "SocketEnd1" || collision.gameObject.tag == "SocketEnd2") && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(16f, 5f, 0);
                        wirePuzzleScript.correctWireRed = false;
                        if (!counterPlugged)
                        {
                            Handheld.Vibrate();
                            wirePuzzleScript.counter++;
                            wirePuzzleScript.flashOnce = false;
                            counterPlugged = true;
                        }
                    }
                }
                //Sixith Flash
                if (wirePuzzleScript.flashOrderStage3Picked == WirePuzzle.flashOrderStage3.ORRB)
                {
                    if (collision.gameObject.tag == "SocketEnd3" && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(16f, 5f, 0);
                        wirePuzzleScript.correctWireRed = true;
                        if (!counterPlugged)
                        {
                            counterPlugged = true;
                        }
                    }
                    if ((collision.gameObject.tag == "SocketEnd4" || collision.gameObject.tag == "SocketEnd1" || collision.gameObject.tag == "SocketEnd2") && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(16f, 5f, 0);
                        wirePuzzleScript.correctWireRed = true;
                        if (!counterPlugged)
                        {
                            Handheld.Vibrate();
                            wirePuzzleScript.counter++;
                            wirePuzzleScript.flashOnce = false;
                            counterPlugged = true;
                        }
                    }
                }
                //Seventh Flash
                if (wirePuzzleScript.flashOrderStage3Picked == WirePuzzle.flashOrderStage3.GGGG)
                {
                    if (collision.gameObject.tag == "SocketEnd2" && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(16f, 5f, 0);
                        wirePuzzleScript.correctWireRed = true;
                        if (!counterPlugged)
                        {
                            counterPlugged = true;
                        }
                    }
                    if ((collision.gameObject.tag == "SocketEnd4" || collision.gameObject.tag == "SocketEnd3" || collision.gameObject.tag == "SocketEnd1") && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(16f, 5f, 0);
                        wirePuzzleScript.correctWireRed = false;
                        if (!counterPlugged)
                        {
                            Handheld.Vibrate();
                            wirePuzzleScript.counter++;
                            wirePuzzleScript.flashOnce = false;
                            counterPlugged = true;
                        }
                    }
                }
                //Eight Flash
                if (wirePuzzleScript.flashOrderStage3Picked == WirePuzzle.flashOrderStage3.RBGO)
                {
                    if (collision.gameObject.tag == "SocketEnd2" && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(16f, 5f, 0);
                        wirePuzzleScript.correctWireRed = true;
                        if (!counterPlugged)
                        {
                            counterPlugged = true;
                        }
                    }
                    if ((collision.gameObject.tag == "SocketEnd1" || collision.gameObject.tag == "SocketEnd3" || collision.gameObject.tag == "SocketEnd4") && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(16f, 5f, 0);
                        wirePuzzleScript.correctWireRed = false;
                        if (!counterPlugged)
                        {
                            Handheld.Vibrate();
                            wirePuzzleScript.counter++;
                            wirePuzzleScript.flashOnce = false;
                            counterPlugged = true;
                        }
                    }
                }
            }

            #endregion

            #region Blue Wire Win Conditions

            if (this.gameObject.tag == "BlueWireEnd")
            {
                //First Flash
                if (wirePuzzleScript.flashOrderStage3Picked == WirePuzzle.flashOrderStage3.GRBGRR)
                {
                    if ((collision.gameObject.tag == "SocketEnd1" || collision.gameObject.tag == "SocketEnd2" || collision.gameObject.tag == "SocketEnd4") && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(16f, -25f, 0);
                        wirePuzzleScript.correctWireBlue = false;
                        if (!counterPlugged)
                        {
                            Handheld.Vibrate();
                            wirePuzzleScript.counter++;
                            wirePuzzleScript.flashOnce = false;
                            counterPlugged = true;
                        }
                    }
                }
                //Second Flash
                if (wirePuzzleScript.flashOrderStage3Picked == WirePuzzle.flashOrderStage3.BGRGOR)
                {
                    if (collision.gameObject.tag == "SocketEnd2" && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(16f, -25f, 0);
                        wirePuzzleScript.correctWireBlue = true;
                        if (!counterPlugged)
                        {
                            counterPlugged = true;
                        }
                    }
                    if ((collision.gameObject.tag == "SocketEnd4" || collision.gameObject.tag == "SocketEnd3" || collision.gameObject.tag == "SocketEnd1") && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(16f, -25f, 0);
                        wirePuzzleScript.correctWireBlue = false;
                        if (!counterPlugged)
                        {
                            Handheld.Vibrate();
                            wirePuzzleScript.counter++;
                            wirePuzzleScript.flashOnce = false;
                            counterPlugged = true;
                        }
                    }
                }
                //Third Flash
                if (wirePuzzleScript.flashOrderStage3Picked == WirePuzzle.flashOrderStage3.BBB)
                {
                    if ((collision.gameObject.tag == "SocketEnd4" || collision.gameObject.tag == "SocketEnd3" || collision.gameObject.tag == "SocketEnd1" || collision.gameObject.tag == "SocketEnd2") && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(16f, -25f, 0);
                        wirePuzzleScript.correctWireBlue = false;
                        if (!counterPlugged)
                        {
                            Handheld.Vibrate();
                            wirePuzzleScript.counter++;
                            wirePuzzleScript.flashOnce = false;
                            counterPlugged = true;
                        }
                    }
                }
                //Fourth Flash
                if (wirePuzzleScript.flashOrderStage3Picked == WirePuzzle.flashOrderStage3.GRGB)
                {
                    if (collision.gameObject.tag == "SocketEnd4" && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(16f, -25f, 0);
                        wirePuzzleScript.correctWireBlue = true;
                        if (!counterPlugged)
                        {
                            counterPlugged = true;
                        }
                    }
                    if ((collision.gameObject.tag == "SocketEnd2" || collision.gameObject.tag == "SocketEnd1" || collision.gameObject.tag == "SocketEnd3") && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(16f, -25f, 0);
                        wirePuzzleScript.correctWireBlue = false;
                        if (!counterPlugged)
                        {
                            Handheld.Vibrate();
                            wirePuzzleScript.counter++;
                            wirePuzzleScript.flashOnce = false;
                            counterPlugged = true;
                        }
                    }
                }
                //Fifth Flash
                if (wirePuzzleScript.flashOrderStage3Picked == WirePuzzle.flashOrderStage3.OBOR)
                {
                    if (collision.gameObject.tag == "SocketEnd1" && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(16f, -25f, 0);
                        wirePuzzleScript.correctWireBlue = true;
                        if (!counterPlugged)
                        {
                            counterPlugged = true;
                        }
                    }
                    if ((collision.gameObject.tag == "SocketEnd3" || collision.gameObject.tag == "SocketEnd4" || collision.gameObject.tag == "SocketEnd2") && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(16f, -25f, 0);
                        wirePuzzleScript.correctWireBlue = false;
                        if (!counterPlugged)
                        {
                            Handheld.Vibrate();
                            wirePuzzleScript.counter++;
                            wirePuzzleScript.flashOnce = false;
                            counterPlugged = true;
                        }
                    }
                }
                //Sixith Flash
                if (wirePuzzleScript.flashOrderStage3Picked == WirePuzzle.flashOrderStage3.ORRB)
                {
                    if (collision.gameObject.tag == "SocketEnd4" && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(16f, -25f, 0);
                        wirePuzzleScript.correctWireBlue = true;
                        if (!counterPlugged)
                        {
                            counterPlugged = true;
                        }
                    }
                    if ((collision.gameObject.tag == "SocketEnd3" || collision.gameObject.tag == "SocketEnd2") || collision.gameObject.tag == "SocketEnd1" && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(16f, -25f, 0);
                        wirePuzzleScript.correctWireBlue = false;
                        if (!counterPlugged)
                        {
                            Handheld.Vibrate();
                            wirePuzzleScript.counter++;
                            wirePuzzleScript.flashOnce = false;
                            counterPlugged = true;
                        }
                    }
                }
                //Seventh Flash
                if (wirePuzzleScript.flashOrderStage3Picked == WirePuzzle.flashOrderStage3.GGGG)
                {
                    if (collision.gameObject.tag == "SocketEnd2" && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(16f, -25f, 0);
                        wirePuzzleScript.correctWireBlue = true;
                        if (!counterPlugged)
                        {
                            counterPlugged = true;
                        }
                    }
                    if ((collision.gameObject.tag == "SocketEnd4" || collision.gameObject.tag == "SocketEnd3" || collision.gameObject.tag == "SocketEnd1") && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(16f, -25f, 0);
                        wirePuzzleScript.correctWireBlue = false;
                        if (!counterPlugged)
                        {
                            Handheld.Vibrate();
                            wirePuzzleScript.counter++;
                            wirePuzzleScript.flashOnce = false;
                            counterPlugged = true;
                        }
                    }
                }
                //Eight Flash
                if (wirePuzzleScript.flashOrderStage3Picked == WirePuzzle.flashOrderStage3.RBGO)
                {
                    if (collision.gameObject.tag == "SocketEnd3" && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(16f, -25f, 0);
                        wirePuzzleScript.correctWireBlue = true;
                        if (!counterPlugged)
                        {
                            counterPlugged = true;
                        }
                    }
                    if ((collision.gameObject.tag == "SocketEnd1" || collision.gameObject.tag == "SocketEnd4" || collision.gameObject.tag == "SocketEnd2") && plugged == false)
                    {
                        plugged = true;
                        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        transform.position = collision.transform.position + new Vector3(16f, -25f, 0);
                        wirePuzzleScript.correctWireBlue = false;
                        if (!counterPlugged)
                        {
                            Handheld.Vibrate();
                            wirePuzzleScript.counter++;
                            wirePuzzleScript.flashOnce = false;
                            counterPlugged = true;
                        }
                    }
                }
            }
            #endregion
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(wirePuzzleScript.pickedFlashStage == WirePuzzle.flashStages.stage1)
        {
            if (this.gameObject.tag == "YellowWireEnd")
            {
                if (wirePuzzleScript.flashOrderSelected == WirePuzzle.FlashOrder.RGBO)
                {
                    if ((collision.gameObject.tag == "SocketEnd1" || collision.gameObject.tag == "SocketEnd2" || collision.gameObject.tag == "SocketEnd3" || collision.gameObject.tag == "SocketEnd4") && plugged == false)
                    {
                        counterPlugged = false;
                        plugged = false;
                        audioPlayed = false;
                    }
                }
                //Second Flash
                if (wirePuzzleScript.flashOrderSelected == WirePuzzle.FlashOrder.RRBG)
                {
                    if ((collision.gameObject.tag == "SocketEnd2" || collision.gameObject.tag == "SocketEnd3" || collision.gameObject.tag == "SocketEnd4" || collision.gameObject.tag == "SocketEnd1") && plugged == false)
                    {
                        counterPlugged = false;
                        plugged = false;
                        audioPlayed = false;
                    }
                }

                if (wirePuzzleScript.flashOrderSelected == WirePuzzle.FlashOrder.OOGG)
                {
                    if ((collision.gameObject.tag == "SocketEnd2" || collision.gameObject.tag == "SocketEnd3" || collision.gameObject.tag == "SocketEnd4" || collision.gameObject.tag == "SocketEnd1") && plugged == false)
                    {
                        counterPlugged = false;
                        plugged = false;
                        audioPlayed = false;
                    }
                }

                //Third Flash
                if (wirePuzzleScript.flashOrderSelected == WirePuzzle.FlashOrder.RGRB)
                {
                    if ((collision.gameObject.tag == "SocketEnd2" || collision.gameObject.tag == "SocketEnd3" || collision.gameObject.tag == "SocketEnd1" || collision.gameObject.tag == "SocketEnd4") && plugged == false)
                    {
                        counterPlugged = false;
                        plugged = false;
                        audioPlayed = false;
                    }
                }
                //Forth Flash
                if (wirePuzzleScript.flashOrderSelected == WirePuzzle.FlashOrder.OBGR)
                {
                    if ((collision.gameObject.tag == "SocketEnd2" || collision.gameObject.tag == "SocketEnd3" || collision.gameObject.tag == "SocketEnd4" || collision.gameObject.tag == "SocketEnd1") && plugged == false)
                    {
                        counterPlugged = false;
                        plugged = false;
                        audioPlayed = false;
                    }
                }
                //Fifth Flash
                if (wirePuzzleScript.flashOrderSelected == WirePuzzle.FlashOrder.OROR)
                {
                    if ((collision.gameObject.tag == "SocketEnd2" || collision.gameObject.tag == "SocketEnd3" || collision.gameObject.tag == "SocketEnd1" || collision.gameObject.tag == "SocketEnd4") && plugged == false)
                    {
                        counterPlugged = false;
                        plugged = false;
                        audioPlayed = false;
                    }
                }
                //SeventhFlash
                if (wirePuzzleScript.flashOrderSelected == WirePuzzle.FlashOrder.GRBOGR)
                {
                    if ((collision.gameObject.tag == "SocketEnd2" || collision.gameObject.tag == "SocketEnd4" || collision.gameObject.tag == "SocketEnd1" || collision.gameObject.tag == "SocketEnd3") && plugged == false)
                    {
                        counterPlugged = false;
                        plugged = false;
                        audioPlayed = false;
                    }
                }
                //Eighth Flash
                if (wirePuzzleScript.flashOrderSelected == WirePuzzle.FlashOrder.BGGR)
                {
                    if ((collision.gameObject.tag == "SocketEnd2" || collision.gameObject.tag == "SocketEnd3" || collision.gameObject.tag == "SocketEnd4" || collision.gameObject.tag == "SocketEnd1") && plugged == false)
                    {
                        counterPlugged = false;
                        plugged = false;
                        audioPlayed = false;
                    }
                }
            }
            if (this.gameObject.tag == "GreenWireEnd")
            {
                //First Flash
                if (wirePuzzleScript.flashOrderSelected == WirePuzzle.FlashOrder.RGBO)
                {
                    if ((collision.gameObject.tag == "SocketEnd1" || collision.gameObject.tag == "SocketEnd3" || collision.gameObject.tag == "SocketEnd4" || collision.gameObject.tag == "SocketEnd2") && plugged == false)
                    {
                        counterPlugged = false;
                        plugged = false;
                        audioPlayed = false;
                    }
                }
                //Second Flash
                if (wirePuzzleScript.flashOrderSelected == WirePuzzle.FlashOrder.RRBG)
                {
                    if ((collision.gameObject.tag == "SocketEnd2" || collision.gameObject.tag == "SocketEnd1" || collision.gameObject.tag == "SocketEnd4" || collision.gameObject.tag == "SocketEnd3") && plugged == false)
                    {
                        counterPlugged = false;
                        plugged = false;
                        audioPlayed = false;
                    }
                }
                //Third Flash
                if (wirePuzzleScript.flashOrderSelected == WirePuzzle.FlashOrder.RGRB)
                {
                    if ((collision.gameObject.tag == "SocketEnd2" || collision.gameObject.tag == "SocketEnd1" || collision.gameObject.tag == "SocketEnd4" || collision.gameObject.tag == "SocketEnd3") && plugged == false)
                    {
                        counterPlugged = false;
                        plugged = false;
                        audioPlayed = false;
                    }
                }
                //Fourth Flash
                if (wirePuzzleScript.flashOrderSelected == WirePuzzle.FlashOrder.OBGR)
                {
                    if ((collision.gameObject.tag == "SocketEnd2" || collision.gameObject.tag == "SocketEnd1" || collision.gameObject.tag == "SocketEnd4" || collision.gameObject.tag == "SocketEnd3") && plugged == false)
                    {
                        counterPlugged = false;
                        plugged = false;
                        audioPlayed = false;
                    }
                }
                //Fifth Flash
                if (wirePuzzleScript.flashOrderSelected == WirePuzzle.FlashOrder.OROR)
                {
                    if ((collision.gameObject.tag == "SocketEnd2" || collision.gameObject.tag == "SocketEnd1" || collision.gameObject.tag == "SocketEnd3" || collision.gameObject.tag == "SocketEnd4") && plugged == false)
                    {
                        counterPlugged = false;
                        plugged = false;
                        audioPlayed = false;
                    }
                }
                //Sixith Flash
                if (wirePuzzleScript.flashOrderSelected == WirePuzzle.FlashOrder.OOGG)
                {
                    if ((collision.gameObject.tag == "SocketEnd3" || collision.gameObject.tag == "SocketEnd1" || collision.gameObject.tag == "SocketEnd4" || collision.gameObject.tag == "SocketEnd2") && plugged == false)
                    {
                        counterPlugged = false;
                        plugged = false;
                        audioPlayed = false;
                    }
                }
                //Sevinth Flash
                if (wirePuzzleScript.flashOrderSelected == WirePuzzle.FlashOrder.GRBOGR)
                {
                    if ((collision.gameObject.tag == "SocketEnd2" || collision.gameObject.tag == "SocketEnd3" || collision.gameObject.tag == "SocketEnd4" || collision.gameObject.tag == "SocketEnd1") && plugged == false)
                    {
                        counterPlugged = false;
                        plugged = false;
                        audioPlayed = false;
                    }
                }
                //Eighth Flash
                if (wirePuzzleScript.flashOrderSelected == WirePuzzle.FlashOrder.BGGR)
                {
                    if ((collision.gameObject.tag == "SocketEnd2" || collision.gameObject.tag == "SocketEnd3" || collision.gameObject.tag == "SocketEnd4" || collision.gameObject.tag == "SocketEnd1") && plugged == false)
                    {
                        counterPlugged = false;
                        plugged = false;
                        audioPlayed = false;
                    }
                }
            }
            if (this.gameObject.tag == "RedWireEnd")
            {
                //First Flash
                if (wirePuzzleScript.flashOrderSelected == WirePuzzle.FlashOrder.RGBO)
                {
                    if ((collision.gameObject.tag == "SocketEnd2" || collision.gameObject.tag == "SocketEnd3" || collision.gameObject.tag == "SocketEnd4" || collision.gameObject.tag == "SocketEnd1") && plugged == false)
                    {
                        counterPlugged = false;
                        plugged = false;
                        audioPlayed = false;
                    }
                }
                //Second Flash
                if (wirePuzzleScript.flashOrderSelected == WirePuzzle.FlashOrder.RRBG)
                {

                    if ((collision.gameObject.tag == "SocketEnd3" || collision.gameObject.tag == "SocketEnd1" || collision.gameObject.tag == "SocketEnd4" || collision.gameObject.tag == "SocketEnd2") && plugged == false)
                    {
                        counterPlugged = false;
                        plugged = false;
                        audioPlayed = false;
                    }
                }
                //Third Flash
                if (wirePuzzleScript.flashOrderSelected == WirePuzzle.FlashOrder.RGRB)
                {

                    if ((collision.gameObject.tag == "SocketEnd2" || collision.gameObject.tag == "SocketEnd1" || collision.gameObject.tag == "SocketEnd4" || collision.gameObject.tag == "SocketEnd3") && plugged == false)
                    {
                        counterPlugged = false;
                        plugged = false;
                        audioPlayed = false;
                    }
                }
                //Forth Flash
                if (wirePuzzleScript.flashOrderSelected == WirePuzzle.FlashOrder.OBGR)
                {

                    if ((collision.gameObject.tag == "SocketEnd3" || collision.gameObject.tag == "SocketEnd1" || collision.gameObject.tag == "SocketEnd2" || collision.gameObject.tag == "SocketEnd4") && plugged == false)
                    {
                        counterPlugged = false;
                        plugged = false;
                        audioPlayed = false;
                    }
                }
                //Fifth Flash
                if (wirePuzzleScript.flashOrderSelected == WirePuzzle.FlashOrder.OROR)
                {

                    if ((collision.gameObject.tag == "SocketEnd3" || collision.gameObject.tag == "SocketEnd1" || collision.gameObject.tag == "SocketEnd4" || collision.gameObject.tag == "SocketEnd2") && plugged == false)
                    {
                        counterPlugged = false;
                        plugged = false;
                        audioPlayed = false;
                    }
                }
                //Sixith Flash
                if (wirePuzzleScript.flashOrderSelected == WirePuzzle.FlashOrder.OOGG)
                {
                    if ((collision.gameObject.tag == "SocketEnd3" || collision.gameObject.tag == "SocketEnd1" || collision.gameObject.tag == "SocketEnd4" || collision.gameObject.tag == "SocketEnd2") && plugged == false)
                    {
                        counterPlugged = false;
                        plugged = false;
                        audioPlayed = false;
                    }
                }
                //Seventh Flash
                if (wirePuzzleScript.flashOrderSelected == WirePuzzle.FlashOrder.GRBOGR)
                {
                    if ((collision.gameObject.tag == "SocketEnd3" || collision.gameObject.tag == "SocketEnd1" || collision.gameObject.tag == "SocketEnd4" || collision.gameObject.tag == "SocketEnd2") && plugged == false)
                    {
                        counterPlugged = false;
                        plugged = false;
                        audioPlayed = false;
                    }
                }
                //Eight Flash
                if (wirePuzzleScript.flashOrderSelected == WirePuzzle.FlashOrder.BGGR)
                {
                    if ((collision.gameObject.tag == "SocketEnd3" || collision.gameObject.tag == "SocketEnd2" || collision.gameObject.tag == "SocketEnd4" || collision.gameObject.tag == "SocketEnd1") && plugged == false)
                    {
                        counterPlugged = false;
                        plugged = false;
                        audioPlayed = false;
                    }
                }
            }
            if (this.gameObject.tag == "BlueWireEnd")
            {
                //First Flash
                if (wirePuzzleScript.flashOrderSelected == WirePuzzle.FlashOrder.RGBO)
                {
                    if ((collision.gameObject.tag == "SocketEnd1" || collision.gameObject.tag == "SocketEnd2" || collision.gameObject.tag == "SocketEnd4" || collision.gameObject.tag == "SocketEnd3") && plugged == false)
                    {
                        counterPlugged = false;
                        plugged = false;
                        audioPlayed = false;
                    }
                }
                //Second Flash
                if (wirePuzzleScript.flashOrderSelected == WirePuzzle.FlashOrder.RRBG)
                {
                    if ((collision.gameObject.tag == "SocketEnd3" || collision.gameObject.tag == "SocketEnd1" || collision.gameObject.tag == "SocketEnd2" || collision.gameObject.tag == "SocketEnd4") && plugged == false)
                    {
                        counterPlugged = false;
                        plugged = false;
                        audioPlayed = false;
                    }
                }
                //Third Flash
                if (wirePuzzleScript.flashOrderSelected == WirePuzzle.FlashOrder.RGRB)
                {
                    if ((collision.gameObject.tag == "SocketEnd3" || collision.gameObject.tag == "SocketEnd1" || collision.gameObject.tag == "SocketEnd2" || collision.gameObject.tag == "SocketEnd4") && plugged == false)
                    {
                        counterPlugged = false;
                        plugged = false;
                        audioPlayed = false;
                    }
                }
                //Fourth Flash
                if (wirePuzzleScript.flashOrderSelected == WirePuzzle.FlashOrder.OBGR)
                {
                    if ((collision.gameObject.tag == "SocketEnd3" || collision.gameObject.tag == "SocketEnd1" || collision.gameObject.tag == "SocketEnd4" || collision.gameObject.tag == "SocketEnd2") && plugged == false)
                    {
                        counterPlugged = false;
                        plugged = false;
                        audioPlayed = false;
                    }
                }
                //Fifth Flash
                if (wirePuzzleScript.flashOrderSelected == WirePuzzle.FlashOrder.OROR)
                {
                    if ((collision.gameObject.tag == "SocketEnd3" || collision.gameObject.tag == "SocketEnd1" || collision.gameObject.tag == "SocketEnd4" || collision.gameObject.tag == "SocketEnd2") && plugged == false)
                    {
                        counterPlugged = false;
                        plugged = false;
                        audioPlayed = false;
                    }
                }
                //Sixith Flash
                if (wirePuzzleScript.flashOrderSelected == WirePuzzle.FlashOrder.OOGG)
                {
                    if ((collision.gameObject.tag == "SocketEnd3" || collision.gameObject.tag == "SocketEnd4" || collision.gameObject.tag == "SocketEnd2" || collision.gameObject.tag == "SocketEnd1") && plugged == false)
                    {
                        if ((collision.gameObject.tag == "SocketEnd3" || collision.gameObject.tag == "SocketEnd4" || collision.gameObject.tag == "SocketEnd2" || collision.gameObject.tag == "SocketEnd1") && plugged == false)
                        {
                            counterPlugged = false;
                            plugged = false;
                            audioPlayed = false;
                        }
                    }
                }
                //Seventh Flash
                if (wirePuzzleScript.flashOrderSelected == WirePuzzle.FlashOrder.GRBOGR)
                {
                    wirePuzzleScript.correctWireBlue = true;
                    plugged = false;
                    //test here
                }
                //Eight Flash
                if (wirePuzzleScript.flashOrderSelected == WirePuzzle.FlashOrder.BGGR)
                {
                    if ((collision.gameObject.tag == "SocketEnd3" || collision.gameObject.tag == "SocketEnd4" || collision.gameObject.tag == "SocketEnd2" || collision.gameObject.tag == "SocketEnd1") && plugged == false)
                    {
                        counterPlugged = false;
                        plugged = false;
                        audioPlayed = false;
                    }
                }
            }
        }

        if(wirePuzzleScript.pickedFlashStage == WirePuzzle.flashStages.stage2)
        {
            if (this.gameObject.tag == "YellowWireEnd")
            {
                if (wirePuzzleScript.flashOrderStage2Picked == WirePuzzle.flashOrderStage2.ROBG)
                {
                    if ((collision.gameObject.tag == "SocketEnd1" || collision.gameObject.tag == "SocketEnd2" || collision.gameObject.tag == "SocketEnd3" || collision.gameObject.tag == "SocketEnd4") && plugged == false)
                    {
                        counterPlugged = false;
                        plugged = false;
                        audioPlayed = false;
                    }
                }
                //Second Flash
                if (wirePuzzleScript.flashOrderStage2Picked == WirePuzzle.flashOrderStage2.OROB)
                {
                    if ((collision.gameObject.tag == "SocketEnd2" || collision.gameObject.tag == "SocketEnd3" || collision.gameObject.tag == "SocketEnd4" || collision.gameObject.tag == "SocketEnd1") && plugged == false)
                    {
                        counterPlugged = false;
                        plugged = false;
                        audioPlayed = false;
                    }
                }
                //Third Flash
                if (wirePuzzleScript.flashOrderStage2Picked == WirePuzzle.flashOrderStage2.OOOO)
                {
                    if ((collision.gameObject.tag == "SocketEnd2" || collision.gameObject.tag == "SocketEnd3" || collision.gameObject.tag == "SocketEnd1" || collision.gameObject.tag == "SocketEnd4") && plugged == false)
                    {
                        counterPlugged = false;
                        plugged = false;
                        audioPlayed = false;
                    }
                }
                //Forth Flash
                if (wirePuzzleScript.flashOrderStage2Picked == WirePuzzle.flashOrderStage2.RBGR)
                {
                    if ((collision.gameObject.tag == "SocketEnd2" || collision.gameObject.tag == "SocketEnd3" || collision.gameObject.tag == "SocketEnd4" || collision.gameObject.tag == "SocketEnd1") && plugged == false)
                    {
                        counterPlugged = false;
                        plugged = false;
                        audioPlayed = false;
                    }
                }
                //Fifth Flash
                if (wirePuzzleScript.flashOrderStage2Picked == WirePuzzle.flashOrderStage2.RRBGR)
                {
                    if ((collision.gameObject.tag == "SocketEnd2" || collision.gameObject.tag == "SocketEnd3" || collision.gameObject.tag == "SocketEnd1" || collision.gameObject.tag == "SocketEnd4") && plugged == false)
                    {
                        counterPlugged = false;
                        plugged = false;
                        audioPlayed = false;
                    }
                }
                //SeventhFlash
                if (wirePuzzleScript.flashOrderStage2Picked == WirePuzzle.flashOrderStage2.BOG)
                {
                    if ((collision.gameObject.tag == "SocketEnd2" || collision.gameObject.tag == "SocketEnd4" || collision.gameObject.tag == "SocketEnd1" || collision.gameObject.tag == "SocketEnd3") && plugged == false)
                    {
                        counterPlugged = false;
                        plugged = false;
                        audioPlayed = false;
                    }
                }
                //Eighth Flash
                if (wirePuzzleScript.flashOrderStage2Picked == WirePuzzle.flashOrderStage2.RRRB)
                {
                    if ((collision.gameObject.tag == "SocketEnd2" || collision.gameObject.tag == "SocketEnd3" || collision.gameObject.tag == "SocketEnd4" || collision.gameObject.tag == "SocketEnd1") && plugged == false)
                    {
                        counterPlugged = false;
                        plugged = false;
                    }
                }
                if (wirePuzzleScript.flashOrderStage2Picked == WirePuzzle.flashOrderStage2.GGGGBR)
                {
                    if ((collision.gameObject.tag == "SocketEnd2" || collision.gameObject.tag == "SocketEnd3" || collision.gameObject.tag == "SocketEnd4" || collision.gameObject.tag == "SocketEnd1") && plugged == false)
                    {
                        counterPlugged = false;
                        plugged = false;
                        audioPlayed = false;
                    }
                }
            }
            if (this.gameObject.tag == "GreenWireEnd")
            {
                if (wirePuzzleScript.flashOrderStage2Picked == WirePuzzle.flashOrderStage2.ROBG)
                {
                    if ((collision.gameObject.tag == "SocketEnd1" || collision.gameObject.tag == "SocketEnd2" || collision.gameObject.tag == "SocketEnd3" || collision.gameObject.tag == "SocketEnd4") && plugged == false)
                    {
                        counterPlugged = false;
                        plugged = false;
                        audioPlayed = false;
                    }
                }
                //Second Flash
                if (wirePuzzleScript.flashOrderStage2Picked == WirePuzzle.flashOrderStage2.OROB)
                {
                    if ((collision.gameObject.tag == "SocketEnd2" || collision.gameObject.tag == "SocketEnd3" || collision.gameObject.tag == "SocketEnd4" || collision.gameObject.tag == "SocketEnd1") && plugged == false)
                    {
                        counterPlugged = false;
                        plugged = false;
                        audioPlayed = false;
                    }
                }
                //Third Flash
                if (wirePuzzleScript.flashOrderStage2Picked == WirePuzzle.flashOrderStage2.OOOO)
                {
                    if ((collision.gameObject.tag == "SocketEnd2" || collision.gameObject.tag == "SocketEnd3" || collision.gameObject.tag == "SocketEnd1" || collision.gameObject.tag == "SocketEnd4") && plugged == false)
                    {
                        counterPlugged = false;
                        plugged = false;
                        audioPlayed = false;
                    }
                }
                //Forth Flash
                if (wirePuzzleScript.flashOrderStage2Picked == WirePuzzle.flashOrderStage2.RBGR)
                {
                    if ((collision.gameObject.tag == "SocketEnd2" || collision.gameObject.tag == "SocketEnd3" || collision.gameObject.tag == "SocketEnd4" || collision.gameObject.tag == "SocketEnd1") && plugged == false)
                    {
                        counterPlugged = false;
                        plugged = false;
                        audioPlayed = false;
                    }
                }
                //Fifth Flash
                if (wirePuzzleScript.flashOrderStage2Picked == WirePuzzle.flashOrderStage2.RRBGR)
                {
                    if ((collision.gameObject.tag == "SocketEnd2" || collision.gameObject.tag == "SocketEnd3" || collision.gameObject.tag == "SocketEnd1" || collision.gameObject.tag == "SocketEnd4") && plugged == false)
                    {
                        counterPlugged = false;
                        plugged = false;
                        audioPlayed = false;
                    }
                }
                //SeventhFlash
                if (wirePuzzleScript.flashOrderStage2Picked == WirePuzzle.flashOrderStage2.BOG)
                {
                    if ((collision.gameObject.tag == "SocketEnd2" || collision.gameObject.tag == "SocketEnd4" || collision.gameObject.tag == "SocketEnd1" || collision.gameObject.tag == "SocketEnd3") && plugged == false)
                    {
                        counterPlugged = false;
                        plugged = false;
                        audioPlayed = false;
                    }
                }
                //Eighth Flash
                if (wirePuzzleScript.flashOrderStage2Picked == WirePuzzle.flashOrderStage2.RRRB)
                {
                    if ((collision.gameObject.tag == "SocketEnd2" || collision.gameObject.tag == "SocketEnd3" || collision.gameObject.tag == "SocketEnd4" || collision.gameObject.tag == "SocketEnd1") && plugged == false)
                    {
                        counterPlugged = false;
                        plugged = false;
                        audioPlayed = false;
                    }
                }
                if (wirePuzzleScript.flashOrderStage2Picked == WirePuzzle.flashOrderStage2.GGGGBR)
                {
                    if ((collision.gameObject.tag == "SocketEnd2" || collision.gameObject.tag == "SocketEnd3" || collision.gameObject.tag == "SocketEnd4" || collision.gameObject.tag == "SocketEnd1") && plugged == false)
                    {
                        counterPlugged = false;
                        plugged = false;
                        audioPlayed = false;
                    }
                }
            }
            if (this.gameObject.tag == "RedWireEnd")
            {
                if (wirePuzzleScript.flashOrderStage2Picked == WirePuzzle.flashOrderStage2.ROBG)
                {
                    if ((collision.gameObject.tag == "SocketEnd1" || collision.gameObject.tag == "SocketEnd2" || collision.gameObject.tag == "SocketEnd3" || collision.gameObject.tag == "SocketEnd4") && plugged == false)
                    {
                        counterPlugged = false;
                        plugged = false;
                        audioPlayed = false;
                    }
                }
                //Second Flash
                if (wirePuzzleScript.flashOrderStage2Picked == WirePuzzle.flashOrderStage2.OROB)
                {
                    if ((collision.gameObject.tag == "SocketEnd2" || collision.gameObject.tag == "SocketEnd3" || collision.gameObject.tag == "SocketEnd4" || collision.gameObject.tag == "SocketEnd1") && plugged == false)
                    {
                        counterPlugged = false;
                        plugged = false;
                        audioPlayed = false;

                    }
                }
                //Third Flash
                if (wirePuzzleScript.flashOrderStage2Picked == WirePuzzle.flashOrderStage2.OOOO)
                {
                    if ((collision.gameObject.tag == "SocketEnd2" || collision.gameObject.tag == "SocketEnd3" || collision.gameObject.tag == "SocketEnd1" || collision.gameObject.tag == "SocketEnd4") && plugged == false)
                    {
                        counterPlugged = false;
                        plugged = false;
                        audioPlayed = false;
                    }
                }
                //Forth Flash
                if (wirePuzzleScript.flashOrderStage2Picked == WirePuzzle.flashOrderStage2.RBGR)
                {
                    if ((collision.gameObject.tag == "SocketEnd2" || collision.gameObject.tag == "SocketEnd3" || collision.gameObject.tag == "SocketEnd4" || collision.gameObject.tag == "SocketEnd1") && plugged == false)
                    {
                        counterPlugged = false;
                        plugged = false;
                        audioPlayed = false;
                    }
                }
                //Fifth Flash
                if (wirePuzzleScript.flashOrderStage2Picked == WirePuzzle.flashOrderStage2.RRBGR)
                {
                    if ((collision.gameObject.tag == "SocketEnd2" || collision.gameObject.tag == "SocketEnd3" || collision.gameObject.tag == "SocketEnd1" || collision.gameObject.tag == "SocketEnd4") && plugged == false)
                    {
                        counterPlugged = false;
                        plugged = false;
                        audioPlayed = false;
                    }
                }
                //SeventhFlash
                if (wirePuzzleScript.flashOrderStage2Picked == WirePuzzle.flashOrderStage2.BOG)
                {
                    if ((collision.gameObject.tag == "SocketEnd2" || collision.gameObject.tag == "SocketEnd4" || collision.gameObject.tag == "SocketEnd1" || collision.gameObject.tag == "SocketEnd3") && plugged == false)
                    {
                        counterPlugged = false;
                        plugged = false;
                        audioPlayed = false;
                    }
                }
                //Eighth Flash
                if (wirePuzzleScript.flashOrderStage2Picked == WirePuzzle.flashOrderStage2.RRRB)
                {
                    if ((collision.gameObject.tag == "SocketEnd2" || collision.gameObject.tag == "SocketEnd3" || collision.gameObject.tag == "SocketEnd4" || collision.gameObject.tag == "SocketEnd1") && plugged == false)
                    {
                        counterPlugged = false;
                        plugged = false;
                        audioPlayed = false;
                    }
                }
                if (wirePuzzleScript.flashOrderStage2Picked == WirePuzzle.flashOrderStage2.GGGGBR)
                {
                    if ((collision.gameObject.tag == "SocketEnd2" || collision.gameObject.tag == "SocketEnd3" || collision.gameObject.tag == "SocketEnd4" || collision.gameObject.tag == "SocketEnd1") && plugged == false)
                    {
                        counterPlugged = false;
                        plugged = false;
                        audioPlayed = false;
                    }
                }
            }
            if (this.gameObject.tag == "BlueWireEnd")
            {
                if (wirePuzzleScript.flashOrderStage2Picked == WirePuzzle.flashOrderStage2.ROBG)
                {
                    if ((collision.gameObject.tag == "SocketEnd1" || collision.gameObject.tag == "SocketEnd2" || collision.gameObject.tag == "SocketEnd3" || collision.gameObject.tag == "SocketEnd4") && plugged == false)
                    {
                        counterPlugged = false;
                        plugged = false;
                        audioPlayed = false;
                    }
                }
                //Second Flash
                if (wirePuzzleScript.flashOrderStage2Picked == WirePuzzle.flashOrderStage2.OROB)
                {
                    if ((collision.gameObject.tag == "SocketEnd2" || collision.gameObject.tag == "SocketEnd3" || collision.gameObject.tag == "SocketEnd4" || collision.gameObject.tag == "SocketEnd1") && plugged == false)
                    {
                        counterPlugged = false;
                        plugged = false;
                        audioPlayed = false;
                    }
                }
                //Third Flash
                if (wirePuzzleScript.flashOrderStage2Picked == WirePuzzle.flashOrderStage2.OOOO)
                {
                    if ((collision.gameObject.tag == "SocketEnd2" || collision.gameObject.tag == "SocketEnd3" || collision.gameObject.tag == "SocketEnd1" || collision.gameObject.tag == "SocketEnd4") && plugged == false)
                    {
                        counterPlugged = false;
                        plugged = false;
                        audioPlayed = false;
                    }
                }
                //Forth Flash
                if (wirePuzzleScript.flashOrderStage2Picked == WirePuzzle.flashOrderStage2.RBGR)
                {
                    if ((collision.gameObject.tag == "SocketEnd2" || collision.gameObject.tag == "SocketEnd3" || collision.gameObject.tag == "SocketEnd4" || collision.gameObject.tag == "SocketEnd1") && plugged == false)
                    {
                        counterPlugged = false;
                        plugged = false;
                        audioPlayed = false;
                    }
                }
                //Fifth Flash
                if (wirePuzzleScript.flashOrderStage2Picked == WirePuzzle.flashOrderStage2.RRBGR)
                {
                    if ((collision.gameObject.tag == "SocketEnd2" || collision.gameObject.tag == "SocketEnd3" || collision.gameObject.tag == "SocketEnd1" || collision.gameObject.tag == "SocketEnd4") && plugged == false)
                    {
                        counterPlugged = false;
                        plugged = false;
                        audioPlayed = false;
                    }
                }
                //SeventhFlash
                if (wirePuzzleScript.flashOrderStage2Picked == WirePuzzle.flashOrderStage2.BOG)
                {
                    if ((collision.gameObject.tag == "SocketEnd2" || collision.gameObject.tag == "SocketEnd4" || collision.gameObject.tag == "SocketEnd1" || collision.gameObject.tag == "SocketEnd3") && plugged == false)
                    {
                        counterPlugged = false;
                        plugged = false;
                        audioPlayed = false;
                    }
                }
                //Eighth Flash
                if (wirePuzzleScript.flashOrderStage2Picked == WirePuzzle.flashOrderStage2.RRRB)
                {
                    if ((collision.gameObject.tag == "SocketEnd2" || collision.gameObject.tag == "SocketEnd3" || collision.gameObject.tag == "SocketEnd4" || collision.gameObject.tag == "SocketEnd1") && plugged == false)
                    {
                        counterPlugged = false;
                        plugged = false;
                        audioPlayed = false;
                    }
                }
                if (wirePuzzleScript.flashOrderStage2Picked == WirePuzzle.flashOrderStage2.GGGGBR)
                {
                    if ((collision.gameObject.tag == "SocketEnd2" || collision.gameObject.tag == "SocketEnd3" || collision.gameObject.tag == "SocketEnd4" || collision.gameObject.tag == "SocketEnd1") && plugged == false)
                    {
                        counterPlugged = false;
                        plugged = false;
                        audioPlayed = false;
                    }
                }
            }
        }

        if(wirePuzzleScript.pickedFlashStage == WirePuzzle.flashStages.stage3)
        {
            if (this.gameObject.tag == "YellowWireEnd")
            {
                if (wirePuzzleScript.flashOrderStage3Picked == WirePuzzle.flashOrderStage3.GRBGRR)
                {
                    if ((collision.gameObject.tag == "SocketEnd1" || collision.gameObject.tag == "SocketEnd2" || collision.gameObject.tag == "SocketEnd3" || collision.gameObject.tag == "SocketEnd4") && plugged == false)
                    {
                        counterPlugged = false;
                        plugged = false;
                        audioPlayed = false;
                    }
                }
                //Second Flash
                if (wirePuzzleScript.flashOrderStage3Picked == WirePuzzle.flashOrderStage3.BGRGOR)
                {
                    if ((collision.gameObject.tag == "SocketEnd2" || collision.gameObject.tag == "SocketEnd3" || collision.gameObject.tag == "SocketEnd4" || collision.gameObject.tag == "SocketEnd1") && plugged == false)
                    {
                        counterPlugged = false;
                        plugged = false;
                        audioPlayed = false;
                    }
                }
                //Third Flash
                if (wirePuzzleScript.flashOrderStage3Picked == WirePuzzle.flashOrderStage3.BBB)
                {
                    if ((collision.gameObject.tag == "SocketEnd2" || collision.gameObject.tag == "SocketEnd3" || collision.gameObject.tag == "SocketEnd1" || collision.gameObject.tag == "SocketEnd4") && plugged == false)
                    {
                        counterPlugged = false;
                        plugged = false;
                        audioPlayed = false;
                    }
                }
                //Forth Flash
                if (wirePuzzleScript.flashOrderStage3Picked == WirePuzzle.flashOrderStage3.GRGB)
                {
                    if ((collision.gameObject.tag == "SocketEnd2" || collision.gameObject.tag == "SocketEnd3" || collision.gameObject.tag == "SocketEnd4" || collision.gameObject.tag == "SocketEnd1") && plugged == false)
                    {
                        counterPlugged = false;
                        plugged = false;
                        audioPlayed = false;
                    }
                }
                //Fifth Flash
                if (wirePuzzleScript.flashOrderStage3Picked == WirePuzzle.flashOrderStage3.OBOR)
                {
                    if ((collision.gameObject.tag == "SocketEnd2" || collision.gameObject.tag == "SocketEnd3" || collision.gameObject.tag == "SocketEnd1" || collision.gameObject.tag == "SocketEnd4") && plugged == false)
                    {
                        counterPlugged = false;
                        plugged = false;
                        audioPlayed = false;
                    }
                }
                //SeventhFlash
                if (wirePuzzleScript.flashOrderStage3Picked == WirePuzzle.flashOrderStage3.ORRB)
                {
                    if ((collision.gameObject.tag == "SocketEnd2" || collision.gameObject.tag == "SocketEnd4" || collision.gameObject.tag == "SocketEnd1" || collision.gameObject.tag == "SocketEnd3") && plugged == false)
                    {
                        counterPlugged = false;
                        plugged = false;
                        audioPlayed = false;
                    }
                }
                //Eighth Flash
                if (wirePuzzleScript.flashOrderStage3Picked == WirePuzzle.flashOrderStage3.GGGG)
                {
                    if ((collision.gameObject.tag == "SocketEnd2" || collision.gameObject.tag == "SocketEnd3" || collision.gameObject.tag == "SocketEnd4" || collision.gameObject.tag == "SocketEnd1") && plugged == false)
                    {
                        counterPlugged = false;
                        plugged = false;
                        audioPlayed = false;
                    }
                }
                if (wirePuzzleScript.flashOrderStage3Picked == WirePuzzle.flashOrderStage3.RBGO)
                {
                    if ((collision.gameObject.tag == "SocketEnd2" || collision.gameObject.tag == "SocketEnd3" || collision.gameObject.tag == "SocketEnd4" || collision.gameObject.tag == "SocketEnd1") && plugged == false)
                    {
                        counterPlugged = false;
                        plugged = false;
                        audioPlayed = false;
                    }
                }
            }
            if (this.gameObject.tag == "GreenWireEnd")
            {
                if (wirePuzzleScript.flashOrderStage3Picked == WirePuzzle.flashOrderStage3.GRBGRR)
                {
                    if ((collision.gameObject.tag == "SocketEnd1" || collision.gameObject.tag == "SocketEnd2" || collision.gameObject.tag == "SocketEnd3" || collision.gameObject.tag == "SocketEnd4") && plugged == false)
                    {
                        counterPlugged = false;
                        plugged = false;
                        audioPlayed = false;
                    }
                }
                //Second Flash
                if (wirePuzzleScript.flashOrderStage3Picked == WirePuzzle.flashOrderStage3.BGRGOR)
                {
                    if ((collision.gameObject.tag == "SocketEnd2" || collision.gameObject.tag == "SocketEnd3" || collision.gameObject.tag == "SocketEnd4" || collision.gameObject.tag == "SocketEnd1") && plugged == false)
                    {
                        counterPlugged = false;
                        plugged = false;
                        audioPlayed = false;
                    }
                }
                //Third Flash
                if (wirePuzzleScript.flashOrderStage3Picked == WirePuzzle.flashOrderStage3.BBB)
                {
                    if ((collision.gameObject.tag == "SocketEnd2" || collision.gameObject.tag == "SocketEnd3" || collision.gameObject.tag == "SocketEnd1" || collision.gameObject.tag == "SocketEnd4") && plugged == false)
                    {
                        counterPlugged = false;
                        plugged = false;
                        audioPlayed = false;
                    }
                }
                //Forth Flash
                if (wirePuzzleScript.flashOrderStage3Picked == WirePuzzle.flashOrderStage3.GRGB)
                {
                    if ((collision.gameObject.tag == "SocketEnd2" || collision.gameObject.tag == "SocketEnd3" || collision.gameObject.tag == "SocketEnd4" || collision.gameObject.tag == "SocketEnd1") && plugged == false)
                    {
                        counterPlugged = false;
                        plugged = false;
                        audioPlayed = false;
                    }
                }
                //Fifth Flash
                if (wirePuzzleScript.flashOrderStage3Picked == WirePuzzle.flashOrderStage3.OBOR)
                {
                    if ((collision.gameObject.tag == "SocketEnd2" || collision.gameObject.tag == "SocketEnd3" || collision.gameObject.tag == "SocketEnd1" || collision.gameObject.tag == "SocketEnd4") && plugged == false)
                    {
                        counterPlugged = false;
                        plugged = false;
                        audioPlayed = false;
                    }
                }
                //SeventhFlash
                if (wirePuzzleScript.flashOrderStage3Picked == WirePuzzle.flashOrderStage3.ORRB)
                {
                    if ((collision.gameObject.tag == "SocketEnd2" || collision.gameObject.tag == "SocketEnd4" || collision.gameObject.tag == "SocketEnd1" || collision.gameObject.tag == "SocketEnd3") && plugged == false)
                    {
                        counterPlugged = false;
                        plugged = false;
                        audioPlayed = false;
                    }
                }
                //Eighth Flash
                if (wirePuzzleScript.flashOrderStage3Picked == WirePuzzle.flashOrderStage3.GGGG)
                {
                    if ((collision.gameObject.tag == "SocketEnd2" || collision.gameObject.tag == "SocketEnd3" || collision.gameObject.tag == "SocketEnd4" || collision.gameObject.tag == "SocketEnd1") && plugged == false)
                    {
                        counterPlugged = false;
                        plugged = false;
                        audioPlayed = false;
                    }
                }
                if (wirePuzzleScript.flashOrderStage3Picked == WirePuzzle.flashOrderStage3.RBGO)
                {
                    if ((collision.gameObject.tag == "SocketEnd2" || collision.gameObject.tag == "SocketEnd3" || collision.gameObject.tag == "SocketEnd4" || collision.gameObject.tag == "SocketEnd1") && plugged == false)
                    {
                        counterPlugged = false;
                        plugged = false;
                        audioPlayed = false;
                    }
                }
            }
            if (this.gameObject.tag == "RedWireEnd")
            {
                if (wirePuzzleScript.flashOrderStage3Picked == WirePuzzle.flashOrderStage3.GRBGRR)
                {
                    if ((collision.gameObject.tag == "SocketEnd1" || collision.gameObject.tag == "SocketEnd2" || collision.gameObject.tag == "SocketEnd3" || collision.gameObject.tag == "SocketEnd4") && plugged == false)
                    {
                        counterPlugged = false;
                        plugged = false;
                        audioPlayed = false;
                    }
                }
                //Second Flash
                if (wirePuzzleScript.flashOrderStage3Picked == WirePuzzle.flashOrderStage3.BGRGOR)
                {
                    if ((collision.gameObject.tag == "SocketEnd2" || collision.gameObject.tag == "SocketEnd3" || collision.gameObject.tag == "SocketEnd4" || collision.gameObject.tag == "SocketEnd1") && plugged == false)
                    {
                        counterPlugged = false;
                        plugged = false;
                        audioPlayed = false;
                    }
                }
                //Third Flash
                if (wirePuzzleScript.flashOrderStage3Picked == WirePuzzle.flashOrderStage3.BBB)
                {
                    if ((collision.gameObject.tag == "SocketEnd2" || collision.gameObject.tag == "SocketEnd3" || collision.gameObject.tag == "SocketEnd1" || collision.gameObject.tag == "SocketEnd4") && plugged == false)
                    {
                        counterPlugged = false;
                        plugged = false;
                        audioPlayed = false;
                    }
                }
                //Forth Flash
                if (wirePuzzleScript.flashOrderStage3Picked == WirePuzzle.flashOrderStage3.GRGB)
                {
                    if ((collision.gameObject.tag == "SocketEnd2" || collision.gameObject.tag == "SocketEnd3" || collision.gameObject.tag == "SocketEnd4" || collision.gameObject.tag == "SocketEnd1") && plugged == false)
                    {
                        counterPlugged = false;
                        plugged = false;
                        audioPlayed = false;
                    }
                }
                //Fifth Flash
                if (wirePuzzleScript.flashOrderStage3Picked == WirePuzzle.flashOrderStage3.OBOR)
                {
                    if ((collision.gameObject.tag == "SocketEnd2" || collision.gameObject.tag == "SocketEnd3" || collision.gameObject.tag == "SocketEnd1" || collision.gameObject.tag == "SocketEnd4") && plugged == false)
                    {
                        counterPlugged = false;
                        plugged = false;
                        audioPlayed = false;
                    }
                }
                //SeventhFlash
                if (wirePuzzleScript.flashOrderStage3Picked == WirePuzzle.flashOrderStage3.ORRB)
                {
                    if ((collision.gameObject.tag == "SocketEnd2" || collision.gameObject.tag == "SocketEnd4" || collision.gameObject.tag == "SocketEnd1" || collision.gameObject.tag == "SocketEnd3") && plugged == false)
                    {
                        counterPlugged = false;
                        plugged = false;
                        audioPlayed = false;
                    }
                }
                //Eighth Flash
                if (wirePuzzleScript.flashOrderStage3Picked == WirePuzzle.flashOrderStage3.GGGG)
                {
                    if ((collision.gameObject.tag == "SocketEnd2" || collision.gameObject.tag == "SocketEnd3" || collision.gameObject.tag == "SocketEnd4" || collision.gameObject.tag == "SocketEnd1") && plugged == false)
                    {
                        counterPlugged = false;
                        plugged = false;
                        audioPlayed = false;
                    }
                }
                if (wirePuzzleScript.flashOrderStage3Picked == WirePuzzle.flashOrderStage3.RBGO)
                {
                    if ((collision.gameObject.tag == "SocketEnd2" || collision.gameObject.tag == "SocketEnd3" || collision.gameObject.tag == "SocketEnd4" || collision.gameObject.tag == "SocketEnd1") && plugged == false)
                    {
                        counterPlugged = false;
                        plugged = false;
                        audioPlayed = false;
                    }
                }
            }
            if (this.gameObject.tag == "BlueWireEnd")
            {
                if (wirePuzzleScript.flashOrderStage3Picked == WirePuzzle.flashOrderStage3.GRBGRR)
                {
                    if ((collision.gameObject.tag == "SocketEnd1" || collision.gameObject.tag == "SocketEnd2" || collision.gameObject.tag == "SocketEnd3" || collision.gameObject.tag == "SocketEnd4") && plugged == false)
                    {
                        counterPlugged = false;
                        plugged = false;
                        audioPlayed = false;
                    }
                }
                //Second Flash
                if (wirePuzzleScript.flashOrderStage3Picked == WirePuzzle.flashOrderStage3.BGRGOR)
                {
                    if ((collision.gameObject.tag == "SocketEnd2" || collision.gameObject.tag == "SocketEnd3" || collision.gameObject.tag == "SocketEnd4" || collision.gameObject.tag == "SocketEnd1") && plugged == false)
                    {
                        counterPlugged = false;
                        plugged = false;
                        audioPlayed = false;
                    }
                }
                //Third Flash
                if (wirePuzzleScript.flashOrderStage3Picked == WirePuzzle.flashOrderStage3.BBB)
                {
                    if ((collision.gameObject.tag == "SocketEnd2" || collision.gameObject.tag == "SocketEnd3" || collision.gameObject.tag == "SocketEnd1" || collision.gameObject.tag == "SocketEnd4") && plugged == false)
                    {
                        counterPlugged = false;
                        plugged = false;
                        audioPlayed = false;
                    }
                }
                //Forth Flash
                if (wirePuzzleScript.flashOrderStage3Picked == WirePuzzle.flashOrderStage3.GRGB)
                {
                    if ((collision.gameObject.tag == "SocketEnd2" || collision.gameObject.tag == "SocketEnd3" || collision.gameObject.tag == "SocketEnd4" || collision.gameObject.tag == "SocketEnd1") && plugged == false)
                    {
                        counterPlugged = false;
                        plugged = false;
                        audioPlayed = false;
                    }
                }
                //Fifth Flash
                if (wirePuzzleScript.flashOrderStage3Picked == WirePuzzle.flashOrderStage3.OBOR)
                {
                    if ((collision.gameObject.tag == "SocketEnd2" || collision.gameObject.tag == "SocketEnd3" || collision.gameObject.tag == "SocketEnd1" || collision.gameObject.tag == "SocketEnd4") && plugged == false)
                    {
                        counterPlugged = false;
                        plugged = false;
                        audioPlayed = false;
                    }
                }
                //SeventhFlash
                if (wirePuzzleScript.flashOrderStage3Picked == WirePuzzle.flashOrderStage3.ORRB)
                {
                    if ((collision.gameObject.tag == "SocketEnd2" || collision.gameObject.tag == "SocketEnd4" || collision.gameObject.tag == "SocketEnd1" || collision.gameObject.tag == "SocketEnd3") && plugged == false)
                    {
                        counterPlugged = false;
                        plugged = false;
                        audioPlayed = false;
                    }
                }
                //Eighth Flash
                if (wirePuzzleScript.flashOrderStage3Picked == WirePuzzle.flashOrderStage3.GGGG)
                {
                    if ((collision.gameObject.tag == "SocketEnd2" || collision.gameObject.tag == "SocketEnd3" || collision.gameObject.tag == "SocketEnd4" || collision.gameObject.tag == "SocketEnd1") && plugged == false)
                    {
                        counterPlugged = false;
                        plugged = false;
                        audioPlayed = false;
                    }
                }
                if (wirePuzzleScript.flashOrderStage3Picked == WirePuzzle.flashOrderStage3.RBGO)
                {
                    if ((collision.gameObject.tag == "SocketEnd2" || collision.gameObject.tag == "SocketEnd3" || collision.gameObject.tag == "SocketEnd4" || collision.gameObject.tag == "SocketEnd1") && plugged == false)
                    {
                        counterPlugged = false;
                        plugged = false;
                        audioPlayed = false;
                    }
                }

            }
        }
    }

    void UnFreezeMe()
    {
        if(!plugged || !counterPlugged)
        {
            rigidBody.constraints = RigidbodyConstraints2D.None;
        }
    }
}
