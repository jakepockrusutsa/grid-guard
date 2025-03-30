using UnityEngine;

// Allows for creation of TowerData assets in unity
[CreateAssetMenu(fileName = "NewTowerData", menuName = "Tower Defense/Tower Data", order = 1)]
public class TowerData : ScriptableObject
{
    public string towerName;

    // Base stats for the tower
    public float range;
    public float attackSpeed;  // Attacks per second
    public float damage;

    // Upgrade increments [POSSIBLY NEEDS EDITING]
    public float rangeUpgradeIncrement;
    public float attackSpeedUpgradeIncrement;
    public float damageUpgradeIncrement;

    // Costs $$$$$$$$$$$$$$$$$$
    public int purchaseCost;
    public int upgradeCost;
}
