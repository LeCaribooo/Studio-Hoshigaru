using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : MonoBehaviour
{
    [SerializeField] BoxCollider2D hitbox;
    public Animator animator;

    private void Start()
    {
        hitbox.enabled = false;
    }

    void Update()
    {
        Attack();
    }

    void Attack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetInteger("AttackStatus", 1);
        }
    }
}
