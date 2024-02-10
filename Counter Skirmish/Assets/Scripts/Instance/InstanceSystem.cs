using System;
using System.Collections;
using UnityEngine;

public class InstanceSystem : MonoBehaviour
{
    public delegate void OnLoadInstance();
    public static event OnLoadInstance onLoadInstance;
    
    [SerializeField] private InstanceUnit _playerUnit;
    [SerializeField] private InstanceUnit _enemyUnit;
    [SerializeField] private CharacterHud _playerHud;
    [SerializeField] private CharacterHud _enemyHud;

    private void Start() => SetupInstance();

    private void SetupInstance()
    {
        onLoadInstance?.Invoke();
        
        _playerHud.SetupHUD(_playerUnit.Creature);
        _enemyHud.SetupHUD(_enemyUnit.Creature);
    }

    private void PerformPlayerAbility()
    {
        // Unit state = casting;

        Ability ability = _playerUnit.Creature.Abilities[0]; // Catch ability from creature
        _playerUnit.Creature.Resource--; // Spend resource
        
        _playerUnit.Creature.PlayAttackAnim(); // Attacking animation
        
        //_enemyUnit.PlayHitAnim(); // Play this on its own by the enemy, not here
        _enemyUnit.Creature.TakeDamage(ability, _playerUnit.Creature);
        _enemyHud.UpdateHealth();
    }
}
