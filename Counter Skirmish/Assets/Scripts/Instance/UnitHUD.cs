using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitHUD : MonoBehaviour
{
    [SerializeField] private InstanceUnit _unit;

    [Header("This should all be filled.")]
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_Text _levelText;
    [SerializeField] private Image _type1, _type2;

    private Transform _mainCam;

    private void Awake()
    {
        if (!Camera.main)
        {
            Debug.Log("No Main Camera in scene :(");
            return;
        }
        
        _mainCam = Camera.main.transform;
    }
    
    private void OnEnable() => _unit.onLoadHUD += SetupHUD;
    private void OnDisable() => _unit.onLoadHUD -= SetupHUD;

    private void SetupHUD(Creature creature)
    {
        Debug.Log(creature.Base.Name + " entered.");
        _nameText.text = creature.Base.Name;
        _levelText.text = creature.Level.ToString();
        
        _type1.sprite = creature.Base.Type1.Icon;
        if (creature.Base.Type2)
            _type2.sprite = creature.Base.Type2.Icon;
        else
            _type2.enabled = false;
    }
    private void LateUpdate() => transform.LookAt(transform.position + _mainCam.rotation * Vector3.forward);
}