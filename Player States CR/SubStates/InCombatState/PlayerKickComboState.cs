using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKickComboState : PlayerCombatState
{

    //  protected AttackDetails attackDetails;


    private float timer = .8f;
    private float timerStartTime;

    public PlayerKickComboState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    
    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        player.RB.constraints = RigidbodyConstraints2D.FreezeAll;
        timerStartTime = Time.time;
        //player.CheckNoOfClicksToStart();
    }

    public override void Exit()
    {
        player.RB.constraints = RigidbodyConstraints2D.FreezeRotation;


        base.Exit();
    }

    public override void LogicUpdate()
    {

        base.LogicUpdate();

        if (Time.time >= timerStartTime + timer)
        {
            //  Debug.Log("Timer over");
            player.playerCombat.SetKickAttack();
            isAbilityDone = true;


        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
