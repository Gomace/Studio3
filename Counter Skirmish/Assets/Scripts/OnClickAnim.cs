using System.Collections;
//using Enemy.Ball;
using UnityEngine;

public class OnClickAnim : MonoBehaviour
{
    public float animationTime;
    public LayerMask layerMask;
    //[SerializeField] private BallController _ballController;
    private Camera _camera;
    private Vector3 _mouseClickPos;
    private ParticleSystem _particleSystem;

    private void Awake()
    {
        _particleSystem = GetComponentInChildren<ParticleSystem>();
        _camera = Camera.main;
    }

    private void Update()
    {
        var ray = _camera.ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(ray, out var hitData, 1000, layerMask.value)) return;
        if (Input.GetMouseButtonDown(0))
        {
            _particleSystem.Play();
            StartCoroutine(Stop());
            transform.position = _mouseClickPos = hitData.point;
            //_ballController.Move(_mouseClickPos);
        }

        if (_particleSystem.isPlaying) return;
        transform.position = hitData.point;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(_mouseClickPos, Vector3.one / 2);
    }


    private IEnumerator Stop()
    {
        yield return new WaitForSeconds(animationTime);
        _particleSystem.Stop();
    }
}