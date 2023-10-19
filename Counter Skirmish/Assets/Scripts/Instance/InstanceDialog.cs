using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InstanceDialog : MonoBehaviour
{
    [SerializeField] private int lettersPerSecond;
    
    [SerializeField] private TextMeshProUGUI dialogText;
    [SerializeField] private GameObject actionSelector;
    [SerializeField] private GameObject abilitySelector;
    [SerializeField] private GameObject abilityDetails;
    
    [SerializeField] private List<TextMeshProUGUI> actionTexts;
    [SerializeField] private List<TextMeshProUGUI> abilityTexts;
    
    [SerializeField] private TextMeshProUGUI resourceTexts;
    [SerializeField] private TextMeshProUGUI typeTexts;

    public void SetDialog(string dialog) => dialogText.text = dialog;

    public IEnumerator TypeDialog(string dialog)
    {
        dialogText.text = "";
        foreach (char letter in dialog.ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(1f/lettersPerSecond);
        }
    }

    public void EnableDialogText(bool enabled) => dialogText.enabled = enabled;
    public void EnableActionSelector(bool enabled) => actionSelector.SetActive(enabled);

    public void SetAbilities(List<Ability> abilities)
    {
        for (int i = 0; i < abilityTexts.Count; ++i)
        {
            if (i < abilities.Count)
                abilityTexts[i].text = abilities[i].Base.Name;
            else
                abilityTexts[i].text = "-";
        }
    }
}
