using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CameraPlayer : MonoBehaviour
{ 
    private Transform target;
    public float smoothSpeed;
    public Vector3 offset;
    private GameObject[] players;
    private PhotonView PV;
    private bool check = false;

    public void CheckPlayer()
    {
        players = GameObject.FindGameObjectsWithTag("Player");  
        foreach(GameObject player in players)
        {
            PV = player.GetComponent<PhotonView>();
            if (PV.IsMine)
            {
                target = player.transform;
                check = true;
                break;
            }
        } 
    }

    private void LateUpdate()
    {
        if (!check)
        {
            CheckPlayer();
        }
        
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
