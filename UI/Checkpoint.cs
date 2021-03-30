using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;





public class Checkpoint : MonoBehaviour
{
    private Light2D light2D;



    private void Start()
    {
        light2D = GetComponentInChildren<Light2D>();
        light2D.intensity = 0f;

    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            light2D.intensity = 2f;

            CheckpointController.instance.SetSpawnPoint(transform.position);




        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        light2D.intensity = 0f;
    }

    
}
