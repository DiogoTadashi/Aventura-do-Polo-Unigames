using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject gameOverScreen;
    public static event Action OnReset;

    public Transform safeHouseSpawnPoint;
    public Transform currentSpawnPoint;

    void Start()
    {
        HealthUI.OnPlayerDied += GameOverScreen;
        gameOverScreen.SetActive(false);
        // Sempre começa no SpawnPoint original ao iniciar um novo jogo (não salva entre sessões)
        if (!PlayerPrefs.HasKey("GameStarted")) 
        {
            PlayerPrefs.SetInt("GameStarted", 1); // Define que o jogo começou
            PlayerPrefs.SetInt("BonecoNeveDerrotado", 0);
            currentSpawnPoint = GameObject.FindWithTag("SpawnPoint").transform;
        }
        else
        {
            // Se o jogador já derrotou o Boneco de Neve, inicia na Casa Segura
            if (PlayerPrefs.GetInt("BonecoNeveDerrotado") == 1)
            {
                currentSpawnPoint = safeHouseSpawnPoint;
            }
            else
            {
                currentSpawnPoint = GameObject.FindWithTag("SpawnPoint").transform;
            }
        }

        // 🚀 Move o jogador para o spawn correto
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            player.transform.position = currentSpawnPoint.position;
        }
    }

    void GameOverScreen()
    {
        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void ResetGame()
    {
        if (!BonecoNeveSH.snowmanDefeated)
        {
            BonecoNeveSH.snowmanDefeated = false;
        }

        OnReset?.Invoke();
        gameOverScreen.SetActive(false);
        Time.timeScale = 1;

        InventoryController inventory = FindAnyObjectByType<InventoryController>();
        if (inventory != null)
        {
            inventory.potionCount = inventory.maxPotions;
            inventory.HasKey = false;
        }

        // Garante que ao resetar ele nasce no lugar certo
        PlayerPrefs.SetInt("GameStarted", 1);
        if (BonecoNeveSH.snowmanDefeated)
        {
            PlayerPrefs.SetInt("BonecoNeveDerrotado", 1); // Marca que o Boneco foi derrotado
        }
        else
        {
            PlayerPrefs.SetInt("BonecoNeveDerrotado", 0);
        }
        PlayerPrefs.Save();

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ExitGame()
    {
        // Ao sair do jogo, apagar progresso salvo (exceto status do Boneco de Neve)
        PlayerPrefs.DeleteKey("GameStarted");
        PlayerPrefs.DeleteKey("BonecoNeveDerrotado");
        PlayerPrefs.DeleteKey("SpawnX");
        PlayerPrefs.DeleteKey("SpawnY");
        PlayerPrefs.DeleteKey("SpawnZ");
        PlayerPrefs.SetInt("HasSword", 0); // Remove a espada ao sair
        PlayerPrefs.Save();
        PlayerPrefs.Save();

        Time.timeScale = 1;
        SceneManager.LoadScene("StartScreen");
    }

    public void ChangeSpawnPoint()
    {
        GameObject safeHouse = GameObject.FindGameObjectWithTag("safeHouseSpawnPoint");

        if (safeHouse != null)
        {
            currentSpawnPoint = safeHouse.transform;
            PlayerPrefs.SetInt("BonecoNeveDerrotado", 1); // Marca que o Boneco foi derrotado
            PlayerPrefs.Save();
        }
    }

    public Transform GetSpawnPoint()
    {
        return currentSpawnPoint;
    }
}