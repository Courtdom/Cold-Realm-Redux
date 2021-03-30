using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerAbilityState
{

    public bool CanDash { get; private set; }

    private bool isHolding;
    private bool dashInputStop;

    private float lastDashTime;

    private Vector2 dashDirection;
    private Vector2 dashDirectionInput;
    private Vector2 lastAfterImagePos;

    public PlayerDashState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }



    public override void Enter()
    {
        base.Enter();

        CanDash = false;
        player.inputHandler.UseDashInput();

        isHolding = true;
        dashDirection = Vector2.right * player.playerMovement.FacingDirection;

        Time.timeScale = player.playerMovement.movementData.holdTimeScale;
        startTime = Time.unscaledTime;

        player.playerMovement.DashDirectionIndicator.gameObject.SetActive(true);
    }

    public override void Exit()
    {

        if (player.playerMovement.CurrentVelocity.y > 0)
        {
            player.playerMovement.SetVelocityY(0);

            //player.CurrentVelocity.y * playerData.dashEndYMultiplier
        }
        //CinemachineShake.Instance.ShakeCamera(1.5f, .25f);
        base.Exit();

        

       
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!isExitingState)

            player.Anim.SetFloat("yVelocity", player.playerMovement.CurrentVelocity.y);
            player.Anim.SetFloat("xVelocity", Mathf.Abs(player.playerMovement.CurrentVelocity.x));
        {
            if(isHolding)
            {
                dashDirectionInput = player.inputHandler.DashDirectionInput;
                dashInputStop = player.inputHandler.DashInputStop;

                if(dashDirectionInput != Vector2.zero)
                {
                    dashDirection = dashDirectionInput;
                    dashDirection.Normalize();
                }

                float angle = Vector2.SignedAngle(Vector2.right, dashDirection);
                player.playerMovement.DashDirectionIndicator.rotation = Quaternion.Euler(0f, 0f, angle - 45f);

                if(dashInputStop || Time.unscaledTime >= startTime + player.playerMovement.movementData.maxHoldTime)
                {
                    isHolding = false;
                    Time.timeScale = 1f;
                    startTime = Time.time;
                    player.playerMovement.CheckIfShouldFlip(Mathf.RoundToInt(dashDirection.x));
                    player.RB.drag = player.playerMovement.movementData.dashDrag;
                    player.playerFX.DashWindFX(angle);
                    player.playerMovement.SetVelocity(player.playerMovement.movementData.dashVelocity, dashDirection);
                    player.playerMovement.DashDirectionIndicator.gameObject.SetActive(false);
                    player.playerFX.PlaceAfterImage();
                }
            }
            else
            {
                player.playerMovement.SetVelocity(player.playerMovement.movementData.dashVelocity, dashDirection);
                player.playerFX.CheckIfShouldPlaceAfterImage();

                if (Time.time >= startTime + player.playerMovement.movementData.dashTime)
                {
                    player.RB.drag = 0.05f; //declare drag as variable in future
                    isAbilityDone = true;
                    lastDashTime = Time.time;
                }
            }
        }
    }





    public bool CheckIfCanDash()
    {
        return CanDash && Time.time >= lastDashTime + player.playerMovement.movementData.dashCooldown;
    }

    public void ResetCanDash()
    {
        CanDash = true;
    }

   
}
