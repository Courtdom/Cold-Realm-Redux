using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private AttackDetails attackDetails;

    private float speed;
    private float travelDistance;
    private float xStartPos;
    [SerializeField]
    private float gravity;
    [SerializeField]
    private float damageRadius;

    private bool isGravityOn;
    private bool hasHitGround;
    private bool hasHitWall;

    [SerializeField]
    private LayerMask whatIsGround;
    [SerializeField]
    private LayerMask whatIsPlayer;
    [SerializeField]
    private LayerMask whatIsWall;
    [SerializeField]
    private Transform damagePosition;

    private Animator Anim;


    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.gravityScale = 0.0f;

        rb.velocity = transform.right * speed;

        isGravityOn = false;

        xStartPos = transform.position.x;

        Anim = GetComponent<Animator>();

    }

    private void Update()
    {
        if(!hasHitGround && !hasHitWall)
        {
            attackDetails.position = transform.position;

            if(isGravityOn)
            {
                float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
        }
    }


    private void FixedUpdate()
    {
        if(!hasHitGround && !hasHitWall)
        {

            Collider2D damageHit = Physics2D.OverlapCircle(damagePosition.position, damageRadius, whatIsPlayer);
            Collider2D groundHit = Physics2D.OverlapCircle(damagePosition.position, damageRadius, whatIsGround);
            Collider2D wallHit = Physics2D.OverlapCircle(damagePosition.position, damageRadius, whatIsWall);

            if(damageHit)
            {
                damageHit.transform.SendMessage("Damage", attackDetails);

                ArrowHit();
            }

            if (wallHit)
            {
                hasHitWall = true;
                rb.gravityScale = 0f;
                rb.velocity = Vector2.zero;
                ArrowHit();
            }

            if (Mathf.Abs(xStartPos - transform.position.x) >= travelDistance && !isGravityOn)
            {
                isGravityOn = true;
                rb.gravityScale = gravity;
            }

            if (groundHit)
            {
                hasHitGround = true;
                rb.gravityScale = 0f;
                rb.velocity = Vector2.zero;
                Destroy(gameObject, 0.5f);

            }

            
        }

       

    }

    public void FIreProjectile(float speed, float travelDistance, int damage)
    {
        this.speed = speed;
        this.travelDistance = travelDistance;
        attackDetails.damageAmount = damage;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(damagePosition.position, damageRadius);
    }


    public void ArrowHit()
    {
        Anim.SetTrigger("Hit");

        Destroy(gameObject, 1f);
    }

}
