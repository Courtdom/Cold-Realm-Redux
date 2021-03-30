using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFX : MonoBehaviour
{ 

    [SerializeField]
    Player player;







    //_________FX Transforms____________
   
    [SerializeField]
    private Transform landDustFPos;
    [SerializeField]
    private Transform puffPos;
    [SerializeField]
    private Transform weakMagicFXPos;
    [SerializeField]
    private Transform fullPowerMagicFXPos;
    [SerializeField]
    private Transform healFXPos;
    [SerializeField]
    private Transform FlameFXPos;
    //__________FX GameObjects_________________

    [SerializeField]
    private GameObject dashWind;
    [SerializeField]
    private GameObject hitParticle;
    [SerializeField]
    private GameObject landDust;
    [SerializeField]
    private GameObject puffFX;
    [SerializeField]
    private GameObject flameMagicFX;
    [SerializeField]
    private GameObject healFX;
    [SerializeField]
    private GameObject weakMagicFX;
    [SerializeField]
    private GameObject fullPowerMagicFX;
  

    //_________Floats_________________
    private float puffStartTime;


    //___________Bools__________________
    private bool canPuff;


    public Vector2 lastAfterImagePos;
    public float distanceBetweenAfterImages = 0.5f;

    //____________Put in FX Data Later_________________
    public float puffCoolDown = .1f;





    void Start()
    {
        canPuff = true;
        puffStartTime = Time.time;
    }

    void Update()
    {
        
    }



    






    #region  After Pool
    public void PlaceAfterImage()
    {
        PlayerAfterImagePool.Instance.GetFromPool();
        lastAfterImagePos = player.transform.position;
    }

    public void CheckIfShouldPlaceAfterImage()
    {
        if (Vector2.Distance(player.transform.position, lastAfterImagePos) >= distanceBetweenAfterImages)
        {
            PlaceAfterImage();
        }
    }

    #endregion


    #region Instantiate FX
    public void MagicStrongCastFX()
    {
        Instantiate(flameMagicFX, FlameFXPos.transform.position, Quaternion.Euler(0f, 0f, player.playerMovement.FacingDirection * 270f));
    }
    public void WeakMagicCastFX()
    {
        Instantiate(weakMagicFX, weakMagicFXPos.transform.position, Quaternion.Euler(0f, 0f, 0f));
    }
    public void FullPowerMagicFX()
    {
        Instantiate(fullPowerMagicFX, fullPowerMagicFXPos.transform.position, Quaternion.Euler(0f, 0f, 0f));
    }
    public void LandingDustFX()
    {
        Instantiate(landDust, landDustFPos.transform.position, Quaternion.Euler(0f, 0f, 0f));

    }
    public void DamageHitParticleFX()
    {
        Instantiate(hitParticle, player.transform.position, Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)));
    }
    public void HealFX()
    {
        Instantiate(healFX, healFXPos.transform.position, Quaternion.Euler(0f, 0f, 0f)); // do from player FX

    }


    public void DashWindFX(float angle)
    {
        Instantiate(dashWind, transform.position, Quaternion.Euler(0f, 0f, angle));

    }

   



    #endregion

    #region Puff

    public void PuffFX()
    {
        Instantiate(puffFX, puffPos.transform.position, Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)));
    }
    public void StartPuffTimer()
    {
        puffStartTime = Time.time;
    }
    public bool puffReadyTImer()
    {
        return Time.time >= puffStartTime + puffCoolDown;
    }

    public void PuffTimer()
    {
        if (puffReadyTImer())
        {
            canPuff = true;
            if (canPuff && puffReadyTImer())
            {
                PuffFX();
                canPuff = false;
                puffStartTime = Time.time;

            }
        }
    }


    #endregion



}
