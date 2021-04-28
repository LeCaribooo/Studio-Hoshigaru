using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Destroy_this_object : MonoBehaviour
{
    static bool created = false;


    private void Awake()
    { 
        if (!created)
        {
            created = true;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
