using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : MonoBehaviour
{
    [SerializeField] private int _unDamage;

    private void OnTriggerEnter(Collider other)
    {
        ISetDamage temp = other.GetComponent<ISetDamage>();
        if (temp != null)
        {
            StartCoroutine(Heal(temp));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        StopAllCoroutines();
    }
    IEnumerator Heal(ISetDamage obj)
    {
        while (true)
        {
            yield return new WaitForSeconds(0.4f);
            obj.SetDamage(-_unDamage);
            Debug.Log("I Healed " + obj + "to " + _unDamage + "hp...");
        }
    }
}
