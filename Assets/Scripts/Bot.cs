using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEditor;
using System.Runtime.CompilerServices;

[RequireComponent(typeof(NavMeshAgent))]

public class Bot : Unit
{
    private NavMeshAgent _agent;
    private Transform _playerPos;
    private bool _grounded;
    private float _groundChkDst = 0.1f;
    

    [SerializeField] private float _stopDistance = 0.2f;
    [SerializeField] private float _attakDistance = 4f;
    [SerializeField] private float _seekDistance = 3f;
    [SerializeField] private float _shootDistance;
    [SerializeField] private float _speed;

    [Header("Bot WaiPoints")]
    [SerializeField] List<Vector3> _wayPoints = new List<Vector3>();
    private int _pointCounter = 0;
    private GameObject _wayPointMain;

    private float _timeWait = 4f;
    private float _timeOut=0f;
    private float delay;

    [SerializeField] private bool _patrol;
    [SerializeField] private bool _attack;
    [SerializeField] private bool _isTarget;

    [SerializeField] private List<Transform> _visibleTargets = new List<Transform>();
    [SerializeField] private float _maxAngle = 35f;
    [SerializeField] private float _maxRadius = 30f;
    [SerializeField] private LayerMask _targetLayer;
    [SerializeField] private LayerMask _obstacleLayer;
    [SerializeField] private bool archer = false;
    [SerializeField] private ISetDamage _iSetDamage;

#if UNITY_EDITOR
    [ContextMenu("Units/Mutant")]
    public void DefaultMutant()
    {
        Health = 80;
        _stopDistance = 0.2f;
        _attakDistance = 3f;
        _damage = 8;
        _patrol = true;
        archer = false;
        _maxAngle = 35;
        _maxRadius = 20;
        _speed = 6;
        delay = 3f;
        _shootDistance = 3;
        
    }
    [ContextMenu("Units/Archer")]
    public void DefaultArcher()
    {
        Health = 100;
        _stopDistance = 0.2f;
        _attakDistance = 45f;
        _seekDistance = 20f;
        _damage = 15;
        _patrol = true;
        archer = true;
        _maxAngle = 35;
        _maxRadius = 50;
        _speed = 5;
        delay = 1f;
        _shootDistance = 500f;

    }

#endif


    [SerializeField] private Vector3 Target;
    private int i = 0;
    private int j;
    private Vector3  _startPos;
    private float _spawnTimer = 9f;
    private bool _startTimer = false;
    private int _startHealth;
    [SerializeField] int _damage;

