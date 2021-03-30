using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJumpState : PlayerAbilityState
{

    private int wallJumpDirection;

    public PlayerWallJumpState(Player player, PlayerStateMachine stateMachine,  string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.inputHandler.UseJumpInput();
        player.JumpState.ResetAmountOfJumpsLeft();
        player.playerMovement.SetVelocity(player.playerMovement.movementData.wallJumpVelocity, player.playerMovement.movementData.wallJumpAngle, wallJumpDirection);
        player.playerMovement.CheckIfShouldFlip(wallJumpDirection);
        player.JumpState.DecreaseAmountOfJumpsLeft();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        player.Anim.SetFloat("yVelocity", player.playerMovement.CurrentVelocity.y);
        player.Anim.SetFloat("xVelocity", Mathf.Abs(player.playerMovement.CurrentVelocity.x));

        if(Time.time >= startTime + player.playerMovement.movementData.wallJumpTime)
        {
            isAbilityDone = true;
        }
    }

    public void DeterminWallJumpDIrection(bool isTouchingWall)
    {
        if(isTouchingWall)
        {
            wallJumpDirection = -player.playerMovement.FacingDirection;
        }
        else
        {
            wallJumpDirection = player.playerMovement.FacingDirection;
        }
    }
}

