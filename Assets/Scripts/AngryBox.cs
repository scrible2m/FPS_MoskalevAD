using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class AngryBox : MonoBehaviour
{
    public Transform Enemy;
    [SerializeField] private IAngry[] _iAngry;
   
    void Start()
    {
        Array.Resize(ref _iAngry, Enemy.childCount);
        for (int i = 0; i<Enemy.childCount;i++)
        {
            _iAngry[i] = Enemy.GetChild(i).GetComponent<IAngry>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<SinglePlayer>())
        {
            Debug.Log("I'm In!");
            foreach(IAngry _ia in _iAngry)
            {
                Agr(_ia);
            }
        }
    }

    private void Agr(IAngry obj)
    {
        if (obj != null)
        {
            obj.Agr();
        }
    }
}
