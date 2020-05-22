using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(NavMeshAgent))]

public class Bot : Unit, IAngry
{
    private NavMeshAgent _agent;
    private Transform _playerPos;
    [SerializeField] private Transform[] TargetBase;
    [SerializeField] private Transform Target;
    private int i = 0;
    private int j;
    [SerializeField] private bool _agr;
    private Transform _deltaTransform;

    protected override void Awake()
    {
        base.Awake();
        Health = 100;
        Dead = false;
        _agent = GetComponent<NavMeshAgent>();
        _agent.updatePosition = true;
        _agent.updateRotation = true;

        _playerPos = FindObjectOfType<SinglePlayer>().transform;
        _agent.stoppingDistance = 1.5f;
        Transform temp = GameObject.FindWithTag("Target").transform;
        Array.Resize(ref TargetBase, temp.childCount);
        for (int k = 0; k<temp.childCount;k++)
        {
          TargetBase[k]  = temp.GetChild(k);
        }
        //_deltaTransform.position = new Vector3(_agent.transform.position.x, _agent.transform.position.y, _agent.transform.position.z);
        Target = TargetBase[i];
        _agent.SetDestination(Target.position);

    }
    

     void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "TargetPoint" && !_agr)
        {
            Debug.Log(gameObject.name + " Enteret " + other.name);
            do
            {
                 j = Mathf.RoundToInt(UnityEngine.Random.Range(0f, TargetBase.Length - 1));


            }
            while (i == j);
            Target = TargetBase[j];
            _agent.SetDestination(Target.position);
        }
    }


    void Update()
    {
        
        //if (!_agent.po)
        //{
        //    Animator.SetBool("Move", true);
        //}
        //else
        //{
        //    Animator.SetBool("Move", false);
        //}
        if (Health <= 0)
        {
            Animator.SetTrigger("Death");
            Destroy(gameObject, 3);
        }

        if (_agent.remainingDistance <1.8f && _agr)
        {
            Animator.SetTrigger("Attack");
        }
        //_deltaTransform.position = new Vector3(_agent.transform.position.x, _agent.transform.position.y, _agent.transform.position.z);
    }

    public void Agr()
    {
        _agent.SetDestination(_playerPos.position);
        _agr = true;
    }
}
 