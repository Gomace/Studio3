using System.Collections.Generic;
using UnityEngine;

public interface CollisionTransmitter
{
    public void Initialize(Stack<GameObject> conjStack, Ability source);
}

[RequireComponent(typeof(Rigidbody))]
public class ConjuredAbility : MonoBehaviour, CollisionTransmitter
{
    #region Events
    /*public delegate void OnConjEnable();
    public event OnConjEnable onConjEnable;*/
    public delegate void OnConjDisable();
    public event OnConjDisable onConjDisable;
    #endregion Events
    
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
            if (value <= 0) // Might not need? ¯\_(ツ)_/¯
                AddToStack();
            
            _hits = value;
        }
    }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.mass = 1;
        _rb.drag = _rb.angularDrag = 0;
        _rb.useGravity = _rb.isKinematic = false;
        _rb.interpolation = RigidbodyInterpolation.Interpolate;
        _rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
    }
    
    private void OnEnable()
    {
        // onConjEnable?.Invoke();
        
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
        onConjDisable?.Invoke();
        _rb.velocity = Vector3.zero;
        gameObject.SetActive(false);
        _conjurations.Push(gameObject);
    }
}