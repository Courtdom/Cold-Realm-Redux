using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stalactites : MonoBehaviour
{
    AttackDetails attackDetails;
    Rigidbody2D stalacititeRB;
    Animator Anim;
    public Transform StagPos;
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

    public int fallingBlockDamage = 5;
    public float AttackKnockbackSpeed = 10f;
    public float attackRadius;
    private void Start()
    {
        stalacititeRB = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();
        stalacititeRB.gravityScale = 0f;
        fallen = false;
        triggered = false;
    }


    private void Update()
    {

        if (Time.time <= StartTime + fallingTimer && fallen)
        {
            canDamage = true;
        }
        else
        {
            canDamage = false;
        }

        if (Time.time >= destroyStartTime + destroyTimer && fallen)
        {
            Destroy(gameObject);

        }

        else if (Time.time >= destroyStartTime + destroyTimer && fallen)
        {
            Destroy(gameObject);

        }
        if (Time.time >= damageStartTime + damageTimer && canDamage)
        {
            FalingBlockDamage();

        }
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "Player" && !triggered)
        {

            stalacititeRB.gravityScale = 5.5f;
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


        Collider2D[] enemyObjects = Physics2D.OverlapCircleAll(StagPos.position, attackRadius, whatIsPlayer);

        attackDetails.damageAmount = fallingBlockDamage;
        attackDetails.damageKnockbackSpeed = AttackKnockbackSpeed;
        attackDetails.position = transform.position;
        attackDetails.hurt = true;
        damageStartTime = Time.time;
        canDamage = false;

        foreach (Collider2D collider in enemyObjects)
        {

            collider.gameObject.SendMessage("Damage", attackDetails);
        }
    }





    public virtual void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(StagPos.transform.position, attackRadius);
    }





}
