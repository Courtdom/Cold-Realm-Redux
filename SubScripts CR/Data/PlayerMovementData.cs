using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Player Data/Player Movement Data")]


public class PlayerMovementData : ScriptableObject
{


    [Header("Ledge Climb State")]
    public Vector2 startOffset;
    public Vector2 stopOffset;

    [Header("Move State")]
    public float movementVelocity = 10f;
    public float sprintMultiplier = 1f;

    [Header("Jump State")]
    public float jumpVelocity = 10.5f;
    public int amountOfJumps = 2;
    public float jumpMultiplier = 1f;
    public float jumpForce = 20f;
    public float jumpHoldTime = 1.25f;
    public float fallingVelocity = 8f;
    public float fallingSpeed = -9.5f;
    public float jumpCooldown = 1.5f;

    [Header("InAir State")]
    public float coyoteTime = 0.2f;
    public float variableJumpHeightMultiplier = 0.5f;
    public float fallingForce = 1f;
    public float flyingKickVelocity = 30f;

    [Header("Wall Slide State")]
    public float wallSlideVelocity = 3f;

    [Header("Wall Climb State")]
    public float wallClimbVelocity = 3f;
    public float WallClimbSpeedMultiplier = 1f;

    [Header("Wall Jump State")]
    public float wallJumpVelocity = 20f;
    public float wallJumpTime = 0.4f;
    public Vector2 wallJumpAngle = new Vector2(1, 2);


    [Header("Dash State")]
    public float dashCooldown = 2f;
    public float maxHoldTime = 1f;
    public float holdTimeScale = 0.35f;
    public float dashTime = 5f;
    public float dashVelocity = 15f;
    public float dashDrag = 20f;
    public float dashEndYMultiplier = -0.2f;

    [Header("Slide State")]
    public float slideCooldown = 1f;
    public float slideMaxHoldTime = 1f;
    // public float slideHoldTimeScale = 1f;
    // public float slideTime = .25f;
    public float slideVelocity = 1.5f;

    [Header("BackFlip")]
    public float backFlipVelocity = 12.5f;

    [Header("Physics")]
    public float maxSpeed = 7f;
    public float linearDrag = 4f;
    public float gravity = 1f;
    public float fallMultiplier = 5f;
}
