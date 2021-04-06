using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class PlayerDeath : MonoBehaviour
{
    private Health health;
    private PhotonView PV;

    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
        health = GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PV.IsMine)
        {
            Death();
        }
    }

    public void Death()
    {
        if (health.numOfHits <= 0)
        {
            PhotonNetwork.Instantiate(Path.Combine("Prefab", "Player", "DeadPlayer"), transform.position, Quaternion.identity, 0); ;
            PhotonNetwork.Destroy(PV.gameObject);
        }
    }
}
