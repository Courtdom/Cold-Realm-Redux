using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Cinemachine;

public class LevelLoader : MonoBehaviour
{

    //public static LevelLoader Instance { get; private set; }
    [SerializeField]
    private Player player;
   // [SerializeField]
  //  private CinemachineVirtualCamera vCam;
    private Animator Anim;
    private float timer;
    private bool timerSet;
    private Transform playerTransform;
    [SerializeField]
    private Canvas mainCanvas;
                        

    private void Start()
    {
        //  DontDestroyOnLoad(gameObject);
        //  Instance = this;

        player = FindObjectOfType<Player>();
      //  vCam = FindObjectOfType<CinemachineVirtualCamera>();
        Anim = GetComponent<Animator>();

        

        


    }


    public void SetTriggerForCastle()
    {
        Debug.Log("Anim Trigger Set");
        Anim.SetTrigger("Start");
        LoadCastleLevel();


    }


    void Update()
    {
      
        
    }


    public void LoadCastleLevel()
    {
        SceneManager.LoadScene("Castle");



        player = FindObjectOfType<Player>();

       // vCam = FindObjectOfType<CinemachineVirtualCamera>();

        playerTransform = player.transform;


       // vCam.LookAt = playerTransform;
       // vCam.Follow = playerTransform;
    }
    public void LoadGrassLandLevel()
    {
        Anim.SetTrigger("Start");
        SceneManager.LoadScene("GrassLand");
    }

    public void ResetTimerSet()
    {
        timerSet = !timerSet;
    }

}
