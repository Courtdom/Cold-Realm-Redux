using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pot : MonoBehaviour
{

    AttackDetails attackDetails;
    private Animator Anim;
    private Rigidbody2D PotRB;
    //public Vector2 CurrentVelocity;
    private Vector2 workspace;
    [SerializeField]
    private GameObject hitParticle;

    private float maxHealth = 30f;
    public float currentHealth;
    private int lastDamageDirection;


    // Start is called before the first frame update
    void Start()
    {
        Anim = GetComponent<Animator>();
        PotRB = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
    }


    public void Damage(AttackDetails attackDetails)
    {
        // Debug.Log("in player script");
        Instantiate(hitParticle, transform.position, Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)));
        currentHealth -= attackDetails.damageAmount;


        // currentStunResistance -= attackDetails.stunDamageAmount;
        if (attackDetails.position.x > transform.position.x)
        {
            lastDamageDirection = -1;
        }
        else
        {
            lastDamageDirection = 1;
        }
        
            DamageHop(attackDetails.damageKnockbackSpeed);
       



        /*_________if (currentStunResistance <= 0)
        {
            isStunned = true;
        }  _______________*/

        if (currentHealth <= 0)
        {
            Anim.SetBool("Destroyed", true);
            //wait some time then destroy object
        }
    }

    public void DamageHop(float damageKnockbackSpeed)
    {
            workspace.Set(damageKnockbackSpeed * lastDamageDirection, PotRB.velocity.y);
            PotRB.velocity = workspace;
    }
}
