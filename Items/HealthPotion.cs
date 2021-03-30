using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : MonoBehaviour
{
    // Start is called before the first frame update

    protected AttackDetails attackDetails;

    public int healthRestore = 25;

    [SerializeField]
    public Transform HealthPos;
    public float healthRadius;
    public LayerMask whatIsPlayer;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        AddToHealth();

        Destroy(gameObject);

    }


    public void AddToHealth()
    {
        Collider2D[] enemyObjects = Physics2D.OverlapCircleAll(HealthPos.position, healthRadius, whatIsPlayer);

        attackDetails.healingAmount = healthRestore;

        foreach (Collider2D collider in enemyObjects)
        {

            collider.gameObject.SendMessage("Heal", attackDetails);
        }
    }


    public virtual void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(HealthPos.transform.position, healthRadius);
    }
}
