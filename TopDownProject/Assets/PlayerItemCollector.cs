using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerItemCollector : MonoBehaviour
{
    private InventoryController inventoryController;

    void Start()
    {
        inventoryController = FindAnyObjectByType<InventoryController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Item"))
        {
            Item item = collision.GetComponent<Item>();
            PlayerCombat playerCombat = GetComponent<PlayerCombat>(); // Obtendo referência ao PlayerCombat

            if (item != null)
            {
                if (item.ID == 1) // Chaves
                {
                    inventoryController.AddKey();
                    Destroy(collision.gameObject);
                }

                else if (item.ID == 2) // Poções
                {
                    bool potionAdded = inventoryController.AddPotion();
                    if (potionAdded) Destroy(collision.gameObject);
                }
                
                else if (item.ID == 3) // **Espada**
                {
                    if (playerCombat != null) 
                    {
                        playerCombat.UnlockAttack();
                    }
                    Destroy(collision.gameObject); 
                }
            }
        }
    }
}