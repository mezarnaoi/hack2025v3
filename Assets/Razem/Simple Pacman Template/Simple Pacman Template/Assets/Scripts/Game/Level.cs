
using UnityEngine;

public class Level: MonoBehaviour
{
    public Transform center;
    public Transform initialGhostPosition;
    public Transform initialPecboyPosition;
    public GameObject[] pointsPrefab;

    public int quantityPoints;
    public int points;

    private void Start()
    {
        quantityPoints = points;
    }

    public void ActivePoints()
    {
        for (int i = 0; i < pointsPrefab.Length; i++)
        {
            pointsPrefab[i].SetActive(true);
        }
        points = quantityPoints;
    }
}
