using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamagePopUpText : MonoBehaviour
{





    private GameObject parent;
    private MeshRenderer MR;



    void Start()
    {

        MR = GetComponent<MeshRenderer>();
        MR.sortingLayerName = "Player";
        MR.sortingOrder = 5;


        parent = transform.parent.gameObject;


    }


    public void DestorySelf()
    {
        Destroy(parent);
    }

}

   
