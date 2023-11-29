using UnityEngine;

public class BranchDisappearer : MonoBehaviour
{
    [SerializeField] private float _linger;
    private float _delay;
    
    private void Update()
    {
        if (_delay > _linger)
        {
            gameObject.SetActive(false);
            RecursiveInstantiator.Deadches.Push(transform);
        }
        _delay += Time.deltaTime;
    }

    private void OnEnable() =>
        _delay = 0;
}
