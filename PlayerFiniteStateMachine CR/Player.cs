using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
//using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Cinemachine;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour
{

    public static Player instance;

    


    #region State Variables
    public PlayerStateMachine StateMachine { get; private set; }

    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerInAirState InAirState { get; private set; }
    public PlayerLandState LandState { get; private set; }
    public PlayerWallSlideState WallSlideState { get; private set; }
    public PlayerWallJumpState WallJumpState { get; private set; }
    public PlayerLedgeClimbState LedgeClimbState { get; private set; }
    public PlayerDashState DashState { get; private set; }
    public PlayerSlideState SlideState { get; private set; }
    public PlayerHurtState HurtState { get; private set; }
    public PlayerSwordComboState SwordComboState { get; private set; }
   
    public PlayerKnockDownState KnockDownState { get; private set; }
    public PlayerGetUpState GetUpState { get; private set; }
    public PlayerKickComboState KickComboState { get; private set; }
    public PlayerMagicStrongState MagicStrongState { get; private set; }
    public PlayerWeakMagicState WeakMagicState { get; private set; }
    public PlayerFullPowerMagicState FullPowerMagicState { get; private set; }
    public PlayerDeadState DeadState { get; private set; }
    public PlayerFlyingKickState FlyingKickState { get; private set; }

    

    #endregion

    #region Components
  
    public PlayerDataStorage dataStorage { get; private set; }
    public Animator Anim { get; private set; }
    public InputHandler inputHandler { get; private set; }
    protected AttackDetails attackDetails;
    protected CheckpointDetails checkpointDetails;
    public Rigidbody2D RB;
    public Transform DashDirectionIndicator { get; private set; }
    public BoxCollider2D MovementCollider { get; private set; }
    public HealthHeartsUI HeartsUI { get; private set; }


    #endregion

   

    #region Sub Scripts

    public PlayerCollision playerCollision;
    public PlayerMovement playerMovement;
    public PlayerFX playerFX;
    public PlayerCombat playerCombat;

    #endregion


    #region Other Variables

    //_____________ Bools

  
    public bool isDead { get; private set; }
  
    public bool inCombatState { get; private set; }

    public bool saveCP { get; private set; }
    public bool loadCP { get; private set; }
    public bool savePlayerPrefs { get; private set; }
    public bool loadPlayerPrefs { get; private set; }
    public bool hologram { get; private set; }

    public bool lvl1Started { get; private set; }
    public bool lvl2Started { get; private set; }
    public bool lvl3Started { get; private set; }
    public bool lvl4Started { get; private set; }




    //___________Floats
    public float ARawMovementInputX { get; private set; }
    public float SprintTargetTime { get; private set; }
    public float startTimeBtwAttack { get; private set; }

    public float attackTime { get; private set; }
 
    public float deadStartTimer { get; private set; }
    public float deadTime { get; private set; }

    public int experiencePoints;
    public int currentHealth;
   
    private TextMeshPro DamageAmount;

  //  private IEnumerator coroutine;


    #endregion




    #region Check Transforms

    
   


    [SerializeField]
    private GameObject damagePoints;
   
    
    public Transform currentCheckPoint;

    [SerializeField]
    private Material defaultMaterial;
    [SerializeField]
    private Material deadMaterial;

    [SerializeField]
    private GameObject deadHologram;

    [SerializeField]
    private GameObject LevelLoader;



    #endregion

    #region Unity Callback Functions
    private void Awake()
    {


        instance = this;

        StateMachine = new PlayerStateMachine();

        IdleState = new PlayerIdleState(this, StateMachine, "idle");
        MoveState = new PlayerMoveState(this, StateMachine, "move");
        JumpState = new PlayerJumpState(this, StateMachine, "inAir");
        InAirState = new PlayerInAirState(this, StateMachine, "inAir");
        LandState = new PlayerLandState(this, StateMachine, "land");
        WallSlideState = new PlayerWallSlideState(this, StateMachine, "wallSlide");
        WallJumpState = new PlayerWallJumpState(this, StateMachine, "inAir");
        LedgeClimbState = new PlayerLedgeClimbState(this, StateMachine, "ledgeClimbState");
        DashState = new PlayerDashState(this, StateMachine, "dash");
        SlideState = new PlayerSlideState(this, StateMachine, "slide");
        HurtState = new PlayerHurtState(this, StateMachine, "hurt");
        SwordComboState = new PlayerSwordComboState(this, StateMachine, "swordCombo");
        KnockDownState = new PlayerKnockDownState(this, StateMachine, "knockdown");
        GetUpState = new PlayerGetUpState(this, StateMachine, "getUp");
        KickComboState = new PlayerKickComboState(this, StateMachine, "kickCombo");
        MagicStrongState = new PlayerMagicStrongState(this, StateMachine, "strongCast");
        WeakMagicState = new PlayerWeakMagicState(this, StateMachine, "weakCast");
        FullPowerMagicState = new PlayerFullPowerMagicState(this, StateMachine, "fullPowerMagicCast");
        DeadState = new PlayerDeadState(this, StateMachine, "dead");
        FlyingKickState = new PlayerFlyingKickState(this, StateMachine,"flyingKick");



    }

  


    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);


        Anim = GetComponent<Animator>();
        inputHandler = GetComponent<InputHandler>();
        RB = GetComponent<Rigidbody2D>();

        // DashDirectionIndicator = transform.Find("DashDirectionIndicator");
        //MovementCollider = GetComponent<BoxCollider2D>();


        StateMachine.Initialize(IdleState);

        Debug.Log("Player Script Started");
    }

    private void Update()
    {
      
        StateMachine.CurrentState.LogicUpdate();


      //  playerMovement.SetCurrentVelocity();
    }


    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }

    #endregion



    private void OnEnable()
    {

        Debug.Log("Enabled");
        dataStorage = PlayerPersistance.LoadData();
     //  SelectStartingPoint();


    }
    private void OnDisable()
    {
        DontDestroy();
        Debug.Log("Disabled");
        PlayerPersistance.SaveData(this);
    }



    public void DontDestroy()
    {
        DontDestroyOnLoad(this.gameObject);
    }

   


    



    public void SelectStartingPoint()
    {
        lvl1Started = true;



        if (!lvl1Started && !lvl2Started && !lvl3Started && !lvl4Started)
        {
          //  transform.position = new Vector3(-13f, -0.85f, 25f);
            lvl1Started = true;
            Debug.Log("lvl1 position loaded");
        }  
        else if (lvl1Started && !lvl2Started && !lvl3Started && !lvl4Started)
        {
            transform.position = new Vector3(-17f, -113.3f, 25f);
           // transform.position = new Vector3(370f, 10f, 25f);
            Debug.Log("lvl2 position loaded");
            lvl2Started = true;

        }
        else if (lvl1Started && lvl2Started && !lvl3Started && !lvl4Started)
        {
            transform.position = new Vector3(-17f, -230f, 25f);
          //  transform.position = new Vector3(910f, 55.1f, 25f);
            Debug.Log("lvl3 position loaded");
            lvl3Started = true;
        }
        else if (lvl1Started && lvl2Started && lvl3Started && !lvl4Started)
        {

            transform.position = new Vector3(-17f, -460f, 25f);
            // transform.position = new Vector3(1752f, 1f, 25f);
            Debug.Log("lvl4 position loaded");
            lvl4Started = true;
        }
        else if (lvl1Started && lvl2Started && lvl3Started && lvl4Started)
        {
            transform.position = new Vector3(-17f, -598f, 25f);
            // transform.position = new Vector3(2635f, 52.2f, 25f);
            Debug.Log("lvl5 position loaded");
        }
    }









    #region Other Functions





    private void AnimationTrigger()
    {
        StateMachine.CurrentState.AnimationTrigger();
    }

    private void AnimationFinishTrigger()
    {
        StateMachine.CurrentState.AnimationFinishTrigger();
    }








    public void LoadData()
    {
        dataStorage = PlayerPersistance.LoadData();
        currentHealth = PlayerPrefs.GetInt("health");
        experiencePoints = dataStorage.XP;
    }

    public void SaveData()
    {
        PlayerPersistance.SaveData(this);
    }

    public void SetData()
    {
        PlayerPrefs.SetInt("health", 125);
        PlayerPrefs.SetInt("xp", 0);
    }
 



















   
    
    
    
 


   

   
   
  
    public void DeadHologram()
    {

        Debug.Log("Dead Hologram called");
        SetHologram();

        Instantiate(deadHologram, transform.position, Quaternion.identity);
        GameObject dh = Instantiate(deadHologram, transform.position, Quaternion.identity) as GameObject;
        DeadHologram deadHolo = dh.GetComponent<DeadHologram>();
        deadHolo.expPoints = experiencePoints;

       experiencePoints = 0;
    }


    public void SetHologram()
    {
        hologram = !hologram;
    }

    public void Death()
    {
        //change materials
       // savePP = true;




        //LevelLoader.SendMessage("LoadGrassLandLevel");
        loadCP = true;

        GetComponent<SpriteRenderer>().material = deadMaterial;

        
        //transform.position = currentCheckPoint.position;

      //  isDead = false;

      //  playerCombat.currentHealth = playerData.Playerhealth;


    }
    public void ResetLoadPP()
    {
        loadCP = false;
    }
   
    public void OutOfGhostMode()
    {
        gameObject.layer = 10; //player layer
        GetComponent<SpriteRenderer>().material = defaultMaterial;
    }
    public void GainEXP(int exp)
    {
        int temp;
        temp = exp;
        experiencePoints += temp;
        Debug.Log("EXP Gained = " + exp);
      //  Debug.Log("Overall EXP = " + experiencePoints);

    }
    public void DeadTimer()
    {
        deadStartTimer = Time.time;
        deadTime = 1.5f;
    }

    public void ResetMaterial()
    {
        GetComponent < SpriteRenderer>().material = defaultMaterial;
    }

   

   

   

  
  
  

    

    #endregion




    #region Combat Functions


   
  
  
 
   

   






  

    


   
    



  

    







    #endregion

    #region On Draw Gizmos


    public virtual void OnDrawGizmos()
    {


        // Gizmos.DrawWireSphere(swordAttackHitPos.transform.position, playerData.attack1Radius);
        // Gizmos.DrawWireSphere(punchAttackHitPos.transform.position, playerData.punchRadius);
        // Gizmos.DrawWireSphere(kickAttackHitPos.transform.position, playerData.kickRadius);
        // Gizmos.DrawWireSphere(magicStrongHitPos.transform.position, playerData.magicStrongRadius);
        //Gizmos.DrawWireSphere(weakMagicHitPos.transform.position, playerData.weakMagicRadius);
        //Gizmos.DrawWireSphere(fullPowerMagicHitPos.transform.position, playerData.fullPowerMagicRadius);

        //Gizmos.DrawLine(wallCheck.position, wallCheck.position + (Vector3)(Vector2.right * facingDirection * entityData.wallCheckDistance));
        // Gizmos.DrawLine(ledgeCheck.position, ledgeCheck.position + (Vector3)(Vector2.down * entityData.ledgeCheckDistance));

        //Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * facingDirection * entityData.closeRangeActionDistance), 0.2f);
        // Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * facingDirection * entityData.minAgroDistance), 0.2f);
        // Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * facingDirection * entityData.maxAgroDistance), 0.2f);

    }


    



  

    #endregion
}
