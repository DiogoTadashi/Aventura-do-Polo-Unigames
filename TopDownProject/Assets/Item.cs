using System.Collections;
using UnityEngine;

public class Item : MonoBehaviour
{
    private Collider2D itemCollider;
    public int ID;
    private bool collected = false;

    void Start()
    {
        itemCollider = GetComponent<Collider2D>();
        StartCoroutine(EnableCollectionAfterDelay(2f));
    }

    private IEnumerator EnableCollectionAfterDelay(float seconds)
    {
        itemCollider.enabled = false;
        yield return new WaitForSeconds(seconds);

        if (!collected)
        {
            itemCollider.enabled = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!collected && other.CompareTag("Player"))
        {
            InventoryController inventory = other.GetComponent<InventoryController>();
            PlayerCombat playerCombat = other.GetComponent<PlayerCombat>();

            if (inventory != null)
            {
                collected = true;
                itemCollider.enabled = false;

                if (ID == 1)
                {
                    inventory.AddKey();
                }
                else if (ID == 2) // Se for poção
                {
                    if (inventory.AddPotion())
                    {
                        PotionUI potionUI = FindAnyObjectByType<PotionUI>();
                        if (potionUI != null)
                        {
                            potionUI.UpdatePotionBar();
                        }
                    }
                }

                else if (ID == 3) // se for espada
                {
                    if (playerCombat != null)
                    {
                        inventory.AddSword();
                        playerCombat.UnlockAttack(); // Habilita o ataque
                    }
                }

                gameObject.SetActive(false);
                Destroy(gameObject, 0.1f);
            }
        }
    }
}