using UnityEngine;

public class DoorOpener : MonoBehaviour
{
    private bool shouldOpen = false;
    public float rotationSpeed = 60f;
    private Quaternion targetRotation;

    void Start()
    {
        targetRotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y + 90, 0);
    }

    void Update()
    {
        if (shouldOpen)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    public void Open()
    {
        shouldOpen = true;
        MeshRenderer mesh = GetComponent<MeshRenderer>();
        if (mesh != null)
            mesh.enabled = false;

        Collider col = GetComponent<Collider>();
        if (col != null)
            col.enabled = false;

        foreach (Transform child in transform)
        {
            if (child.GetComponent<MeshRenderer>() != null)
                child.GetComponent<MeshRenderer>().enabled = false;
        }

        Debug.Log("Poarta s-a deschis (vizual și fizic)!");
    }
}
