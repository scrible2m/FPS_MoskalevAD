using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupGun : MonoBehaviour
{
    public int weaponID;
    public GameObject infoList;

    public void PickupItem(IInventory obj)
    {
        if (obj != null)
        {
            obj.PickupItem(weaponID);
            Destroy(gameObject);
        }

    }
    public void Info(bool flag)
    {
        infoList.SetActive(flag);
    }
    public void DebugLog()
    {
        Debug.Log("I'm Here!");
    }
}

