using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
public class ChickenPlayerController : MonoBehaviour
{
    public float moveSpeed = 1.2f;
    public float turnSpeed = 360f;

    private CharacterController controller;
    private Animator animator;
    private Camera mainCam;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        mainCam = Camera.main;
    }

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Vector3 input = new Vector3(h, 0, v).normalized;

        Vector3 forward = mainCam.transform.forward;
        forward.y = 0;
        forward.Normalize();

        Vector3 right = mainCam.transform.right;
        right.y = 0;
        right.Normalize();

        Vector3 moveDir = (forward * v + right * h).normalized;

        if (moveDir.magnitude >= 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDir);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
        }

        controller.Move(moveDir * moveSpeed * Time.deltaTime + Vector3.down * 1f);

        animator.SetFloat("Speed", moveDir.magnitude);
    }
}
