using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    private bool playerInRange = false;
    private InventorySystem inventory;
    private InventoryItem item; // Referencia al script del objeto

    void Start()
    {
        
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            PickUp();
        }
        if(inventory == null){inventory = FindObjectOfType<InventorySystem>();
        item = GetComponent<InventoryItem>();}
        

        if (inventory == null)
        {
            Debug.LogError("No se encontró el InventorySystem en la escena.");
        }
        if (item == null)
        {
            Debug.LogError("El objeto no tiene un componente InventoryItem.");
        }
    }

    void PickUp()
    {
        if (inventory != null && item != null)
        {
            inventory.AddItem(item.itemID); // Envía el ID del objeto al inventario
            Destroy(gameObject); // Elimina el objeto del mundo
            Debug.Log("Item recogido: " + item.itemName);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            Debug.Log("Presiona E para recoger " + (item != null ? item.itemName : "Objeto"));
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}