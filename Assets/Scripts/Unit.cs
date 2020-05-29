using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : BaseObject, ISetDamage
{
    [SerializeField] private int _health;
    [SerializeField] private bool _dead;
    [SerializeField] private bool _singlePlayer;
    [SerializeField] private SinglePlayer _sp;

    public int Health { get => _health; set => _health = value; }
    public bool Dead { get => _dead; set => _dead = value; }
    
    
    public void SetDamage( int damage)
    {
        if (_health >0 )
        {
            _health -= damage;
        }
        if (_health <=0)
        {
            _health = 0;
            if (tag != "Player")
            {
                _dead = true;
            }
        } 
    }
}
