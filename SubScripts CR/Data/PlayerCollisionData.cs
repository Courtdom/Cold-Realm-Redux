using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Player Data/Player Collision Data")]



public class PlayerCollisionData : ScriptableObject
{



    [Header("Check Variables")]
    public float groundCheckRadius = 0.3f;
    public float wallCheckDistance = 0.5f;
    public LayerMask whatIsGround;

















}
