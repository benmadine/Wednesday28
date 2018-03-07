using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraWork : MonoBehaviour
{
    private PlayerSelection playerSectionScript;

    Transform cameraTrans;

    Transform thiefCollector, thiefHacker;

    private void Start()
    {
        playerSectionScript = GameObject.Find("RolePickerCanvas").GetComponent<PlayerSelection>();

        cameraTrans = this.transform;


    }

    private void Update()
    {
        if(playerSectionScript.player1Activated && playerSectionScript.player2Activated)
        {
            thiefCollector = GameObject.Find("Thief(Collector)(Clone)").GetComponent<Transform>();
            thiefHacker = GameObject.Find("Thief(Hacker)(Clone)").GetComponent<Transform>();
            CameraFollow();
        }
    }

    private void CameraFollow()
    {
        Vector2 targetCentre = (new Vector2(thiefCollector.position.x, thiefCollector.position.y) + new Vector2(thiefHacker.position.x, thiefHacker.position.y)) / 2;
        if(Vector2.Distance(thiefCollector.position, thiefHacker.position) > 10)
        {

        }
        else
        {
            cameraTrans.position = new Vector3(targetCentre.x, targetCentre.y, -10);
        }
    }

}
