using UnityEngine;

public class PlayerAttack1 : MonoBehaviour
{
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // click stânga
        {
            animator.SetTrigger("attack");
        }
    }
}
