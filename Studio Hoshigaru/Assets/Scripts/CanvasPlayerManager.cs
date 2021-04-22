using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasPlayerManager : MonoBehaviour
{
    bool isWeaponSelectionOpen = false;
    public GameObject weaponSelection;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isWeaponSelectionOpen)
            {
                weaponSelection.SetActive(false);
            }
            else
            {
                weaponSelection.SetActive(true);
            }
            isWeaponSelectionOpen = !isWeaponSelectionOpen;
        }
    }
}
