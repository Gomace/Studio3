using UnityEngine;

public class KeyCollector : MonoBehaviour
{

    public GameObject keyModel;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            keyModel.SetActive(false);
        }
    }


}
