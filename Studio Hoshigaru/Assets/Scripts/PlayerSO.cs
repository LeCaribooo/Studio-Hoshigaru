using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Player", menuName = "Player")]
public class PlayerSO : EntitySO
{
    public int extraJumpsValue;
    public bool isGrounded;           //Booléen pour savoir si il est sur le sol
    public float checkRadius;         //Radius de check
    public LayerMask whatIsGround;    //Layer qui select quel layer est le ground
    public float jumpTime;
}
