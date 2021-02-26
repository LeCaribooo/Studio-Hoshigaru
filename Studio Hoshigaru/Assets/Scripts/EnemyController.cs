using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public EnemySO enemySO;

    public int health;
    // Start is called before the first frame update
    void Start()
    {
        health = enemySO.health;
    }

    // Update is called once per frame
    void Update()
    {
        Death();
    }

    private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                GameObject player = other.gameObject;
                Health health = player.GetComponent<Health>();
                health.numOfHits -= enemySO.damage;
            }
        }
    


    public void Death()
    {
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }


}
