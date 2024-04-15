using UnityEngine;
using UnityEngine.UI;

public class HuntEnd : MonoBehaviour
{
    [Header("These should already be filled out.")]
    [SerializeField] private GameObject _huntScreen;
    [SerializeField] private Image _result;
    [SerializeField] private Sprite _success, _fail;

    public static bool Win { private get; set; }
    
    public void HuntResult()
    {
        _result.sprite = Win ? _success : _fail;
        _huntScreen.SetActive(true);
    }
}