using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public int potionCount = 3;
    public int maxPotions = 3;
    public bool HasKey = false;
    public bool HasSword = false;

    private KeyUI keyUI;
    private PotionUI potionUI;

    void Start()
    {
        keyUI = FindAnyObjectByType<KeyUI>();
        potionUI = FindAnyObjectByType<PotionUI>();

        if (keyUI == null)
        {
            Debug.LogError("KeyUI não foi encontrado! Certifique-se de que está na cena.");
        }

        if (potionUI == null)
        {
            Debug.LogError("PotionUI não foi encontrado! Certifique-se de que está na cena.");
        }
        
        potionCount = maxPotions;

        UpdateUI();
    }

    public bool AddPotion()
    {
        if (potionCount < maxPotions)
        {
            potionCount++;
            UpdateUI();
            return true;
        }
        return false;
    }

    public void UsePotion()
    {
        if (potionCount > 0)
        {
            potionCount--;
            UpdateUI();
        }
    }

    public void AddKey()
    {
        HasKey = true;
        UpdateUI();
    }

    public void UseKey()
    {
        HasKey = false;
        UpdateUI();
    }

    public void AddSword()
    {
        HasSword = true;
    }

    public void UpdateUI()
    {
        if (keyUI != null)
        {
            keyUI.UpdateKeyUI(HasKey);
        }

        if (potionUI != null)
        {
            potionUI.UpdatePotionBar();
        }
    }
}