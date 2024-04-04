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

    public void LeaveInstance()
    {
        if (_loadingScreen)
            _loadingScreen.LoadScene("Scenes/Hub");
        else
            SceneManager.LoadScene("Scenes/Hub");
    }

    public void QuitToDekstop() => Application.Quit();
}
