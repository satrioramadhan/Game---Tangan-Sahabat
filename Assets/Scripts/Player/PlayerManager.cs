using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    public static bool isGameOver;
    public GameObject gameOverScreen;
    public GameObject pauseMenuScreen;
    public GameObject player;
    public static Vector2 lastCheckPointPos = new Vector2(-7, 0);
    public static int numberOfMemo;
    public TextMeshProUGUI memoText;

    [Header("Jatuh Mati")]
    public float deathY = -10f;

    private void Awake()
    {
        numberOfMemo = 0;
        isGameOver = false;
        var spawn = GameObject.Find("SpawnPoint");
        if (spawn != null)
        {
            GameObject.FindGameObjectWithTag("Player").transform.position = spawn.transform.position;
        }
        else
        {
            GameObject.FindGameObjectWithTag("Player").transform.position = lastCheckPointPos;
        }

    }

    void Update()
    {
        memoText.text = numberOfMemo.ToString();

        if (isGameOver)
        {
            gameOverScreen.SetActive(true);
        }

        if (!isGameOver && player.transform.position.y < deathY)
        {
            Die();
        }
    }

    public void Die()
    {
        isGameOver = true;
        gameOverScreen.SetActive(true);

        if (AudioManager.instance != null)
        {
            AudioManager.instance.Play("GameOver");
        }

        player.GetComponent<PlayerMovement>().controls.Disable(); // 🔥 Disable input biar gak nyangkut
        player.SetActive(false);
    }


    public void ReplayLevel()
    {
        isGameOver = false;
        gameOverScreen.SetActive(false);
        player.SetActive(true);
        player.transform.position = lastCheckPointPos;

        // Tambahan penting bro!
        player.GetComponent<PlayerMovement>().Unfreeze();
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        pauseMenuScreen.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        pauseMenuScreen.SetActive(false);
    }

    public void GoToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }
}
