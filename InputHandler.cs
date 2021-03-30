using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public static InputHandler instance;
    public GameObject actor;
    public Animator anim { get; private set; }

    private Camera cam;

    protected Player player;
   


    public float RawMovementInputX { get; private set; }
    public float AbsRawMovementInputX { get; private set; }
    public float AbsRawMovementInputY { get; private set; }
    public float RawMovementInputY { get; private set; }
    public Vector2 RawInputXY { get; private set; }
    public Vector2 NormInputXY { get; private set; }

    public Vector2 RawDashDirectionInput { get; private set; }
    public Vector2Int DashDirectionInput { get; private set; }
    public int NormInputX { get; private set; }
    public int NormInputY { get; private set; }
    public bool JumpInput { get; private set; }
    public bool JumpInputStop { get; private set; }
    public bool GetUpInput { get; private set; }
    public bool BackFlipInput { get; private set; }
    public bool BackFlipInputStop { get; private set; }
    public bool DashInput { get; private set; }
    public bool DashInputStop { get; private set; }
    public bool SlideInput { get; private set; }
    public bool SlideInputStop { get; private set; }
    public bool KickInput { get; private set; }
    public bool swordInput { get; private set; }
    public bool magicInput { get; private set; }
    public bool weakMagicInput { get; private set; }
    public bool fullPowerMagicInput { get; private set; }
    public bool flyingKickInput { get; private set; }
       //public bool CrouchInput { get; private set; }
    public float dashInputHoldTime = 0.4f;
    private float slideInputHoldTime = 0.75f;

    public float jumpInputStartTime { get; private set; }
    public float dashInputStartTime { get; private set; }
    public float slideInputStartTime { get; private set; }

    private bool isGrounded;
    private bool isTouchingWall;
    private bool isTouchingLedge;
    private bool isSprinting;
    private bool knockdown;
    private bool hurt;
    private bool canMove;
    private bool canFlyingKick;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        anim = GetComponent<Animator>();
        cam = Camera.main;
        player = GetComponent<Player>();
        DoChecks();

    }

    void Update()
    {
        DoChecks();
        HandleInput();
        //player = GetComponent<Player>();


    }


    public void DoChecks()
    {

        CheckRawMovementVector2(RawInputXY);
        isGrounded = player.playerCollision.CheckIfGrounded();
        isTouchingWall = player.playerCollision.CheckIfTouchingWall();
        isTouchingLedge = player.playerCollision.CheckIfTouchingLedge();
        isSprinting = player.playerMovement.isSprinting;
        knockdown = player.playerCombat.knockdown;
        hurt = player.playerCombat.hurt;
        canMove = player.playerMovement.CheckIfCanMove();
        canFlyingKick = !player.playerCollision.CheckIfCannotFlyingKick();


        anim.SetFloat("xRaw", AbsRawMovementInputX);


    }






    void HandleInput()
    {
        if (PauseMenu.GameIsPaused == false)
        {


            if (knockdown)
            {
                if (Input.GetButtonDown("Cross") || Input.GetButtonDown("Jump"))
                {
                    OnGetUpInput();
                }
               
            }
            else if (!knockdown)
            {
                onMoveInput();


               

                if (Input.GetButtonDown("Cross") || Input.GetButtonDown("Jump"))
                {
                    OnJumpInput();

                }

                else if (Input.GetButtonUp("Cross") || Input.GetButtonUp("Jump"))
                {
                    OnJumpInputReleased();
                }


                else if (Input.GetButtonDown("L3") && isGrounded )
                {
                    OnBackFlipInput();
                }
                else if (Input.GetButtonUp("L3")  && isGrounded)
                {
                    OnBackFlipInputReleased();
                }

               

                else if ((Input.GetButtonDown("Circle") || Input.GetButtonDown("Dash"))  && canMove)
                {
                    if (!isGrounded && !isTouchingWall && player.playerMovement.CheckIfCanDash())  // Dash
                    {
                        DashInput = true;
                        DashInputStop = false;
                        dashInputStartTime = Time.time;

                        if (Time.time >= dashInputStartTime + dashInputHoldTime)
                        {
                            DashInput = false;
                        }
                    }
                    if (isGrounded && isSprinting && player.playerMovement.CheckCanSlide())
                    {

                        SlideInput = true;
                        SlideInputStop = false;
                        slideInputStartTime = Time.time;

                        if (Time.time >= slideInputStartTime + slideInputHoldTime)
                        {
                            SlideInput = false;
                        }
                    }
                }


                



            }



            OnDashDirectionInput();




          


            if (Input.GetButtonDown("Triangle") && isGrounded && !knockdown && !isSprinting)
            {
                //Debug.Log("Punch inputted");
                KickComboInput();
                player.playerCombat.SetKickAttack();
            }
            if ( Input.GetMouseButtonDown(1) && isGrounded && !knockdown && !isSprinting)
            {
                //Debug.Log("Punch inputted");
                KickComboInput();
                player.playerCombat.SetKickAttack();
            }
            if (Input.GetButtonDown("Triangle")  && canFlyingKick && !isGrounded && !isTouchingWall && !isTouchingLedge && !knockdown)
            {
                Debug.Log("Flying Kick Input");
                FlyingKickInput();
                player.playerCombat.SetFlyingKickAttack();
            }
            if (Input.GetMouseButtonDown(1) && canFlyingKick && !isGrounded && !isTouchingWall && !isTouchingLedge && !knockdown)
            {
                FlyingKickInput();
                player.playerCombat.SetFlyingKickAttack();
            }
            if (Input.GetButtonUp("Triangle") || Input.GetMouseButtonUp(1))
            {
                FlyingKickInputReleased();
            }
            
       
            if (Input.GetButtonDown("TouchPad") && isGrounded && !knockdown && !isSprinting && player.playerCombat.comboCount > 6)
            {
                FullPowerMagicCastInput();
                player.playerCombat.SetFullPowerMagicAttack();
                player.playerCombat.SetComboCountToZero();
                Debug.Log("fullpower magic");
            }
            if ( Input.GetMouseButtonDown(2) && isGrounded && !knockdown && !isSprinting && player.playerCombat.comboCount > 6)
            {
                FullPowerMagicCastInput();
                player.playerCombat.SetFullPowerMagicAttack();
                player.playerCombat.SetComboCountToZero();
                Debug.Log("fullpower magic");
            }
            else if (Input.GetButtonDown("TouchPad") && isGrounded && !knockdown && !isSprinting && player.playerCombat.comboCount > 4)
            {
                MagicCastInput();
                player.playerCombat.SetMagicAttack();
                player.playerCombat.SetComboCountToZero();
            }
            else if ( Input.GetMouseButtonDown(2) && isGrounded && !knockdown && !isSprinting && player.playerCombat.comboCount > 4)
            {
                MagicCastInput();
                player.playerCombat.SetMagicAttack();
                player.playerCombat.SetComboCountToZero();
            }
            else if (Input.GetButtonDown("TouchPad")  && isGrounded && !knockdown && !isSprinting &&  player.playerCombat.comboCount > 2)
            {
                WeakMagicCastInput();
                player.playerCombat.SetWeakMagicAttack();
                player.playerCombat.SubtractFromComboCount();
            }
            else if (Input.GetMouseButtonDown(2) && isGrounded && !knockdown && !isSprinting && player.playerCombat.comboCount > 2)
            {
                WeakMagicCastInput();
                player.playerCombat.SetWeakMagicAttack();
                player.playerCombat.SubtractFromComboCount();
            }







            else if (Input.GetButtonDown("Square") || Input.GetMouseButtonDown(0) && !knockdown)
            {
                SwordAttackInput();
                player.playerCombat.SwordAttack();
            }


           
            
            else if (Input.GetButtonUp("Circle") || Input.GetButtonUp("Dash"))
            {
                if (isGrounded)
                {
                    SlideInputStop = true;
                }
                else
                {
                    DashInputStop = true;
                }

            }


            onMoveInput();

        }




    }









    public void onMoveInput()
    {

        RawMovementInputX = Input.GetAxisRaw("MoveHorizontal") + Input.GetAxisRaw("Horizontal");
        AbsRawMovementInputX = (Mathf.Abs(RawMovementInputX));
        RawMovementInputY = Input.GetAxisRaw("MoveVertical") + Input.GetAxisRaw("Vertical");
        AbsRawMovementInputY = (Mathf.Abs(RawMovementInputY));

        anim.SetFloat("xRaw", AbsRawMovementInputX);
        anim.SetFloat("yRaw", AbsRawMovementInputY);

        if (AbsRawMovementInputX > 0.1f)
        {
            NormInputX = (int)(RawMovementInputX * Vector2.right).normalized.x;
        }
        else
        {
            NormInputX = 0;
        }

        if (AbsRawMovementInputY > 0.2f)
        {
            NormInputY = (int)(RawMovementInputY * Vector2.down).normalized.y;
        }
        else
        {
            NormInputY = 0;
        }
    }

    public void OnJumpInput()
    {
       
            JumpInput = true;
            JumpInputStop = false;
            jumpInputStartTime = Time.time;
        

       
    }
   
    

    public void OnJumpInputReleased()
    {
        JumpInputStop = true;
    }
    
    public void OnGetUpInput()
    {
        GetUpInput = true;
    }
    public void OnGetUpInputReleased()
    {
        GetUpInput = false;
    }

    public void OnBackFlipInput()
    {

        BackFlipInput = true;
        BackFlipInputStop = false;
        jumpInputStartTime = Time.time;



    }


    public void OnBackFlipInputReleased()
    {
        JumpInputStop = true;
    }

    public void SwordAttackInput()
    {
        swordInput = true;
    }

    public void SwordAttackInputReleased()
    {
        swordInput = false;
    }

    
    public void KickComboInput()
    {
        KickInput = true;
    }
    public void KickInputReleased()
    {
        KickInput = false;
    }
    public void FlyingKickInput()
    {
        flyingKickInput = true;
    }
    public void FlyingKickInputReleased()
    {
        flyingKickInput = false;

    }
    public void MagicCastInput()
    {
        magicInput = true;
    }
    public void MagicCastReleased()
    {
        magicInput = false;
    }
    public void WeakMagicCastInput()
    {
        weakMagicInput = true;
    }
    public void WeakMagicCastInputReleased()
    {
        weakMagicInput = false;
    }
    public void FullPowerMagicCastInput()
    {
        fullPowerMagicInput = true;

    }
    public void FullPowerMagicCastInputReleased()
    {
        fullPowerMagicInput = false;

    }
    

   /*     public void OnGrabInput()
    {
        if (GrabInput)
        {
            GrabInput = true;
        }

        if (GrabInput)
        {
            GrabInput = false;
        }
    }      */

    public void OnDashInput()
    {
        if (DashInput)
        {
            DashInput = true;
            DashInputStop = false;
            dashInputStartTime = Time.time;
        }
        else if (DashInput)
        {
            DashInputStop = true;
        }
    }

    public void OnDashDirectionInput()
    {
      

        RawDashDirectionInput = new Vector2(NormInputX, NormInputY);

        DashDirectionInput = Vector2Int.RoundToInt(RawDashDirectionInput.normalized);

    }

   public void UseJumpInput()
    {
        JumpInput = false;
    } 
    public void UseBackFlipInput()
    {
        BackFlipInput = false;
    }

    public void UseDashInput()
    {
        DashInput = false;
    }
    public void UseSlideInput()
    {
        SlideInput = false;
    }

    public void CheckJumpInputHoldTime()
    {
        if (Time.time >= jumpInputStartTime + dashInputHoldTime)
        {
            JumpInput = false;
        }
    }

    public void CheckDashInputHoldTime()
    {
        if (Time.time >= dashInputStartTime + dashInputHoldTime)
        {
            DashInput = false;
        }
    }


    public Vector2 CheckRawMovementVector2(Vector2 RawInputXY)
    {
         Vector2 rawInputXY = new Vector2(RawMovementInputX, RawMovementInputY);
        RawInputXY = rawInputXY;

       // Debug.Log(AbsRawMovementInputX);

        return RawInputXY;
       
    }

  


   

   


}
