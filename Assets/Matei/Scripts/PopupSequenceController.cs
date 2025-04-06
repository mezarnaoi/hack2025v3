using UnityEngine;
using System.Collections;

public class PopupSequenceController : MonoBehaviour
{
    public GameObject popup1;
    public GameObject popup2;

    public float delayBetween = 2f;
    public float popup2Duration = 2f;

    void Start()
    {
        StartCoroutine(HandlePopups());
    }

    IEnumerator HandlePopups()
    {
        yield return new WaitForSeconds(delayBetween);
        if (popup1 != null)
            popup1.SetActive(false);

        if (popup2 != null)
            popup2.SetActive(true);

        yield return new WaitForSeconds(popup2Duration);
        if (popup2 != null)
            popup2.SetActive(false);
    }
}
