using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatState : PlayerState
{

    protected bool isAbilityDone;
    
    private bool isGrounded;
    private bool kickInput;
    private bool flameInput;
    private bool weakMagicInput;

    public bool swordInput;

    public PlayerCombatState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isGrounded = player.playerCollision.CheckIfGrounded();
    }

    public override void Enter()
    {
        base.Enter();
        player.playerMovement.SetCombatVelocityInCombat();
        isAbilityDone = false;
        player.playerCombat.SetAttackFinished();
        //Debug.Log("Enter combat state");
    }

    public override void Exit()
    {

        player.playerMovement.SetCombatVelocityOutOfCombat();
        //  Debug.Log("exit combat state");

        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        

        swordInput = player.inputHandler.swordInput;

        if (swordInput && player.playerCombat.swordAttack)
        {
            player.playerCombat.AddToNoClicks();
            stateMachine.ChangeState(player.SwordComboState);
        }
       

        if (isAbilityDone)
        {

            if (isGrounded)
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
