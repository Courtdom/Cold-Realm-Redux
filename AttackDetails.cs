using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct AttackDetails 
{
    public Vector2 position;
    public int damageAmount;
    public int stunDamageAmount;
    public bool Knockback;
    public bool hurt;
    public float damageKnockbackSpeed;
    public int healingAmount;
}
