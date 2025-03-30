using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 15f;
    public float lifetime = 5f;    // Lifetime before the projectile is destroyed
    private float damage;
    private Transform target;      // The target enemy

    // assigns projectiles target and damage
    public void SetTarget(Transform targetTransform, float dmg)
    {
        target = targetTransform;
        damage = dmg;
    }

    void Update()
    {
        // move at assigned target
        if (target != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
        }
        else
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

        // Decrease lifetime and destroy if time is up
        lifetime -= Time.deltaTime;
        if (lifetime <= 0f)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // If hit an enemy deal damage and destroy the projectile
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Target has been hit!");
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
    }
}
