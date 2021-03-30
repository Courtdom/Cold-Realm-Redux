using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwordComboState : PlayerCombatState
{


    private float timer = .4f;
    private float timerStartTime;




    // protected AttackDetails attackDetails;




    public PlayerSwordComboState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();


        // attackDetails.damageAmount = player.swordAttackDamage;
        //  attackDetails.position = player.transform.position;
        player.RB.constraints = RigidbodyConstraints2D.FreezeAll;

        player.playerCombat.CheckNoOfClicksToStart();
        //Debug.Log("no. of clicks in sword combo state = " + player.noOfClicks);

       

    }

    public override void Exit()
    {
        //player.SetAttackFinished();
        player.RB.constraints = RigidbodyConstraints2D.FreezeRotation;

        base.Exit();
        
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Time.time >= timerStartTime + timer)
        {
            player.playerCombat.SwordAttack(); 
            isAbilityDone = true;


        }





    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
        
    }

    

   


    


}
