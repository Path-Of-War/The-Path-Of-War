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
    Player p;

    void Start()
    {
        p = Player.instance;
    }
    void Update()
    {
        if(Vector3.Distance(p.transform.position,transform.position) <= range)
        {
            if ((lastTimeAttacked + (1f / attackSpeed)) <= Time.time)
            {
                //Deal the damage in this loop
                Debug.Log("enemy attack");
                p.TakeDamage(attack);
                lastTimeAttacked = Time.time;
            }
        }
        
    }
    public void TakeDamage(int amount)
    {
        health -= amount - armor;
        if (health <= 0)
            Die();
    }

    void Die()
    {
        //TODO Dying animation/particule
        Debug.Log(enemyName + " Died");
    }
}
