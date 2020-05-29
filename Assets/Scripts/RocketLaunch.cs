using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Effects;

public class RocketLaunch : BaseObject
{
    private Transform flyght;
    [SerializeField] float _speed;
    [SerializeField] IRocket _iRocket;
    [SerializeField] GameObject _explosion;
    private Transform TMCam;
    public int _damage;
    protected override void Awake()
    {
        base.Awake();
    }
    void Start()
    {
        TMCam = Camera.main.transform;
        flyght = gameObject.transform;
        _explosion = Resources.Load<GameObject>("Prefabs/Explosion_1");
        _iRocket = GameObject.FindWithTag("Player").GetComponentInChildren<IRocket>();
        RocketState(_iRocket, false);
    }

   
    void Update()
    {
        flyght.Translate(0, 0, _speed + Time.deltaTime);
        flyght.rotation = TMCam.rotation;
        if (Input.GetButtonDown("Fire1"))
        {
            
            DestroyObject();

        }
    }
    

    

    public void RocketState(IRocket obj, bool flag)
    {
        obj.RocketFlight(flag);
    }



    public void OnCollisionEnter(Collision other)
    {
        DestroyObject();
    }

    private void DestroyObject()
    {
        Collider[] _objInCollider = Physics.OverlapSphere(GOtransform.position, 8);
        foreach (var item in _objInCollider)
        {
            SetDamage(item.gameObject.GetComponent<ISetDamage>());
            if (item.GetComponent<Rigidbody>())
            { 
                item.GetComponent<Rigidbody>().AddForce(((item.transform.position - GOtransform.position))*45, ForceMode.Impulse);
            }
        }
        Instantiate(_explosion, gameObject.transform.position, Quaternion.identity);
        RocketState(_iRocket, true);
        Destroy(gameObject);
    }
    private void SetDamage(ISetDamage obj)
    {
        if (obj != null)
        {
            obj.SetDamage(_damage);

        }
    }
}

  
