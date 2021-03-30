using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{
    AttackDetails attackDetails;


    public int lavaDamage = 20;
    public float AttackKnockbackSpeed = 20f;
    public float attackRadius;


    private void Start()
    {
    }


    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "Player")
        {
            attackDetails.damageAmount = lavaDamage;
            attackDetails.damageKnockbackSpeed = AttackKnockbackSpeed;
            attackDetails.position = transform.position;
            attackDetails.Knockback = true;




            target.gameObject.SendMessage("Damage", attackDetails);
        }

    }


}
