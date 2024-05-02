using UnityEngine;

public class GateAnimOpen : MonoBehaviour
{

    public Animator animator;
    public Animator lockAnim;
    public GameObject openCollider;

    public GameObject keyModelChecker;
    public GameObject keyAnimated;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !keyModelChecker.activeInHierarchy)
        {
            keyAnimated.SetActive(true);
            animator.SetBool("Idle", false);
            lockAnim.SetBool("doorOpening", true);
            openCollider.SetActive(false);       
        }
    }
}
