using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerControler : MonoBehaviour
{
    public PlayerSO playerSO;

    private PhotonView PV;

    private float movementSpeed;       //Speed du joueur
    private float jumpForce;           //Puissance de saut
    private float movementInput;      //(-1 ou 1/ Gauche Droite)
   
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

    public Canvas UI;

    private CameraPlayer cameraPlayer;
   
    void PlayerSO()
    {
        movementSpeed = playerSO.movementSpeed;
        jumpForce = playerSO.jumpForce;
        isGrounded = playerSO.isGrounded;
        checkRadius = playerSO.checkRadius;
        whatIsGround = playerSO.whatIsGround;
        extraJumpsValue = playerSO.extraJumpsValue;
    }


    void Start()
    {
        PlayerSO();
        PV = GetComponent<PhotonView>();
        if (!PV.IsMine)
        {
            UI.enabled = false;
        }
        extraJumps = extraJumpsValue;
        rb = GetComponent<Rigidbody2D>();
        hitbox = GetComponent<CapsuleCollider2D>();
        attackHitboxCollider = attackHitbox.GetComponent<CapsuleCollider2D>();
        attackHitboxCollider.enabled = false;
    }

    void FixedUpdate()
    {
        if (PV.IsMine)
        {
           Move();
        }

        if (facingRight == true && movementInput > 0)
        {
            PV.RPC("Flip", RpcTarget.All);
        }
        else if (facingRight == false && movementInput < 0)
        {
            PV.RPC("Flip", RpcTarget.All);
        }

        float characterVelocity = Mathf.Abs(rb.velocity.x);
        animator.SetFloat("Speed", characterVelocity);
        
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
        if (isGrounded == true)
        {
            extraJumps = extraJumpsValue;
        }
        if (Input.GetButtonDown("Jump") && extraJumps > 0)
        {
            rb.velocity = Vector2.up * jumpForce ;
            extraJumps--;
        }
        else if (Input.GetButtonDown("Jump")&& extraJumps == 0 && isGrounded == true)
        {
            rb.velocity = Vector2.up * jumpForce;
        }
    }

    
    void Move()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
        
        movementInput = Input.GetAxisRaw("Horizontal");
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
            time += 1;
        }
        if (Input.GetMouseButtonDown(0) && attackStatus == 0)
        {
            attackStatus += 1;
            animator.SetInteger("AttackStatus", attackStatus);
            PV.RPC("AttackEnabled", RpcTarget.All, true);
        }
        else if (Input.GetMouseButtonDown(0) && attackStatus == 1 && time > 50)
        {
            time = 0;
            attackStatus += 1;
            animator.SetInteger("AttackStatus", attackStatus);
            PV.RPC("AttackEnabled", RpcTarget.All, true);
        }
        else if (Input.GetMouseButtonDown(0) && attackStatus == 2 && time > 50)
        {
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



    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Physics2D.IgnoreCollision(hitbox, other);
        }
    }
}
