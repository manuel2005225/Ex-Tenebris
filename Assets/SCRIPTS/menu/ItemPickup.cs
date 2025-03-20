using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    // ID del objeto que representa
    public int itemID;
    
    // Referencia al sistema de inventario
    private InventorySystem inventory;
    
    void Start()
    {
        // Buscar el sistema de inventario en la escena
        inventory = FindObjectOfType<InventorySystem>();
        
        if (inventory == null)
        {
            Debug.LogError("¡No se encontró el sistema de inventario en la escena!");
        }
    }
    
    // Método para recoger el objeto
    public void PickUp()
    {
        if (inventory != null)
        {
            // Añadir el objeto al inventario
            inventory.AddItem(itemID);
            
            // Destruir el objeto del mundo
            Destroy(gameObject);
        }
    }
    
    // Ejemplo de detección de interacción con el objeto
    void OnTriggerEnter(Collider other)
    {
        // Verificar si es el jugador
        if (other.CompareTag("Player"))
        {
            PickUp();
        }
    }
}