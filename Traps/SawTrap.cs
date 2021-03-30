using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawTrap : MonoBehaviour
{

    protected AttackDetails attackDetails;

    public Animator Anim;
    public Rigidbody2D SawRB;
    public Vector2 CurrentVelocity;

    private float waitTimer;
    private float startTime;
    private float sawDamagerTime;
    private float sawDamageTimer;
    private float step;
    private float speed;

    public bool vulnerable; //if it's still and can attack
    private bool start;
    private bool stop;
    private bool spinning;
    private bool movingUp;
    private bool movingDown;


    private float AttackKnockbackSpeed;
    public int sawDamage = 10;
    public float sawAttackRadius;
    public Transform SawDamageHitPos;
    public Transform topTransform;
    public Transform bottomTransform;
    public LayerMask whatIsPlayer;
    private Vector2 targetTop;
    private Vector2 targetBottom;
    private Vector2 position;




    private void Start()
    {
        Anim = GetComponent<Animator>();
        SawRB = GetComponent<Rigidbody2D>();

        waitTimer = Random.Range(2.5f, 5f);
        startTime = Time.time;
        sawDamagerTime = Time.time;
        sawDamageTimer = 0.25f;
        start = true;
        spinning = true;
        AttackKnockbackSpeed = 20f;

        targetTop = topTransform.transform.position;
        targetBottom = bottomTransform.transform.position;
        position = gameObject.transform.position;
        speed = 1f;
        movingUp = true;
    }

    private void Update()
    {
        
        Anim.SetBool("Start", start);
        Anim.SetBool("Stop", stop);
        Anim.SetBool("Spin", spinning);



        //Debug.Log("Inside Update of Saw");



        if (Time.time >= startTime + waitTimer) 
        {
            if(start) //stop saw
            {
               // Debug.Log("Stop Saw");

                start = false;
                stop = true;
                spinning = false;
                startTime = Time.time;
                vulnerable = true;

            }
            else //start saw
            {
              //  Debug.Log("Start Saw");

                stop = false;
                start = true;
                spinning = true;
                startTime = Time.time;
                vulnerable = false;
                
            }

        }
        if(Time.time >= sawDamagerTime + sawDamageTimer && spinning)
        {
            SawDamage();
            sawDamagerTime = Time.time;

        }

        MoveUpandDown();
    }

    private void FixedUpdate()
    {
        CurrentVelocity = SawRB.velocity;
    }



    public void SawDamage()
    {
        Collider2D[] enemyObjects = Physics2D.OverlapCircleAll(SawDamageHitPos.position, sawAttackRadius, whatIsPlayer);
        
        attackDetails.damageAmount = sawDamage;
        attackDetails.damageKnockbackSpeed = AttackKnockbackSpeed;
        attackDetails.position = transform.position;
        attackDetails.Knockback = true;


        foreach (Collider2D collider in enemyObjects)
        {
            start = false;
            stop = true;
            collider.gameObject.SendMessage("Damage", attackDetails);
            // Debug.Log("we hit" + collider.name);
            //instantiate hit particle
        }
    }

    public virtual void OnDrawGizmos()
    {


        Gizmos.DrawWireSphere(SawDamageHitPos.transform.position, sawAttackRadius);
    }




    public void MoveUpandDown()
    {
        
        if (spinning)
        {
            position = gameObject.transform.position;
           // Debug.Log("have reached top = " + ReachedTop());
            if (!ReachedTop() && movingUp)
            {
                step = speed * Time.deltaTime;
                transform.position = Vector2.MoveTowards(transform.position, targetTop, step);
                
            }
            else if (ReachedTop())
            {
                movingUp = false;
                movingDown = true;
            }
           if(!ReachedBottom() && movingDown)
            {
                step = speed * Time.deltaTime;
                transform.position = Vector2.MoveTowards(transform.position, targetBottom, step);
            }
            else if(ReachedBottom())
            {
                movingDown = false;
                movingUp = true;
            }

           
            
        }
    }
   

    public bool ReachedTop()
    {
        return targetTop == position;
    }
    public bool ReachedBottom()
    {
        return targetBottom == position;
    }




}

