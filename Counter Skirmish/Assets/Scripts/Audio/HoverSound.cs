using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HoverSound : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] private AudioSource _enter;
    [SerializeField] private float _offset;

    private AudioManager _audioManager;
    
    private void Start()
    {
        if (_enter) return; // Cards are instantiated, so we know they are empty and will get their specific sound - Make better when needed.
        
        _audioManager = GameObject.Find("GameManager").GetComponent<AudioManager>();
        _enter = _audioManager.SFXS[2];

        if (TryGetComponent(out Button thisButton))
            thisButton.onClick.AddListener(ActivateClickSound);
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_enter.isPlaying)
            _enter.Stop();
        _enter.time = _offset;
        _enter.Play();
    }

    public void ActivateClickSound()
    {
        _audioManager.SFXS[3].time = 0.08f;
        _audioManager.SFXS[3].Play();
    }
}