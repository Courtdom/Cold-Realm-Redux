using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectionLoader : MonoBehaviour
{


    [SerializeField]
    private GameObject GO1;

    [SerializeField]
    private GameObject GO2;
    [SerializeField]
    private GameObject GO3;
    [SerializeField]
    private GameObject GO4;
    [SerializeField]
    private GameObject GO5;
    [SerializeField]
    private GameObject GO6;
    [SerializeField]
    private GameObject GO7;
    [SerializeField]
    private GameObject GO8;
    [SerializeField]
    private GameObject GO9;
    [SerializeField]
    private GameObject G10;
    [SerializeField]
    private GameObject G11;





    void Start()
    {
        
    }

   



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            LoadAllObjects();
        }
    }




    private void LoadAllObjects()
    {
        GO1.SetActive(true);
        GO2.SetActive(true);
        GO3.SetActive(true);
        GO4.SetActive(true);
        GO5.SetActive(true);
        GO6.SetActive(true);
        GO7.SetActive(true);
        GO8.SetActive(true);
        GO9.SetActive(true);
        G10.SetActive(true);
        G11.SetActive(true);
        // set all objects as set to active
    }
}
