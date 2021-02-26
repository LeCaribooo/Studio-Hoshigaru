using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            GameObject enemy = other.gameObject;
            EnemyController enemyController = enemy.GetComponent<EnemyController>();
            enemyController.health -= 5;
        }
    }
}
