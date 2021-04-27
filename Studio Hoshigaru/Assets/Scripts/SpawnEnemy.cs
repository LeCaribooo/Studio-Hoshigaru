using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;


public class SpawnEnemy : MonoBehaviour
{
    public GameObject EnemySpawner;
    private CircleCollider2D collider2D;
    private PhotonView PV;
    bool isSpawned = false;
    void Start()
    {
        PV = GetComponent<PhotonView>();
        collider2D = GetComponent<CircleCollider2D>();
    }

    private void Update()
    {
        if(collider2D.enabled == false && !isSpawned)
        {
            PhotonNetwork.Instantiate(Path.Combine("Prefab", "Enemy", "DarkAngel 1"), new Vector3(0,-11,0), Quaternion.identity);
            isSpawned = true;
        }
    }

    [PunRPC]
    private void IsTrigger()
    {
        collider2D.enabled = false;
        EnemySpawner.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PV.RPC("IsTrigger", RpcTarget.AllBuffered);
        }
    }
}
