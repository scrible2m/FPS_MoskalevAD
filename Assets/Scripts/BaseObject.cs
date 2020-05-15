using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseObject : MonoBehaviour
{
    private Transform _GOtransform;
    private GameObject _GOInstance;
    private string _name;
    private bool _isVisible;

    private Vector3 _position;
    private Vector3 _scale;
    private Quaternion _rotation;

    private Rigidbody _rigidbody;
    private Material _material;
    private Color _color;

    private Animator _animator;

    protected Transform GOtransform { get => _GOtransform; set => _GOtransform = value; }
    protected GameObject GOInstance { get => _GOInstance;}
    protected string Name 
    { get => _name;
        set
        {
            _name = value;
            gameObject.name = _name;
        }
    }
    protected bool IsVisible 
    { get => _isVisible;
        set
        {
            _isVisible = value;
            if (GetComponent<Renderer>())
            {
                GetComponent<Renderer>().enabled = _isVisible;
            }
        }
    }
    protected Vector3 Position { get => _position; set => _position = value; }
    protected Vector3 Scale 
    { get
        {
            _scale = _GOtransform.localScale;
            return _scale;
        }

        set
        {
            _scale = value;
            _GOtransform.localScale = _scale;
        }
    }
    protected Quaternion Rotation { get => _rotation; set => _rotation = value; }
    protected Rigidbody Rogodbody { get => _rigidbody;}
    protected Material Material { get => _material;}
    protected Color Color { get => _color; set => _color = value; }
    protected Animator Animator { get => _animator;}

   
    protected virtual void Awake()
    {
        _GOInstance = gameObject;
        _GOtransform = gameObject.transform;
        _name = gameObject.name;

        if (GetComponent<Rigidbody>())
        {
            _rigidbody = GetComponent<Rigidbody>();
        }
        if (GetComponent<Animator>())
        {
            _animator = GetComponent<Animator>();
        }
        if (GetComponent<Renderer>())
        {
           _material = GetComponent<Renderer>().material;
        
        _color = GetComponent<Renderer>().material.color;
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
