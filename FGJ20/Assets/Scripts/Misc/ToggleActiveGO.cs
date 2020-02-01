using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleActiveGO : MonoBehaviour
{
    public GameObject axe;
    public GameObject weapon;

    public void EnableAxe()
    {
        axe.SetActive(true);
    }

    public void DisableAxe()
    {
        axe.SetActive(false);
    }

    public void EnableWeapon()
    {
        weapon.SetActive(true);
    }

    public void DisableWeapon()
    {
        weapon.SetActive(false);
    }
}
