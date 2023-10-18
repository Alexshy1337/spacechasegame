using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    public int OreAmount = 3;


    public void MinedOut()
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.black;
    }



    //public int Hardness;

    /*
     
     private void Start()
    {
        OreAmount = 3;
    }

     */
}
