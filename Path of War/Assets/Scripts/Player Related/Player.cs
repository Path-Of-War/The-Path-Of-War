using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    //singleton
    public static Player instance;

    #region properties
    public PlayerEffect pEffect;


    //movement
    public float movementSpeed;
    public GameObject clickPoint;
    Transform pmr;
    GameObject triggerPMR;
    public bool canMove = true;

    //states
    bool isIdle;
    bool isAttacking;
    bool isLooting;

    //animation
    Animation anim;
    private string currentAnimName;

    //combat
    GameObject target;
    AEnemy enemy;
    public float range;
    public float attackSpeed; //formula is: 1 second / attackSpeed
    float lastTimeAttacked = 0f;

    //gathering
    public float lootRange;
    public GameObject lootingInterface;
    public GameObject lootingGrid;
    private List<GameObject> currentLoots = new List<GameObject>();

    //inventory
    public List<GameObject> inventoryData;
    public GameObject inventoryGrid;

    #region stats
    //stats
    public int attack;
    public int currentHealth;
    public int maxHealth;
    public int armor;
    public int currentMana;
    public int maxMana;

    public int currentLevel = 0;
    public int currentXp = 0;
    public int xpToLevel = 10;
    #endregion

    #endregion

    #region Functions

    #region built in functions
    void Awake()
    {
        currentHealth = maxHealth;
        currentMana = maxMana;
        if (instance)
            instance = null;
        instance = this;
        anim = GetComponent<Animation>();
    }

    void Update()
    {
        //Player Movement
        Plane playerPlane = new Plane(Vector3.up, transform.position);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float hitDistance = 0.0f;

        //if the player have a target point move the player to it
        if (pmr && canMove) {
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
        else if (isLooting)
        {
            Loot();
        }
        lootingInterface.SetActive(isLooting);

        if (Input.GetKeyDown(KeyCode.Escape) && isLooting)
        {
            CloseLootingTab();
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            //TODO menue
        }

        //create the target point of the player
        if (playerPlane.Raycast(ray, out hitDistance) && canMove)
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

    private void CloseLootingTab()
    {
        SetTarget(null);
        currentLoots = new List<GameObject>();
        isLooting = false;
        canMove = true;
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
        if (target && !enemy.isDead)
        {
            if (Vector3.Distance(transform.position, target.transform.position) <= range)
            {
                isAttacking = true;
                return true;
            }
        }
        else if(target && enemy )
        {
            if (enemy.isDead) { 
                if (Vector3.Distance(transform.position, target.transform.position) <= lootRange)
                {
                    canMove = false;
                    isLooting = true;
                    return true;
                }
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
            if (currentAnimName == "attack" && anim.isPlaying)
            {
                //Wait
            }
            else { 
                
                transform.position = Vector3.MoveTowards(transform.position, pmr.position, movementSpeed * Time.deltaTime);
                if (currentAnimName != "walk") {
                    currentAnimName = "walk";
                    anim.CrossFade("walk");
                }
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
                enemy.TakeDamage(attack);
                currentAnimName = "attack";
                anim.CrossFade("attack");
                lastTimeAttacked = Time.time;
            }
        }
        //That mean we still target the enemy but he is too far (bcs he fleed ?) (enemies fleeing isn't planned)
        else
        {
            pmr = target.transform;
        }
    }

    void Loot()
    {
        if (!enemy.isLooted) { 
            List<GameObject> loots = enemy.getLoots();
            foreach (GameObject loot in loots)
            {
                currentLoots.Add(Instantiate(loot, lootingGrid.transform));
            }
        }
    }

    public void UseItem(GameObject item)
    {
        int index = inventoryData.FindIndex(i => i == item);
        Destroy(inventoryData[index]);
    }
    public void Itemlooted(GameObject item)
    {
        int index = currentLoots.FindIndex(i => i == item);
        currentLoots[index].transform.SetParent(inventoryGrid.transform);
        currentLoots.RemoveAt(index);
        if(currentLoots.Count == 0)
        {
            CloseLootingTab();
        }
    }

    public void SetTarget(GameObject t)
    {
        if (t) { 
        target = t;
        enemy = t.GetComponent<AEnemy>();
        }
        else
        {
            target = null;
            enemy = null;
        }
    }


    
    #endregion
}
