using UnityEngine;

public class PlayerTransformation1 : MonoBehaviour
{
    public GameObject playerNormal;
    public GameObject chickenForm;
    public GameObject chickenFor;

    public void TransformBackToHuman()
    {

        // Salvează și setează poziția găinii pentru om
        Vector3 chickenPosition = chickenForm.transform.position;
        Quaternion chickenRotation = chickenForm.transform.rotation;

        playerNormal.transform.position = chickenPosition;
        playerNormal.transform.rotation = chickenRotation;
        playerNormal.SetActive(true);

        // Dezactivează găina
        chickenForm.SetActive(false);
        chickenFor.SetActive(false);
    }
}