using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySystem : MonoBehaviour
{
    // Panel principal del inventario
    public GameObject inventoryPanel;
    
    // Arreglo de slots en el inventario
    public GameObject[] slots;
    
    // Índice del slot seleccionado actualmente
    private int currentSlotIndex = 0;

    // Indicador visual del slot seleccionado
    public GameObject slotSelector;

    // Diccionario para almacenar los objetos en el inventario por su ID
    private Dictionary<int, GameObject> inventoryItems = new Dictionary<int, GameObject>();

    // Prefabs de los objetos que pueden estar en el inventario
    public GameObject[] itemPrefabs;

    void Start()
    {
        // Ocultar el inventario al inicio
        inventoryPanel.SetActive(false);
        
        // Configurar el selector en el primer slot
        UpdateSelectorPosition();
    }

    void Update()
    {
        // Mostrar/ocultar inventario con la tecla I
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventory();
        }

        // Solo procesar entradas si el inventario está activo
        if (inventoryPanel.activeSelf)
        {
            // Navegación entre slots con WASD
            if (Input.GetKeyDown(KeyCode.A))
            {
                MoveSelector(-1, 0);
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                MoveSelector(1, 0);
            }
            else if (Input.GetKeyDown(KeyCode.W))
            {
                MoveSelector(0, -1);
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                MoveSelector(0, 1);
            }

            // Usar objeto con la tecla E
            if (Input.GetKeyDown(KeyCode.E))
            {
                UseCurrentItem();
            }
        }
    }

    // Mostrar u ocultar el inventario
    void ToggleInventory()
{
    // Comprueba que tenemos la referencia
    if (inventoryPanel != null)
    {
        // Guarda el estado actual en una variable local
        bool currentState = inventoryPanel.activeSelf;
        
        // Cambia al estado opuesto
        inventoryPanel.SetActive(!currentState);
        
        // Depuración
        Debug.Log("Inventario cambiado de " + currentState + " a " + inventoryPanel.activeSelf);
    }
    else
    {
        Debug.LogError("No hay referencia al panel de inventario");
    }
}

    // Mover el selector entre slots
    void MoveSelector(int horizontalMove, int verticalMove)
    {
        // Calcular cuántos slots hay por fila (ajustar según diseño)
        int slotsPerRow = 6;
        
        // Calcular la nueva posición
        int newIndex = currentSlotIndex;
        
        // Movimiento horizontal
        newIndex += horizontalMove;
        
        // Movimiento vertical
        newIndex += verticalMove * slotsPerRow;
        
        // Verificar que el nuevo índice sea válido
        if (newIndex >= 0 && newIndex < slots.Length)
        {
            currentSlotIndex = newIndex;
            UpdateSelectorPosition();
        }
    }

    // Actualizar la posición visual del selector
    void UpdateSelectorPosition()
    {
        // Posicionar el selector en el slot actual
        slotSelector.transform.position = slots[currentSlotIndex].transform.position;
    }

    // Añadir un objeto al inventario por su ID
    public void AddItem(int itemID)
    {
        // Buscar el primer slot vacío
        for (int i = 0; i < slots.Length; i++)
        {
            if (!inventoryItems.ContainsKey(i))
            {
                // Verificar que el ID del objeto sea válido
                if (itemID >= 0 && itemID < itemPrefabs.Length)
                {
                    // Instanciar el objeto en el slot
                    GameObject newItem = Instantiate(itemPrefabs[itemID], slots[i].transform);
                    
                    // Ajustar la posición y escala del objeto
                    newItem.transform.localPosition = Vector3.zero;
                    newItem.transform.localScale = Vector3.one;
                    
                    // Almacenar el objeto en el diccionario
                    inventoryItems[i] = newItem;
                    
                    Debug.Log("Objeto con ID " + itemID + " añadido al inventario en el slot " + i);
                    return;
                }
                else
                {
                    Debug.LogError("ID de objeto no válido: " + itemID);
                    return;
                }
            }
        }
        
        Debug.Log("Inventario lleno. No se puede añadir el objeto.");
    }

    // Usar el objeto seleccionado actualmente
    void UseCurrentItem()
    {
        // Verificar si hay un objeto en el slot actual
        if (inventoryItems.ContainsKey(currentSlotIndex))
        {
            // Obtener el objeto
            GameObject item = inventoryItems[currentSlotIndex];
            
            // Obtener el componente del objeto
            InventoryItem itemComponent = item.GetComponent<InventoryItem>();
            
            if (itemComponent != null)
            {
                // Usar el objeto
                itemComponent.UseItem();
                
                // Aquí puedes agregar lógica adicional, como eliminar el objeto después de usarlo
                // Ejemplo: RemoveItem(currentSlotIndex);
            }
            else
            {
                Debug.Log("Se usó un objeto en el slot " + currentSlotIndex + " (sin componente InventoryItem)");
            }
        }
        else
        {
            Debug.Log("No hay objeto en el slot seleccionado.");
        }
    }

    // Eliminar un objeto del inventario
    public void RemoveItem(int slotIndex)
    {
        if (inventoryItems.ContainsKey(slotIndex))
        {
            // Destruir el objeto
            Destroy(inventoryItems[slotIndex]);
            
            // Eliminar la referencia del diccionario
            inventoryItems.Remove(slotIndex);
            
            Debug.Log("Objeto eliminado del slot " + slotIndex);
        }
    }
}