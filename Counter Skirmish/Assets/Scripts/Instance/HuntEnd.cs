using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class HuntEnd : MonoBehaviour
{
    #region Events
    public delegate void OnBossKill();
    public static event OnBossKill onBossKill;
    #endregion
    
    [Header("These should already be filled out.")]
    [SerializeField] private GameObject _huntScreen;
    [SerializeField] private Image _result;
    [SerializeField] private Sprite _success, _fail;

    public static bool Win { private get; set; }

    public void OnEnable() => onBossKill += HuntResult;
    public void OnDisable() => onBossKill -= HuntResult;

    public static void WinGame() => onBossKill?.Invoke();
    
    public void HuntResult()
    {
        _result.sprite = Win ? _success : _fail;
        _huntScreen.SetActive(true);
    }
}