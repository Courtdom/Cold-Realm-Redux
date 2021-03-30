using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAfterImageSprite : MonoBehaviour
{

    [SerializeField]
    private float activeTime = 0.1f;
    private float timeActivated;
    private float alpha;
    [SerializeField]
    private float alphaSet = 0.8f;
    [SerializeField]
    private float alphaDecay = 0.85f;


    private SpriteRenderer theSR;
    private SpriteRenderer playerSR;
//    private GameObject player;
    private Color color;

    private void OnEnable()
    {
        theSR = GetComponent<SpriteRenderer>();
        // player = GameObject.FindGameObjectWithTag("Player").transform;
       
        playerSR = Player.instance.GetComponent<SpriteRenderer>();

        alpha = alphaSet;
        theSR.sprite = playerSR.sprite;
        transform.position = Player.instance.transform.position;
        transform.rotation = Player.instance.transform.rotation;
        timeActivated = Time.time;
    }

    private void Update()
    {
        alpha -= alphaDecay * Time.deltaTime;
        color = new Color(1f, 1f, 1f, alpha);
        theSR.color = color;

        if(Time.time >= (timeActivated + activeTime))
        {
            PlayerAfterImagePool.Instance.AddToPool(gameObject);
        }

    }

}
