using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PotionUI : MonoBehaviour
{
    public int maxPotions = 3;
    public Image potionBarImage;
    public Sprite[] potionBarSprites;
    public HealthUI healthUI;

    private InventoryController inventory;

    void Start()
    {
        StartCoroutine(InitializePotionUI());
    }

    private IEnumerator InitializePotionUI()
    {
        yield return new WaitForSeconds(0.1f); // delay pro inv carregar
        
        inventory = FindAnyObjectByType<InventoryController>();
        healthUI = FindAnyObjectByType<HealthUI>();
        
        if (inventory != null)
        {
            UpdatePotionBar(); // carregou o inv
        }
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H) || Input.GetKeyDown(KeyCode.Alpha4)) 
        {
            UsePotion();
        }
    }

    public void UsePotion()
    {
        if (inventory != null && inventory.potionCount > 0)
        {
            inventory.potionCount--;
            UpdatePotionBar();

            if (healthUI != null)
            {
                healthUI.Heal(2);
            }
        }
    }

    public void UpdatePotionBar()
    {
        if (inventory != null)
        {
            int spriteIndex = inventory.potionCount;

            if (spriteIndex >= 0 && spriteIndex < potionBarSprites.Length)
            {
                potionBarImage.sprite = potionBarSprites[spriteIndex];
            }
            else
            {
                Debug.LogError($"Index fora dos limites do array potionBarSprites: {spriteIndex}");
            }
        }
    }
}