using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class WeaponSelection : MonoBehaviourPun
{
    [SerializeField] private Animator animator;
    [SerializeField] public GameObject sword;
    [SerializeField] public GameObject bow;
    [SerializeField] public GameObject shuriken;
    [SerializeField] public GameObject hammer;
    public GameObject actualWeapon = null;
    private string actualWeaponString = "";

    public void selectSword()
    {
        if (actualWeaponString == "hasSword")
            return;
        base.photonView.RPC("SetSwordActive", RpcTarget.All, true);
        animator.SetBool("hasSword", true);
        if (actualWeaponString != "")
        {
            base.photonView.RPC("SetActualActive", RpcTarget.All, false);
            animator.SetBool(actualWeaponString, false);
        }
        actualWeapon = sword;
        actualWeaponString = "hasSword";
    }

    public void selectBow()
    {
        if (actualWeaponString == "hasBow")
            return;
        base.photonView.RPC("SetBowActive", RpcTarget.All, true);
        animator.SetBool("hasBow", true);
        if (actualWeaponString != "")
        {
            base.photonView.RPC("SetActualActive", RpcTarget.All, false);
            animator.SetBool(actualWeaponString, false);
        }   
        actualWeapon = bow;
        actualWeaponString = "hasBow";
    }

    public void selectShuriken()
    {
        if (actualWeaponString == "hasShuriken")
            return;
        base.photonView.RPC("SetShurikenActive", RpcTarget.All, true);
        animator.SetBool("hasShuriken", true);
        if (actualWeaponString != "")
        {
            base.photonView.RPC("SetActualActive", RpcTarget.All, false);
            animator.SetBool(actualWeaponString, false);
        }
        actualWeapon = shuriken;
        actualWeaponString = "hasShuriken";
    }

    public void selectHammer()
    {
        if (actualWeaponString == "hasHammer")
            return;
        base.photonView.RPC("SetHammerActive", RpcTarget.All, true);
        animator.SetBool("hasHammer", true);
        if (actualWeaponString != "")
        {
            base.photonView.RPC("SetActualActive", RpcTarget.All, false);
            animator.SetBool(actualWeaponString, false);
        }
        actualWeapon = hammer;
        actualWeaponString = "hasHammer";
    }

    [PunRPC]
    public void SetSwordActive(bool isActive)
    {
        sword.SetActive(isActive);
    }

    [PunRPC]
    public void SetBowActive(bool isActive)
    {
        bow.SetActive(isActive);
    }
    
    [PunRPC]
    public void SetShurikenActive(bool isActive)
    {
        shuriken.SetActive(isActive);
    }

    [PunRPC]
    public void SetHammerActive(bool isActive)
    {
        hammer.SetActive(isActive);
    }

    [PunRPC]
    public void SetActualActive(bool isActive)
    {
        actualWeapon.SetActive(isActive);
    }




}


