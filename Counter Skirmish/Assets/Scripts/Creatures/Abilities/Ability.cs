using System.Collections.Generic;
using UnityEngine;

public class Ability
{
    [SerializeField] private AbilityBase _base;
    
    private Stack<GameObject> _conjurations = new();

    private float _cooldown;

    public AbilityBase Base
    {
        get => _base;
        set => _base = value;
    }
    public Creature Creature { get; private set; }

    public float Cooldown
    {
        get => _cooldown;
        set => _cooldown = Mathf.Max(value, 0f);
    }

    public Ability(AbilityBase aBase, Creature creature)
    {
        _base = aBase;
        Creature = creature;

        Cooldown = 0;
    }

    public void Cast(Vector3 mouse) // Cast Ability
    {
        if (!_conjurations.TryPop(out GameObject conjuration)) // Check for used model
        {
            conjuration = GameObject.Instantiate(_base.Model);
            conjuration.GetComponent<CollisionTransmitter>().Initialize(_conjurations, this);
        }
        
        ConjTransform(conjuration.transform, Creature.Unit, mouse);
        conjuration.gameObject.SetActive(true);
        
        //Debug.Log($"{creature.Base.Name} cast a {Base.Name}");
    }
    
    private void ConjTransform(Transform conj, InstanceUnit unit, Vector3 mouse)
    {
        Vector3 unitPos = unit.transform.position;
        
        Quaternion conjRot = Quaternion.LookRotation(mouse - unitPos, Vector3.up); // Rotation
        conjRot.eulerAngles = new Vector3(0f, conjRot.eulerAngles.y, 0);
        conj.rotation = conjRot;
        
        unit.Character.rotation = conjRot; // Face creature to cast angle

        unitPos += _base.Model.transform.localPosition; // Position
        conj.position = unitPos;
                
        conj.localScale = new Vector3(_base.IndHitBox.x, _base.IndHitBox.x, _base.IndHitBox.x); // Scale
    }
}