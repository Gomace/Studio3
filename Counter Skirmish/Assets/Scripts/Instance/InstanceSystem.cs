using System;
using System.Collections;
using UnityEngine;

//public enum InstanceState { Start, PlayerAction, PlayerAbility, EnemyAbility, Busy }

public class InstanceSystem : MonoBehaviour
{
    [SerializeField] private InstanceUnit _playerUnit;
    [SerializeField] private InstanceUnit _enemyUnit;
    [SerializeField] private CharacterHud _playerHud;
    [SerializeField] private CharacterHud _enemyHud;
    //[SerializeField] private InstanceDialog _dialogBox;
    
    //private InstanceState _state;
    //private int _currentAction;
    
    //private void Start() => SetupInstance(); // StartCoroutine(SetupInstance())

    public void SetupInstance() // IEnumerator
    {
        _playerUnit.Setup();
        _enemyUnit.Setup();
        _playerHud.SetData(_playerUnit.Creature);
        _enemyHud.SetData(_enemyUnit.Creature);

        //_dialogBox.SetAbilityNames(_playerUnit.Creature.Abilities);

        /*yield return _dialogBox.TypeDialog($"A wild {_enemyUnit.Creature.Base.Name} appeared.");
        yield return new WaitForSeconds(1f);

        PlayerAction();*/
    }

    /*private void PlayerAction()
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
    
    IEnumerator PerformPlayerAbility()
    {
        _state = InstanceState.Busy;
        
        Ability ability = _playerUnit.Creature.Abilities[_currentAbility];
        yield return _dialogBox.TypeDialog($"{_playerUnit.Base.Name} used {ability.Base.Name}");
        
        yield return new WaitForSeconds(1f);
        
        bool isDead = _enemyUnit.Creature.TakeDamage(ability, _playerUnit.Creature);
        yield return _enemyHud.UpdateHealth();
        
        if (isDead)
            yield return _dialogBox.TypeDialog($"{_enemyUnit.Creature.Base.Name} died");
        else
            StartCoroutine(EnemyAbility());
    }
    
    IEnumerator EnemyAbility()
    {
        _state = InstanceState.EnemyAbility;
        
        Ability ability = _enemyUnit.Creature.GetRandomAbility();
        yield return _dialogBox.TypeDialog($"{_enemyUnit.Base.Name} used {ability.Base.Name}");
        
        yield return new WaitForSeconds(1f);
        
        bool isDead = _playerUnit.Creature.TakeDamage(ability, _playerUnit.Creature);
        yield return _playerHud.UpdateHealth();
        
        if (isDead)
            yield return _dialogBox.TypeDialog($"{_playerUnit.Creature.Base.Name} died");
        else
            PlayerAction();
    }
    
    private void Update()
    {
        if (_state == InstanceState.PlayerAction)
            HandleActionSelection();
        else if (_state == InstanceState.PlayerAbility)
            HandleAbilitySelection();
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
    */
}
