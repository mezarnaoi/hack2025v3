using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float distance = 5f;
    [SerializeField] private float sensitivity = 2f;
    [SerializeField] private float minY = -20f, maxY = 80f;
    [SerializeField] private float smoothTime = 0.1f; // Ajustează pentru netezime

    private float currentX = 0f;
    private float currentY = 30f;
    private Vector3 velocity = Vector3.zero;

    private void LateUpdate()
    {
        if (Input.GetMouseButton(1))
        {
            currentX += Input.GetAxis("Mouse X") * sensitivity;
            currentY -= Input.GetAxis("Mouse Y") * sensitivity;
            currentY = Mathf.Clamp(currentY, minY, maxY);
        }

        Vector3 dir = new Vector3(0, 0, -distance);
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        Vector3 targetPosition = target.position + rotation * dir;

        // Folosește SmoothDamp pentru mișcare netedă
        transform.position = Vector3.SmoothDamp(
            transform.position,
            targetPosition,
            ref velocity,
            smoothTime
        );

        transform.LookAt(target.position);
    }
}