using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node {

    public Vector3 worldPosition;

    public GameObject fog;

    public Node(Vector3 _worldPos, GameObject _fog)
    {
        worldPosition = _worldPos;
        fog = _fog;
    }
}
