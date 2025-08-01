using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Health playerHeath;

    public static GameManager Instance { get; private set; }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        playerHeath = GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
