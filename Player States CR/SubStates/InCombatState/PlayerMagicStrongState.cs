using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMagicStrongState : PlayerCombatState
{


    protected Data_MagicStrongState magicStrongStateData;
    protected AttackDetails attackDetails;

    private float timer = 1.2f;
    private float timerStartTime;



    public PlayerMagicStrongState(Player player, PlayerStateMachine stateMachine,  string animBoolName) : base(player, stateMachine, animBoolName)
    {
      
    }

    
    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        player.RB.constraints = RigidbodyConstraints2D.FreezeAll;
        timerStartTime = Time.time;
        //player.CheckNoOfClicksToStart();
    }

    public override void Exit()
    {

        player.RB.constraints = RigidbodyConstraints2D.FreezeRotation;
        isAbilityDone = true;


        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();




        if (Time.time >= timerStartTime + timer )
        {
            player.playerCombat.SetMagicAttack();
            isAbilityDone = true;

            Debug.Log("timer done");
        }


    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }


   
}
