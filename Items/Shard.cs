using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shard : MonoBehaviour
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
       // Debug.Log("exp in shard = " + expPoints);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (    Time.time >= startTimer + 1f && collision.tag == "Player")
        {
            collision.SendMessage("GainEXP", expPoints);
            Destroy(gameObject);
        }
    }



    public void SetEXPPoint(int exp)
    {
        expPoints = exp;
    }
}
