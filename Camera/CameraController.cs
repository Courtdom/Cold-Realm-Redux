using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    private Transform target;

    public float minHeight, maxHeight;


    public float zAxis;

    public float YOffset;



    void Start()
    {
        target = Player.instance.transform;
    }

    void Update()
    {
        transform.position = new Vector3(target.position.x, target.position.y, zAxis);


        float clampedY = Mathf.Clamp(transform.position.y + YOffset, minHeight, maxHeight);

        transform.position = new Vector3(transform.position.x, clampedY, zAxis);

    }
















}
