using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    #region properties

    //movement
    public float movementSpeed;
    public GameObject clickPoint;
    Transform pmr;
    GameObject triggerPMR;

    //states
    bool isIdle;
    bool isAttacking;

    //animation
    Animation anim;
    private string currentAnimName;

    //combat
    public GameObject target;
    public float range;
    GameObject enemy;
    public float attackSpeed; //formula is: 1 second / attackSpeed
    float lastTimeAttacked = 0f;

    //stats
    public int damage;
    public int health;
    public int armor;
    public int mana;

    #endregion

    #region Functions

    #region built in functions
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

        if (isAttacking)
        {
            Attack();
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
                pmr.GetComponent<PMR>().p = this;

                
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

    #endregion

    bool CheckRange()
    {
        if (target)
        {
            if (Vector3.Distance(transform.position, target.transform.position) <= range)
            {
                isAttacking = true;
                return true;
            }
        }
        return false;
    }
    private void Move()
    {
        if (!CheckRange()) 
        { 
            isAttacking = false;
            isIdle = false;
            transform.LookAt(pmr.transform);
            transform.position = Vector3.MoveTowards(transform.position, pmr.position, movementSpeed * Time.deltaTime);
            if (currentAnimName != "walk") {
                currentAnimName = "walk";
                anim.CrossFade("walk");
            }
        }
    }

    private void Idle()
    {
        isAttacking = false;
        isIdle = true;

        currentAnimName = "idle";
        anim.CrossFade("idle");
    }

    void Attack()
    {
        if (Vector3.Distance(transform.position, target.transform.position) <= range)
        {
            isIdle = false;
            isAttacking = true;
            if ((lastTimeAttacked + (1f / attackSpeed)) <= Time.time)
            {
                //Deal the damage in this loop
                currentAnimName = "attack";
                anim.CrossFade("attack");
                lastTimeAttacked = Time.time;
            }
        }
        //That mean we still target the enemy but he is too far (bcs he fleed ?) (enemies fleeing isn't planned)
        else
        {
            pmr = enemy.transform;
        }
    }

    #endregion
}
