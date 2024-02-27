/*using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private float _speed = 1f;
    [Header("Rotation Configs")]
    [SerializeField] private float _rotationAmount = 90f;
    [SerializeField] private bool _forwardDirection = false;

    private Vector3 _startRotation;
    private Quaternion _endRotation;

    private Coroutine _animationCoroutine;
    
    public bool IsOpen { get; set; } = false;
    
    private void Awake()
    {
        _startRotation = transform.rotation.eulerAngles;

        if (IsOpen)
            _startRotation.y += (_forwardDirection
            ? - _rotationAmount
            : _rotationAmount);
        
        _endRotation = _forwardDirection
            ? Quaternion.Euler(new Vector3(0, _startRotation.y + _rotationAmount, 0))
            : Quaternion.Euler(new Vector3(0, _startRotation.y - _rotationAmount, 0));
    }

    public void OpenClose()
    {
        if (_animationCoroutine != null)
            StopCoroutine(_animationCoroutine);
        
        _animationCoroutine = StartCoroutine(IsOpen ? DoRotationClose() : DoRotationOpen());
    }

    private IEnumerator DoRotationOpen()
    {
        Quaternion startRotation = transform.rotation;
        
        IsOpen = true;

        float time = 0;
        while (time < 1)
        {
            transform.rotation = Quaternion.Slerp(startRotation, _endRotation, time);
            yield return null;
            time += Time.deltaTime * _speed;
        }
    }

    private IEnumerator DoRotationClose()
    {
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = Quaternion.Euler(_startRotation);

        IsOpen = false;

        float time = 0;
        while (time < 1)
        {
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, time);
            yield return null;
            time += Time.deltaTime * _speed;
        }
        transform.rotation = Quaternion.Slerp(startRotation, endRotation, time);
    }    
}*/