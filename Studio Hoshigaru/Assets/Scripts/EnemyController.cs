using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Pathfinding;


public class EnemyController : MonoBehaviour
{
    private AIPath aIPath;
    public HealthBar healthbar;
    public EnemyHealth health;
    public EnemySO enemySO;
    private PhotonView PV;
    private Animator animator;
    public bool cooling;
    public bool wait;
    public GameObject Parent;

    public int maxHealth;
    // Start is called before the first frame update
    void Start()
    {
        aIPath = GetComponentInParent<AIPath>();
        PV = GetComponent<PhotonView>();
        health.health = enemySO.health;
        healthbar.SetMaxHealth(health.health);
        animator = GetComponent<Animator>();
        cooling = true;
    }
    private void FixedUpdate()
    {
            float enemyvelocity = Mathf.Abs(aIPath.velocity.x);
            animator.SetFloat("speed", enemyvelocity);
    }

    // Update is called once per frame
    void Update()
    {
        healthbar.SetHealth(health.health);
        Death();
        if(wait)
        {
            cooling = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Attack(other);
        
    }

    [PunRPC]
    public void Death()
    {
        if (health.health <= 0)
        {
            PhotonNetwork.Destroy(Parent);
        }
    }

    public void Attack(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && cooling)
        {
            GameObject player = other.gameObject;
            Health health = player.GetComponent<Health>();
            health.numOfHits -= enemySO.damage;
            cooling = false;
            Cooler();
            Debug.Log("i just attacked");
        }  
    }

    public void Cooler()
    {
        wait = false;
        StartCoroutine(Test());
    }
    IEnumerator Test()
    {
        yield return new WaitForSeconds(2f);
        wait = true;
        Debug.Log("attack again");
    }
}
