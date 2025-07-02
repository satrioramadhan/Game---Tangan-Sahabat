using UnityEngine;
using UnityEngine.SceneManagement;

public class CeritaTrigger : MonoBehaviour
{
    public GameObject ceritaPanel;     // Panel berisi cerita + tombol lanjutkan
    public GameObject player;          // Referensi player
    public string nextSceneName;       // Contoh: "Scenes/Level2"

    private bool sudahTrigger = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!sudahTrigger && other.CompareTag("Player"))
        {
            sudahTrigger = true;

            ceritaPanel.SetActive(true); // Tampilkan panel cerita
            Time.timeScale = 0f;         // Pause game

            if (player != null)
                player.GetComponent<PlayerMovement>().isFrozen = true;

            AudioManager.instance.Stop("Steps");
        }
    }

    // Fungsi ini dipanggil saat tombol "Lanjutkan" diklik
    public void KonfirmasiLanjut()
    {
        Time.timeScale = 1f;

        if (player != null)
            player.GetComponent<PlayerMovement>().isFrozen = false;

        SceneManager.LoadScene(nextSceneName);
    }
}
