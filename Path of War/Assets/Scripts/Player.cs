using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    #region properties

    public float movementSpeed;

    public GameObject clickPoint;
    Transform pmr;

    GameObject triggerPMR;

    bool isIdle;
    Animation anim;
    private string currentAnimName;

    #endregion

    #region Functions

    void Start()
    {
        anim = GetComponent<Animation>();
    }

    void Update()
    {
        //Player Movement
        Plane playerPlane = new Plane(Vector3.up, transform.position);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float hitDistance = 0.0f;

        //if the player have a target point move the player to it
        if (pmr) {
            Move();
        }
        else
        {
            Idle();
        }

        //create the target point of the player
        if (playerPlane.Raycast(ray, out hitDistance))
        {
            Vector3 mousePosition = ray.GetPoint(hitDistance);
            if (Input.GetMouseButtonDown(0))
            {
                //We want only one target position at a time
                if (pmr)
                    DestroyImmediate(pmr.gameObject);

                pmr = Instantiate(clickPoint.transform, mousePosition, Quaternion.identity);

                
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PMR")
        {
            triggerPMR = other.gameObject;
            triggerPMR.GetComponent<PMR>().DestroyPMR();
        }
    }

    private void Move()
    {
        isIdle = false;
        transform.LookAt(pmr.transform);
        transform.position = Vector3.MoveTowards(transform.position, pmr.position, movementSpeed * Time.deltaTime);
        if (currentAnimName != "walk") {
            currentAnimName = "walk";
            anim.CrossFade("walk");
        }
    }

    private void Idle()
    {
        isIdle = true;

        currentAnimName = "idle";
        anim.CrossFade("idle");
    }

    #endregion
}
