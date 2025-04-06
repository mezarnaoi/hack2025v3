using UnityEngine;

public class PlayerTransformation : MonoBehaviour
{
    public GameObject playerNormal;
    public GameObject chickenForm;
    public GameObject chickenFor;

    public void TransformIntoChicken()
    {
        Vector3 humanPosition = playerNormal.transform.position;
        Quaternion humanRotation = playerNormal.transform.rotation;
        chickenForm.transform.position = humanPosition;
        chickenForm.transform.rotation = humanRotation;
        chickenForm.SetActive(true);
        chickenFor.SetActive(true);

        // Dezactivăm player-ul normal
        playerNormal.SetActive(false);
    }
}
