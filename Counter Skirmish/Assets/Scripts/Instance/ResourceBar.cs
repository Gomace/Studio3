using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ResourceBar : MonoBehaviour
{
    [SerializeField] private InstanceUnit _unit;
    
    [Header("These should all be filled.")]
    [SerializeField] private Image _mainBar;
    [SerializeField] private Image _incBar, _lossBar;

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

    private void SetResource(Creature creature) => _mainBar.fillAmount = _incBar.fillAmount = _lossBar.fillAmount = (float)creature.Resource / creature.MaxResource;
    
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