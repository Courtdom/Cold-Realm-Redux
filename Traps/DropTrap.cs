using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropTrap : MonoBehaviour
{
    AttackDetails attackDetails;
    //Rigidbody2D DropTrapRB;
    Animator Anim;
    public Transform TrapPos;
    public LayerMask whatIsPlayer;

    private float fallingTimer = 1f;
    private float StartTime;
    private float destroyTimer = 1.5f;
    private float hurtTimer = 0f;
    private float destroyStartTime;
    private float damageTimer = 0.1f;
    private float damageStartTime;
    private bool falling;
    private bool fallen;
    private bool triggered;
    private bool canDamage;

    public int dropTrapDamage = 5;
    public float AttackKnockbackSpeed = 25f;
    public float attackRadius;


    private void Start()
    {
       // DropTrapRB = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();
        fallen = false;
        triggered = false;
    }


    private void Update()
    {


        if (Time.time >= destroyStartTime + destroyTimer && fallen)
        {
            Destroy(gameObject);

        }


    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "Player" && !triggered)
        {

            Anim.SetBool("TrapTriggered", true);
            StartTime = Time.time;
            //falling = true;
            fallen = true;
            destroyStartTime = Time.time;
            triggered = true;


        }

    }






    public void DropTrapDamage()
    {
        //destroyTimer = 2.5f;


        Collider2D[] enemyObjects = Physics2D.OverlapCircleAll(TrapPos.position, attackRadius, whatIsPlayer);

        attackDetails.damageAmount = dropTrapDamage;
        attackDetails.damageKnockbackSpeed = AttackKnockbackSpeed;
        attackDetails.position = transform.position;
        attackDetails.hurt = true;
        damageStartTime = Time.time;

        foreach (Collider2D collider in enemyObjects)
        {

            collider.gameObject.SendMessage("Damage", attackDetails);
        }
    }





    public virtual void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(TrapPos.transform.position, attackRadius);
    }

}

