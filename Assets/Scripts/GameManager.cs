using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public GameObject player;
    public GameObject startButton;
    public TextMeshProUGUI gameOverCounter;
    public float counterTimer = 5f;
    private bool isGameOver = false;
    void Start()
    {
        gameOverCounter.gameObject.SetActive(false);
        Time.timeScale = 0f;
    }

    void Update()
    {
        if (isGameOver)
        {
            counterTimer -= Time.unscaledDeltaTime;
            gameOverCounter.text = "Restarting in: " + Mathf.Ceil(counterTimer).ToString();
            if (counterTimer <= 0)
            {
                //SceneManager.LoadScene(SceneManager.GetActiveScene().name());
                SceneManager.LoadScene("GamePlay"); // Name of My Scene
                Time.timeScale = 1f;
            }
        }
    }

    public void StartGame()
    {
        startButton.SetActive(false);
        player.GetComponent<Player>().EnableBirdPhysics();
        Time.timeScale = 1f;
    }

    public void GameOver()
    {
        isGameOver = true;
        gameOverCounter.gameObject.SetActive(true);
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        // SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene("GamePlay"); // Name of My Scene
        Time.timeScale = 1f;
    }
}
