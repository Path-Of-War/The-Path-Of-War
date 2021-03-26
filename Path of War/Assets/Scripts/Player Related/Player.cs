using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class Player : MonoBehaviour
{

    //singleton
    public static Player instance;

    #region properties
    public PlayerEffect pEffect;
    public Equipement pEquipement;


    //movement
    public float movementSpeed;
    public NavMeshAgent agent;
    public bool canMove = true;


    //animation
    public AnimationController animCtrl;

    //combat
    public GameObject target;
    public AEnemy enemy;
    public AInteractable interactable;
    public GameObject interactInterface;
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

    //UI
    public GameObject popupText;
    public float popupTime = 5f;
    public float popupDeactivate = 0;
    public EndQuestUI endQuestUI;


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
        animCtrl = GetComponent<AnimationController>();
        agent = GetComponent<NavMeshAgent>();
        agent.speed = movementSpeed;
        baseAttackSpeed = attackSpeed;
        baseRange = range;
        currentHealth = maxHealth;
        currentMana = maxMana;
        if (instance)
            instance = null;
        instance = this;
        //anim = GetComponent<Animation>();
    }

    void Update()
    {

        if(popupDeactivate <= Time.time)
        {
            popupText.SetActive(false);
        }
        //Player Movement
        Plane playerPlane = new Plane(Vector3.up, transform.position);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float hitDistance = 0.0f;

        

        //if the player have a target point move the player to it
        if (agent.destination == transform.position && target == null)
        {
            Idle();
        }
        else if (enemy == null)
        {
            animCtrl.state = AnimationController.States.walking;
            //anim.CrossFade("walk");
        }
        else if (enemy != null)
        {
            if (Vector3.Distance(transform.position, enemy.transform.position) > range)
            {
                animCtrl.state = AnimationController.States.walking;
                //anim.CrossFade("walk");
            }
        }


        if (Input.GetKeyDown(KeyCode.Escape) && lootingInterface.activeInHierarchy)
        {
            CloseLootingTab();
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            //TODO menue
        }
        if (interactable) { 
            if(Vector3.Distance(transform.position, interactable.pos.transform.position) <= lootRange && !interactable.textInterface.activeInHierarchy)
            {
                interactable.InteractWith();
            }
        }
        

        if (enemy && !enemy.isDead)
        {
            if (Vector3.Distance(transform.position, enemy.transform.position) <= range && !enemy.isDead)
            {
                animCtrl.state = AnimationController.States.fighting;
                if ((lastTimeAttacked + (1f / attackSpeed)) <= Time.time)
                {
                    
                    //Deal the damage in this loop
                    //anim.CrossFade("attack", 0.1f);
                    lastTimeAttacked = Time.time;
                    enemy.TakeDamage(attack);
                }
            }
            
        }
        if (enemy && enemy.isDead)
        {
            animCtrl.state = AnimationController.States.idling;
            if (Vector3.Distance(transform.position, enemy.transform.position) <= lootRange && enemy.isDead && !enemy.isLooted)
            {
                Loot();
            }
        }


        if (Input.GetKeyDown(KeyCode.Return) && interactInterface.activeInHierarchy && interactable)
        {
            interactable.NextText();
        }
        else if (Input.GetKeyDown(KeyCode.Return) && interactInterface.activeInHierarchy && !interactable)
        {
            interactInterface.gameObject.SetActive(false);
        }

        if (Input.GetMouseButtonDown(0) && !lootingInterface.activeInHierarchy && !inventoryGrid.activeInHierarchy && !interactInterface.activeInHierarchy && !endQuestUI.childContainer.activeInHierarchy)
        {
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity))
            {
                if(hit.transform.tag == "Enemy")
                {
                    Debug.Log("target enemy");
                    target = hit.transform.gameObject;
                    enemy = target.GetComponent<AEnemy>();
                    if (!enemy.isDead) {
                        agent.SetDestination(enemy.pos.transform.position);
                        agent.stoppingDistance = range - 1;
                    }
                    else if (enemy.isDead)
                    {
                        agent.SetDestination(enemy.pos.transform.position);
                        agent.stoppingDistance = lootRange - 1;
                    }
                }
                else if(hit.transform.tag == "NPC")
                {
                    interactable = hit.transform.gameObject.GetComponent<AInteractable>();
                    agent.SetDestination(interactable.pos.transform.position);
                    target = null;
                    enemy = null;
                }
                else
                {
                    interactable = null;
                    agent.SetDestination(hit.point);
                    agent.stoppingDistance = 0;
                }

            }
        }
    }

    private void CloseLootingTab()
    {
        lootingInterface.SetActive(false);
        enemy.isLooted = true;
        Destroy(enemy.gameObject);
        SetTarget(null);
        currentLoots = new List<GameObject>();
        canMove = true;
    }

    #endregion
    

    private void Idle()
    {
        if(animCtrl.state != AnimationController.States.fighting) { 
            animCtrl.state = AnimationController.States.idling;
            //anim.CrossFade("idle");
        }
    }

    void Loot()
    {
        Debug.Log("loot");
        lootingInterface.SetActive(true);
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

    public void EarnItem(GameObject item)
    {
        GameObject tempLoot = Instantiate(item, inventoryGrid.transform);
        tempLoot.GetComponent<ALoot>().isInInventory = true;
        inventoryData.Add(tempLoot);
    }

    public void EarnItem(List<GameObject> items)
    {
        foreach (GameObject item in items)
        {
            GameObject tempLoot = Instantiate(item, inventoryGrid.transform);
            tempLoot.GetComponent<ALoot>().isInInventory = true;
            inventoryData.Add(tempLoot);
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
            agent.SetDestination(transform.position);
            target = null;
            enemy = null;
        }
    }

    
    #endregion
}
