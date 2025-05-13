using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    private GameController gameController;

    void Start()
    {
        gameController = FindAnyObjectByType<GameController>();

        if (gameController == null)
        {
            Debug.LogError("🚨 ERRO: GameController não encontrado! Certifique-se de que está na cena.");
            return; // Evita que RespawnPlayer seja chamado sem um GameController válido
        }

        RespawnPlayer();
    }

    public void RespawnPlayer()
    {
        Transform spawnPoint = gameController.GetSpawnPoint();

        if (spawnPoint == null)
        {
            Debug.Log("🚨 ERRO: SpawnPoint não definido! Usando posição padrão.");
            spawnPoint = transform;
        }

        transform.position = spawnPoint.position;
    }
}