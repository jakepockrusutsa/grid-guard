using UnityEngine;

public class TowerBase : MonoBehaviour
{
    [Header("Tower Data")]
    public TowerData data;

    [Header("Current Stats")]
    public float currentRange;
    public float currentAttackSpeed;
    public float currentDamage;

    private float attackCooldown = 0f;

    // Implement later. Involves moving tower part that shoots projectiles
    public Transform towerHead;
    public GameObject projectilePrefab;


    void Start()
    {
        // initialize starting stats from selected TowerData
        if (data != null)
        {
            currentRange = data.range;
            currentAttackSpeed = data.attackSpeed;
            currentDamage = data.damage;
        }
        else
        {
            Debug.LogError("TowerData not assigned on " + gameObject.name);
        }
    }

    void Update()
    {
        if (attackCooldown > 0f)
            attackCooldown -= Time.deltaTime;
        else
        {
            // Find a target within range
            GameObject target = FindTarget();
            if (target != null)
            {
                Attack(target);
                attackCooldown = 1f / currentAttackSpeed; // Using attackSpeed as attacks per second.
            }
        }
    }

    GameObject FindTarget()
    {
        // Check for enemies within a sphere around the tower.
        Collider[] hits = Physics.OverlapSphere(transform.position, currentRange);
        foreach (Collider hit in hits)
        {
            if (hit.CompareTag("Enemy"))
                return hit.gameObject;
        }
        return null;
    }

    void Attack(GameObject target)
    {
        // Rotate tower head to face target. IMPLEMENT TOWER HEAD LATER
        if (towerHead != null)
        {
            Vector3 direction = target.transform.position - towerHead.position;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            towerHead.rotation = Quaternion.Slerp(towerHead.rotation, lookRotation, Time.deltaTime * 10f);
        }
        
        // Instantiate projectile at the tower heads position.
        if (projectilePrefab != null)
        {
            GameObject projInstance = Instantiate(projectilePrefab, towerHead.position, Quaternion.identity);
            Projectile projScript = projInstance.GetComponent<Projectile>();
            if (projScript != null)
            {
                // Set the target and damage for the projectile.
                projScript.SetTarget(target.transform, currentDamage);
            }
        }
        else
        {
            Debug.LogError("Projectile prefab not assigned on " + gameObject.name);
        }
    }


    // Upgrade methods
    public void UpgradeRange()
    {
        currentRange += data.rangeUpgradeIncrement;
    }

    public void UpgradeAttackSpeed()
    {
        currentAttackSpeed += data.attackSpeedUpgradeIncrement;
    }

    public void UpgradeDamage()
    {
        currentDamage += data.damageUpgradeIncrement;
    }
}
