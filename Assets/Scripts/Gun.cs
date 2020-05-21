using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gun : BaseWeapon, IRocket
{
    [SerializeField] private int _stockBulletCount;
    [SerializeField] private float _shootDistance = 1000f;
    [SerializeField] private int _damage;
    [SerializeField] private bool prefab;
    [SerializeField] private int _totalBulletCount;
    private int _bulletCount;
    [SerializeField] private Transform[] _rocketBase;
    [SerializeField] private GameObject _rocket;
    [SerializeField] private Renderer[] _rocketBaseRenderer;
    int rocketBaseID = 0;
    public KeyCode reload = KeyCode.R;
    private GameObject _missle;
    private LineRenderer line;
    private Transform TMcam;
    private bool reloading = false;

    protected override void Awake()
    {
        base.Awake();
    }
    void Start()
    {
        _bulletCount = _stockBulletCount;
        TMcam = Camera.main.transform;
        if (prefab)
        {
            int i = 0;
            foreach(Transform child in GOtransform)
            {
                if (child.gameObject.tag == "Rocket")
                {
                    Array.Resize(ref _rocketBase, i + 1);
                    Array.Resize(ref _rocketBaseRenderer, i + 1);
                    _rocketBase[i] = child;
                    _rocketBaseRenderer[i] = child.GetComponentInChildren<Renderer>();
                    Debug.Log(_rocketBase[i], _rocketBaseRenderer[i]);
                    i++;
                }
            }
            _rocket = Resources.Load<GameObject>("Prefabs/Rocket");
           

        }
    }

    private void SetDamage(ISetDamage obj)
    {
        if (obj != null)
        {
            obj.SetDamage(_damage);
        }
    }
    private void CreateParticleHit(RaycastHit hit)
    {
        GameObject tempHit = Instantiate(_hitParticle, hit.point, Quaternion.identity);
        tempHit.transform.parent = hit.transform;
        Destroy(tempHit, UnityEngine.Random.Range(0.1f, 0.7f));
    }
    public override void Fire()
    {
        if (_bulletCount > 0 && _fire)
        {
            Animator.SetTrigger("Fire");
            if (!prefab)
            {
                _muzzle.Play();
            }
            _bulletCount--;



            if (prefab)
            {
                Instantiate(_rocket, _rocketBase[rocketBaseID].position, Quaternion.Euler(TMcam.eulerAngles));
                _rocketBaseRenderer[rocketBaseID].enabled = false;
                if(rocketBaseID>2)
                {
                    rocketBaseID = 0;
                }
                else
                {
                    rocketBaseID++;
                }
            }
            else
            {
                RaycastHit hit;
                Ray ray = new Ray(TMcam.position, TMcam.forward);
                if (Physics.Raycast(ray, out hit, _shootDistance))
                {
                    if (hit.collider.tag == "Player")
                    {
                        return;
                    }
                    else
                    {
                        SetDamage(hit.collider.GetComponent<ISetDamage>());
                        CreateParticleHit(hit);
                    }
                }
            }
        }
        else if (_fire || reloading)
        {
            Reload();
           
        }
    }
    private void Reload()
    {
        if (_totalBulletCount > 0)
        { 
            _fire = false;
            reloading = true;
            Animator.SetTrigger("Reload");
        }
    }

    private void Reloaded()
    {
        if (_stockBulletCount-_bulletCount > _totalBulletCount)
        {
            _bulletCount += _totalBulletCount;
            _totalBulletCount = 0;
            Debug.Log("Empty");
            
        }
        else
        {
            _totalBulletCount -= _stockBulletCount - _bulletCount;
            _bulletCount = _stockBulletCount;
            Debug.Log("NotEmpty");
        }
        if (prefab)
        {
            foreach (Renderer rocket in _rocketBaseRenderer)
            {
                rocket.enabled = true;
            }
            rocketBaseID = 0;
        }

        reloading = false;
        _fire = true;
    }
    void Update()
    {
        if(!_cross.GetComponent<Image>().enabled)
        {
            _cross.GetComponent<Image>().enabled = true;
            _ammo.enabled = true;
        }
        if (Input.GetButtonDown("Fire1"))
        {
            Fire();
        }
        if (Input.GetKeyDown(reload) && !reloading)
        {
            Reload();
        }
        _ammo.text = _bulletCount.ToString() + " / " +  _totalBulletCount.ToString();
    }

    public void RocketFlight(bool flag)
    {
        _fire = flag;
    }
}
