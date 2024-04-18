using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CollisionTransmitter : MonoBehaviour
{
    private Rigidbody _rb;
    
    private Stack<GameObject> _conjurations;
    private Ability _conjurer;
    private Creature _creature;
    private bool _initialized = false; 
    
    private List<Creature> _affected = new();
    private int _hits;
    private string[] _canAffect;
    private float _distance;

    private int _Hits
    {
        get => _hits;
        set
        {
            //Debug.Log($"The hit counter became {value}");
            if (value <= 0) // Might not need? ¯\_(ツ)_/¯
                AddToStack();
            
            _hits = value;
        }
    }

    private void Awake() => _rb = GetComponent<Rigidbody>();

    private void OnEnable()
    {
        if (_initialized)
            Activate();
    }

    public void Initialize(Stack<GameObject> conjStack, Ability source)
    {
        _conjurations = conjStack;
        _conjurer = source;
        _creature = source.Creature;
        
        _canAffect = source.Base.CanAffect(_creature.Unit.gameObject); // find tags through checking enemy vs ally
        
        _initialized = true;
    }

    private void Activate()
    {
        _affected.Clear();
        _hits = _conjurer.Base.Hits;

        _distance = 0;
        _rb.AddForce(transform.forward * _conjurer.Base.Force);
    }

    private void FixedUpdate()
    {
        if (_conjurer.Base.IndHitBox.z < _distance)
            AddToStack();
        
        // transform.Translate(transform.forward * (_conjurer.Base.Force * Time.deltaTime), Space.World);
        _distance += _rb.velocity.magnitude * (Time.fixedDeltaTime / _conjurer.Base.Deviation);
    }

    private void OnTriggerEnter(Collider other)
    {
        foreach (string hitTag in _canAffect)
        {
            if (other.CompareTag(hitTag))
                Affect(other.gameObject.GetComponent<InstanceUnit>().Creature);
        }
    }
    
    private void Affect(Creature hitTarget)
    {
        if (_affected.Contains(hitTarget) || _hits < 1)
            return;
        
        _Hits -= 1;
        _affected.Add(hitTarget);
        hitTarget.TakeDamage(_conjurer, _creature);
    }
    
    private void AddToStack()
    {
        // GameObject dissipate animation
        gameObject.SetActive(false);
        _rb.velocity = Vector3.zero;
        _conjurations.Push(gameObject);
    }
}