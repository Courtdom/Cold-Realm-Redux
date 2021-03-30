using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{

    [SerializeField]
    Player player;

    public PlayerCombatData combatData;

    protected AttackDetails attackDetails;

    private IEnumerator coroutine;


    #region Transforms & Components

    //_____Sword & Kicks______
    public Transform swordAttackHitPos;
    public Transform kickAttackHitPos;
    public Transform flyingKickHitPos;


    //____Magic_____
    public Transform weakMagicHitPos;
    public Transform fullPowerMagicHitPos;
    public Transform magicStrongHitPos;


    public Material defaultMaterial;
    public Material deadMaterial;
    public GameObject deadHologram;
    public TextMeshPro DamageAmount;
    public GameObject damagePoints;
    #endregion

    #region Variables

    //_____Ints_________
    public int noOfClicks { get; private set; }
    public int damageIndex { get; private set; }
    public int comboCount { get; private set; }
    public int swordAttackDamage { get; private set; }
    public int lastDamageDirection { get; private set; }
  //  public int currentHealth;
  //  public int XP;


    //______Bools____________
    public bool flameMagic { get; private set; }
    public bool fullPowerMagicAttack { get; private set; }
    public bool weakMagicAttack { get; private set; }
    public bool swordAttack { get; private set; }
    public bool flyingKickAttack { get; private set; }
    public bool hurt { get; private set; }
    public bool knockdown { get; private set; }
    public bool isDead { get; private set; }
    public bool kickAttack { get; private set; }
    public bool attackFinished { get; private set; }
    public bool immuneToDamage { get; private set; }

    //_________Floats________
    public float comboCounterTimer { get; private set; }

    public float lastDamageTime;

    public float knockDownStartTime { get; private set; }

    public float SwordAttackStartTime { get; private set; }

    #endregion




  //  public PlayerStatistics localPlayerData = new PlayerStatistics();


    // Start is called before the first frame update
    void Start()
    {
        //currentHealth = combatData.Playerhealth;

        //  currentHealth = GlobalControl.Instance.HP;
        //  XP = GlobalControl.Instance.XP;


        comboCounterTimer = Time.time;
        SetWeaponDamage();



    }

    // Update is called once per frame
    void Update()
    {
    }





   



    #region Health

    

    #endregion 


    #region Attack Details


    private void Heal(AttackDetails attackDetails)
    {
        player.currentHealth += attackDetails.healingAmount;
        player.playerFX.HealFX();
    }


    private void Damage(AttackDetails attackDetails)
    {
        if (!hurt || !knockdown)
        {
            // Debug.Log("in player script");
            player.playerFX.DamageHitParticleFX();
            player.currentHealth -= attackDetails.damageAmount;

            if (player.currentHealth <= 0)
            {

               //  coroutine = DeathAndRespawn();
              //  StartCoroutine(coroutine);
                isDead = true;
            }
            else
            {
                hurt = attackDetails.hurt;
                knockdown = attackDetails.Knockback;
                lastDamageTime = Time.time;



                // currentStunResistance -= attackDetails.stunDamageAmount;
                if (attackDetails.position.x > transform.position.x)
                {
                    lastDamageDirection = -1;
                }
                else
                {
                    lastDamageDirection = 1;
                }
                if (hurt)
                {
                    player.playerMovement.DamageHop(attackDetails.damageKnockbackSpeed);
                }
                else if (knockdown)
                {
                    player.playerMovement.DamageHopKnockDown(attackDetails.damageKnockbackSpeed);
                }
            }
        }

    }





    #endregion


    


    #region Damage 

    #region Sword

    public void SwordComboDamage()
        {

        Collider2D[] enemyObjects = Physics2D.OverlapCircleAll(swordAttackHitPos.position, combatData.attack1Radius, combatData.whatIsDamageable);

        SetWeaponDamage();
        attackDetails.damageAmount = combatData.swordAttackDamage * noOfClicks;
        attackDetails.position = transform.position;
        attackDetails.damageKnockbackSpeed = combatData.swordKnockbackSpeed;


        foreach (Collider2D collider in enemyObjects)
        {
            if (collider.gameObject.tag == "Enemy")
            {
                SetDamageIndex(1);
                SetDamagePopUpText();
                Instantiate(damagePoints, collider.transform.position, Quaternion.identity); //do from playerFX


                AddToComboCount();
            }


            collider.transform.parent.SendMessage("Damage", attackDetails);
            // Debug.Log("we hit" + collider.name);
            //instantiate hit particle
            }
        }

    #endregion

    #region Kick & Flying Kick
    public void KickComboDamage()
    {
        player.inputHandler.KickInputReleased();

        Collider2D[] enemyObjects = Physics2D.OverlapCircleAll(kickAttackHitPos.position, combatData.kickRadius, combatData.whatIsDamageable);

        attackDetails.damageAmount = combatData.kickDamage;
        attackDetails.position = transform.position;
        attackDetails.damageKnockbackSpeed = combatData.kickKnockbackSpeed;

        foreach (Collider2D collider in enemyObjects)
        {
            if (collider.gameObject.tag == "Enemy")
            {
                SetDamageIndex(3);
                SetDamagePopUpText();
                Instantiate(damagePoints, collider.transform.position, Quaternion.identity);

                AddToComboCount();
            }


            collider.transform.parent.SendMessage("Damage", attackDetails);
            // Debug.Log("we hit" + collider.name);
            //instantiate hit particle
        }
    }

    public void FlyingKickComboDamage()
    {
        player.inputHandler.FlyingKickInputReleased();

        Collider2D[] enemyObjects = Physics2D.OverlapCircleAll(flyingKickHitPos.position, combatData.flyingKickRadius, combatData.whatIsDamageable);

        attackDetails.damageAmount = combatData.flyingKickDamage;
        attackDetails.position = transform.position;
        attackDetails.damageKnockbackSpeed = combatData.flyingKickKnockbackSpeed;

        foreach (Collider2D collider in enemyObjects)
        {
            if (collider.gameObject.tag == "Enemy")
            {
                SetDamageIndex(2);
                SetDamagePopUpText();
                Instantiate(damagePoints, collider.transform.position, Quaternion.identity);

                AddToComboCount();
            }


            collider.transform.parent.SendMessage("Damage", attackDetails);
            // Debug.Log("we hit" + collider.name);
            //instantiate hit particle
        }
    }








    #endregion

    #region Magic

    public void FlameMagicDamage()
    {
        player.inputHandler.MagicCastReleased();


        Collider2D[] enemyObjects = Physics2D.OverlapCircleAll(magicStrongHitPos.position, combatData.magicStrongRadius, combatData.whatIsDamageable);

        attackDetails.damageAmount = combatData.magicStrongDamage;

        attackDetails.position = transform.position;

        attackDetails.damageKnockbackSpeed = combatData.magicStrongKnockbackSpeed;
        foreach (Collider2D collider in enemyObjects)
        {
            if (collider.gameObject.tag == "Enemy")
            {
                SetDamageIndex(4);
                SetDamagePopUpText();
                Instantiate(damagePoints, collider.transform.position, Quaternion.identity);


                collider.transform.parent.SendMessage("Damage", attackDetails);
            }
        }
    }
    public void WeakMagicDamage()
    {

        player.inputHandler.WeakMagicCastInputReleased();


        Collider2D[] enemyObjects = Physics2D.OverlapCircleAll(weakMagicHitPos.position, combatData.weakMagicRadius, combatData.whatIsDamageable);

        attackDetails.damageAmount = combatData.weakMagicDamage;

        attackDetails.position = transform.position;

        attackDetails.damageKnockbackSpeed = combatData.weakMagicKnockbackSpeed;
        foreach (Collider2D collider in enemyObjects)
        {

            if (collider.gameObject.tag == "Enemy")
            {

                SetDamageIndex(5);
                SetDamagePopUpText();
                Instantiate(damagePoints, collider.transform.position, Quaternion.identity);

                collider.transform.parent.SendMessage("Damage", attackDetails);

            }

        }
    }

    public void FullPowerMagicDamage()
    {
        player.inputHandler.FullPowerMagicCastInputReleased();

        Collider2D[] enemyObjects = Physics2D.OverlapCircleAll(fullPowerMagicHitPos.position, combatData.fullPowerMagicRadius, combatData.whatIsDamageable);

        attackDetails.damageAmount = combatData.fullPowerMagicDamage;

        attackDetails.position = transform.position;

        attackDetails.damageKnockbackSpeed = combatData.fullPowerMagicKnockbackSpeed;
        foreach (Collider2D collider in enemyObjects)
        {
            if (collider.gameObject.tag == "Enemy")
            {

                SetDamageIndex(6);
                SetDamagePopUpText();
                Instantiate(damagePoints, collider.transform.position, Quaternion.identity);

                collider.transform.parent.SendMessage("Damage", attackDetails);


            }

        }
    }


    #endregion


    #region Set

    public void SetDamageIndex(int amount)
    {
        damageIndex = amount;
    }

    public void SetAttackFinished()
    {
        attackFinished = !attackFinished;
    }

    public void SetWeaponDamage()
    {
        swordAttackDamage = combatData.swordAttackDamage * combatData.swordStrength;
    }

    public void ResetImmuneToDamage()
    {
        immuneToDamage = !immuneToDamage;
    }


    #endregion

    #endregion




    #region Damage Pop Up

    public int DamagePopupAmount(int damageIndex)
    {
        if (damageIndex == 1)
        {
            return combatData.swordAttackDamage * noOfClicks; ;

        }
        else if (damageIndex == 2)
        {
            return combatData.flyingKickDamage;
        }
        else if (damageIndex == 3)
        {
            return combatData.kickDamage;

        }
        else if (damageIndex == 4)
        {
            return combatData.magicStrongDamage;
        }
        else if (damageIndex == 5)
        {
            return combatData.weakMagicDamage;
        }
        else   /*  if (damageIndex == 6)      */
        {
            return combatData.fullPowerMagicDamage;
        }
        
    }

  

    public void SetDamagePopUpText()
    {
        DamageAmount = damagePoints.GetComponentInChildren<TextMeshPro>();
        int temp = DamagePopupAmount(damageIndex);
        //  Debug.Log("temp = " + temp);
        DamageAmount.SetText(temp.ToString());

    }

    public void DamagePoints()
    {
        Instantiate(damagePoints, transform.position, Quaternion.identity);
    }
    #endregion






    #region Set Functions


    #region Set Sword & Kick
    
    public void SwordAttack()
    {
        swordAttack = !swordAttack;
    }

    public void SetKickAttack()
    {
        kickAttack = !kickAttack;
    }
    public void SetFlyingKickAttack()
    {
        flyingKickAttack = !flyingKickAttack;
    }

    #endregion

   

    #endregion





    #region Sword Combo System


    public void CheckNoOfClicksToStart()

    {
        player.inputHandler.SwordAttackInputReleased();
        if (noOfClicks == 1)
        {
            SetSwordOneTrue();
            //inputHandler.SwordAttackInputReleased();
        }

    }

    public void SetNoOfClicksToZero()
    {
        noOfClicks = 0;
    }
    public void return1()
    {
        if (noOfClicks >= 2)
        {
            player.inputHandler.SwordAttackInputReleased();
            SetSwordTwoTrue();
        }
        else
        {
            noOfClicks = 0;
            player.inputHandler.SwordAttackInputReleased();
            SetSwordOneFalse();

        }
    }

    public void return2()
    {
        if (noOfClicks >= 3)
        {
            player.inputHandler.SwordAttackInputReleased();
            SetSwordThreeTrue();
        }
        else
        {
            noOfClicks = 0;
            player.inputHandler.SwordAttackInputReleased();
            SetSwordOneFalse();
            SetSwordTwoFalse();
        }
    }

    public void return3()
    {
        noOfClicks = 0;
        player.inputHandler.SwordAttackInputReleased();
        SetSwordOneFalse();
        SetSwordTwoFalse();
        SetSwordThreeFalse();
    }
    public void AddToNoClicks()
    {
        noOfClicks++;
        noOfClicks = Mathf.Clamp(noOfClicks, 0, 3);
    }


    public void SetSwordOneTrue()
    {
        player.Anim.SetBool("Sword1", true);
        //Debug.Log("Sword1 is true");
    }
    public void SetSwordOneFalse()
    {
        player.Anim.SetBool("Sword1", false);
        // Debug.Log("sword1 is false");
    }
    public void SetSwordTwoTrue()
    {
        player.Anim.SetBool("Sword2", true);
        // Debug.Log("Sword2 is true");
    }
    public void SetSwordTwoFalse()
    {
        player.Anim.SetBool("Sword2", false);
        //Debug.Log("Sword 2 is false");
    }
    public void SetSwordThreeTrue()
    {
        player.Anim.SetBool("Sword3", true);
        // Debug.Log("Sword3 is false");
    }
    public void SetSwordThreeFalse()
    {
        player.Anim.SetBool("Sword3", false);
        // Debug.Log("Sword 3 is true");
    }

    #endregion





    #region  Combo Points System
    public void OnComboStartTime()
    {
        SwordAttackStartTime = Time.time;

    }
    public void CallComboCountTimer()
    {
        comboCounterTimer = Time.time;
    }
    public void AddToComboCount()
    {
        //  Debug.Log("Combo Count Called");
        comboCount++;
    }
    public void SubtractFromComboCount()
    {
        comboCount--;
    }
    public void SetComboCountToZero()
    {
        comboCount = 0;
    }
    public bool MaxComboDelayCheck()
    {
        return Time.time - SwordAttackStartTime > SwordAttackStartTime;
    }

    #endregion


    #region Set Magic
    public void SetMagicAttack()
    {
        flameMagic = !flameMagic;
    }
    public void SetWeakMagicAttack()
    {
        weakMagicAttack = !weakMagicAttack;
    }
    public void SetFullPowerMagicAttack()
    {
        fullPowerMagicAttack = !fullPowerMagicAttack;
    }

    #endregion


    #region KnockDown & Hurt

    public bool CanBeHurt()
    {
        return Time.time >= player.HurtState.lastHurtTime + combatData.hurtCooldown;
    }
    public void ResetHurt()
    {
        hurt = false;
    }
    public void SetKnockdown()
    {
        knockdown = true;
    }
    public void ResetKnockdown()
    {
        knockdown = false;
    }
    public void SetKnockDownStartTime()
    {
        knockDownStartTime = Time.time;
    }
    public bool CheckKnockBackTimeInput()
    {
        return Time.time > knockDownStartTime + combatData.knockbackTime;
    }


    #endregion




    #region Death & Respawn & Checkpoints

   

    public void Respawn()
    {
        isDead = false;
        player.gameObject.layer = 10;
        player.RB.constraints = RigidbodyConstraints2D.FreezeRotation;
        player.currentHealth = combatData.Playerhealth;
        player.StateMachine.ChangeState(player.IdleState);
        
    }

    public void RespawnLevelBoundary()
    {
        isDead = false;
        player.gameObject.layer = 10;
        player.RB.constraints = RigidbodyConstraints2D.FreezeRotation;
        player.StateMachine.ChangeState(player.IdleState);
    }
   

    #endregion

}
