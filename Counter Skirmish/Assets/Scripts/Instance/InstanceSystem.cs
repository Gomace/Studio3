using System;
using System.Collections;
using UnityEngine;

//public enum InstanceState { Start, PlayerAction, PlayerMove, EnemyMove, Busy }

public class InstanceSystem : MonoBehaviour
{
    [SerializeField] private InstanceUnit _playerUnit;
    [SerializeField] private InstanceUnit _enemyUnit;
    [SerializeField] private CharacterHud _playerHud;
    [SerializeField] private CharacterHud _enemyHud;
    //[SerializeField] private InstanceDialog _dialogBox;

    //private InstanceState _state;
    
    //private void Start() => SetupInstance(); // StartCoroutine(SetupInstance())

    public void SetupInstance() // IEnumerator
    {
        _playerUnit.Setup();
        _enemyUnit.Setup();
        _playerHud.SetData(_playerUnit.Creature);
        _enemyHud.SetData(_enemyUnit.Creature);

        //_dialogBox.SetAbilities(_playerUnit.Creature.Abilities);

        /*yield return _dialogBox.TypeDialog($"A wild {_enemyUnit.Creature.Base.Name} appeared.");
        yield return new WaitForSeconds(1f);

        PlayerAction();*/
    }

    /*private void PlayerAction()
    {
        _state = InstanceState.PlayerAction;
        StartCoroutine(_dialogBox.TypeDialog("Choose an action."));
        _dialogBox.EnableActionSelector(true);
    }*/
}
