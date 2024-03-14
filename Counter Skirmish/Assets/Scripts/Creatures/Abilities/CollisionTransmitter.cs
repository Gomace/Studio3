using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class CollisionTransmitter : MonoBehaviour
{
    private Stack<GameObject> _conjurations;
    private Ability _conjurer;
    private Creature _creature;
    
    private List<Creature> _affected = new();
    private int _hits;
    private string[] _canAffect;
    private bool _initialized = false;
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

    private void OnEnable()
    {
        if (_initialized)
            Activate();
    }

    public void Initialize(Stack<GameObject> conjStack, Creature caster, Ability source)
    {
        _conjurations = conjStack;
        _creature = caster;
        _conjurer = source;
        
        _canAffect = source.Base.CanAffect; // find tags through checking enemy vs ally
        
        _initialized = true;
    }

    private void Activate()
    {
        _affected.Clear();
        _hits = _conjurer.Base.Hits;

        _distance = 0;
        GetComponent<Rigidbody>().AddForce(transform.forward * _conjurer.Base.AbiClass.Speed);
    }

    private void FixedUpdate()
    {
        if (_conjurer.Base.IndHitBox.z < _distance)
            AddToStack();
        
        //transform.Translate(transform.forward * (_conjurer.Base.AbiClass.Speed * Time.deltaTime), Space.World);
        //_distance += _conjurer.Base.AbiClass.Speed * Time.fixedDeltaTime;
    }

    private void OnCollisionEnter(Collision other)
    {
        foreach (string hitTag in _canAffect)
        {
            if (other.gameObject.CompareTag(hitTag))
                Affect(other.gameObject.GetComponent<InstanceUnit>().Creature);
        }
    }
    
    private void Affect(Creature hitTarget)
    {
        if (_affected.Contains(hitTarget) || _hits < 1)
            return;
        
        _affected.Add(hitTarget);
        hitTarget.TakeDamage(_conjurer, _creature);
        _Hits -= 1;
        
        //_enemyHud.UpdateHealth();
    }
    
    private void AddToStack() 
    {
        // GameObject dissipate animation
        gameObject.SetActive(false);
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        _conjurations.Push(gameObject);
    }
}
