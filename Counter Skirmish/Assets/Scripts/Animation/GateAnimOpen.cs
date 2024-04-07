using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateAnimOpen : MonoBehaviour
{

    public Animator animator;
    public Animator lockAnim;
    public GameObject openCollider;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("The player has entered the collider and door should open");
            animator.SetBool("Idle", false);
            lockAnim.SetBool("doorOpening", true);
            openCollider.SetActive(false);
        }
    }
}
