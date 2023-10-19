using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyTypes : MonoBehaviour
{
    //Strings can contain text, numbers, just a space and even no letters or spacing at all.
    public string message = "Strongs contain text, not cheese";
    public string phoneNumber = "12345678";
    public string whiteSpace = " ";
    public string emptyString = "";

    public char tempType = 'C';

    //Simple true or false value
    public bool wetClothesKill = true;
    public bool testTest = false;
    //Int = number. Use underscores as spaces to more easily read large numbers
    public int myNumber = 2_142_423_223;
    //A int can only go from -billion to a billion as it is a 32bit number, to expand it use: uint (cannot contain negative number)
    public uint anotherNumber = 2147483648;
    //long is a 64bit number so it can be double the int size.
    public long reallyBigNumber = 9_223_372_036_854_775_807;
    //Double is the standard decimal type in Unity being 64bit, floats are 32bit and therefore less precise.
    public double pi = 3.14159265359;
    //When writing a decimal number, Unity assumes we are writing a double, thus we need to end it on "f" to tell it that we are using a float
    //A lot of times we do not need the precision that a double provides, so we can save memory on using a float.
    public float temp = 95.6f;
    //Use decimal is 128bit, never use other types in anything related to money as it will give errors, use "m" at the end to define it.
    public decimal cashOnHand = 1.99m;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
