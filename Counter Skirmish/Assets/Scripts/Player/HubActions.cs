using UnityEngine;
using UnityEngine.InputSystem;

public class HubActions : MonoBehaviour
{
    [SerializeField] private SceneLoader _sceneLoader;
    [SerializeField] private GameObject _inventory, _collection, _shop, _settings;
    
    private CameraController _camCont;
    private LayerMask _useLayer = 1 << 6;

    private void Awake()
    {
        if (!_settings)
            Debug.Log("Missing inspector drag & drop reference. Please help :[");

        if (Camera.main != null || Camera.main.GetComponent<CameraController>())
            _camCont = Camera.main.GetComponent<CameraController>();
        else
            Debug.Log("Put CameraController script on Camera :|");
    }

    #region Actions
    private void OnCameraZoom(InputValue value) => _camCont.CameraZoom(value.Get<float>());
    
    private void OnInventory() => _inventory.SetActive(!_inventory.activeSelf);

    private void OnCollection() => _collection.SetActive(!_collection.activeSelf);

    private void OnShop() => _shop.SetActive(!_shop.activeSelf);
    
    private void OnMenu() => _settings.SetActive(!_settings.activeSelf);
    
    private void OnStart() => _sceneLoader.LoadScene("StartMenu");
    private void OnInstance() => _sceneLoader.LoadScene("Forest");

    #endregion Actions
}