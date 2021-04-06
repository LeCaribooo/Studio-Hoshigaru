using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class PlayerAttack : MonoBehaviour
{
    private PhotonView PV;

    private void Start()
    {
        PV = GetComponentInParent<PhotonView>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            EnemyController enemyController = other.gameObject.GetComponent<EnemyController>();
            enemyController.health -= 5;
        }
    }
}
