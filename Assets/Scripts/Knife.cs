using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Knife : BaseWeapon
{
   
    [SerializeField] private int _stockBulletCount;
    [SerializeField] private float _shootDistance = 7f;
    [SerializeField] private int _damage;
    [SerializeField] private bool prefab;
    private int _bulletCount;
    private Transform TMcam;


    protected override void Awake()
    {
        base.Awake();
    }
    void Start()
    {
        TMcam = Camera.main.transform;
        if (prefab)
        {
            //GetPrefab
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
        Destroy(tempHit, Random.Range(0.1f, 0.7f));
    }
    public override void Fire()
    {


        Animator.SetTrigger("Fire");
        if (prefab)
        {

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
                   
                }
            }
        }
    }



    void Update()
    {
        if (_cross.GetComponent<Image>().enabled)
        {
            _cross.GetComponent<Image>().enabled = false;
            _ammo.enabled = false;
        }
        if (Input.GetButtonDown("Fire1"))
        {
        
            Fire();
        }

    }
}


