using UnityEngine;
using UnityEngine.SceneManagement;

public class TouchTeleport : MonoBehaviour
{
    [SerializeField] private LoadingScreen _loadingScreen;
    [SerializeField] private string _teleportScene;
    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;
        
        if (_loadingScreen)
            _loadingScreen.LoadScene(_teleportScene);
        else
            SceneManager.LoadScene(_teleportScene);
    }
}
