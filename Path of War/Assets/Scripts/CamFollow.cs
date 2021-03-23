using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    Player p;
    Transform myTransform;
    Vector3 offset;

    void Start()
    {
        p = Player.instance;
        myTransform = transform;
        offset = p.transform.position + myTransform.position;

    }

    void LateUpdate()
    {

        myTransform.position = p.transform.position + offset;

    }
}
