using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MadLibs : MonoBehaviour
{

    private TextMeshProUGUI textMeshPro;

    private bool statement =false;
    private string verb = "Say";
    private string noun = "Voyage is Bad";
    private string adjective = "Trustworthy";
    private int number = 35;
    private string pluralNoun = "Stokers";
    private float percent = 84.0f;

    // Start is called before the first frame update
    void Start()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();
        textMeshPro.text = $"The is statement is {statement}. " +
            $"I did not {verb} that {noun}. I am not guilty. " +
            $"I am a {adjective} person. " +
            $"The act was performed by {number} wandering {pluralNoun}. " +
            $"I am {percent}% sure of this.";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
