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



    public void OnTriggerEnter(Collider other)
    {
        DestroyObject();
    }

    private void DestroyObject()
    {
        Instantiate(_explosion, gameObject.transform.position, Quaternion.identity);
        RocketState(_iRocket, true);
        Destroy(gameObject);
    }
}

  
