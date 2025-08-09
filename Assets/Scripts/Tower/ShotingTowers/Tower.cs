using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public abstract class Tower : MonoBehaviour
{
    public float FireCoolDown = 1;
    public int TowerCost;
    private TowerPlacementManager placementManager;

    protected float currentFireCoolDown = 0;
    protected List<Enemy> enemiesInRange = new List<Enemy>();


    protected virtual void Update()
    {
        currentFireCoolDown -= Time.deltaTime;
        Enemy enemy = GetTargetEnemy();
        placementManager = FindAnyObjectByType<TowerPlacementManager>();
        if (enemy != null && currentFireCoolDown <= 0)
        {
            if (!placementManager.isPlacingTower)
            {
                FireAt(enemy);
                currentFireCoolDown = FireCoolDown;
            }
        }
    }
    protected void ClearDestroyedEnemies()
    {
        for (int i = enemiesInRange.Count - 1; i >= 0; i--)
        {
            if (enemiesInRange[i] == null)
            {
                enemiesInRange.RemoveAt(i);
            }
        }
    }

    protected abstract Enemy GetTargetEnemy();

    protected abstract void FireAt(Enemy target);

    private void OnTriggerEnter(Collider other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy != null && !enemiesInRange.Contains(enemy))
        {
            enemiesInRange.Add(enemy);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Enemy enemy = GetComponent<Enemy>();
        if (enemy != null && enemiesInRange.Contains(enemy))
        {
            enemiesInRange.Remove(enemy);
        }
    }
}
