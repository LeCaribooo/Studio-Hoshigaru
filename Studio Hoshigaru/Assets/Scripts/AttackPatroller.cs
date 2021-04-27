using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPatroller : MonoBehaviour
{
    public int damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            GameObject player = collision.gameObject;
            Health health = player.GetComponent<Health>();
            health.numOfHits -= damage;
        }
    }
}
