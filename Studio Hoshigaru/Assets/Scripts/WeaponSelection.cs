using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSelection : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject sword;
    [SerializeField] private GameObject bow;
    [SerializeField] private GameObject shuriken;
    [SerializeField] private GameObject hammer;
    private GameObject actualWeapon = null;
    private string actualWeaponString = "";

    public void selectSword()
    {
        if (actualWeaponString == "hasSword")
            return;
        sword.SetActive(true);
        animator.SetBool("hasSword", true);
        if (actualWeaponString != "")
        {
            actualWeapon.SetActive(false);
            animator.SetBool(actualWeaponString, false);
        }
        actualWeapon = sword;
        actualWeaponString = "hasSword";
    }

    public void selectBow()
    {
        if (actualWeaponString == "hasBow")
            return;
        bow.SetActive(true);
        animator.SetBool("hasBow", true);
        if (actualWeaponString != "")
        {
            actualWeapon.SetActive(false);
            animator.SetBool(actualWeaponString, false);
        }   
        actualWeapon = bow;
        actualWeaponString = "hasBow";
    }

    public void selectShuriken()
    {
        if (actualWeaponString == "hasShuriken")
            return;
        shuriken.SetActive(true);
        animator.SetBool("hasShuriken", true);
        if (actualWeaponString != "")
        {
            actualWeapon.SetActive(false);
            animator.SetBool(actualWeaponString, false);
        }
        actualWeapon = shuriken;
        actualWeaponString = "hasShuriken";
    }

    public void selectHammer()
    {
        if (actualWeaponString == "hasHammer")
            return;
        hammer.SetActive(true);
        animator.SetBool("hasHammer", true);
        if (actualWeaponString != "")
        {
            actualWeapon.SetActive(false);
            animator.SetBool(actualWeaponString, false);
        }
        actualWeapon = hammer;
        actualWeaponString = "hasHammer";
    }
}
