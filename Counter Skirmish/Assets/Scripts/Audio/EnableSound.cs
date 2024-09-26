using UnityEngine;

public class EnableSound : MonoBehaviour
{
    [SerializeField] private AudioSource _enable;
    
    private AudioManager _audioManager;
    
    private void OnEnable() { ActivateSound(); }
    private void OnDisable() { ActivateSound(); }

    private void ActivateSound()
    {
        if (_enable.isPlaying)
            _enable.Stop();
        
        _enable.Play();
    }
}