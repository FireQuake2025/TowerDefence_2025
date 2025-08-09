using UnityEngine;

public class CannonTower : Tower
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private int towerUpgradeCost;

    private CannonBall cannonBall;
    private GoldManagment goldManagment;

    public GameObject bolder;
    public Transform cannon;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    //Tower Upgraded on press of 3 key
    protected override void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            goldManagment = FindAnyObjectByType<GoldManagment>();
            cannonBall = bolder.GetComponent<CannonBall>();
            if(goldManagment.currentGold <= towerUpgradeCost)
            {
                goldManagment.currentGold -= towerUpgradeCost;
                cannonBall.explosionRadius = 10;
                cannonBall.damage = 50;
            }
        }
    }

    //Creats projectile
    protected override void FireAt(Enemy target)
    {
        if (projectilePrefab != null)
        {
            GameObject projectileInstance = Instantiate(projectilePrefab, cannon.transform.position, Quaternion.identity);
            projectileInstance.GetComponent<Projectile>().SetTarget(target.transform);
        }
    }

    //Sets enemy game objext to target
    protected override Enemy GetTargetEnemy()
    {
        ClearDestroyedEnemies();

        Enemy closestEnemy = null;
        float closestDistance = float.MaxValue;
        foreach (Enemy enemy in enemiesInRange)
        {
            if (enemy != null)
            {
                float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
                if (distanceToEnemy < closestDistance)
                {
                    closestDistance = distanceToEnemy;
                    closestEnemy = enemy;
                }
            }

        }

        return closestEnemy;
    }
}
