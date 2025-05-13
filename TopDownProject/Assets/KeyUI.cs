using UnityEngine;
using UnityEngine.UI;

public class KeyUI : MonoBehaviour
{
    public Image keyImage; // Imagem da chave na UI
    public Sprite keySpriteActive; // Sprite da chave quando coletada
    public Sprite keySpriteInactive; // Sprite da chave quando não coletada

    private InventoryController inventory;

    void Start()
    {
        inventory = FindAnyObjectByType<InventoryController>();

        if (inventory == null)
        {
            Debug.LogError("InventoryController não foi encontrado! Certifique-se de que está presente na cena.");
            return;
        }

        UpdateKeyUI(inventory.HasKey);
    }

    public void UpdateKeyUI(bool hasKey)
    {
        keyImage.sprite = hasKey ? keySpriteActive : keySpriteInactive;
    }
}