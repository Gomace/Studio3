using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstanceSystem : MonoBehaviour
{
    [SerializeField] private InstanceUnit playerUnit;
    [SerializeField] private InstanceUnit enemyUnit;
    [SerializeField] private InstanceHud playerHud;
    [SerializeField] private InstanceHud enemyHud;

    private void Start()
    {
        SetupInstance();
    }

    public void SetupInstance()
    {
        playerUnit.Setup();
        enemyUnit.Setup();
        playerHud.SetData(playerUnit.Creature);
        enemyHud.SetData(enemyUnit.Creature);
    }
}
