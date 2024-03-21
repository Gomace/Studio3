/*using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class BasicEnemy : MonoBehaviour
{
    [SerializeField] private Transform _character;
    
    private NavMeshAgent _navMA;
    
    private const float _maxUseDistance = 200f; // Might not use
    private Ray _ray; // - || -
    private RaycastHit _hit; // - || -

    private void Awake()
    {
        _navMA = GetComponent<NavMeshAgent>();
        _navMA.updateRotation = false;
    }

    private void Update()
    {
        _ray = _mainCam.ScreenPointToRay(Mouse.current.position.ReadValue());
    }

    private void LateUpdate()
    {
        if (_navMA.velocity.sqrMagnitude > Mathf.Epsilon)
            _character.rotation = Quaternion.LookRotation(_navMA.velocity.normalized);
    }

    private void MoveUnit(Vector3 mouse)
    {
        _navMA.SetDestination(mouse);
    }

    private Vector3 MouseLocation()
    {
        if (Physics.Raycast(_ray, out _hit, _maxUseDistance, _groundLayer))
            return _hit.point;
    }
}*/