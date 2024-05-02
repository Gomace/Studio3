using UnityEngine;

public class NavMeshEnabler : MonoBehaviour
{

    [SerializeField] GameObject navMeshCubeParent;

    // Start is called before the first frame update
    void Start()
    {

        navMeshCubeParent.SetActive(true);

        foreach (Renderer renderer in gameObject.GetComponentsInChildren(typeof(Renderer)))
        {
            renderer.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
