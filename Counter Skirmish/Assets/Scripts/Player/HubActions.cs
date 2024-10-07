using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(PlayerMovement))]
public class HubActions : MonoBehaviour
{
    [SerializeField] private GameObject _inventory, _collection, _shop, _escMenu;

    private PlayerMovement _movement;
    private CameraController _camCont;

    private void Awake()
    {
        if (Camera.main != null || Camera.main.GetComponent<CameraController>())
            _camCont = Camera.main.GetComponent<CameraController>();
        else
            Debug.Log("Put CameraController script on Camera :|");

        _movement = GetComponent<PlayerMovement>();
    }

    #region Actions
    private void OnCameraZoom(InputValue value) => _camCont.CameraZoom(value.Get<float>());
    
    private void OnInventory() => _inventory.SetActive(!_inventory.activeSelf);
    public void OnCollection() => _collection.SetActive(!_collection.activeSelf);

    private void OnShop() => _shop.SetActive(!_shop.activeSelf);
    
    private void OnMenu()
    {
        if (_inventory.activeSelf)
        {
            _inventory.SetActive(false);
            return;
        }
        if (_collection.activeSelf)
        {
            _collection.SetActive(false);
            return;
        }
        if (_shop.activeSelf)
        {
            _shop.SetActive(false);
            return;
        }
            
        _escMenu.SetActive(!_escMenu.activeSelf);
    }
    
    private void OnInstance() => SceneManager.LoadScene("Scenes/Instances/Forest");
    #endregion Actions
}