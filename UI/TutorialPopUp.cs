using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPopUp : MonoBehaviour
{

    [SerializeField]
    private GameObject UITut;

    
    private float startTime;
    private bool timerStarted = false;

    private void Start()
    {
        
    }




    private void OnTriggerEnter2D(Collider2D collision)
    {
        UITut.SetActive(true); 
        startTime = Time.time;
        timerStarted = true;
    }



    private void Update()
    {
        if (Time.time > startTime + 2.5f && timerStarted)
        {
            Destroy(UITut);
            Destroy(gameObject);
        }
    }
    

   



}
