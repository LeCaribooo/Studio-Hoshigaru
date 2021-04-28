using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class PlayerControler : MonoBehaviour
{
    [SerializeField] private GameObject bow;

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

    [HideInInspector] public bool facingRight = true;

    private PlayerDeath playerDeath;
    [SerializeField] private Sword sword;

    public Canvas UI;

    public Camera camera;
    
    private static PlayerControler playerInstance;

    void Awake()
    {
        DontDestroyOnLoad(this);

        if (playerInstance == null && PV.IsMine)
        {
            playerInstance = this;
        }
        else if (PV.IsMine)
        {
            PhotonNetwork.Destroy(gameObject);
        }
    }

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
        DontDestroyOnLoad(this.gameObject);
        PlayerSO();
        playerDeath = GetComponent<PlayerDeath>();
        playerDeath.enabled = true;
        if (!PV.IsMine)
        {
            UI.enabled = false;
        }
        extraJumps = extraJumpsValue;
        rb = GetComponent<Rigidbody2D>();
        hitbox = GetComponent<CapsuleCollider2D>();
    }

    void FixedUpdate()
    {
        if (PV.IsMine)
        {
             Move();
            if ((facingRight && movementInput > 0) || (!facingRight && movementInput < 0))
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
        }   
    }

    void Jump()
    {
        if (isGrounded)
        {
            movementSpeed = playerSO.movementSpeed;
            extraJumps = extraJumpsValue;
            jumpTimeCounter = jumpTime;
            sword.canAttack = true;
            animator.SetBool("isJumping", false);
        }
        else
        {
            sword.canAttack = false; 
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

    public void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
        Vector3 ScalerBow = bow.transform.localScale;
        ScalerBow.x *= -1;
        bow.transform.localScale = ScalerBow;
    }

    public void SetAttackStatus(int AttackStatus)
    {
        animator.SetInteger("AttackStatus", AttackStatus);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Physics2D.IgnoreCollision(hitbox, other);
        }
    }

}
