using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class HubActions : MonoBehaviour
{
    [SerializeField] private GameObject _inventory, _collection, _shop, _escMenu;
    
    private CameraController _camCont;

    private void Awake()
    {
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
    
    private void OnMenu() => _escMenu.SetActive(!_escMenu.activeSelf);
    
    private void OnInstance() => SceneManager.LoadScene("Scenes/Instances/Forest");
    #endregion Actions
}