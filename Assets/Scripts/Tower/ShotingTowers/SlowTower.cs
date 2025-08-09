using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class SlowTower : Tower
{
    [SerializeField] private int towerUpgradeCost;

    private bool isUpgraded = false;
    private GoldManagment goldManagment;

    public Transform upgradeTower;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        goldManagment = FindAnyObjectByType<GoldManagment>();
    }

    // Update is called once per frame
    protected override void  Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (goldManagment.currentGold >= towerUpgradeCost)
            {
                upgradeTower.transform.localScale = new Vector3(2,2,2);
                goldManagment.currentGold -= towerUpgradeCost;
                isUpgraded = true;
            }
        }
    }

    //Lower enemies speed 
    //Temporarily stops enemy on upgrade
    private IEnumerator OnTriggerEnter(Collider other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        NavMeshAgent agent = other.GetComponent<NavMeshAgent>();
        if (enemy != null)
        {
            if (isUpgraded)
            {
                enemy.speed = 0;
                agent.speed = enemy.speed;
                yield return new WaitForSeconds(3);
                enemy.speed = 1;
                agent.speed = enemy.speed;
            }
            else
            {
                enemy.speed = 1;
                agent.speed = enemy.speed;
            }
        }
    }

    //Returns speed to normal
    private void OnTriggerExit(Collider other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        NavMeshAgent agent = other.GetComponent <NavMeshAgent>();
        if (enemy != null)
        {
            enemy.speed = 3;
            agent.speed = enemy.speed;
        }
    }



    protected override Enemy GetTargetEnemy()
    {
        throw new System.NotImplementedException();
    }

    protected override void FireAt(Enemy target)
    {
        throw new System.NotImplementedException();
    }

    
}
