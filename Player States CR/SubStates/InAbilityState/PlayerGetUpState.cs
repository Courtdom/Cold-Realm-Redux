using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGetUpState : PlayerState
{


    //public bool hurt { get; private set; }
   
    //public float lastHurtTime { get; private set; }





    public PlayerGetUpState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();

        player.gameObject.layer = 10;

        player.playerMovement.SetVelocityX(0f);
        startTime = Time.time;

      
    }

    public override void Exit()
    {
        player.playerCombat.ResetImmuneToDamage();   


        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();




        if (Time.time >= startTime + player.playerCombat.combatData.knockbackTime)
        {

            stateMachine.ChangeState(player.IdleState);
            
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }







   
    
}
