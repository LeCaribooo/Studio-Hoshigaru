using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSelection : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject sword;
    [SerializeField] private GameObject bow;
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
}
