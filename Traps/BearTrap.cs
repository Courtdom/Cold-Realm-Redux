using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearTrap : MonoBehaviour
{

    AttackDetails attackDetails;
    private Animator Anim;

    public int bearTrapDamage = 10;
    public float AttackKnockbackSpeed = 15f;

    private bool bearTrapSet;
    private bool bearTrapTriggered;
    public Transform BearTrapDamageHitPos;
    public float BearTrapAttackRadius;


    public LayerMask whatIsPlayer;

    // Start is called before the first frame update
    void Start()
    {
        Anim = GetComponent<Animator>();
        bearTrapSet = true;
        bearTrapTriggered = false;
        BearTrapAttackRadius = .3f;

    }

    // Update is called once per frame
    void Update()
    {
        Anim.SetBool("TrapTriggered", bearTrapTriggered);
    }


    public void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "Player")
        {

            bearTrapTriggered = true;
            attackDetails.damageAmount = bearTrapDamage;
            attackDetails.damageKnockbackSpeed = AttackKnockbackSpeed;
            attackDetails.position = transform.position;
            attackDetails.Knockback = true;



            target.gameObject.SendMessage("Damage", attackDetails);
            Debug.Log("we hit bear trap");
            //instantiate hit particle
        }

}


    public virtual void OnDrawGizmos()
    {


        Gizmos.DrawWireSphere(BearTrapDamageHitPos.transform.position, BearTrapAttackRadius);
    }



}
