using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimbDamage : MonoBehaviour,  ISetDamage
{
    [SerializeField] float _damageCount;
    [SerializeField] ISetDamage _iSetDamage;
    [SerializeField] GameObject _Unit;
    
    private void Start()
    {
        _iSetDamage = _Unit.GetComponent<ISetDamage>();
    }
    public void SetDamage(int damage)
    {
        
        if (_iSetDamage != null)
        {
            _iSetDamage.SetDamage(Mathf.RoundToInt(damage * _damageCount));
        }   
    }
}

