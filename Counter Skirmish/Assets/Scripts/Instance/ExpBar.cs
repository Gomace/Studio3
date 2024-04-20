using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExpBar : MonoBehaviour
{
    [SerializeField] private InstanceUnit _unit;
    
    [Header("This should be filled.")]
    [SerializeField] private Image _mainBar;
    [SerializeField] private Image _gainBar;
    [SerializeField] private TMP_Text _num;
    
    private Creature _creature;

    private int _curLvlExp, _nxtLvlExp;

    private float _barSpeed = 0.2f;
    private Coroutine _barMove, _lvlUp;

    private void Awake() => _mainBar.fillAmount = _gainBar.fillAmount = 0f;

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
        _creature = creature;
        
        if (_barMove != null)
            StopCoroutine(_barMove);
        
        _mainBar.fillAmount = GetNormExp();
        if (_num ) // Check if _num is referenced
            _num.text = $"{(_creature.Exp - _curLvlExp)}/{(_nxtLvlExp - _curLvlExp)}";
    }
    
    private void SetBar(bool lvled = false)
    {
        float newXP = GetNormExp(lvled); // Get new fill amount

        if (_mainBar.fillAmount >= newXP) // Only pass if gained Exp
            return;
        
        if (_lvlUp != null)
            StopCoroutine(_lvlUp);
        _lvlUp = StartCoroutine(WaitForLvlUp(newXP, lvled));
    }

    private IEnumerator WaitForLvlUp(float newXP, bool lvled = false)
    {
        _gainBar.fillAmount = newXP; // Set _gainBar to inc Exp
        if (_num && _creature != null)
            _num.text = $"{(float)(_creature.Exp - _curLvlExp)}/{(_nxtLvlExp - _curLvlExp)}";

        if (_barMove != null)
            StopCoroutine(_barMove);
        yield return _barMove = StartCoroutine(MoveBar(newXP));

        _mainBar.fillAmount = newXP; // Change _mainBar to new value

        if (lvled) // check if lvled
        {
            _mainBar.fillAmount = 0;

            _creature?.Unit.LvlUp(_creature.CheckForLvlUp()); // Send if lvled again, and lvl up one
        }
    }
    
    private IEnumerator MoveBar(float newXP)
    {
        while (_mainBar.fillAmount < newXP) // While _mainBar can grow, go agane
        {
            _mainBar.fillAmount += Time.deltaTime * _barSpeed; // _mainBar increases to Exp percentage
            yield return null;
        }
    }

    private float GetNormExp(bool lvled = false)
    {
        if (_creature == null)
            return 0;
        
        _curLvlExp = _creature.Base.GetExpForLevel(_creature.Level - (lvled ? 1 : 0));
        _nxtLvlExp = _creature.Base.GetExpForLevel(_creature.Level + (lvled ? 0 : 1));

        return Mathf.Clamp01((float)(_creature.Exp - _curLvlExp) / (_nxtLvlExp - _curLvlExp));
    }
}
