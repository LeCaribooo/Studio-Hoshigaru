using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Patroller : MonoBehaviour
{
    public HealthBar healthbar;
    public int Maxhealth;
    public EnemyHealth health; 
    public GameObject hitbox;
    public bool finishedAttack;
    public bool dead = false;
    public bool attackMode;
    #region Patrolling
    [SerializeField]
    Transform castPos;
    [SerializeField]
    Transform castJump;

    public float baseCastDist;
    public float downCastDist;

    const string LEFT = "left";
    const string RIGHT = "right";

    string facingDirection;
    private PhotonView PV;
    Vector3 baseScale;
    #endregion

    #region Public Variables
    Rigidbody2D rb2d;
    public float attackDistance; //distance à partir de laquelle on attaque
    public float movementSpeed;//vitesse de déplacement
    public float timer;//durée entre deux attaques
    public Transform target = null;
    public bool inRange;//si le player est à portée
    public GameObject hotZone;
    public GameObject triggerArea;
    public bool cooling;//si le cooldown de l'attaque est bien à 0
    #endregion

    #region Private Variables
    private Animator anim;
    private float distance;//distance du joueur par rapport à this
    
    
    private float intTimer;
    #endregion


    private void Start()
    {
        health.health = Maxhealth;
        healthbar.SetMaxHealth(health.health);
        finishedAttack = true;
    }
    private void Awake()
    {
        PV = GetComponent<PhotonView>();
        rb2d = GetComponent<Rigidbody2D>();
        baseScale = transform.localScale;
        facingDirection = RIGHT;
        intTimer = timer; //prend la valuer initial de timer
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if(dead)
        {
            Destroy(this.gameObject);
            GameObject.Find("patroller").SetActive(false);
        }
        healthbar.SetHealth(health.health);
        Death();
        if (!attackMode && !cooling && finishedAttack && !anim.GetCurrentAnimatorStateInfo(0).IsName("death"))
        {
            Move();
        }
        if (cooling && finishedAttack && !anim.GetCurrentAnimatorStateInfo(0).IsName("death"))
        {
            anim.SetBool("attack", false);
            Cooldown();
        }
        if (IsNearEdge() && target == null && finishedAttack && !anim.GetCurrentAnimatorStateInfo(0).IsName("death"))
        {
            if (facingDirection == LEFT)
            {
                Flip(RIGHT);
            }
            else
            {
                Flip(LEFT);
            }
        }
        if(IsHittingWall() && finishedAttack && !anim.GetCurrentAnimatorStateInfo(0).IsName("death"))
        {
            if(CanJump())
            {
                rb2d.velocity = Vector2.up * 3f;
            }
            else
            {
                if (facingDirection == LEFT)
                {
                    Flip(RIGHT);
                }
                else
                {
                    Flip(LEFT);
                }
            }
        }

        if(inRange && !anim.GetCurrentAnimatorStateInfo(0).IsName("death"))
        {
            EnemyLogic();
        }
    }

    public void Hit()
    {
        hitbox.SetActive(true);
    }

    public void StopHit()
    {
        hitbox.SetActive(false);
    }

    public void Death()
    {
        if (health.health <= 0)
        {
            rb2d.velocity = new Vector2(0, rb2d.velocity.y);
            anim.SetBool("idle", false);
            anim.SetBool("attack", false);
            anim.SetBool("canWalk", false);
            anim.SetBool("death", true);
            hitbox.SetActive(false);
        }
    }

    public void Destroy()
    {
        dead = true;
    }

    public void EnemyLogic()
    {
        if (target != null)
        {

            distance = Vector2.Distance(transform.position, target.position);

            if (distance > attackDistance && finishedAttack)
            {
                StopAttack();
            }
            else if (attackDistance > distance && !cooling)
            {
                Attack();
            }
            if (cooling && finishedAttack)
            {
                anim.SetBool("attack", false);
                Cooldown();
            }
        }
    }

    public void Move()
    {
        anim.SetBool("canWalk", true);
        if(!anim.GetCurrentAnimatorStateInfo(0).IsName("attack") && !anim.GetCurrentAnimatorStateInfo(0).IsName("death"))
        {
            
            if (target != null)
            {
                Vector2 targetPosition = new Vector2(target.position.x, transform.position.y);
                transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementSpeed * Time.deltaTime);
            }
            else
            {
                float vX = movementSpeed;
                if (facingDirection == LEFT)
                {
                    vX = -movementSpeed;
                }
                rb2d.velocity = new Vector2(vX, rb2d.velocity.y);
            }
        }
        
    }

    void Attack()
    {
        timer = intTimer; //reset le timer si l'ennemy est dans la range d'attack
        attackMode = true; // check si l'ennemy peut encore attaquer

        anim.SetBool("canWalk", false);
        anim.SetBool("attack", true);
        finishedAttack = false;
    }

    public void Stopped()
    {
        finishedAttack = true;
    }

    void Cooldown()
    {
        timer -= Time.deltaTime;
        if(timer <= 0 && cooling && attackMode)
        {
            cooling = false;
            attackMode = false;
            timer = intTimer;
        }
    }

    void StopAttack()
    {
        if(timer == 2)
        {
            cooling = false;
            attackMode = false;
        }
        anim.SetBool("attack", false);

    }

    public void TriggerCooling()
    {
        cooling = true;
    }

    public void Flip(string newDIrection)
    {
        Vector3 newScale = baseScale;

        if(newDIrection == LEFT)
        {
            newScale.x = -baseScale.x;
        }
        else
        {
            newScale.x = baseScale.x;
        }

        transform.localScale = newScale;
        facingDirection = newDIrection;
    }

    bool IsHittingWall()
    {
        bool val = false;
        float castDist = baseCastDist;
        //define the cast distance
        if(facingDirection == LEFT)
        {
            castDist = -baseCastDist;
        }
        //determine the target destination based on the cast distance
        Vector3 targetPos = castPos.position;
        targetPos.x += castDist;
        Debug.DrawLine(castPos.position, targetPos, Color.green);
        if (Physics2D.Linecast(castPos.position,targetPos,1<<LayerMask.NameToLayer("Ground")))
        {

            val = true;
        }
        else
        {
            val = false;
        }
        return val; 
    }

    bool IsNearEdge()
    {
        bool val = true;
        float castDist = downCastDist;
        //determine the target destination based on the cast distance
        Vector3 targetPos = castPos.position;
        targetPos.y -= castDist;

        if (Physics2D.Linecast(castPos.position, targetPos, 1 << LayerMask.NameToLayer("Ground")))
        {

            val = false;
        }
        else
        {
            val = true;
        }
        return val;
    }
    bool CanJump()
    {
        bool val = false;
        float castDist = baseCastDist;
        //define the cast distance
        if (facingDirection == LEFT)
        {
            castDist = -baseCastDist;
        }
        //determine the target destination based on the cast distance
        Vector3 targetPos = castJump.position;
        targetPos.x += castDist;

        if (Physics2D.Linecast(castJump.position, targetPos, 1 << LayerMask.NameToLayer("Ground")))
        {
            val = false;
        }
        else
        {
            val = true;
        }
        return val;
    }
}

