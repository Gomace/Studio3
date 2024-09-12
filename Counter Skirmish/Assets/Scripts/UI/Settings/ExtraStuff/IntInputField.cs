using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(InputField))]
public class IntInputField : MonoBehaviour
{
    private InputField _inputField;

    private void Awake() { GetComponent<InputField>(); }

    public void IntsOnly(string input)
    {
        string validated = Regex.Replace(input, @"[^0-9]", "");
        
        if (validated != input)
            _inputField.text = validated;
    }
}
