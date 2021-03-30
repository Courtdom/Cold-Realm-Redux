using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInAirState : PlayerState
{

    //Input
    private int xInput;
    private bool jumpInput;
    private bool dashInput;

    //Checks
    private bool isGrounded;
    private bool isTouchingWall;
    private bool oldIsTouchingWall;
    private bool oldIsTouchingWallBack;
    private bool isJumping;
    private bool isTouchingWallBack;
    private bool isTouchingLedge;
    private bool knockdown;
    private bool hurt;
    private bool flyingKickInput;

    private float startWallJumpCoyoteTime;
    private bool jumpInputStop;
    private bool coyoteTime;
    private bool wallJumpCoyoteTime;
   


    public PlayerInAirState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();

        oldIsTouchingWall = isTouchingWall;
        oldIsTouchingWallBack = isTouchingWallBack;

        isGrounded = player.playerCollision.CheckIfGrounded();
        isTouchingWall = player.playerCollision.CheckIfTouchingWall();
        isTouchingWallBack = player.playerCollision.CheckIfTouchingWallBack();
        isTouchingLedge = player.playerCollision.CheckIfTouchingLedge();

        if(isTouchingWall && !isTouchingLedge)
        {
            player.playerMovement.SetDetectedPosition(player.transform.position);
        }

        if(!wallJumpCoyoteTime && !isTouchingWall && !isTouchingWallBack &&(oldIsTouchingWall || oldIsTouchingWallBack))
        {
            StartWallJumpCoyoteTime();
        }
    }

    public override void Enter()
    {
        base.Enter();
        player.GetComponent<Rigidbody2D>().sharedMaterial.friction = 0;
    }

    public override void Exit()
    {
        base.Exit();
       // coyoteTime = false;
        oldIsTouchingWall = false;
        oldIsTouchingWallBack = false;
        isTouchingWall = false;
        isTouchingWallBack = false;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        CheckCoyoteTime();
        CheckWallJumpCoyoteTime();

        xInput = player.inputHandler.NormInputX;
        jumpInput = player.inputHandler.JumpInput;
        jumpInputStop = player.inputHandler.JumpInputStop;
        dashInput = player.inputHandler.DashInput;
        knockdown = player.playerCombat.knockdown;
        hurt = player.playerCombat.hurt;
        flyingKickInput = player.inputHandler.flyingKickInput;
        CheckJumpMultiplier();



              if (knockdown)
             {
            stateMachine.ChangeState(player.KnockDownState);
             }
           else if (isGrounded && player.playerMovement.CurrentVelocity.y < 0.01f)
            {
                stateMachine.ChangeState(player.LandState);
            }
            else if (isTouchingWall && !isTouchingLedge && !isGrounded)
            {
                stateMachine.ChangeState(player.LedgeClimbState);
            }
            else if (jumpInput && (isTouchingWall || isTouchingWallBack || wallJumpCoyoteTime))
            {
                StopWallJumpCoyoteTime();
                isTouchingWall = player.playerCollision.CheckIfTouchingWall();
                player.WallJumpState.DeterminWallJumpDIrection(isTouchingWall);
                stateMachine.ChangeState(player.WallJumpState);
            }
            else if (jumpInput && player.JumpState.CanJump())
            {
                stateMachine.ChangeState(player.JumpState);
            }
         
            else if (isTouchingWall && xInput == player.playerMovement.FacingDirection && player.playerMovement.CurrentVelocity.y <= 0)
            {
                stateMachine.ChangeState(player.WallSlideState);
            }
            else if (dashInput && player.DashState.CheckIfCanDash())
            {
                stateMachine.ChangeState(player.DashState);
            }
            else if (hurt   &&  player.playerCombat.CanBeHurt())
            {
                stateMachine.ChangeState(player.HurtState);
            }
              else if (flyingKickInput && player.playerCombat.flyingKickAttack)
            {
                stateMachine.ChangeState(player.FlyingKickState);
            }
             

        
        
        else
        {
            player.playerMovement.CheckIfShouldFlip(xInput);
            player.playerMovement.SetFallingVelocity();
            player.playerMovement.IncreaseFallingSpeed();



            player.Anim.SetFloat("yVelocity", player.playerMovement.CurrentVelocity.y);
            player.Anim.SetFloat("xVelocity", Mathf.Abs(player.playerMovement.CurrentVelocity.x));


        }
    }


    private void CheckJumpMultiplier()
    {
        if (isJumping)
        {
            if (jumpInputStop)
            {
                player.playerMovement.SetVelocityY(player.playerMovement.CurrentVelocity.y * player.playerMovement.movementData.variableJumpHeightMultiplier);
                isJumping = false;
            }
            else if (player.playerMovement.CurrentVelocity.y <= 0f)
            {
                isJumping = false;
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }


    

    private void CheckCoyoteTime()
    {
        if(coyoteTime && Time.time > startTime + player.playerMovement.movementData.coyoteTime)
        {
            coyoteTime = false;
            player.JumpState.DecreaseAmountOfJumpsLeft();
        }
    }
    private void CheckWallJumpCoyoteTime()
    {
        if(wallJumpCoyoteTime && Time.time > startWallJumpCoyoteTime + player.playerMovement.movementData.coyoteTime)
        {
            wallJumpCoyoteTime = false;
        }

    }

    public void StartCoyoteTIme()
    {
        coyoteTime = true;
    }

    public void StartWallJumpCoyoteTime()
    {
        wallJumpCoyoteTime = true;
        startWallJumpCoyoteTime = Time.time;
    }
    public void StopWallJumpCoyoteTime()
    {
        wallJumpCoyoteTime = false;
    }

    public void SetIsJumping()
    {
        isJumping = true;
    }
}
