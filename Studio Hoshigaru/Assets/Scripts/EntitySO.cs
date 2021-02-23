using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

[CreateAssetMenu(fileName = "New Entity", menuName = "Entity")]
public class EntitySO : ScriptableObject
{ 
    public float movementSpeed;
    public float jumpForce;   

    public int numOfHearts;
    public int numOfHits;
}