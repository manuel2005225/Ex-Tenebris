using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public List<Image> slots; // Lista de slots de inventario
    public List<Sprite> itemSprites; // Lista de sprites de objetos
    public int selectedSlot = 0; // Slot seleccionado
    public GameObject inventoryPanel; // Referencia al Panel del inventario

    private List<int> inventory = new List<int>(); // Lista de IDs de objetos en el inventario
    private bool isInventoryVisible = false; // Estado de visibilidad del inventario

    void Start()
    {
        UpdateInventoryUI(); // Actualiza la interfaz al inicio
        ToggleInventory(false); // Oculta el inventario al inicio
    }

    void Update()
    {
        HandleInput(); // Maneja la entrada del usuario
    }

    void HandleInput()
{
    // Mostrar u ocultar el inventario con la tecla I
    if (Input.GetKeyDown(KeyCode.I))
    {
        ToggleInventory(!isInventoryVisible);
    }

    // Si el inventario está visible, manejar el movimiento y uso de objetos
    if (isInventoryVisible)
    {
        // Moverse entre slots con W (arriba), S (abajo), A (izquierda), D (derecha)
        if (Input.GetKeyDown(KeyCode.W) && selectedSlot > 0)
        {
            selectedSlot--;
            UpdateInventoryUI();
        }
        if (Input.GetKeyDown(KeyCode.S) && selectedSlot < slots.Count - 1)
        {
            selectedSlot++;
            UpdateInventoryUI();
        }

        // Usar objeto con E
        if (Input.GetKeyDown(KeyCode.E))
        {
            UseItem(selectedSlot);
        }
    }
}

    void ToggleInventory(bool visible)
    {
        isInventoryVisible = visible; // Cambia el estado de visibilidad
        inventoryPanel.SetActive(isInventoryVisible); // Muestra u oculta el Panel
    }

    void UpdateInventoryUI()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (i < inventory.Count)
            {
                // Asigna el sprite del objeto al slot
                slots[i].sprite = itemSprites[inventory[i]];
                slots[i].color = Color.white; // Muestra el slot con color normal
            }
            else
            {
                // Si no hay objeto, oculta el sprite
                slots[i].sprite = null;
                slots[i].color = Color.clear; // Oculta el slot
            }

            // Resalta el slot seleccionado
            if (i == selectedSlot)
            {
                slots[i].color = Color.yellow; // Cambia el color del slot seleccionado
            }
        }
    }

    public void AddItem(int itemID)
    {
        if (inventory.Count < slots.Count)
        {
            inventory.Add(itemID); // Agrega el objeto al inventario
            UpdateInventoryUI(); // Actualiza la interfaz
        }
    }

    void UseItem(int slotIndex)
    {
        if (slotIndex < inventory.Count)
        {
            int itemID = inventory[slotIndex];
            Debug.Log("Usando objeto con ID: " + itemID); // Muestra un mensaje en la consola
            inventory.RemoveAt(slotIndex); // Elimina el objeto del inventario
            UpdateInventoryUI(); // Actualiza la interfaz
        }
    }
}