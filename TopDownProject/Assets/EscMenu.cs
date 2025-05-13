using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscMenu : MonoBehaviour
{
    public GameObject escMenuUI;
    private bool isGamePaused = false;

    void Start()
    {
        if (escMenuUI != null)
        {
            escMenuUI.SetActive(false);
        }
        else
        {
            Debug.LogError("escMenuUI não está atribuído!");
        }

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        if (escMenuUI == null)
        {
            Debug.LogError("escMenuUI não está atribuído!");
            return;
        }

        isGamePaused = !isGamePaused;
        Time.timeScale = isGamePaused ? 0f : 1f;
        escMenuUI.SetActive(isGamePaused);
    }

    public void CloseTutorial(GameObject tutorialPopup)
    {
        if (!escMenuUI.activeSelf)
        {
            Time.timeScale = 1f;
            isGamePaused = false;
        }
    }

    public void OnRestartClick()
    {
        PlayerPrefs.DeleteKey("GameStarted");
        PlayerPrefs.DeleteKey("BonecoNeveDerrotado");
        PlayerPrefs.DeleteKey("SpawnX");
        PlayerPrefs.DeleteKey("SpawnY");
        PlayerPrefs.DeleteKey("SpawnZ");
        PlayerPrefs.SetInt("HasSword", 0);
        PlayerPrefs.Save();

        BonecoNeveSH.snowmanDefeated = false;

        SceneManager.LoadScene("GameScene");
    }

    public void ExitGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("StartScreen");
    }
}