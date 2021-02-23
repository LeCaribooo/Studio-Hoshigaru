using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameObject enemy = other.gameObject;
            Health health = enemy.GetComponent<Health>();
            health.numOfHits -= 2;
        }
    }
}
