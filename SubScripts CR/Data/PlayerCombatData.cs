using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Player Data/Player Combat Data")]


public class PlayerCombatData : ScriptableObject
{


    public LayerMask whatIsDamageable;
    public GameObject damagePoints;






    [Header("Sword State")]
    public float swordMoveVelocity = 5f;
    public float attack1Radius = 1.5f;
    public int swordAttackDamage = 10;
    public int swordStrength = 2;
    public float swordCombotimer = 1f;
    public float swordKnockbackSpeed = 10f;

    [Header("Kick Stats")]
    public int kickDamage = 8;
    public float kickRadius = 0.6f;
    public float kickKnockbackSpeed = 5f;

    public int flyingKickDamage = 20;
    public float flyingKickKnockbackSpeed = 10f;
    public float flyingKickRadius = 1f;



    [Header("Player Stats")]
    public int Playerhealth = 125;


    [Header("Magic")]
    public int magicStrongDamage = 20;
    public float magicStrongRadius = 1.25f;
    public float magicStrongKnockbackSpeed = 50f;

    public float weakMagicRadius = 1f;
    public int weakMagicDamage = 5;
    public float weakMagicKnockbackSpeed = 20f;

    public int fullPowerMagicDamage = 25;
    public float fullPowerMagicKnockbackSpeed = 60f;
    public float fullPowerMagicRadius = 2.5f;


    [Header("KnockDown & Hurt State")]
    public float damageHopSpeed = 20f;
    public float hurtTime = .5f;
    public float hurtCooldown = 20f;
    public float knockbackTime = 0.5f;




}
