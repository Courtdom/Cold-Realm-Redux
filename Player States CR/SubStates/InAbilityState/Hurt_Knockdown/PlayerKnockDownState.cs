using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKnockDownState : PlayerGroundedState
{


    //public bool hurt { get; private set; }
   
    public float lastHurtTime { get; private set; }

    private bool getUpInput;
    public float knockDownStartTime { get; private set; }

    private bool isGrounded;


    public PlayerKnockDownState(Player player, PlayerStateMachine stateMachine,  string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();

        player.gameObject.layer = 20;

        // Debug.Log("knockdown = " + player.knockdown);
        SetKnockDownStartTime();
        player.playerCombat.SetKnockdown();




    }

    public override void Exit()
    {
        player.playerCombat.ResetKnockdown();

        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        getUpInput = player.inputHandler.GetUpInput;
        player.playerMovement.SetFallingVelocity();
        player.playerMovement.IncreaseFallingSpeed();
        isGrounded = player.playerCollision.CheckIfGrounded();

        if(isGrounded)
        {
            player.playerMovement.SetVelocityZero();
        }

        if (getUpInput && isGrounded) 
        {
            player.inputHandler.OnGetUpInputReleased();
            stateMachine.ChangeState(player.GetUpState);
        }   
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }







  


    public void SetKnockDownStartTime()
    {
        knockDownStartTime = Time.time;
    }
    public bool CheckKnockBackTimeInput()
    {
        return Time.time > knockDownStartTime + player.playerCombat.combatData.knockbackTime;
    }


}
