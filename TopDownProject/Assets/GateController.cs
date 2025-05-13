using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GateController : MonoBehaviour, IInteractable
{
    public bool IsOpened { get; private set; }
    public string GateId { get; private set; }
    public Sprite openedSprite;
    public Collider2D gateCollider; // Collider do portão
    private Animator animator;
    private InventoryController inventory;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        GateId ??= GlobalHelper.GenerateUniqueID(gameObject);
        inventory = FindAnyObjectByType<InventoryController>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (inventory == null)
            Debug.LogError("❌ InventoryController não encontrado!");
        if (animator == null)
            Debug.LogError("❌ Animator não encontrado!");
        if (spriteRenderer == null)
            Debug.LogError("❌ SpriteRenderer não encontrado!");
    }

    public bool CanInteract()
    {
        return !IsOpened && inventory.HasKey; // Verifica se o jogador tem a chave
    }

    public void Interact()
    {
        if (!CanInteract()) 
        {
            return;
        }

        inventory.UseKey(); // Remove a chave
        animator.SetTrigger("OpenGate"); // Inicia a animação
        StartCoroutine(OpenGateAfterAnimation()); // Aguarda a animação antes de mudar o sprite
    }

    private IEnumerator OpenGateAfterAnimation()
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        if (openedSprite != null)
        {
            spriteRenderer.sprite = openedSprite;
        }
        else
        {
            Debug.LogError("❌ O sprite do portão aberto não foi configurado.");
        }

        if (gateCollider != null)
        {
            gateCollider.enabled = false;
        }
        else
        {
            Debug.LogError("❌ Collider do portão não encontrado!");
        }

        IsOpened = true;
    }
}