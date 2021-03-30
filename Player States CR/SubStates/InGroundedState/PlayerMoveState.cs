using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerGroundedState
{

    public PlayerMoveState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {

    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();



        player.playerMovement.ResetSprinting();
        player.playerMovement.ResetIsSprinting();
        player.playerMovement.SprintingTimerReset();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        player.playerMovement.CheckIfShouldFlip(xInput);

        player.playerMovement.SetSprintingVelocity();


        player.playerMovement.SetIsSprinting();


        if (player.playerCombat.knockdown)
        {
            stateMachine.ChangeState(player.KnockDownState);
        }
      // else if (player.playerCombat.CheckKnockBackTimeInput())
      //  {
               else if (xInput == 0 && !isExitingState)
            {
                stateMachine.ChangeState(player.IdleState);
            }
            else if (player.playerCombat.hurt   &&  player.playerCombat.CanBeHurt())
            {
                stateMachine.ChangeState(player.HurtState);
            }
         
            else if (player.inputHandler.KickInput && player.playerCombat.kickAttack && !player.playerMovement.isSprinting)
            {
                stateMachine.ChangeState(player.KickComboState);
            }
            
      //  }
        

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
