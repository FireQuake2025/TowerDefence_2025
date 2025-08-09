using TMPro;
using UnityEngine;

public class GoldManagment : MonoBehaviour
{
    public int currentGold = 20;

    private Enemy enemy;

    [SerializeField] private TextMeshProUGUI goldText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemy = GetComponent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        goldText.text = "Gold: " + currentGold;
    }

    //adds gold to players resorces
    public void DropGold()
    {
        enemy = FindAnyObjectByType<Enemy>();
        currentGold += enemy.addGold;
    }
}
