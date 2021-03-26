using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public Animator anim;
    public enum States
    {
        looting,
        walking,
        fighting,
        idling
    }

    public States state = States.idling;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        ResetState();
        switch (state)
        {
            case States.looting:
                anim.SetBool("isWalking", true);
                anim.speed = 1;
                break;
            case States.walking:
                anim.speed = 1;
                anim.SetBool("isWalking", true);
                break;
            case States.fighting:
                anim.speed = Player.instance.attackSpeed;
                anim.SetBool("isAttacking", true);
                break;
            case States.idling:
                anim.speed = 1;
                break;
            default:
                anim.speed = 1;
                break;
        }
    }

    void ResetState()
    {
        anim.SetBool("isAttacking", false);
        anim.SetBool("isWalking", false);
    }
}
