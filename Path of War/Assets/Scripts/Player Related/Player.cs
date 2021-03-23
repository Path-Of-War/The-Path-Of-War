using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{

    //singleton
    public static Player instance;

    #region properties
    public PlayerEffect pEffect;
    public Equipement pEquipement;


    //movement
    public float movementSpeed;
    public GameObject clickPoint;
    public Transform pmr;
    GameObject triggerPMR;
    public NavMeshAgent agent;
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

    public float baseRange;
    public float baseAttackSpeed;

    public int currentLevel = 0;
    public int currentXp = 0;
    public int xpToLevel = 10;
    #endregion

    #endregion

    #region Functions

    #region built in functions
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = movementSpeed;
        baseAttackSpeed = attackSpeed;
        baseRange = range;
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

        //speed the attack animation to be good with the attack speed
        if (isAttacking)
        {
            foreach (AnimationState aState in anim)
            {
                aState.speed = 1f * attackSpeed +0.1f;
            }
        }
        else
        {
            foreach (AnimationState aState in anim)
            {
                aState.speed = 1;
            }
        }

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
        if (!CheckRange() && canMove) 
        {
            if (currentAnimName == "idle" && anim.isPlaying)
            {
                isAttacking = false;
                isIdle = false;
                //TODO change for pathfinding
                agent.SetDestination(pmr.position);
                transform.LookAt(pmr.transform);
                //transform.position = Vector3.MoveTowards(transform.position, pmr.position, movementSpeed * Time.deltaTime);

                if (currentAnimName != "walk")
                {
                    currentAnimName = "walk";
                    anim.CrossFade("walk");
                }
            }
            else {
                isAttacking = false;
                isIdle = false;
                //TODO change for pathfinding
                agent.SetDestination(pmr.position);
                transform.LookAt(pmr.transform);
                //transform.position = Vector3.MoveTowards(transform.position, pmr.position, movementSpeed * Time.deltaTime);

                if (currentAnimName != "walk") {
                    currentAnimName = "walk";
                    anim.CrossFadeQueued("walk");
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
        if (enemy) { 
            if (Vector3.Distance(transform.position, enemy.transform.position) <= range)
            {
                isIdle = false;
                canMove = false;
                isAttacking = true;
                if ((lastTimeAttacked + (1f / attackSpeed)) <= Time.time)
                {
                    //Deal the damage in this loop

                    currentAnimName = "attack";
                    anim.CrossFade("attack",0.1f);
                    lastTimeAttacked = Time.time;
                    enemy.TakeDamage(attack);
                }
            }
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
        if (item.GetComponent<ALoot>().GetLootType() == ALoot.LootType.consomable)
            Destroy(inventoryData[index]);
        inventoryData.RemoveAll(i => i == null);
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


    public void EnemyKilled()
    {
        isIdle = true;
        isAttacking = false;
        isLooting = false;
        canMove = true;
    }
    
    #endregion
}
