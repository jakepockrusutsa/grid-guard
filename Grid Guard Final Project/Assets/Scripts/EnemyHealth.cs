using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health = 50f;

    public void TakeDamage(float amount)
    {
        health -= amount;
        Debug.Log(gameObject.name + " takes " + amount + " damage. Remaining health: " + health);
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
