using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItems : Photon.MonoBehaviour, IPunObservable
{
    private List<Transform> itemSpawns = new List<Transform>();
    public List<GameObject> lootCollect = new List<GameObject>();

    [SerializeField]
    private int maxSpawnItems = 15;

	// Use this for initialization
	void Start ()
    {

        foreach (Transform childSpawns in transform)
        {
            itemSpawns.Add(childSpawns.transform);
        }
        if (PhotonNetwork.isMasterClient)
        {
            for (int i = 0; i < maxSpawnItems; i++)
            {
                Transform spawnLocation = itemSpawns[Random.Range(0, itemSpawns.Count)];
                itemSpawns.Remove(spawnLocation);
                lootCollect.Add(PhotonNetwork.Instantiate("Loot", spawnLocation.position, Quaternion.identity, 0));
            }
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        
    }
}
