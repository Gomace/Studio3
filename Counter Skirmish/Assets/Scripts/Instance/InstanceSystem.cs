using System;
using System.Collections;
using UnityEngine;

//public enum InstanceState { Start, PlayerAction, PlayerMove, EnemyMove, Busy }

public class InstanceSystem : MonoBehaviour
{
    [SerializeField] private InstanceUnit playerUnit;
    [SerializeField] private InstanceUnit enemyUnit;
    [SerializeField] private CharacterHud playerHud;
    [SerializeField] private CharacterHud enemyHud;
    //[SerializeField] private InstanceDialog dialogBox;

    //private InstanceState state;
    
    //private void Start() => SetupInstance(); // StartCoroutine(SetupInstance())

    public void SetupInstance() // IEnumerator
    {
        playerUnit.Setup();
        enemyUnit.Setup();
        playerHud.SetData(playerUnit.Creature);
        enemyHud.SetData(enemyUnit.Creature);

        //dialogBox.SetAbilities(playerUnit.Creature.Abilities);

        /*yield return dialogBox.TypeDialog($"A wild {enemyUnit.Creature.Base.Name} appeared.");
        yield return new WaitForSeconds(1f);

        PlayerAction();*/
    }

    /*private void PlayerAction()
    {
        state = InstanceState.PlayerAction;
        StartCoroutine(dialogBox.TypeDialog("Choose an action."));
        dialogBox.EnableActionSelector(true);
    }*/
}
