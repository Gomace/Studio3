using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    // Arrow
    [SerializeField] private Transform arrowEffect;
    private Transform circle;
    private Transform[] arrows = new Transform[4];
    private Quaternion[] arrowRotsFrom = {Quaternion.Euler(-90f, 0f, 0f),
                                        Quaternion.Euler(90f, 0f, 90f),
                                        Quaternion.Euler(90f, 0f, 0f),
                                        Quaternion.Euler(-90f, 0f, -90f)};
    private Quaternion[] arrowRotsTo = {Quaternion.identity,
                                        Quaternion.Euler(0f, -90f, 0f),
                                        Quaternion.identity,
                                        Quaternion.Euler(0f, -90f, 0f)};
    private Vector3[] arrowPos = {new Vector3(0f, 0.5f, -0.5f),
                                new Vector3(-0.5f, 0.5f, 0f),
                                new Vector3(0f, 0.5f, 0.5f),
                                new Vector3(0.5f, 0.5f, 0f)};

    private float downA = 40f, forwardA = 3.5f, timeDiv = 0.2f, circleS = 0.5f;
    
    // Movement
    [SerializeField] private Transform _character;
    private NavMeshAgent navMA;
    private LayerMask useLayer = 1 << 7, groundLayer = (1 << 6);
    
    private float maxUseDistance = 1000f;
    private Ray ray;
    private RaycastHit hit;
    //private bool isOverUI;

    private Coroutine clickAnim;

    private void Awake()
    {
        // Arrow
        circle = arrowEffect.GetChild(0);
        
        for (int i = 0; i < arrows.Length; i++)
            arrows[i] = arrowEffect.GetChild(i+1);
        
        // Movement
        navMA = GetComponent<NavMeshAgent>();
        navMA.updateRotation = false;
    }
    
    private void LateUpdate()
    {
        if (navMA.velocity.sqrMagnitude > Mathf.Epsilon)
            _character.rotation = Quaternion.LookRotation(navMA.velocity.normalized);
    }

    private void OnMove()
    {
        ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        
        if (Physics.Raycast(ray, out hit, maxUseDistance, groundLayer))
        {
            navMA.SetDestination(hit.point); // Movement
            
            if (clickAnim != null) // Arrow
                StopCoroutine(clickAnim);
            clickAnim = StartCoroutine(ClickMarker(hit.point));
        }
    }

    #region Arrow
    private IEnumerator ClickMarker(Vector3 clickSpot)
    {
        arrowEffect.gameObject.SetActive(false);
        
        foreach (Transform arrow in arrows)
        {
            arrow.gameObject.SetActive(false);
            arrow.GetChild(0).GetChild(0).GetComponent<TrailRenderer>().Clear();
        }
        
        arrowEffect.position = clickSpot;
        
        circle.localScale = new Vector3(0.175f, circle.localScale.y, 0.175f);
        circle.gameObject.SetActive(true);

        for (int i = 0; i < arrows.Length; i++)
        {
            arrows[i].localPosition = arrowPos[i];
            arrows[i].localRotation = arrowRotsFrom[i];
        }

        arrowEffect.gameObject.SetActive(true);
        
        float time = 0f;
        while (time < 0.1f)
        {
            circle.localScale -= new Vector3(circleS, 0f, circleS) * Time.deltaTime;
            yield return null;
            time += Time.deltaTime;
        }
        foreach (Transform arrow in arrows)
            arrow.gameObject.SetActive(true);
        
        while (time < 0.2f)
        {
            circle.localScale -= new Vector3(circleS, 0f, circleS) * Time.deltaTime;

            arrows[0].localPosition -= new Vector3(0f, (time - 0.1f) * downA, -forwardA * (1 - (time - 0.1f) / timeDiv)) * Time.deltaTime;
            arrows[1].localPosition -= new Vector3(-forwardA * (1 - (time - 0.1f) / timeDiv), (time - 0.1f) * downA, 0f) * Time.deltaTime;
            arrows[2].localPosition -= new Vector3(0f, (time - 0.1f) * downA, forwardA * (1 - (time - 0.1f) / timeDiv)) * Time.deltaTime;
            arrows[3].localPosition -= new Vector3(forwardA * (1 - (time - 0.1f) / timeDiv), (time - 0.1f) * downA, 0f) * Time.deltaTime;
            
            for (int i = 0; i < arrows.Length; i++)
                arrows[i].localRotation = Quaternion.Slerp(arrowRotsFrom[i], arrowRotsTo[i], (time - 0.1f) / timeDiv);

            yield return null;
            time += Time.deltaTime;
        }
        circle.gameObject.SetActive(false);
        
        while (time < 0.3f)
        {
            arrows[0].localPosition -= new Vector3(0f, (time - 0.1f) * downA, -forwardA * (1 - (time - 0.1f) / timeDiv)) * Time.deltaTime;
            arrows[1].localPosition -= new Vector3(-forwardA * (1 - (time - 0.1f) / timeDiv), (time - 0.1f) * downA, 0f) * Time.deltaTime;
            arrows[2].localPosition -= new Vector3(0f, (time - 0.1f) * downA, forwardA * (1 - (time - 0.1f) / timeDiv)) * Time.deltaTime;
            arrows[3].localPosition -= new Vector3(forwardA * (1 - (time - 0.1f) / timeDiv), (time - 0.1f) * downA, 0f) * Time.deltaTime;
            
            for (int i = 0; i < arrows.Length; i++)
                arrows[i].localRotation = Quaternion.Slerp(arrowRotsFrom[i], arrowRotsTo[i], (time - 0.1f) / timeDiv);
            
            yield return null;
            time += Time.deltaTime;
        }
        
        for (int i = 0; i < arrows.Length; i++)
            arrows[i].localRotation = Quaternion.Slerp(arrowRotsFrom[i], arrowRotsTo[i], (time - 0.1f) / timeDiv);
        
        yield return new WaitForSeconds(0.5f);
        arrowEffect.gameObject.SetActive(false);
    }
    #endregion
}