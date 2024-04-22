using UnityEngine;

public class TrailClearer : MonoBehaviour
{
    [SerializeField] private ConjuredAbility _transmitter;
    [SerializeField] private TrailRenderer _trail;

    private void OnEnable() => _transmitter.onConjDisable += ClearTrail;
    private void OnDisable() => _transmitter.onConjDisable -= ClearTrail;

    private void ClearTrail() => _trail.Clear();
}
