using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerSlideState : PlayerAbilityState
{


    public bool CanSlide { get; private set; }


    private bool isHolding;
    private bool slideInputStop;
    private bool slideStart;
    public float lastSlideTime { get; private set; }
    public float slideStartTime { get; private set; }

    private Vector2 SlideDirection;

    public float slideTime = 0.25f;


    public PlayerSlideState(Player player, PlayerStateMachine stateMachine,  string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }
    public override void Enter()
    {
        base.Enter();

        CanSlide = false;
       // player.inputHandler.UseSlideInput();
        slideStart = false;
        isHolding = true;

        //CinemachineShake.Instance.ShakeCamera(.25f, 1f);
        player.GetComponent<Rigidbody2D>().sharedMaterial.friction = 0f;

        // Time.timeScale = playerData.slideHoldTimeScale;
        slideStartTime = Time.time;

        SlideDirection = player.playerMovement.FacingDirection * Vector2.right;
        SlideDirection.Normalize();
        player.GetComponent<CapsuleCollider2D>().enabled = false;
        player.GetComponent<CircleCollider2D>().enabled = true;

    }
    public override void Exit()
    {
        if (player.playerMovement.CurrentVelocity.x > 0)
        {

            player.playerMovement.SetVelocityX(0);
            // player.SetVelocityY(player.CurrentVelocity.y * 0.2f);

            //player.CurrentVelocity.y * playerData.dashEndYMultiplier
        }

        slideStart = false;
        player.GetComponent<CircleCollider2D>().enabled = false;
        player.GetComponent<CapsuleCollider2D>().enabled = true;

        player.inputHandler.UseSlideInput();
        base.Exit();
    }
    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public override void DoChecks()
    {
        base.DoChecks();
        CheckIfCanSlide();
    }

   
    

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        CheckIfCanSlide();

        player.Anim.SetFloat("yVelocity", player.playerMovement.CurrentVelocity.y);
        player.Anim.SetFloat("xVelocity", Mathf.Abs(player.playerMovement.CurrentVelocity.x));

        slideInputStop = player.inputHandler.SlideInputStop;
        // SlideDirection.Normalize();
      //  Debug.Log("Velocty = " + player.CurrentVelocity);


        if (!isExitingState)
        {
            if (!slideStart)
            {
                player.playerMovement.SetVelocity(player.playerMovement.movementData.slideVelocity, SlideDirection);
                slideStart = true;
                startTime = Time.time;
            }

            if (isHolding)
            {





                player.playerMovement.SetVelocity(player.playerMovement.movementData.slideVelocity, SlideDirection);


                if (slideInputStop || Time.time >= startTime + player.playerMovement.movementData.slideMaxHoldTime || !player.playerCollision.CheckIfGroundLedgeCheck())
                {
                    isHolding = false;
                    // Time.timeScale = 1f;
                    // startTime = Time.time;
                    // player.CheckIfShouldFlip(Mathf.RoundToInt(dashDirection.x));
                    // player.RB.drag = playerData.drag;
                    player.playerMovement.SetVelocity(player.playerMovement.movementData.slideVelocity, SlideDirection);


                }
            }
            else
            {

                player.playerMovement.SetVelocity(player.playerMovement.movementData.slideVelocity, SlideDirection);

                 player.playerMovement.SlideForce(SlideDirection);


                if (Time.time >= slideStartTime + slideTime) //dash is over might need to change this for animation trigger
                {
                   // player.RB.drag = 0.02f; //declare drag as variable in future
                    isAbilityDone = true;
                    lastSlideTime = Time.time;
                    slideStart = false;
                }
            }
        }




    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }






    public bool CheckIfCanSlide()
    {
        return CanSlide && Time.time >= lastSlideTime + player.playerMovement.movementData.slideCooldown;
    }

    public void ResetCanSlide()
    {
        CanSlide = true;
    }






}
