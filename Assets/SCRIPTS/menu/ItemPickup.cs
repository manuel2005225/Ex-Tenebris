using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    private bool playerInRange = false;
    private InventorySystem inventory;
    private InventoryItem item; // Referencia al script del objeto

    void Start()
    {
        inventory = FindObjectOfType<InventorySystem>();
        item = GetComponent<InventoryItem>();
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            PickUp();
        }

        if (inventory == null)
        {
            inventory = FindObjectOfType<InventorySystem>();
        }

        if (item == null)
        {
            item = GetComponent<InventoryItem>();
        }

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
            inventory.AddItem(item.itemID);

            // Mostrar notificación de recogida durante 2.5 segundos
            

            Destroy(gameObject);
            Debug.Log("Item recogido: " + item.itemName);
        }
    }

    

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;

            // Ocultar notificación al alejarse
            if (NotificationManager.instance != null)
            {
                NotificationManager.instance.HideNotification();
            }
        }
    }
}
