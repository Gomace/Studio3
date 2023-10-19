public class Ability
{
    public AbilityBase Base { get; set; }
    public int Resource { get; set; }

    public Ability(AbilityBase cBase)
    {
        Base = cBase;
        Resource = cBase.Resource;
    }
}
