using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Effects;

public class RocketLaunch : MonoBehaviour
{
    private Transform flyght;
    [SerializeField] float _speed;
    [SerializeField] IRocket _iRocket;
    [SerializeField] GameObject _explosion;
    private Transform TMCam;
    public int _damage;
    HashSet<GameObject> _objInCollider = new HashSet<GameObject>();
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
    

    void OnTriggerEnter(Collider other)
    {
        _objInCollider.Add(other.gameObject);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        _objInCollider.Remove(other.gameObject);
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
        foreach (var item in _objInCollider)
        {
            SetDamage(item.gameObject.GetComponent<ISetDamage>());
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

  
