using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class EnemyController : MonoBehaviour
{
    public EnemySO enemySO;
    private PhotonView PV;

    public int health;
    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
        health = enemySO.health;
    }

    // Update is called once per frame
    void Update()
    {
        Death();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Attack(other);
    }
    

    [PunRPC]
    public void Death()
    {
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void Attack(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameObject player = other.gameObject;
            Health health = player.GetComponent<Health>();
            health.numOfHits -= enemySO.damage;
        }
    }
}
