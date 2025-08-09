using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public GameObject GameOverScreen;
    public Health health;
    public GameObject player;

    [SerializeField] string LevelRestart;
    [SerializeField] string NextLevel;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        PlayerDeath();
    }

    private void PlayerDeath()
    {
        health = player.GetComponent<Health>();
        if(health.currentHealth <= 0)
        {
            GameOverScreen.SetActive(true);
        }
    }

    public void NextLevelOnClick()
    {
        SceneManager.LoadScene(NextLevel);
    }

    //Restarts level
    public void RestartOnClick()
    {
        SceneManager.LoadScene(LevelRestart);
    }

    //Exits game to main menu
    public void GoToMenuOnClick()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
