using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeWeapon : MonoBehaviour, IInventory
{
    [SerializeField] private int weaponID = 0;
    private Transform GOTransform;
    private int prevWeaponID;
    private bool[] weaponPickup;
    void Start()
    {
        GOTransform = transform;
        Array.Resize(ref weaponPickup, GOTransform.childCount);
        weaponPickup[0] = true;
        for (int i = 1; i < GOTransform.childCount - 1; i++)
        {
            weaponPickup[i] = false;
        }
       
    }
    public void PickupItem(int i)
    {
        weaponPickup[i] = true;
        weaponID = i;
        SelectWeapon();
    }
    private void SelectWeapon()
    {
        var i = 0;
        foreach (Transform weapon in transform)
        {
            if (i == weaponID)
            {
                weapon.gameObject.SetActive(true);
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
            i++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        prevWeaponID = weaponID;
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            do
            {
                if (weaponID <= 0)
                {
                    weaponID = GOTransform.childCount - 1;
                }
                else
                {
                    weaponID--;
                }
            } while (!weaponPickup[weaponID]);
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            do
            {
                if (weaponID >= GOTransform.childCount - 1)
                {
                    weaponID = 0;
                }
                else
                {
                    weaponID++;
                }
            } while (!weaponPickup[weaponID]);
        }

        if (prevWeaponID != weaponID)
        {
            SelectWeapon();
        }

    }
}
