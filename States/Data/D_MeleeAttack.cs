using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newMeleeAttackStateData", menuName = "Data/State Data/Melee Attack State")]

public class D_MeleeAttack : ScriptableObject
{
    public float attackRadius = 0.5f;

    public LayerMask whatIsPlayer;
    public int attackDamage = 10;
    public float damageKnockbackSpeed = 50f;
    public bool hurt;
 }
