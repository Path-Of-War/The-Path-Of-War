using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    Player p;
    Vector3 offset;
    public float smoothFactor = 0.5f;

    public float rotationSpeed;

    void Start()
    {
        p = Player.instance;
        offset = transform.position - p.transform.position;

    }

    void LateUpdate()
    {
        Quaternion camTurnAngle;
        camTurnAngle = Quaternion.AngleAxis(Input.GetAxis("Mouse ScrollWheel") * rotationSpeed, Vector3.up);
        offset = camTurnAngle * offset;

        Vector3 newPos = p.transform.position + offset;

        transform.position = Vector3.Slerp(transform.position, newPos, smoothFactor);
        transform.LookAt(p.transform.position);

    }

}
