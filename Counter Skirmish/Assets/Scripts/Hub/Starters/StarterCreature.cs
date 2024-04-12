using UnityEngine;

public class StarterCreature : MonoBehaviour
{
    public HubCharacter Player { private get; set; }

    public void SelectStarter(CreatureInfo creature)
    {
        Player.AddCreatureToRoster(creature);
        gameObject.SetActive(false);
    }
}