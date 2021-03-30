using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadHologram : MonoBehaviour
{
    private float startTimer;

    public int expPoints;
    // Start is called before the first frame update
    void Start()
    {
        startTimer = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Time.time >= startTimer + 2f && collision.tag == "Player")
        {
            collision.SendMessage("OutOfGhostMode"); 
            collision.SendMessage("GainEXP", expPoints);
            Destroy(gameObject);
        }
    }
}
