using UnityEngine;

public class DogEnemy : MonoBehaviour
{
    public float health = 100f;
    public GameObject text;

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        
        gameObject.SetActive(false);
        text.SetActive(true);
    }
}
