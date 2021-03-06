﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Shuriken : MonoBehaviour
{
    public GameObject shuriken;
    GameObject[] players;
    private PlayerControler playerControler;
    public int numberOfShuriken;
    public Animator animator;

    private void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        playerControler = getPlayer().GetComponent<PlayerControler>();
    }

    GameObject getPlayer()
    {
        foreach (GameObject player in players)
        {
            if (player.GetPhotonView().IsMine)
                return player;
        }
        return null;
    }

    void Update()
    { 
        if(Input.GetMouseButtonDown(0) && numberOfShuriken > 0)
        {
            SetAttackStatus(1);
        }
        if (Input.GetMouseButtonDown(1) && numberOfShuriken > 0 && animator.GetInteger("AttackStatus") == 0)
        {
            numberOfShuriken--;
            if (!playerControler.facingRight)
            {
                Instantiate(shuriken, new Vector3(transform.position.x + 0.5f, transform.position.y, transform.position.z), transform.rotation);
            }
            else
            {
                Instantiate(shuriken, new Vector3(transform.position.x - 0.5f, transform.position.y, transform.position.z), transform.rotation);
            }
        }
    }

    public void SetAttackStatus(int AttackStatus)
    {
        animator.SetInteger("AttackStatus", AttackStatus);
    }
}
