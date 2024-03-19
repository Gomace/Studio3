using UnityEngine;

public class Passive
{
    public PassiveBase Base { get; set; }
    
    public Passive(PassiveBase pBase)
    {
        Base = pBase;
    }
}
