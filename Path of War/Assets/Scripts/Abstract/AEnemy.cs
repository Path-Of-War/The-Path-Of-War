using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AEnemy : MonoBehaviour
{
    public string enemyName;
    public int maxHealth;
    public int health;
    public int attack;
    public float attackSpeed;
    float lastTimeAttacked;
    public int armor;
    public int range;
    public List<GameObject> loots;
    public List<float> lootPercentage;
    Player p;
    public bool isDead = false;
    public bool isLooted = false;
    bool lootGenerated = false;

    public GameObject healthBarPrefab;
    public GameObject healthBarObj;
    public Slider healthBarSlider;

    public GameObject healthBarPos;


    public GameObject pos;

    public int experiencePoint;

    void Start()
    {
        maxHealth = health;
        p = Player.instance;
        healthBarSlider = healthBarObj.GetComponent<Slider>();
    }
    void Update()
    {
        if (!isDead) { 
            if(Vector3.Distance(p.transform.position,transform.position) <= range)
            {
                if ((lastTimeAttacked + (1f / attackSpeed)) <= Time.time)
                {
                    //Deal the damage in this loop
                    p.pEffect.TakeDamage(attack);
                    lastTimeAttacked = Time.time;
                }
            }
        }
        UpdateHealth();

    }
    public void TakeDamage(int amount)
    {
        
        health -= amount - armor;
        UpdateHealth();
        if (health <= 0 && !isDead)
            Die();
    }

    void UpdateHealth()
    {
        healthBarSlider.GetComponentInParent<Transform>().LookAt(Camera.main.transform.position);
        healthBarSlider.maxValue = maxHealth;
        healthBarSlider.value = health;
    }

    void Die()
    {
        //TODO Dying animation/particule
        isDead = true;
        Debug.Log(enemyName + " Died");
        p.pEffect.EarnExpereience(experiencePoint);
        p.SetTarget(null);
        Quaternion target = Quaternion.Euler(transform.rotation.x, transform.rotation.y, -90);
        Vector3 targetPos = new Vector3(transform.position.x, transform.position.y - (transform.localScale.z / 2), transform.position.z);
        transform.rotation = target;
        transform.position = targetPos;
        healthBarObj.SetActive(false);
    }

    public List<GameObject> getLoots()
    {
        List<GameObject> lootsEarned = new List<GameObject>();
        if (!lootGenerated) { 
            
            for (int i = 0; i < loots.Count; i++)
            {
                if (lootPercentage[i] >= Random.Range(0, 100))
                {
                    lootsEarned.Add(loots[i]);
                }
            }
            lootGenerated = true;
        }
        return lootsEarned;
    }
}
