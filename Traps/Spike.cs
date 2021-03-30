using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    AttackDetails attackDetails;


    public int lavaDamage = 5;
    public float AttackKnockbackSpeed = 20f;


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
