using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour
{

    public static CheckpointController instance;

    
    public Vector3 spawnPoint;



    private void Awake()
    {
        instance = this;
    }




    private void Start()
    {
       

    }


   

    // Update is called once per frame
    void Update()
    {
        
    }



   








    public void SetSpawnPoint(Vector3 newSpawnPoint)
    {
        spawnPoint = newSpawnPoint;
    }
}
