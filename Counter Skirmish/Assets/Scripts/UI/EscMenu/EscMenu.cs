using UnityEngine;
using UnityEngine.SceneManagement;

public class EscMenu : MonoBehaviour
{
    [SerializeField] private LoadingScreen _loadingScreen;

    public void Restart()
    {
        if (_loadingScreen)
            _loadingScreen.LoadScene(SceneManager.GetActiveScene().name);
        else
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Leave(string sceneName)
    {
        if (_loadingScreen)
            _loadingScreen.LoadScene(sceneName);
        else
            SceneManager.LoadScene(sceneName);
    }

    public void QuitToDesktop() => Application.Quit();
}
