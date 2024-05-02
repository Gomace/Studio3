using UnityEngine;

public class GateAnimClose : MonoBehaviour
{

    public Animator animator;
    public GameObject closeCollider;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            closeCollider.SetActive(false);
            animator.SetBool("doorOpen", false);
        }   
    }
}
