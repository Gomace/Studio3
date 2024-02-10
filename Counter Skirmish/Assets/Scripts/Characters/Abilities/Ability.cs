public class Ability
{
    public AbilityBase Base { get; set; }

    public Ability(AbilityBase cBase)
    {
        Base = cBase;
    }
}