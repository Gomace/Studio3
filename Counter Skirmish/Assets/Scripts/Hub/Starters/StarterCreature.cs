using UnityEngine;

public class StarterCreature : MonoBehaviour
{
    public HubCharacter Player { private get; set; }

    public void SelectStarter(CreatureBase cBase)
    {
        Player.AddCreatureToRoster(new CreatureInfo(cBase, 1, 0));
        gameObject.SetActive(false);
    }
}