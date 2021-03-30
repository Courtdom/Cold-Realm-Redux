using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FalingStoneBlock : MonoBehaviour
{
    AttackDetails attackDetails;
    Rigidbody2D stoneRB;
    Animator Anim;
    public Transform FallingObjectHitPos;
    public LayerMask whatIsPlayer;

    private float fallingTimer = 1f;
    private float StartTime;
    private float destroyTimer = 2.5f;
    private float hurtTimer = 0f;
    private float destroyStartTime;
    private float damageTimer = 0.25f;
    private float damageStartTime;
    private bool falling;
    private bool fallen;
    private bool triggered;
    private bool canDamage;

    public int fallingBlockDamage = 10;
    public float AttackKnockbackSpeed = 20f;
    public float attackRadius;
    private void Start()
    {
        stoneRB = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();
        stoneRB.gravityScale = 0f;
        fallen = false;
        triggered = false;
    }


    private void Update()
    {

        if (Time.time <= StartTime + fallingTimer && fallen)
        {
            canDamage = true;
           // Debug.Log("Can Damge is true");
        }
        else
        {
            canDamage = false;
           // Debug.Log("Can Damage is false");
        }

         if (Time.time >= destroyStartTime + destroyTimer && fallen)
        {
            Destroy(gameObject);
           // Debug.Log("Entered destroy gameobject Timer");

        }
       
        else if (Time.time >= destroyStartTime + destroyTimer && fallen)
        {
            Destroy(gameObject);
           // Debug.Log("Entered destroy gameobject Timer");

        }
        if (Time.time >= damageStartTime + damageTimer && canDamage)
        {
            FalingBlockDamage();

        }
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "Player"&& !triggered)
        {

            stoneRB.gravityScale = 5f;
            Anim.SetBool("FallTriggered", true);
            StartTime = Time.time;
            //falling = true;
            fallen = true;
            destroyStartTime = Time.time;
            triggered = true;


        }

    }


   



    public void FalingBlockDamage()
    {
        //destroyTimer = 2.5f;


        Collider2D[] enemyObjects = Physics2D.OverlapCircleAll(FallingObjectHitPos.position, attackRadius, whatIsPlayer);
        attackDetails.damageAmount = fallingBlockDamage;
        attackDetails.damageKnockbackSpeed = AttackKnockbackSpeed;
        attackDetails.position = transform.position;
        attackDetails.Knockback = true;
        damageStartTime = Time.time;
        canDamage = false;

        foreach (Collider2D collider in enemyObjects)
        {
            
            //falling = false;
            collider.gameObject.SendMessage("Damage", attackDetails);
            //instantiate hit particle
        }
    }





    public virtual void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(FallingObjectHitPos.transform.position, attackRadius);
    }





}
