using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTouchingWallState : PlayerState
{

    protected bool isGrounded;
    protected bool isTouchingWall;
    protected bool jumpInput;
    protected bool isTouchingLedge;
    protected int xInput;
    protected int yInput;
    protected int facingDirection;



    public PlayerTouchingWallState(Player player, PlayerStateMachine stateMachine,  string animBoolName) : base(player, stateMachine, animBoolName)
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

        isGrounded = player.playerCollision.CheckIfGrounded();
        isTouchingWall = player.playerCollision.CheckIfTouchingWall();
        isTouchingLedge = player.playerCollision.CheckIfTouchingLedge();

        if(isTouchingWall && !isTouchingLedge)
        {
            player.playerMovement.SetDetectedPosition(player.transform.position);
        }
    }

    public override void Enter()
    {
         
        base.Enter();
        player.playerMovement.ResetFallingSpeed();

        player.GetComponent<Rigidbody2D>().sharedMaterial.friction = 0;

    }

    public override void Exit()
    {
        base.Exit();

    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        xInput = player.inputHandler.NormInputX;
        yInput = player.inputHandler.NormInputY;
        jumpInput = player.inputHandler.JumpInput;
        facingDirection = player.playerMovement.FacingDirection;


        if(jumpInput)
        {
            player.WallJumpState.DeterminWallJumpDIrection(isTouchingWall);
            stateMachine.ChangeState(player.WallJumpState);
        }
        else if(isGrounded)
        {
            stateMachine.ChangeState(player.IdleState);
        }
        else if (!isTouchingWall || (xInput != facingDirection))
        {
            stateMachine.ChangeState(player.InAirState);
        }
        else if(isTouchingWall && !isTouchingLedge)
        {
            stateMachine.ChangeState(player.LedgeClimbState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
