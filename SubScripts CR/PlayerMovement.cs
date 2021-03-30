using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField]
    Player player;

   public PlayerMovementData movementData;
    public Transform DashDirectionIndicator;



    protected float rawXInput;
    protected float rawYInput;

    private bool isGrounded;
    public bool hurt { get; private set; }
    public bool knockdown { get; private set; }
    public bool sprintingTimerReset { get; private set; }
    protected bool canMove { get; private set; }
    
    public Vector2 CurrentVelocity { get; private set; }

    public Vector2 workspace;

    //______Wall Grab & Ledge Climb
    public Vector2 holdPosition;
    private Vector2 detectedPos;
    private Vector2 cornerPos;
    private Vector2 startPos;
    private Vector2 stopPos;

    //_____Dash
    private Vector2 dashDirection;
    private Vector2 dashDirectionInput;
    private Vector2 lastAfterImagePos;

    public float sprintMultiplier { get; private set; }

    public float combatMovementSpeed { get; private set; }

    public float SprintTargetTime { get; private set; }

    public int FacingDirection { get; private set; }

   // public float sprintMultiplier { get; private set; }


    public bool isSprinting { get; private set; }


    #region Unity Callbacks

    void Start()
    {


        FacingDirection = 1;

        sprintMultiplier = 1f;
        combatMovementSpeed = 1f;
        canMove = true;

    }

    void Update()
    {
        isGrounded = player.playerCollision.CheckIfGrounded();
        rawXInput = player.inputHandler.RawMovementInputX;
        rawYInput = player.inputHandler.RawMovementInputY;
        hurt = player.playerCombat.hurt;
        knockdown = player.playerCombat.knockdown;



        player.Anim.SetFloat("yVelocity", player.playerMovement.CurrentVelocity.y);
        player.Anim.SetFloat("xVelocity", Mathf.Abs(player.playerMovement.CurrentVelocity.x));
        //   Debug.Log("Current X Velocity = " + CurrentVelocity.x);
        // Debug.Log("RawYInput = " + rawYInput);
        //  Debug.Log("isGrounded = " + isGrounded);

        //  player.Anim.SetFloat("yVelocity", player.playerMovement.CurrentVelocity.y);
        // player.Anim.SetFloat("xVelocity", Mathf.Abs(player.playerMovement.CurrentVelocity.x));


       CurrentVelocity = player.RB.velocity;




    }
    private void FixedUpdate()
    {
        

      /*  if (isGrounded)
        {
            SetVelocityX(movementData.movementVelocity * rawXInput * sprintMultiplier * combatMovementSpeed);
            
        }        */




    }

    #endregion




    #region Set Velocities

    public void SetCurrentVelocity()
    {
        CurrentVelocity = player.RB.velocity;
       // Debug.Log("current vlocity = " + CurrentVelocity);

    }

    public void SetVelocityZero()
    {
        player.RB.velocity = Vector2.zero;
        CurrentVelocity = Vector2.zero;
    }

    public void SetVelocityX(float velocity)
    {
        workspace.Set(velocity, CurrentVelocity.y);
        player.RB.velocity = workspace;
        CurrentVelocity = workspace;
    }

    public void SetVelocityY(float velocity)
    {
        workspace.Set(CurrentVelocity.x, velocity);
        player.RB.velocity = workspace;
        CurrentVelocity = workspace;
    }
    public void SetVelocity(float velocity, Vector2 angle, int direction)
    {
        angle.Normalize();
        workspace.Set(angle.x * velocity * direction, angle.y * velocity);
        player.RB.velocity = workspace;
        CurrentVelocity = workspace;
    }

    public void SetVelocity(float velocity, Vector2 direction)
    {
        workspace = direction * velocity;
        player.RB.velocity = workspace;
        CurrentVelocity = workspace;
    }
    
    
    public void SetCombatVelocityInCombat()
    {
        combatMovementSpeed = 0.5f;
    }
    public void SetCombatVelocityOutOfCombat()
    {
        combatMovementSpeed = 1f;
    }
    



    #endregion


    #region Checks

    public bool CheckIfCanMove()
    {
        return canMove;
    }
    public bool CheckIfCanDash()
    {
        return player.DashState.CheckIfCanDash();
    }


    #endregion


    #region Flipping

    public void CheckIfShouldFlip(int xInput)
    {
        if (xInput != 0 && xInput != FacingDirection)
        {
            Flip();
        }
    }

    private void Flip()
    {
        FacingDirection *= -1;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }


    #endregion

    #region Sprinting


    public void SetSprintingVelocity()
    {
        workspace.Set(movementData.movementVelocity * rawXInput * movementData.sprintMultiplier, CurrentVelocity.y);
        player.RB.velocity = workspace;
        CurrentVelocity = workspace;
    }
 



    public void SetIsSprinting()
    {
        if (isGrounded)
        {
            if (sprintingTimerReset)
            {
                SprintTargetTime = Time.time;
                SprintingTimerReset();
            }
            else if (player.inputHandler.AbsRawMovementInputX == 1)
            {
                if (Time.time >= SprintTargetTime + 1.75f)
                {
                    player.playerFX.PuffTimer();

                    movementData.sprintMultiplier = 2f;

                }
                else if (Time.time >= SprintTargetTime + 1f)
                {
                    player.playerFX.PuffTimer();
                    movementData.sprintMultiplier = 1.5f;

                    isSprinting = true;
                    player.Anim.SetBool("isSprinting", isSprinting);

                }
            }
            else
            {
                isSprinting = false;
                player.Anim.SetBool("isSprinting", isSprinting);
                movementData.sprintMultiplier = 1f;
                movementData.jumpMultiplier = 1f;
                SprintingTimerReset();
                SprintTargetTime = Time.time;

            }
        }

    }

    public void ResetSprinting()
    {
        SprintTargetTime = Time.time;
        movementData.sprintMultiplier = 1f;

    }
    public void ResetIsSprinting()
    {
        isSprinting = false;
    }
    public void SprintingTimerReset()
    {
        sprintingTimerReset = !sprintingTimerReset;
    }

    #endregion

    #region Sliding
    public bool CheckCanSlide()
    {
        return player.SlideState.CanSlide && Time.time >= player.SlideState.lastSlideTime + player.playerMovement.movementData.slideCooldown;
    }
    public void SlideForce(Vector2 slideDirection)
    {
        player.RB.AddForce(slideDirection * 750f);
    }
    #endregion

    #region Jumping

    public void SetJumpVelocity()
    {
        workspace.Set(CurrentVelocity.x , movementData.jumpVelocity * movementData.jumpMultiplier) ;
        player.RB.velocity = workspace;
        CurrentVelocity = workspace;
    }

    public void AddJumpForce()
    {
        player.RB.velocity = Vector2.up * movementData.jumpForce;
       
    }
    
    public void IncreaseJumpVelocity()
    {
        float temp;
        temp =  movementData.jumpVelocity +  0.1f;
        movementData.jumpVelocity = temp;
    }
    public void ResetJumpVelocity()
    {
        movementData.jumpVelocity = 10.5f;
    }
    public void SetFallingVelocity()
    {
        workspace.Set(movementData.fallingVelocity * rawXInput, movementData.fallingSpeed);
        player.RB.velocity = workspace;
        CurrentVelocity = workspace;
    }
    public void IncreaseFallingSpeed()
    {
        float temp;
        temp = movementData.fallingSpeed - .1f;
        movementData.fallingSpeed = temp;
    }
    public void ResetFallingSpeed()
    {
        movementData.fallingSpeed = -9.5f;
    }
   

    #endregion

    #region Ledge Climb

    public void StartLedgeClimb()
    {
        SetVelocityZero();
        player.transform.position = detectedPos;
        cornerPos = DetermineCornerPosition();

        startPos.Set(cornerPos.x - (FacingDirection * movementData.startOffset.x), cornerPos.y - movementData.startOffset.y);
        stopPos.Set(cornerPos.x + (FacingDirection * movementData.stopOffset.x), cornerPos.y + movementData.stopOffset.y);

        player.transform.position = startPos;

    }

    public void ExitLedgeClimb()
    {

        player.transform.position = stopPos;


    }

    public void SetDetectedPosition(Vector2 pos)
    {
        detectedPos = pos;
    }

    public void SetStartPos()
    {
        player.transform.position = startPos;

    }


    public Vector2 DetermineCornerPosition()
    {
        RaycastHit2D xHit = Physics2D.Raycast(player.playerCollision.wallCheck.position, Vector2.right * FacingDirection, player.playerCollision.wallCheckDistance, player.playerCollision.whatIsGround);
        float xDist = xHit.distance;
        workspace.Set(xDist * FacingDirection, 0f);
        RaycastHit2D yHit = Physics2D.Raycast(player.playerCollision.ledgeCheck.position + (Vector3)(workspace), Vector2.down, player.playerCollision.ledgeCheck.position.y - player.playerCollision.wallCheck.position.y, player.playerCollision.whatIsGround);
        float yDist = yHit.distance;

        workspace.Set(player.playerCollision.wallCheck.position.x + (xDist * FacingDirection), player.playerCollision.ledgeCheck.position.y - yDist);
        return workspace;
    }


    #endregion

    #region Wall Grab

    public void HoldPosition()
    {
        player.transform.position = holdPosition;

        SetVelocityX(0f);
        SetVelocityY(0f);
    }
    public void SetHoldPosition()
    {
        holdPosition = player.transform.position;
    }

    #endregion


    #region Wall Slide

    public void SetWallSlideVelocity()
    {
        workspace.Set(CurrentVelocity.x, -movementData.wallSlideVelocity);
        player.RB.velocity = workspace;
        CurrentVelocity = workspace;
    }

    #endregion


    #region Wall Climb


    public void SetWallClimbVelocity()
    {
        workspace.Set(CurrentVelocity.x, movementData.wallClimbVelocity * movementData.WallClimbSpeedMultiplier);
        player.RB.velocity = workspace;
        CurrentVelocity = workspace;
    }

    public void SetClimbingSpeedMultiplier()
    {
        if (rawYInput > .6f)
        {
            movementData.WallClimbSpeedMultiplier = 1.75f;
        }
        else
        {
            movementData.WallClimbSpeedMultiplier = 1f;

        }
    }




    #endregion


    #region Flying Kick 

    public void  SetKickVelocity()
    {
        workspace.Set( movementData.flyingKickVelocity * FacingDirection, -movementData.flyingKickVelocity);
        player.RB.velocity = workspace;
        CurrentVelocity = workspace;
    }




    #endregion

    #region Dash



    #endregion


    #region Damage Hop & Knocback

    public void DamageHop(float damageKnockbackSpeed)
    {

        workspace.Set(damageKnockbackSpeed * player.playerCombat.lastDamageDirection, player.RB.velocity.y);
        player.RB.velocity = workspace;
    }

    public void DamageHopKnockDown(float damageKnockbackSpeed)
    {
        workspace.Set(damageKnockbackSpeed * player.playerCombat.lastDamageDirection, damageKnockbackSpeed * 0.75f);
        player.RB.velocity = workspace;
    }


    #endregion



}
