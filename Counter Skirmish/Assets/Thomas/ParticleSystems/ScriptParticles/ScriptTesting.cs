using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MyVariables : MonoBehaviour
{
    // Just a scratchpad for playing with variables - not to be used in production
    //Int = Number
    private int health = 100;
    private int myHealth = 100;
    //Constant int = number that cannot be changed in code later
    private const int PlayerScore = 100;
    // private const int PLAYER_SCORE = 100;

    //String = Text
    private string messageForTheTextView = "This space for rent";
    public string firstName;
    public string lastName;
    private TextMeshProUGUI textMeshPro;

    // Start is called before the first frame update
    void Start()
    {
        
        textMeshPro = GetComponent<TextMeshProUGUI>();
        textMeshPro.text = $"Hello {firstName} {lastName}!";
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
