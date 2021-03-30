using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosive : MonoBehaviour
{

    AttackDetails attackDetails;
    private Animator Anim;

    public Transform ExplosivePos;
    public LayerMask WhatIsDamagable;
    public float attackRadius;

    private bool Triggered;
    public int explosiveDamage = 20;
    public float AttackKnockbackSpeed = 20f;
    private float waitTillDestroy = 0.75f;
    private float startTime;



    private void Start()
    {
        Anim = GetComponent<Animator>();
    }


    private void Update()
    {
        if (Time.time >= startTime + waitTillDestroy && Triggered)
        {
            Destroy(gameObject);
            // Debug.Log("Entered destroy gameobject Timer");

        }
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "Player")
        {
            startTime = Time.time;
            Triggered = true;
            Anim.SetBool("Triggered",true);
            

        }

    }


    private void ExplosiveDamage()
    {

        Collider2D[] enemyObjects = Physics2D.OverlapCircleAll(ExplosivePos.position, attackRadius, WhatIsDamagable);

        attackDetails.damageAmount = explosiveDamage;
        attackDetails.damageKnockbackSpeed = AttackKnockbackSpeed;
        attackDetails.position = transform.position;
        attackDetails.Knockback = true;


        foreach (Collider2D collider in enemyObjects)
        {

            collider.gameObject.SendMessage("Damage", attackDetails);
        }
    }




    public virtual void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(ExplosivePos.transform.position, attackRadius);
    }

}
