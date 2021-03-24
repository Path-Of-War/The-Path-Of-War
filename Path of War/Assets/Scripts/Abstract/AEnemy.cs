using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AEnemy : MonoBehaviour
{
    public string enemyName;
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


    public GameObject pos;

    public int experiencePoint;

    void Start()
    {
        p = Player.instance;
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

    }
    public void TakeDamage(int amount)
    {
        health -= amount - armor;
        if (health <= 0 && !isDead)
            Die();
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
