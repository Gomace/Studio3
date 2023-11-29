using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RecursiveInstantiator : MonoBehaviour
{
    [SerializeField] private Transform _original;
    
    [Header("Random branch cutoff:")]
    [SerializeField] private int _stages = 5;
    [SerializeField] private int _stageReduce;

    [Header("Random split amount:")]
    [SerializeField] private int _splits = 2;
    [SerializeField] private int _splitLess;

    [Header("Random size:")]
    [SerializeField] private float _scaleMin;
    [SerializeField] private float _scaleMax;
    
    [Header("Random angle:")]
    [SerializeField] private float _angleMin;
    [SerializeField] private float _angleMax;
    
    [Header("Random strike timer:")]
    [SerializeField] private float _timerMin;
    [SerializeField] private float _timerMax;

    [Header("Just checking things:")]
    [SerializeField] private int _stackSize;
    [SerializeField] private int _branchNumber;

    private float _delay, _timer; // Zap interval timer

    private static Stack<Transform> _deadches;
    
    public static Stack<Transform> Deadches => _deadches;

    private void Awake()
    {
        _delay = 0; // Perpetual lightning strikes
        _timer = Random.Range(_timerMin, _timerMax);

        _deadches = new Stack<Transform>(); // Container for unused branches
    }
    
    private void Start() =>
        CreateBranch(_original, 1);

    private void Update() // Lightning strikes forever
    {
        if (_delay > _timer)
        {
            CreateBranch(_original, 1);
            
            _timer = Random.Range(_timerMin, _timerMax);
            _delay = 0;
        }
        _delay += Time.deltaTime;
    }
    
    private void CreateBranch(Transform prevBranch, int stage) // Creates a branch, and tells the next branch which branch it is splitting from
    {
        if (stage > _stages)
            return;

        _stackSize = _deadches.Count;
        for (int i = 0; i < _splits; i = LessSplits(i))
        {
            if (_deadches.TryPop(out Transform branch))
            {
                ResetBranch(prevBranch, branch);
                branch.gameObject.SetActive(true);
            }
            else
            {
                branch = Instantiate(prevBranch, _original.parent);
                branch.GetComponent<BranchDisappearer>().enabled = true;
                ++_branchNumber;
            }

            BranchScale(branch);
            BranchPos(prevBranch, branch);
            BranchRot(branch, i);
            
            CreateBranch(branch, StageCutoff(stage));
        }
    }

    private int StageCutoff(int stage)
    {
        int c = Random.Range(1, 2+_stageReduce); // Biased towards shorter lengths
        switch (c)
        {
            case 1:
                return _stages; //Random.Range(++stage, _stages);
            default:
                return ++stage;
        }
    }
    
    private int LessSplits(int i) // Makes the lightning split less often
    {
        int c = Random.Range(1, 2+_splitLess); // Biased towards not splitting
        switch (c)
        {
            case 1:
                return Random.Range(++i, _splits);
            default:
                return _splits;
        }
    }

    #region Behaviour

    private void ResetBranch(Transform prevBranch, Transform branch)
    { 
        branch.localPosition = prevBranch.localPosition;
        branch.localRotation = prevBranch.localRotation;
    }
    
    private void BranchScale(Transform branch)
    {
        float scale = Random.Range(_scaleMin, _scaleMax);
        
        Vector3 tempScale = _original.localScale;
        tempScale.y *= scale;
        branch.localScale = new Vector3(tempScale.x, tempScale.y, tempScale.z);
    }
    
    private void BranchPos(Transform prevBranch, Transform branch) =>
        branch.localPosition += branch.up * prevBranch.localScale.y;
    
    private void BranchRot(Transform branch, int index)
    {
        float angleX = Random.Range(_angleMin, _angleMax),
            //angleY = Random.Range(_angleMin, _angleMax),
            angleZ = Random.Range(_angleMin, _angleMax);

        float indexering = ((index * 2) - 1);

        branch.localRotation = _original.localRotation * Quaternion.Euler(angleX * indexering, 0, angleZ * indexering);
        branch.localPosition += branch.up * (branch.localScale.y * 0.95f);
    }
    #endregion Behaviour
}