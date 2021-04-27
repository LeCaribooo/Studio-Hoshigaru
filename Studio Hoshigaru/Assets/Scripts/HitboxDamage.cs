using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxDamage : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            EnemyHealth enemyhealth = other.gameObject.GetComponent<EnemyHealth>();
            enemyhealth.health -= 5;
        }
    }
}
