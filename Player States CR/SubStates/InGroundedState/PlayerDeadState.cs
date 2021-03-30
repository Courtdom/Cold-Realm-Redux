using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeadState : PlayerState
{
    private float deadStartTimer;
    private float deadTime = 1.5f;


    public PlayerDeadState(Player player, PlayerStateMachine stateMachine,  string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        deadStartTimer = Time.time;
        player.RB.constraints = RigidbodyConstraints2D.FreezeAll;
        player.gameObject.layer = 20;

        base.Enter();
    }

    public override void Exit()
    {

        player.SetHologram();

        base.Exit();
    }

    public override void LogicUpdate()
    {
        if(Time.time >= deadStartTimer + deadTime )
        {


            if(player.hologram)
            {
                player.DeadHologram();
            }
            LevelManager.instance.RespawnPlayer();
        }
        

        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
