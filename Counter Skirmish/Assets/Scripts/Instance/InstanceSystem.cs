using UnityEngine;

public class InstanceSystem : MonoBehaviour
{
    #region Events
    public delegate void OnLoadInstance();
    public static event OnLoadInstance onLoadInstance;
    #endregion Events

    private void Start() => onLoadInstance?.Invoke(); // SetupInstance();

    /*private void SetupInstance()
    {
        onLoadInstance?.Invoke();
    }*/
}
