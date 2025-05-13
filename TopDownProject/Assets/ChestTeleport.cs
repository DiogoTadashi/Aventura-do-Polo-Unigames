using UnityEngine;
using System.Collections;

public class ChestTeleport : MonoBehaviour
{
    public GameObject blackScreen;
    public Transform teleportLocation;
    private InventoryController inventory;
    public ScreenFade screenFade;

    void Start()
    {
        blackScreen.SetActive(false);
        inventory = FindAnyObjectByType<InventoryController>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            blackScreen.SetActive(true);
            blackScreen.GetComponent<CanvasGroup>().alpha = 0;
            StartCoroutine(CheckForKeyAfterDelay());
        }
    }

    IEnumerator CheckForKeyAfterDelay()
    {
        yield return new WaitForSeconds(7); // Espera 7 segundos
        
        if (inventory.HasKey) // Se o jogador tem a chave
        {
            yield return StartCoroutine(TeleportPlayer()); // Certifica que a corrotina está sendo aguardada
        }
    }

    IEnumerator TeleportPlayer()
    {
        yield return StartCoroutine(screenFade.FadeIn(1f)); // Aguarda fade-in

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = teleportLocation.position;

        yield return new WaitForSeconds(1f); // Tempo curto para garantir que o teleporte ocorre antes do fade-out
        yield return StartCoroutine(screenFade.FadeOut(1f)); // Faz fade-out

        yield return new WaitForSeconds(0.2f); // Pequena pausa antes de desativar a tela preta
        blackScreen.SetActive(false);
    }
}