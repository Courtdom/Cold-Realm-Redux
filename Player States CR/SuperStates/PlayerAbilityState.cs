using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityState : PlayerState
{

    protected bool isAbilityDone;

    private bool isGrounded;
    private bool hurt;
    private bool knockdown;
    private bool jumpInput;
    private bool isDead;


    public PlayerAbilityState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isGrounded = player.playerCollision.CheckIfGrounded();
        hurt = player.playerCombat.hurt;
        knockdown = player.playerCombat.knockdown;
        jumpInput = player.inputHandler.JumpInput;
        isDead = player.playerCombat.isDead;
    }

    public override void Enter()
    {
        base.Enter();

        isAbilityDone = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

       

        if(isDead)
        {
            stateMachine.ChangeState(player.DeadState);
        }
        else if (hurt /* && player.CanBeHurt() */)
        {
            stateMachine.ChangeState(player.HurtState);
        }
        else if(knockdown)
        {
            stateMachine.ChangeState(player.KnockDownState);
        }
        else if (jumpInput && player.JumpState.CanJump())
        {
            stateMachine.ChangeState(player.JumpState);
        }
        

        else if (isAbilityDone && !knockdown && !isDead)
        {
            

            if (isGrounded && player.playerMovement.CurrentVelocity.y < 0.01f && !knockdown && !hurt)
            {
                stateMachine.ChangeState(player.IdleState);
            }
            else 
            {
                stateMachine.ChangeState(player.InAirState);
            }
        }
        

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
      
    }
}
