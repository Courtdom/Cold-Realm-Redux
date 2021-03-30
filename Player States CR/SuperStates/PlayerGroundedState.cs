using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{

    protected int xInput;
    protected float rawXInput;
    protected bool isTouchingCeiling;
    private bool jumpInput;
    private bool isGrounded;
    private bool isTouchingWall;
    private bool isTouchingLedge;
    private bool dashInput;
    private bool isSprinting;
    protected bool slideInput;
    private bool backFlipInput;
    private bool kickInput;
    private bool magicInput;
    private bool weakMagicInput;
    private bool fullPowerMagicInput;
    public bool swordInput;
    private bool hurt;
    private bool knockdown;
    private bool isDead;
    // private bool crouchInput;


    public PlayerGroundedState(Player player, PlayerStateMachine stateMachine,  string animBoolName) : base(player, stateMachine, animBoolName)
    {


    }

    public override void DoChecks()
    {
        base.DoChecks();

        isGrounded = player.playerCollision.CheckIfGrounded();
        isTouchingWall = player.playerCollision.CheckIfTouchingWall();
        isTouchingLedge = player.playerCollision.CheckIfTouchingLedge();


    }

    public override void Enter()
    {
        base.Enter();

        player.JumpState.ResetAmountOfJumpsLeft();
        player.DashState.ResetCanDash();
        player.SlideState.ResetCanSlide();
        player.GetComponent<Rigidbody2D>().sharedMaterial.friction = 0.75f;
        player.playerMovement.ResetFallingSpeed();




    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

      
       

        if(!knockdown)
        {
            xInput = player.inputHandler.NormInputX;
        }

        jumpInput = player.inputHandler.JumpInput;
        dashInput = player.inputHandler.DashInput;
        rawXInput = player.inputHandler.RawMovementInputX;
        slideInput = player.inputHandler.SlideInput;
        backFlipInput = player.inputHandler.BackFlipInput;
        kickInput = player.inputHandler.KickInput;
        magicInput = player.inputHandler.magicInput;
        weakMagicInput = player.inputHandler.weakMagicInput;
        fullPowerMagicInput = player.inputHandler.fullPowerMagicInput;
        swordInput = player.inputHandler.swordInput;
        hurt = player.playerCombat.hurt;
        knockdown = player.playerCombat.knockdown; ;
        isSprinting = player.playerMovement.isSprinting;
        isDead = player.playerCombat.isDead;


        if (isDead)
        {
            stateMachine.ChangeState(player.DeadState);
        }
         else if (hurt /*  &&  player.CanBeHurt()  */)
        {
            stateMachine.ChangeState(player.HurtState);
        }
        else if (knockdown)
        {
            stateMachine.ChangeState(player.KnockDownState);
        }
        else if (!knockdown && !isDead)
        {
            //crouchInput = player.InputHandler.CrouchInput;
            if (swordInput && player.playerCombat.swordAttack)
            {
                player.playerCombat.AddToNoClicks();
                stateMachine.ChangeState(player.SwordComboState);
            }

            else if (kickInput && player.playerCombat.kickAttack )
            {
                stateMachine.ChangeState(player.KickComboState);
            }
            else if(fullPowerMagicInput && player.playerCombat.fullPowerMagicAttack)
            {
                stateMachine.ChangeState(player.FullPowerMagicState);
            }
            else if ( magicInput && player.playerCombat.flameMagic )
            {
                stateMachine.ChangeState(player.MagicStrongState);
            }
            else if (weakMagicInput && player.playerCombat.weakMagicAttack )
            {
                stateMachine.ChangeState(player.WeakMagicState);
            }

            else if (jumpInput && player.JumpState.CanJump())
            {
                stateMachine.ChangeState(player.JumpState);
            }
          
            else if (!isGrounded)
            {

                player.InAirState.StartCoyoteTIme();
                stateMachine.ChangeState(player.InAirState);
            }
            
            else if (slideInput && !isTouchingWall && !isTouchingLedge && player.playerMovement.CheckCanSlide())
            {
                stateMachine.ChangeState(player.SlideState);
            }
            

        }
       

       
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
       
    }


}
