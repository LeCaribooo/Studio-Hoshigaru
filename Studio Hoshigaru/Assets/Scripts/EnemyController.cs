using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Pathfinding;


public class EnemyController : MonoBehaviour
{
    public AIPath aIPath;
    public HealthBar healthbar;
    public EnemySO enemySO;
    private PhotonView PV;
    private Animator animator;

    public int health;
    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
        health = enemySO.health;
        healthbar.SetMaxHealth(health);
        animator = GetComponent<Animator>();
    }
    private void FixedUpdate()
    {
        float enemyvelocity = Mathf.Abs(aIPath.velocity.x);
        animator.SetFloat("speed", enemyvelocity);
    }
    // Update is called once per frame
    void Update()
    {
        healthbar.SetHealth(health);
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
