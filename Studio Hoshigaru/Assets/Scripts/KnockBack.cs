using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : MonoBehaviour
{
    public Rigidbody2D rdb2;

    private void Start()
    {
        rdb2 = GetComponent<Rigidbody2D>();
    }
}
