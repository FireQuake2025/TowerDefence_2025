using JetBrains.Annotations;
using UnityEngine;

public class BallistaTower : Tower
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private int towerUpgradeCost;

    public Transform ballista;
    public GameObject UpgradedTower;
    public GameObject bullet;

    private GoldManagment goldManagment;
    private BallistaArrow arrow;
    private AudioSource BulletShot;
    public void Start()
    {
        goldManagment = FindAnyObjectByType<GoldManagment>();
        BulletShot = GetComponent<AudioSource>();
    }
    protected override void Update()
    {
        base.Update();
        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            if (goldManagment.currentGold >= towerUpgradeCost)
            {
                goldManagment.currentGold -= towerUpgradeCost;
                UpGradeTurrentTowerOnClik();
            }
            else
            {
                Debug.Log("Not Enough Gold");
            }
        }
        if (Input.GetKeyUp(KeyCode.Alpha4))
        {
            if (goldManagment.currentGold >= towerUpgradeCost)
            {
                goldManagment.currentGold -= towerUpgradeCost;
                UpGradeBallistaTowerOnClick();
            }
        }
    }

    protected override void FireAt(Enemy target)
    {
        if (projectilePrefab != null)
        {
            GameObject projectileInstance = Instantiate(projectilePrefab, ballista.transform.position, Quaternion.identity);
            projectileInstance.GetComponent<Projectile>().SetTarget(target.transform);
            BulletShot.Play();
        }
    }

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

    public void UpGradeTurrentTowerOnClik()
    {
        arrow = bullet.GetComponent<BallistaArrow>();
        arrow.damage = 20;
        UpgradedTower.SetActive(true);
    }

    public void UpGradeBallistaTowerOnClick()
    {
        SphereCollider collider = gameObject.GetComponent<SphereCollider>(); 
        arrow = bullet.GetComponent <BallistaArrow>();
        collider.radius = 10;
        arrow.damage = 50;
        UpgradedTower.SetActive(true);
    }
}
