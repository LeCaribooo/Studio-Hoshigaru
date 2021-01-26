using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerControler : MonoBehaviour
{
    private PhotonView PV;
    
   public float movementSpeed;       //Speed du joueur
   public float jumpForce;           //Puissance de saut
   private float movementInput;      //(-1 ou 1/ Gauche Droite)
   
   private Rigidbody2D rb;
    public Animator animator;

   public bool isGrounded;           //Booléen pour savoir si il est sur le sol
   public Transform groundCheck;     //Coordonnées des pieds du character
   public float checkRadius;         //Radius de check
   public LayerMask whatIsGround;    //Layer qui select quel layer est le ground

    public Collider2D defaultCollider;

    [SerializeField]
    private PolygonCollider2D[] colliders;
    private int currentColliderIndex = 0;
    

    public int extraJumpsValue;
   private int extraJumps;
   
   private bool facingRight = true;

    public void SetColliderForSprite(int spriteNum)
    {
        colliders[currentColliderIndex].enabled = false;
        defaultCollider.enabled = false;
        currentColliderIndex = spriteNum;
        colliders[currentColliderIndex].enabled = true;
    }

   public void SetColliderDefault()
    {
        colliders[currentColliderIndex].enabled = false;
        defaultCollider.enabled = true;
    }

    void Start()
    {
        PV = GetComponent<PhotonView>();
        extraJumps = extraJumpsValue;
        rb = GetComponent<Rigidbody2D>();
    }

    
    void FixedUpdate()
    {
        PhotonView photonView = PhotonView.Get(this);
        if (PV.IsMine)
        {
           Move();
        }

        if (facingRight == false && movementInput > 0)
        {
            photonView.RPC("Flip", RpcTarget.All);
        }
        else if (facingRight == true && movementInput < 0)
        {
            photonView.RPC("Flip", RpcTarget.All);
        }

        float characterVelocity = Mathf.Abs(rb.velocity.x);
        animator.SetFloat("Speed", characterVelocity);
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
}
