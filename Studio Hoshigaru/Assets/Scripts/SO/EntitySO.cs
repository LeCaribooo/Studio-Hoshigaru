using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

[CreateAssetMenu(fileName = "New Entity", menuName = "Entity")]
public class EntitySO : ScriptableObject
{
    public string name;

    public float movementSpeed;
    public float jumpForce;

    public Sprite sprite;

    private Rigidbody2D rb;
    public Animator animator;
    private Collider2D hitbox;

    public int health;
    public int currentHealth;

    private PhotonView PV;
}