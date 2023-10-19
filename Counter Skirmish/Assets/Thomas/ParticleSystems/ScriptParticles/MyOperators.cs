using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MyOperators : MonoBehaviour
{

    private TextMeshProUGUI textMeshPro;

    // Start is called before the first frame update
    void Start()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisplayMessage()
    {
        int xp = 990;
       
        xp = xp + 10;
       
        //Add
        xp += 10;
        //Subtract
        xp -= 10;
        //Multiply 
        xp *= 10;
        //Divide
        xp /= 10;

        int strength = 18;
        int stat = strength++;

        textMeshPro.text = $"{stat}";
    }

}
