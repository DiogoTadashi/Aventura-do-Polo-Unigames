using System.Collections;
using UnityEngine;

public class SlimeAttack : MonoBehaviour
{
    public int contactDamage = 1; // Dano causado ao jogador
    public LayerMask playerLayer;
    private HealthUI healthUI;
    private bool canDealDamage = true; // Controla se o dano pode ser aplicado
    public float damageCooldown = 2f; // Tempo entre cada dano

    void Start()
    {
        healthUI = FindAnyObjectByType<HealthUI>();

        if (healthUI == null)
        {
            Debug.LogError("🚨 HealthUI não foi encontrado! Certifique-se de que está na cena.");
        }
    }

    private void OnTriggerStay2D(Collider2D other) // Aplica dano continuamente enquanto encostado
    {
        if (other.CompareTag("Player") && canDealDamage)
        {
            ApplyDamage();
        }
    }

    private void OnTriggerEnter2D(Collider2D other) // Aplica dano ao tocar pela primeira vez
    {
        if (other.CompareTag("Player") && canDealDamage)
        {
            ApplyDamage();
        }
    }

    private void ApplyDamage()
    {
        if (healthUI != null)
        {
            healthUI.TakeDamage(contactDamage);

            StartCoroutine(DamageCooldown()); // Inicia cooldown para evitar dano excessivo
        }
    }

    IEnumerator DamageCooldown()
    {
        canDealDamage = false;
        yield return new WaitForSeconds(damageCooldown);
        canDealDamage = true;
    }
}