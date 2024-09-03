using UnityEngine;
using UnityEngine.EventSystems;

public class HoverSound : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] private AudioSource _enter;
    [SerializeField] private float _offset;
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_enter.isPlaying)
            _enter.Stop();
        _enter.time = _offset;
        _enter.Play();
    }
}