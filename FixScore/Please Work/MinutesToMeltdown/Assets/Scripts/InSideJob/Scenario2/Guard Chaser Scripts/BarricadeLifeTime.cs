using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarricadeLifeTime : Photon.MonoBehaviour, IPunObservable
{
    private GuardChaserManager guardChaseScript;
    private float lifeSpan = 30f;

    private void Start()
    {
        guardChaseScript = GameObject.FindGameObjectWithTag("GuardChaser").GetComponent<GuardChaserManager>();
    }

    private void Update()
    {
        LifeOver();
    }

    private void LifeOver()
    {
        lifeSpan -= Time.deltaTime;
        if (lifeSpan <= 0)
        {
            guardChaseScript.maxBarricades.Remove(this.gameObject);
            PhotonNetwork.Destroy(gameObject);
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {

    }
}
