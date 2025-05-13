using UnityEngine;

public class Slot : MonoBehaviour
{
    public GameObject allowedItem; // Item permitido no slot
    public int itemCount = 0; // Quantidade de itens no slot

    public void AddItem(GameObject item)
    {
        if (item == allowedItem)
        {
            itemCount++;
            Debug.Log($"Item adicionado: {item.name}. Total no slot: {itemCount}");
        }
    }
}