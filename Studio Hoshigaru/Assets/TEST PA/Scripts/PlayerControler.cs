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

   public bool isGrounded;           //Booléen pour savoir si il est sur le sol
   public Transform groundCheck;     //Coordonnées des pieds du character
   public float checkRadius;         //Radius de check
   public LayerMask whatIsGround;    //Layer qui select quel layer est le ground

   public int extraJumpsValue;
   private int extraJumps;
   
    void Start()
    {
        PV = GetComponent<PhotonView>();
        extraJumps = extraJumpsValue;
        rb = GetComponent<Rigidbody2D>();
    }

    
    void FixedUpdate()
    {
        if (PV.IsMine)
        {
           Move();
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
}
