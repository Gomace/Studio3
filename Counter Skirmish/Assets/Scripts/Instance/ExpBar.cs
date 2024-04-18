using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExpBar : MonoBehaviour
{
    [SerializeField] private InstanceUnit _unit;
    
    [Header("This should be filled.")]
    [SerializeField] private Image _mainBar;
    [SerializeField] private TMP_Text _num;
    
    private Creature _creature;
    
    private float _barSpeed = 0.5f;
    private Coroutine _barMove;

    private void Awake() => _mainBar.fillAmount = 1f;

    private void OnEnable()
    {
        _unit.onLoadHUD += SetExp;
        _unit.onExpEarned += SetBar;
    }
    private void OnDisable()
    {
        _unit.onLoadHUD -= SetExp;
        _unit.onExpEarned -= SetBar;
    }

    private void SetExp(Creature creature)
    {
        if (_barMove != null)
            StopCoroutine(_barMove);
        
        _mainBar.fillAmount = (float)creature.Base.GetExpForLevel(creature.Level) / creature.Base.GetExpForLevel(creature.Level + 1);
        _creature = creature;
    }
    
    private void SetBar(float newXP)
    {
        /*if (_mainBar.fillAmount < newXP) // TODO make bar level up and go around
        {
            if (_barMove != null)
                StopCoroutine(_barMove);
    
            _barMove = StartCoroutine(MoveBar(newXP));
        }
        else
        {
            if (_barMove != null)
                StopCoroutine(_barMove);
            _barMove = StartCoroutine(MoveBar(newXP));
        }*/
        
        _mainBar.fillAmount = newXP; // Change _mainBar to new value
        if (_num && _creature != null)
            _num.text = $"{(float)_creature.Exp}/{(float)_creature.Base.GetExpForLevel(_creature.Level + 1)}";
    }
    
    private IEnumerator MoveBar(float newXP)
    {
        while (_mainBar.fillAmount < newXP) // While _mainBar can grow, go agane
        {
            _mainBar.fillAmount += Time.deltaTime * _barSpeed; // _mainBar increases to Exp percentage
            yield return null;
        }
    }
}
