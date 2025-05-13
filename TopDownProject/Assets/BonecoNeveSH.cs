using UnityEngine;

public class BonecoNeveSH : MonoBehaviour
{
    private GameController gameController;
    public static bool snowmanDefeated = false;

    void Awake()
    {
        // 🔄 Reseta estado do Boneco de Neve ao iniciar um novo jogo!
        if (!PlayerPrefs.HasKey("GameStarted"))
        {
            snowmanDefeated = false;  // ✅ Agora o Boneco sempre renasce ao iniciar um jogo novo
            PlayerPrefs.SetInt("BonecoNeveDerrotado", 0);
            PlayerPrefs.Save();
        }

        CheckSnowmanState(); // ✅ Removido o Invoke para evitar delay desnecessário

        gameController = FindAnyObjectByType<GameController>();
    }

    void CheckSnowmanState()
    {
        if (snowmanDefeated)
        {
            Debug.Log("🚨 Boneco de Neve derrotado! Destruindo.");
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        if (!snowmanDefeated && gameObject.scene.isLoaded) // ✅ Garante que ele só seja derrotado na cena ativa!
        {
            snowmanDefeated = true;

            if (gameController != null)
            {
                gameController.ChangeSpawnPoint();
            }

            PlayerPrefs.SetInt("BonecoNeveDerrotado", 1); // ✅ Agora salva corretamente que o Boneco foi derrotado
            PlayerPrefs.Save();

            Debug.Log("✅ Boneco de Neve realmente derrotado! Marcando como eliminado.");
        }
    }
}