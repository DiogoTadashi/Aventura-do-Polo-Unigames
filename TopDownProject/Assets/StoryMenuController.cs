using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoryMenuController : MonoBehaviour
{
    public void OnContinueClick()
    {
        // 🔄 Resetar progresso ao iniciar um novo jogo
        PlayerPrefs.DeleteKey("GameStarted");
        PlayerPrefs.DeleteKey("BonecoNeveDerrotado"); // ✅ Garantir que o Boneco de Neve renasça
        PlayerPrefs.DeleteKey("SpawnX");
        PlayerPrefs.DeleteKey("SpawnY");
        PlayerPrefs.DeleteKey("SpawnZ");
        PlayerPrefs.SetInt("HasSword", 0);
        PlayerPrefs.Save(); // 🔄 Apagar tudo antes de carregar a cena

        BonecoNeveSH.snowmanDefeated = false;

        SceneManager.LoadScene("GameScene");
    }


    public void ExitGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("StartScreen");
    }
}
