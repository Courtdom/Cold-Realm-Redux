using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFlyingKickState : PlayerCombatState
{

    //  protected AttackDetails attackDetails;
    public Vector2 flyingKickAngle;

    private float timer = .5f;
    private float timerStartTime;

    public PlayerFlyingKickState(Player player, PlayerStateMachine stateMachine,  string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    
    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        
        player.playerFX.DashWindFX(-45f * player.playerMovement.FacingDirection);

        player.playerMovement.SetKickVelocity();
        Time.timeScale = 0.65f;

        timerStartTime = Time.time;
    }

    public override void Exit()
    {
        player.playerMovement.SetVelocityZero();
        player.playerCombat.SetFlyingKickAttack();

        Time.timeScale = 1f;
        base.Exit();
    }

    public override void LogicUpdate()
    {

        base.LogicUpdate();

        if (Time.time >= timerStartTime + timer /* || player.playerCollision.CheckIfGrounded()  */)
        {
          //  player.playerCombat.SetFlyingKickAttack();
           // player.playerMovement.SetVelocityZero();
            isAbilityDone = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
