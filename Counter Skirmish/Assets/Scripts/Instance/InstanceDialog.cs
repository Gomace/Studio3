using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InstanceDialog : MonoBehaviour
{
    [SerializeField] private int _lettersPerSec;
    
    [SerializeField] private TMP_Text _dialog;
    [SerializeField] private GameObject _actionSel, _abilitySel, _abilityDet;
    
    [SerializeField] private List<TMP_Text> _actionTxts, _abilityTxts;
    
    [SerializeField] private TMP_Text _resourceTxts, _typeTxts;

    public void SetDialog(string dialog) => _dialog.text = dialog;

    public IEnumerator TypeDialog(string dialog)
    {
        _dialog.text = "";
        foreach (char letter in dialog.ToCharArray())
        {
            _dialog.text += letter;
            yield return new WaitForSeconds(1f/_lettersPerSec);
        }
    }

    public void EnableDialogText(bool enabled) => _dialog.enabled = enabled;
    public void EnableActionSelector(bool enabled) => _actionSel.SetActive(enabled);

    public void SetAbilities(List<Ability> abilities)
    {
        for (int i = 0; i < _abilityTxts.Count; ++i)
        {
            if (i < abilities.Count)
                _abilityTxts[i].text = abilities[i].Base.Name;
            else
                _abilityTxts[i].text = "-";
        }
    }
}
