using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLandState : PlayerGroundedState
{


    private GameObject dust;




    public PlayerLandState(Player player, PlayerStateMachine stateMachine,  string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.playerFX.LandingDustFX();
        player.playerMovement.ResetSprinting();
        player.playerMovement.ResetIsSprinting();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (!isExitingState)
        {
             if (player.playerCombat.knockdown)
            {
                stateMachine.ChangeState(player.KnockDownState);
            }
           else  if (xInput != 0)
            {

                stateMachine.ChangeState(player.MoveState);
            }
            else if (isAnimationFinished && !player.playerCombat.knockdown)
            {

                stateMachine.ChangeState(player.IdleState);
            }
            
        }
    }
}
