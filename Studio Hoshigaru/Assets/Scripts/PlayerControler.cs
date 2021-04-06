using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class PlayerControler : MonoBehaviour
{
    public PlayerSO playerSO;

    public PhotonView PV;

    private float movementSpeed;       //Speed du joueur
    private float jumpForce;           //Puissance de saut
    private float movementInput;       //(-1 ou 1 Gauche Droite)
    private float jumpTimeCounter;
    private float jumpTime;
   
    private Rigidbody2D rb;
    public Animator animator;
    private Collider2D hitbox;

    private bool isGrounded;           //Booléen pour savoir si il est sur le sol
    public Transform groundCheck;     //Coordonnées des pieds du character
    private float checkRadius;         //Radius de check
    private LayerMask whatIsGround;    //Layer qui select quel layer est le ground
 
    private int extraJumpsValue;
    private int extraJumps;

    private int attackStatus = 0;
    private int time = 0;
    public GameObject attackHitbox;
    private CapsuleCollider2D attackHitboxCollider;

    private bool facingRight = true;

    private SpriteRenderer spriteRenderer;

    private PlayerDeath playerDeath;

    public Canvas UI;

    public Camera camera;

    public bool isDead = false;


    void PlayerSO()
    {
        movementSpeed = playerSO.movementSpeed;
        jumpForce = playerSO.jumpForce;
        isGrounded = playerSO.isGrounded;
        checkRadius = playerSO.checkRadius;
        whatIsGround = playerSO.whatIsGround;
        extraJumpsValue = playerSO.extraJumpsValue;
        jumpTime = playerSO.jumpTime;
    }


    void Start()
    {
        PlayerSO();
        PV = GetComponent<PhotonView>();
        playerDeath = GetComponent<PlayerDeath>();
        playerDeath.enabled = true;
        if (!PV.IsMine)
        {
            UI.enabled = false;
        }
        
        extraJumps = extraJumpsValue;
        rb = GetComponent<Rigidbody2D>();
        hitbox = GetComponent<CapsuleCollider2D>();
        attackHitboxCollider = attackHitbox.GetComponent<CapsuleCollider2D>();
        attackHitboxCollider.enabled = false;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        if (PV.IsMine)
        {
             Move();
            if (facingRight && movementInput > 0)
            {
                Flip();
            }
            else if (!facingRight && movementInput < 0)
            {
                Flip();
            }
        }

        
    }
    
    private void Update()
    {
        if (PV.IsMine)
        {
            Jump();
            Attack();
        }   
    }

    void Jump()
    {
        if (isGrounded)
        {
            movementSpeed = playerSO.movementSpeed;
            extraJumps = extraJumpsValue;
            jumpTimeCounter = jumpTime;
            animator.SetBool("isJumping", false);
        }
        else
        {
            animator.SetBool("isJumping", true);
        }

        if (Input.GetKeyDown(KeyCode.Space) && extraJumps > 0)
        {
            rb.velocity = Vector2.up * jumpForce;
            extraJumps--;
            animator.SetTrigger("takeOf");
        }

        if (Input.GetKey(KeyCode.Space) && !isGrounded && extraJumps == extraJumpsValue)
        {
            if(jumpTimeCounter > 0)
            {
                movementSpeed = 3;
                rb.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            }
        }           
    }

    
    void Move()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
        movementInput = Input.GetAxisRaw("Horizontal");
        if(movementInput == 0)
        {
            animator.SetBool("isRunning", false);
        }
        else
        {
            animator.SetBool("isRunning", true);
        }
        rb.velocity = new Vector2(movementInput * movementSpeed, rb.velocity.y); //Déplace le rigibody
    }

    [PunRPC]
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }

    public void Attack()
    {
        if (attackStatus != 0)
        {
            time += 1 ;
        }
        if (Input.GetMouseButtonDown(0) && attackStatus == 0)
        {
            PV.RPC("AttackEnabled", RpcTarget.All, false);
            attackStatus += 1;
            animator.SetInteger("AttackStatus", attackStatus);
            PV.RPC("AttackEnabled", RpcTarget.All, true);
        }
        else if (Input.GetMouseButtonDown(0) && attackStatus == 1 && time > 50)
        {
            PV.RPC("AttackEnabled", RpcTarget.All, false);
            time = 0;
            attackStatus += 1;
            animator.SetInteger("AttackStatus", attackStatus);
            PV.RPC("AttackEnabled", RpcTarget.All, true);
        }
        else if (Input.GetMouseButtonDown(0) && attackStatus == 2 && time > 50)
        {
            PV.RPC("AttackEnabled", RpcTarget.All, false);
            time = 0;
            attackStatus -= 1;
            animator.SetInteger("AttackStatus", attackStatus);
            PV.RPC("AttackEnabled", RpcTarget.All, true);

        }
        else if (time > 100)
        {
            time = 0;
            attackStatus = 0;
            animator.SetInteger("AttackStatus", attackStatus);
            PV.RPC("AttackEnabled", RpcTarget.All, false);
        }
    }

    [PunRPC]
    void AttackEnabled(bool isEnable)
    {
        attackHitboxCollider.enabled = isEnable;
    }

    /*public void DisableEverythingWhenDead()
    {
        hitbox.enabled = false;
        animator.enabled = false;
        attackHitbox.SetActive(false);
        UI.enabled = false;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        spriteRenderer.enabled = false;
        groundCheck.gameObject.SetActive(false);
        this.tag = "Untagged";
        camera.gameObject.SetActive(false);
    }*/

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Physics2D.IgnoreCollision(hitbox, other);
        }
    }
}
