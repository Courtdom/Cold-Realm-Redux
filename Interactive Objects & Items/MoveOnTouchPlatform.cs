using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOnTouchPlatform : MonoBehaviour
{
   // [SerializeField]
    public Vector2 CurrentVelocity;
    [SerializeField]
    private bool movingOnTouch;


    public Transform rightTransform;
    public Transform leftTransform;
    private Vector2 targetRight;
    private Vector2 targetLeft;
    private Vector2 position;


    private bool movingRight;
    private bool movingLeft;
    private bool moving;

    private float step;
    [SerializeField]
    private float speed;

    private void Start()
    {
        targetRight = rightTransform.transform.position;
        targetLeft = leftTransform.transform.position;
        position = gameObject.transform.position;
        movingRight = true;

        if(movingOnTouch)
        {
            moving = false;
        }
        else
        {
            moving = true;
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            moving = true;
            collision.collider.transform.SetParent(transform);
        }
    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if(movingOnTouch)
            {
                moving = false;
            }
            collision.collider.transform.SetParent(null);
        }
    }




    // Start is called before the first frame update
    private void FixedUpdate()
    {
        MoveUpandDown();
    }










    public void MoveUpandDown()
    {

        if (moving)
        {
            position = gameObject.transform.position;
            // Debug.Log("have reached top = " + ReachedTop());
            if (!ReachedTop() && movingRight)
            {
                step = speed * Time.deltaTime;
                transform.position = Vector2.MoveTowards(transform.position, targetRight, step);

            }
            else if (ReachedTop())
            {
                movingRight = false;
                movingLeft = true;
            }
            if (!ReachedBottom() && movingLeft)
            {
                step = speed * Time.deltaTime;
                transform.position = Vector2.MoveTowards(transform.position, targetLeft, step);
            }
            else if (ReachedBottom())
            {
                movingLeft = false;
                movingRight = true;
            }



        }
    }


    public bool ReachedTop()
    {
        return targetRight == position;
    }
    public bool ReachedBottom()
    {
        return targetLeft == position;
    }

}