    IEnumerator Attack()
    {
        while (true)
        {

            Animator.SetTrigger("Attack");
            RB.isKinematic = true;
            yield return new WaitForSeconds(delay);
        }
    }
    IEnumerator FindTargets(float _delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(_delay);
            FindVisibleTargets();
        }
    }
    protected override void Awake()
    {
        base.Awake();
        Dead = false;
        _agent = GetComponent<NavMeshAgent>();
        _agent.updatePosition = true;
        _agent.updateRotation = true;

        _playerPos = FindObjectOfType<SinglePlayer>().transform;
        _agent.stoppingDistance = _stopDistance;

        _wayPointMain = FindObjectOfType<PathWaypoints>().gameObject;
        foreach (Transform T in _wayPointMain.transform)
        {
            _wayPoints.Add(T.position);
        }
        _patrol = true;
        _agent.speed = _speed;

        _startPos = Position;
        _startHealth = Health;

        StartCoroutine("FindTargets", 0.1f);

    }

    private void FindVisibleTargets()
    {
   Collider[] _targetInViewRadius = Physics.OverlapSphere(transform.position, _maxRadius, _targetLayer);
        for (int i = 0; i< _targetInViewRadius.Length; i++)
        {
            
            Transform target = _targetInViewRadius[i].transform;
            Vector3 dirToTarget = (target.position - GOtransform.position).normalized;
            float targetAngle = Vector3.Angle(transform.forward, dirToTarget);
            if ((-_maxAngle)<targetAngle && _maxAngle>targetAngle)
            {
                float distToTarget = Vector3.Distance(GOtransform.position, target.position);
                if(!Physics.Raycast((transform.position+Vector3.up), dirToTarget, _obstacleLayer))
                {
                    if(!_visibleTargets.Contains(target))
                    {
                        _visibleTargets.Add(target);
                    }
                }
            }
        }
        
    }
    void Update()
    {
        if (_visibleTargets.Count > 0)
        {
            _patrol = false;
            Target = _visibleTargets[0].position;
            if (Vector3.Distance(Position, Target) > _maxRadius)
            {
                _visibleTargets.Clear();
            }
        }
        else
        {
            _patrol = true;
        }
        if (_patrol)
        {
            _agent.speed = _speed;
            if(archer)
            {
                Animator.SetBool("Run", false);
            }
            if (_wayPoints.Count > 1)
            {
                _agent.stoppingDistance = _stopDistance;
                _agent.SetDestination(_wayPoints[_pointCounter]);
                if (_agent.remainingDistance<0.5f)
                {
                    RB.isKinematic = true;
                    _timeOut += Time.deltaTime;
                    if (_timeOut > _timeWait)
                    { 
                        _timeOut = 0;
                        if (_pointCounter < _wayPoints.Count - 1)
                        {
                            _pointCounter++;
                        }
                        else
                        {
                            _pointCounter = 0;
                        }
                        RB.isKinematic = false;
                    }
                
                }
            }
            else
            {
                _agent.SetDestination(_playerPos.position);
                _agent.stoppingDistance = _attakDistance;
                if (archer)
                {
                    _agent.speed = _speed * 1.7f;
                    Animator.SetBool("Run", true);
                }
            }
        }
        else
        {
            _agent.stoppingDistance = _attakDistance;
            _agent.SetDestination(Target);
            Vector3 pos = transform.position + Vector3.up;
            Ray ray = new Ray(pos, transform.forward);
            RaycastHit hit;
            transform.LookAt(new Vector3(Target.x, transform.position.y, Target.z));
            if (Physics.Raycast(ray, out hit, _shootDistance, _targetLayer))
            {
                if ((hit.collider.tag == "Player" || hit.collider.tag == "Enemy") && !_attack)
                {
                    _attack = true;
                    _agent.ResetPath();
                    _iSetDamage = hit.collider.GetComponent<ISetDamage>();
                    StartCoroutine("Attack");
                    
                    
                }
                else if (archer)
                {
                    _agent.stoppingDistance = _seekDistance;
                    _agent.SetDestination(Target);
                    StopCoroutine("Attack");
                    _attack = false;
                    RB.isKinematic = false;
                    _agent.speed = _speed *2f;
                    Animator.SetBool("Run", true);
                }
               
            }
            else
            {
                _agent.SetDestination(Target);
                StopCoroutine("Attack");
                _attack = false;
                RB.isKinematic = false;
                _iSetDamage = null;
            }
        }
        
        if (Health <= _startHealth*0.2)
        {
            Target = GameObject.FindGameObjectWithTag("HealthPack").transform.position;
            _agent.SetDestination(Target);
            _patrol = false;
            if (archer)
            {
                _agent.speed = _speed * 2;
                Animator.SetBool("Run", false);

            }
            
        }
        if (Health >= _startHealth)
        {
            Health = _startHealth;
            _patrol = true;
        }

        if (_agent.remainingDistance>_agent.stoppingDistance)
        {
            Animator.SetBool("Move", true);
        }
        else
        {
            Animator.SetBool("Move", false);
        }

        if (Health <= 0)
        {
            _agent.ResetPath();
            RB.isKinematic = true;
            Animator.SetBool("Death", true);
            _startTimer = true;
        }

        if (_startTimer)
        {
            _spawnTimer -= Time.deltaTime;
            if (_spawnTimer <=0)
            {
                Spawn();
            }
        }

       
        
    }

    private void Damage()
    {
        if (!Dead)
        {
            if (_iSetDamage != null)
            {
                _iSetDamage.SetDamage(_damage);
            }
        }
    }

    private void Spawn()
    {
        Position = _startPos;
        IsVisible = true;
        Health = _startHealth;
        Dead = false;
        Animator.SetBool("Death", false);
        Animator.SetTrigger("Idle");
        _spawnTimer = 9f;
        _startTimer = false;
        RB.isKinematic = false;
    }
}
 