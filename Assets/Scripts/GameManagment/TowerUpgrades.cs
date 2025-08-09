using UnityEngine;

public class TowerUpgrades : MonoBehaviour
{
    public BallistaTower ballistaTower;
    public BallistaArrow ballistaArrow;
    private GoldManagment goldManagment;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ballistaTower = FindAnyObjectByType<BallistaTower>();
        ballistaArrow = FindAnyObjectByType<BallistaArrow>();
        goldManagment = FindAnyObjectByType<GoldManagment>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpGradeBallistaTowerOnClik()
    {
        if (goldManagment.currentGold >= 500)
        {
            ballistaTower.FireCoolDown = 1;
            ballistaArrow.damage = 50;
        }
    }

    void UpGradeCatapultTowerOnClik()
    {

    }

    

    void UpGradeSlowTowerOnClik()
    {

    }
}
