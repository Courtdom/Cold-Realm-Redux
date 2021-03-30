using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallGrabState : PlayerTouchingWallState
{

    public PlayerWallGrabState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();

        player.playerMovement.SetHoldPosition();
        player.playerMovement.HoldPosition();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!isExitingState)
        {
            player.playerMovement.HoldPosition();

             if (yInput < 0)
            {
                stateMachine.ChangeState(player.WallSlideState);
            }
        }
        
        
        
    }

  

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
