using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxDamage : MonoBehaviour
{
    public GameObject player;
    public int dmg;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            GameObject enemy = other.gameObject;
            EnemyHealth enemyhealth = other.gameObject.GetComponent<EnemyHealth>();
            enemyhealth.health -= dmg;
            bool right = player.GetComponent<PlayerControler>().facingRight;
            Knockback(enemy,right);
        }
    }
   
    private void Knockback(GameObject enemy, bool right)
    {
        Debug.Log("almost");
        
        if(!right)
        {
            Debug.Log("r");
        }
        else
        {
            Debug.Log("l");
        }
        string weapon = this.gameObject.GetComponent<WeaponSelection>().actualWeaponString;
        switch (weapon)
        {
            case "hasSword":
                Debug.Log("it's ok");
                if (!right)
                {
                    enemy.GetComponent<KnockBack>().rdb2.velocity = Vector2.right * 3;
                }
                else
                {
                    enemy.GetComponent<KnockBack>().rdb2.velocity = Vector2.left * 3;
                }
                break;
            case "hasShuriken":
                Debug.Log("it's ok");
                break;
            case "hasHammer":
                Debug.Log("it's ok");
                if (!right)
                {
                    enemy.GetComponent<KnockBack>().rdb2.velocity = Vector2.right * 6;
                }
                else
                {
                    enemy.GetComponent<KnockBack>().rdb2.velocity = Vector2.left * 6;
                }
                break;
        }
    }
}
