using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourceBar : MonoBehaviour
{
    [SerializeField] private InstanceUnit _unit;
    
    [Header("These should all be filled.")]
    [SerializeField] private Image _mainBar;
    [SerializeField] private Image _incBar, _lossBar;
    [Header("Ignore this on UnitHUD.")]
    [SerializeField] private TMP_Text _num;

    private Creature _creature;
    
    private float _barSpeed = 0.5f;
    private Coroutine _damageBar;

    private void Awake()
    {
        _mainBar.fillAmount = 1f;
        _incBar.fillAmount = 1f;
        _lossBar.fillAmount = 1f;
    }

    private void OnEnable()
    {
        _unit.onLoadHUD += SetResource;
        _unit.onResourceChanged += SetBar;
    }
    private void OnDisable()
    {
        _unit.onLoadHUD -= SetResource;
        _unit.onResourceChanged -= SetBar;
    }

    private void SetResource(Creature creature)
    {
        if (_damageBar != null)
            StopCoroutine(_damageBar);
        
        _mainBar.fillAmount = _incBar.fillAmount = _lossBar.fillAmount = (float)creature.Resource / creature.MaxResource;
        _creature = creature;
    }
    
    private void SetBar(float newR)
    {
        if (newR < _mainBar.fillAmount) // Spent
        {
            _incBar.fillAmount -= _mainBar.fillAmount - newR; // _incBar has same incoming Resource
            // Dead?
            
            if (_damageBar != null)
                StopCoroutine(_damageBar);
    
            _damageBar = StartCoroutine(DamageBar(newR));
        }
        else // Gained
        {
            if (_lossBar.fillAmount < newR)
            {
                if (_damageBar != null)
                    StopCoroutine(_damageBar);
                _lossBar.fillAmount = newR;
            }
            
            if (_incBar.fillAmount < newR)
                _incBar.fillAmount = newR; // _incBar did its job
        }
        
        _mainBar.fillAmount = newR; // Change _mainBar to new value
        if (_num && _creature != null)
            _num.text = $"{_creature.Resource.ToString()}/{_creature.MaxResource.ToString()}";
    }
    
    private IEnumerator DamageBar(float newR)
    {
        while (newR < _lossBar.fillAmount) // While _lossBar can shrink, go agane
        {
            _lossBar.fillAmount -= Time.deltaTime * _barSpeed; // _lossBar shrinks to _mainBar size
            yield return null;
        }
    }
}