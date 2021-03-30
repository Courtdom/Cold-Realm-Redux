using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParralaxBG : MonoBehaviour
{

    [SerializeField]   
    private Vector2 parrallaxEffectMultiplier;
    [SerializeField]
    private Transform cameraTransform;
    [SerializeField]
    private Vector3 lastCameraPosition;










    void Start()
    {
       // cameraTransform = Camera.main.transform;
        lastCameraPosition = cameraTransform.position;

    }

    private void LateUpdate()
    {
        Vector3 deltaMovement = cameraTransform.position - lastCameraPosition;
        transform.position += new Vector3(deltaMovement.x * parrallaxEffectMultiplier.x, deltaMovement.y * parrallaxEffectMultiplier.y);
        lastCameraPosition = cameraTransform.position;

    }
}
