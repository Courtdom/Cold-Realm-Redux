using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeakMagicState : PlayerCombatState
{

    //  protected AttackDetails attackDetails;

    private float timer = 1f;
    private float timerStartTime;

    public PlayerWeakMagicState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
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
       
        if(Time.time >= timerStartTime + timer)
        {
          //  Debug.Log("Timer over");
            player.playerCombat.SetWeakMagicAttack();
            isAbilityDone = true;

        }
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
