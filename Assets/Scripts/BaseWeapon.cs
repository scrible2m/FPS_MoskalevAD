using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseWeapon : BaseObject
{
    protected Transform _gunT;
    protected ParticleSystem _muzzle;
    protected GameObject _hitParticle;
    [SerializeField] protected GameObject _cross;
    protected bool _fire = true;
    [SerializeField] protected AudioClip[] _gunSound;
    protected Text _ammo;
    protected override void Awake()
    {
        base.Awake();
        _gunT = GOtransform.GetChild(2);
        _muzzle = GetComponentInChildren<ParticleSystem>();
        _cross = GameObject.FindWithTag("Cross");
        _hitParticle = Resources.Load<GameObject>("Prefabs/Flare");
        _ammo = GameObject.FindWithTag("Ammo").GetComponent<Text>();
    }

    public abstract void Fire();
    
    void Update()
    {
        
    }
}
