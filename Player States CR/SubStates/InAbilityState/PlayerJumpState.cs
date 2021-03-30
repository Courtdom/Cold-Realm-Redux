using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerAbilityState
{
    private int amountOfJumpsLeft;
    private bool JumpInputStop;
    public bool canJump { get; private set; }
    private float lastJumpTime;


    public PlayerJumpState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
        amountOfJumpsLeft = player.playerMovement.movementData.amountOfJumps;
    }

    public override void Enter()
    {
        base.Enter();
        player.inputHandler.UseJumpInput();
        player.playerMovement.SetVelocityY(player.playerMovement.movementData.jumpVelocity * player.playerMovement.movementData.jumpMultiplier);
        amountOfJumpsLeft--;
        player.InAirState.SetIsJumping();
    }

    public bool CanJump()
    {
        if (amountOfJumpsLeft > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void ResetAmountOfJumpsLeft()
    {
        amountOfJumpsLeft = player.playerMovement.movementData.amountOfJumps;
    }

    public void DecreaseAmountOfJumpsLeft()
    {
        amountOfJumpsLeft--;
    }

    public override void Exit()
    {

        base.Exit();

    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        JumpInputStop = player.inputHandler.JumpInputStop;
        if (JumpInputStop || Time.time >= player.inputHandler.jumpInputStartTime + player.inputHandler.dashInputHoldTime)
        {
            isAbilityDone = true;
            player.playerMovement.ResetJumpVelocity();

        }
        else
        {
           player.playerMovement.IncreaseJumpVelocity();

        }

    }

   










}
