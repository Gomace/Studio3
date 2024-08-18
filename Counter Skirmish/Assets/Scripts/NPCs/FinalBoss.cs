using UnityEngine;

[RequireComponent(typeof(CreatureRoster))]
public class FinalBoss : MonoBehaviour
{
    private CreatureRoster _roster;
    
    private void Awake() => _roster = GetComponent<CreatureRoster>();

    private void OnEnable() => _roster.onFullDead += SetWin;
    private void OnDisable() => _roster.onFullDead -= SetWin;

    private void SetWin()
    { 
        HuntEnd.Win = true;
        HuntEnd.WinGame();
    }
}