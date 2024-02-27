/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InstanceDialog : MonoBehaviour
{
    [SerializeField] private int _lettersPerSec;
    [SerializeField] private Color _highlightedColor;
    
    [SerializeField] private TMP_Text _dialog;
    [SerializeField] private GameObject _actionSel, _abilitySel, _abilityDet;
    
    [SerializeField] private List<TMP_Text> _actionTxts, _abilityTxts;
    
    [SerializeField] private TMP_Text _resourceTxt, _typeTxt;


    public void SetDialog(string dialog) => _dialog.text = dialog;

    public IEnumerator TypeDialog(string dialog)
    {
        _dialog.text = "";
        foreach (char letter in dialog.ToCharArray())
        {
            _dialog.text += letter;
            yield return new WaitForSeconds(1f/_lettersPerSec);
        }

        yield return new WaitForSeconds(1f);
    }

    public void EnableDialogText(bool enabled) => _dialog.enabled = enabled;
    public void EnableActionSelector(bool enabled) => _actionSel.SetActive(enabled);
    public void EnableAbilitySelector(bool enabled)
    {
        _abilitySel.SetActive(enabled);
        _abilityDet.SetActive(enabled);
    }

    public void UpdateActionSelection(int selectedAction)
    {
        for (int i = 0; i < _actionTxts.Count; ++i)
        {
            if (i == selectedAction)
                _actionTxts[i].color = _highlightedColor;
        }
    }

    public void UpdateAbilitySelection(int selectedAbility, Ability ability)
    {
        for (int i = 0; i < _abilityTxts.Count; ++i)
        {
            if (i == selectedAbility)
                _abilityTxts[i].color = _highlightedColor;
            else
                _abilityTxts[i].color = Color.black;
        }

        //_resourceTxt.text = $"Resource {ability.Resource}/{ability.Base.Resource}";
        _typeTxt.text = ability.Base.Type.ToString();
    }

    public void SetAbilityNames(List<Ability> abilities)
    {
        for (int i = 0; i < _abilityTxts.Count; ++i)
        {
            if (i < abilities.Count)
                _abilityTxts[i].text = abilities[i].Base.Name;
            else
                _abilityTxts[i].text = "-";
        }
    }
}*/
