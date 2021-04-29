using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPatroller : MonoBehaviour
{
    public int damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && !GetComponentInParent<Patroller>().alreadyAttacked)
        {
            GameObject player = collision.gameObject;
            player.GetComponent<Health>().numOfHits -= damage;
            GetComponentInParent<Patroller>().alreadyAttacked = true;
        }
    }
}
