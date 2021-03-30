using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHurtState : PlayerAbilityState
{


    public bool hurt { get; private set; }
   
    public float lastHurtTime { get; private set; }





    public PlayerHurtState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
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

        hurt = true;

        startTime = Time.time;

       // player.DamageHop(playerData.damageHopSpeed);
      
    }

    public override void Exit()
    {
        player.playerCombat.ResetHurt();
        player.gameObject.layer = 10;

        base.Exit();
       // Debug.Log(player.CanBeHurt());
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Time.time >= startTime + player.playerCombat.combatData.hurtTime) 
        {
            
            isAbilityDone = true;
            
            lastHurtTime = Time.time;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }







   public bool CheckIfCanBeHurt()
    {
        return Time.time >= lastHurtTime + player.playerCombat.combatData.hurtCooldown;
    }

    
}
