using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitHUD : MonoBehaviour
{
    [SerializeField] private InstanceUnit _unit;

    [Header("This should all be filled.")]
    [SerializeField] private TMP_Text _name;
    [SerializeField] private TMP_Text _lvl;
    [SerializeField] private Image _type1, _type2;
    [SerializeField] private GameObject _dead;

    private Creature _creature;
    
    public string Txt { get; set; }
    
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
    
    private void OnEnable()
    {
        _unit.onLoadHUD += SetupHUD;
        _unit.onLvlUp += SetLvl;
        _unit.onDead += DeadHUD;
    }
    private void OnDisable()
    {
        _unit.onLoadHUD -= SetupHUD;
        _unit.onLvlUp -= SetLvl;
        _unit.onDead -= DeadHUD;
    }

    private void SetupHUD(Creature creature)
    {
        _creature = creature;
        
        _name.text = creature.Base.Name;

        _type1.sprite = creature.Base.Type1.Icon;
        if (creature.Base.Type2)
            _type2.sprite = creature.Base.Type2.Icon;
        _type2.enabled = creature.Base.Type2;

        SetLvl();
        
        _dead.SetActive(false);
    }

    private void SetLvl()
    {
        if (_creature != null)
            _lvl.text = _creature.Level.ToString();
    }

    private void DeadHUD() => _dead.SetActive(true);
    
    private void LateUpdate() => transform.LookAt(transform.position + _mainCam.rotation * Vector3.forward);
}