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
        if (Input.GetMouseButtonDown(0)) // click st�nga
        {
            animator.SetTrigger("attack");
        }
    }
}
