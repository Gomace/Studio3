using System.Collections.Generic;
using UnityEngine;

public class ParentActiveToggle : MonoBehaviour, CollisionTransmitter
{
    [SerializeField] private ConjuredAbility _transmitter;

    private Stack<GameObject> _conjurations;
    private Stack<GameObject> _decoyConjs = new();
    
    private Vector3 _startPos;

    private bool _initialized = false;
    
    private void OnEnable()
    {
        _transmitter.onConjDisable += TurnOffSelf;
        
        if (_initialized)
            Activate();
    }
    private void OnDisable() => _transmitter.onConjDisable -= TurnOffSelf;

    public void Initialize(Stack<GameObject> conjStack, Ability source)
    {
        _conjurations = conjStack;

        _startPos = _transmitter.transform.localPosition;
        _transmitter.Initialize(_decoyConjs, source);
        
        _initialized = true;
    }

    private void Activate()
    {
        _decoyConjs.Clear();
        
        _transmitter.transform.localPosition = _startPos;
        _transmitter.gameObject.SetActive(true);
    }

    private void TurnOffSelf()
    {
        gameObject.SetActive(false);
        _conjurations.Push(gameObject);
    }
}