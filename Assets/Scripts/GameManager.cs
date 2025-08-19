using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Game Over UI")]
    public GameObject gameOverPanel;  // Assign in Inspector

    private bool isGameOver = false;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Update()
    {
        if (isGameOver && Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }
    }

    public void GameOver()
    {
        Time.timeScale = 0f; // Pause game
        isGameOver = true;

        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        Debug.Log("Game Over! Press R to Restart.");
    }

    public void RestartGame()
    {
        Time.timeScale = 1f; // Resume
        isGameOver = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
