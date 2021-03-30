using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{





    [SerializeField]
    Player player;
   // [SerializeField]
   // PlayerCollisionData collisionData;


      public Transform groundCheck;
      public Transform wallCheck;
      public Transform ledgeCheck;
      public Transform GroundLedgeCheck;
      public Transform FlyingKickCheck;


    [Header("Check Variables")]
    public float groundCheckRadius = 0.3f;
    public float wallCheckDistance = 0.5f;
    public LayerMask whatIsGround;














    public bool CheckIfGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
    }
    public bool CheckIfTouchingWall()
    {
        return Physics2D.Raycast(wallCheck.position, Vector2.right * player.playerMovement.FacingDirection, wallCheckDistance, whatIsGround);
    }
    public bool CheckIfTouchingWallBack()
    {
        return Physics2D.Raycast(wallCheck.position, Vector2.right * -player.playerMovement.FacingDirection, wallCheckDistance, whatIsGround);
    }
    public bool CheckIfGroundLedgeCheck()
    {
        return Physics2D.OverlapCircle(GroundLedgeCheck.position, groundCheckRadius, whatIsGround);
    }
    public bool CheckIfTouchingLedge()
    {
        return Physics2D.Raycast(ledgeCheck.position, Vector2.right * player.playerMovement.FacingDirection, wallCheckDistance, whatIsGround);
    }
    public bool CheckIfCannotFlyingKick()
    {
        return Physics2D.OverlapCircle(FlyingKickCheck.position, 2.5f, whatIsGround);
    }















}
