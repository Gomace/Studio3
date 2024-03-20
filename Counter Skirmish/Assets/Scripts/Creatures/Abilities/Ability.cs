using System.Collections.Generic;
using UnityEngine;

public class Ability
{
    private Stack<GameObject> _conjurations = new();

    private float _cooldown;
    
    public AbilityBase Base { get; set; }
    public Creature Creature { get; set; }

    public float Cooldown
    {
        get => _cooldown;
        set => _cooldown = Mathf.Max(value, 0f);
    }

    public Ability(AbilityBase aBase, Creature creature)
    {
        Base = aBase;
        Creature = creature;

        Cooldown = 0;
    }

    public void Cast(Vector3 mouse) // Cast Ability
    {
        if (!_conjurations.TryPop(out GameObject conjuration)) // Check for used model
        {
            conjuration = GameObject.Instantiate(Base.Model);
            conjuration.GetComponent<CollisionTransmitter>().Initialize(_conjurations, this);
        }
        
        ConjTransform(conjuration.transform, Creature.Unit.transform.position, mouse);
        conjuration.gameObject.SetActive(true);
        
        //Debug.Log($"{creature.Base.Name} cast a {Base.Name}");
    }
    
    private void ConjTransform(Transform conj, Vector3 unitPos, Vector3 mouse)
    {
        Quaternion conjRot = Quaternion.LookRotation(mouse - unitPos, Vector3.up); // Rotation
        conjRot.eulerAngles = new Vector3(0f, conjRot.eulerAngles.y, 0);
        conj.rotation = conjRot;
        
        unitPos += Base.Model.transform.localPosition; // Position
        conj.position = unitPos;
                
        conj.localScale = new Vector3(Base.IndHitBox.x, Base.IndHitBox.x, Base.IndHitBox.x); // Scale
    }
}