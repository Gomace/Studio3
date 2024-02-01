using System;
using System.Collections;
using UnityEngine;

public enum InstanceState { Start, PlayerAction, PlayerAbility, EnemyAbility, Busy }

public class InstanceSystem : MonoBehaviour
{
    [SerializeField] private InstanceUnit _playerUnit;
    [SerializeField] private InstanceUnit _enemyUnit;
    [SerializeField] private CharacterHud _playerHud;
    [SerializeField] private CharacterHud _enemyHud;
    [SerializeField] private InstanceDialog _dialogBox;
    
    private InstanceState _state;
    private int _currentAction;
    private int _currentAbility; // DialogBox menu

    private void Start() => /*SetupInstance();*/ StartCoroutine(SetupInstance());

    private void Update()
    {
        if (_state == InstanceState.PlayerAction)
            HandleActionSelection();
        else if (_state == InstanceState.PlayerAbility)
            HandleAbilitySelection();
    }
    
    public IEnumerator SetupInstance() // IEnumerator
    {
        _playerUnit.Setup();
        _enemyUnit.Setup();
        _playerHud.SetData(_playerUnit.Creature);
        _enemyHud.SetData(_enemyUnit.Creature);

        _dialogBox.SetAbilityNames(_playerUnit.Creature.Abilities);

        yield return _dialogBox.TypeDialog($"A wild {_enemyUnit.Creature.Base.Name} appeared.");

        PlayerAction();
    }

    private void PlayerAction()
    {
        _state = InstanceState.PlayerAction;
        StartCoroutine(_dialogBox.TypeDialog("Choose an action."));
        _dialogBox.EnableActionSelector(true);
    }
    
    private void PlayerAbility()
    {
        _state = InstanceState.PlayerAbility;
        _dialogBox.EnableActionSelector(false);
        _dialogBox.EnableDialogText(false);
        _dialogBox.EnableAbilitySelector(true);
    }
    
    private IEnumerator PerformPlayerAbility()
    {
        _state = InstanceState.Busy;
        
        Ability ability = _playerUnit.Creature.Abilities[_currentAbility];
        yield return _dialogBox.TypeDialog($"{_playerUnit.Base.Name} used {ability.Base.Name}");
        
        _playerUnit.PlayAttackAnim();
        yield return new WaitForSeconds(1f);
        
        _enemyUnit.PlayHitAnim();
        DamageDetails damageDetails = _enemyUnit.Creature.TakeDamage(ability, _playerUnit.Creature);
        yield return _enemyHud.UpdateHealth();
        yield return ShowDamageDetails(damageDetails);

        if (damageDetails.Fainted)
        {
            yield return _dialogBox.TypeDialog($"{_enemyUnit.Creature.Base.Name} died");
            _enemyUnit.PlayFaintAnim();
        }
        else
            StartCoroutine(EnemyAbility());
    }
    
    private IEnumerator EnemyAbility()
    {
        _state = InstanceState.EnemyAbility;
        
        Ability ability = _enemyUnit.Creature.GetRandomAbility();
        yield return _dialogBox.TypeDialog($"{_enemyUnit.Base.Name} used {ability.Base.Name}");
        
        _enemyUnit.PlayAttackAnim();
        yield return new WaitForSeconds(1f);
        
        _playerUnit.PlayHitAnim();
        DamageDetails damageDetails = _playerUnit.Creature.TakeDamage(ability, _playerUnit.Creature);
        yield return _playerHud.UpdateHealth();
        yield return ShowDamageDetails(damageDetails);

        if (damageDetails.Fainted)
        {
            yield return _dialogBox.TypeDialog($"{_playerUnit.Creature.Base.Name} died");
            _playerUnit.PlayFaintAnim();
        }
        else
            PlayerAction();
    }

    private IEnumerator ShowDamageDetails(DamageDetails damageDetails)
    {
        if (damageDetails.Critical > 1f)
            yield return _dialogBox.TypeDialog("A critical hit!");
        
        if (damageDetails.TypeEffectiveness > 1f)
            yield return _dialogBox.TypeDialog("It's super effective!");
        else if (damageDetails.TypeEffectiveness < 1f)
            yield return _dialogBox.TypeDialog("It's not very effective.");
    }

    private void HandleActionSelection()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (_currentAction < 1)
                ++_currentAction;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (_currentAction > 0)
                --_currentAction;
        }
        
        _dialogBox.UpdateActionSelection(_currentAction);
        
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (_currentAction == 0)
            {
                // Fight
                PlayerAbility();
            }
            else if (_currentAction == 1)
            {
                // Run
            }
        }
    }
    
    
    private void HandleAbilitySelection()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (_currentAbility < _playerUnit.Creature.Abilities.Count - 1)
                ++_currentAbility;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (_currentAbility > 0)
                --_currentAbility;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (_currentAbility < _playerUnit.Creature.Abilities.Count - 2)
                _currentAbility += 2;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (_currentAbility > 1)
                _currentAbility -= 2;
        }
        
        _dialogBox.UpdateAbilitySelection(_currentAbility, _playerUnit.Creature.Abilities[_currentAbility]);
        
        if (Input.GetKeyDown(KeyCode.Z))
        {
            _dialogBox.EnableAbilitySelector(false);
            _dialogBox.EnableDialogText(true);
            StartCoroutine(PerformPlayerAbility());
        }
    }
}
