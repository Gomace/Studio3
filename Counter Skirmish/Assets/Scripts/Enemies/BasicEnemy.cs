using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class BasicEnemy : MonoBehaviour
{
    [SerializeField] private Transform _character;
    [SerializeField] private Transform _testPlayer;
    
    private Vector3 _randPos;
    private NavMeshAgent _navMA;
    private readonly LayerMask _groundLayer = (1 << 6);

    private const float _maxUseDistance = 20f;
    private Ray _ray;
    private RaycastHit _hit;
    
    private void Awake()
    {
        _navMA = GetComponent<NavMeshAgent>();
        _navMA.updateRotation = false;
    }
    
    private void Update()
    {
        Vector3 dir = transform.position - _testPlayer.position;
        
        Debug.DrawRay(_testPlayer.position, dir, Color.red);

        Vector3 perpDir = new Vector3(dir.z, 0, -dir.x);
        
        Debug.DrawRay((transform.position - perpDir), perpDir, Color.white);
    }
    
    private void LateUpdate()
    {
        if (_navMA.velocity.sqrMagnitude > Mathf.Epsilon)
            _character.rotation = Quaternion.LookRotation(_navMA.velocity.normalized);
    }
    
    private void MoveUnit(Vector3 mouse) => _navMA.SetDestination(mouse);

    private Vector3 MouseLocation()
    {
        _ray = new Ray(transform.position + new Vector3(0f, 2f, 0f) + _randPos, Vector3.down);
        
        return Physics.Raycast(_ray, out _hit, _maxUseDistance, _groundLayer)
            ? _hit.point : Vector3.zero;
    }
    
    private void RandomizeMovePos()
    {
        _randPos = Vector3.up;
    }
}