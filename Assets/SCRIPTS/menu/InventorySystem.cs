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
    private bool isPaused = false;

    // Prefabs de los objetos que pueden estar en el inventario
    public GameObject[] itemPrefabs;

    // Referencia al objeto jugador
    public Transform player;

    public Text descriptionText;  // Referencia al texto que muestra la descripción

    public GameObject mapainve;  // Mini mapa que aparece solo con el inventario

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

        // Detener rotación del jugador si el juego está pausado
        if (!isPaused)
        {
            // Aquí va la lógica normal de rotación del jugador
            player.Rotate(Vector3.up * 50f * Time.deltaTime); 
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
        inventoryPanel.SetActive(!inventoryPanel.activeSelf);
        isPaused = inventoryPanel.activeSelf;

        if (isPaused)
        {
            Time.timeScale = 0f;
            Debug.Log("Juego pausado - Inventario abierto");

        if (descriptionText != null)
            descriptionText.text = "Selecciona un ítem para ver su descripción.";

        if (mapainve != null)
            mapainve.SetActive(true); // Activar el mapa
        }
        else
        {
            Time.timeScale = 1f;
            Debug.Log("Juego reanudado - Inventario cerrado");

            if (descriptionText != null)
                descriptionText.text = "";

            if (mapainve != null)
              mapainve.SetActive(false); // Ocultar el mapa
        }
    }

    void OnDisable()
    {
        Time.timeScale = 1f;
    }

    // Mover el selector entre slots
    void MoveSelector(int horizontalMove, int verticalMove)
    {
        int slotsPerRow = 6;
        int newIndex = currentSlotIndex;

        newIndex += horizontalMove;
        newIndex += verticalMove * slotsPerRow;

        if (newIndex >= 0 && newIndex < slots.Length)
        {
            currentSlotIndex = newIndex;
            UpdateSelectorPosition();
            UpdateItemDescription();
        }
    }

    // Actualizar la posición visual del selector
    void UpdateSelectorPosition()
    {
        slotSelector.transform.position = slots[currentSlotIndex].transform.position;
    }

    // Añadir un objeto al inventario por su ID
    public void AddItem(int itemID)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (!inventoryItems.ContainsKey(i))
            {
                if (itemID >= 0 && itemID < itemPrefabs.Length)
                {
                    GameObject newItem = Instantiate(itemPrefabs[itemID], slots[i].transform);
                    newItem.transform.localPosition = Vector3.zero;
                    newItem.transform.localScale = Vector3.one;

                    InventoryItem itemComponent = newItem.GetComponent<InventoryItem>();
                    if (itemComponent != null)
                    {
                        Transform itemImageTransform = slots[i].transform.Find("ItemImage");
                        if (itemImageTransform != null)
                        {
                            Image itemImage = itemImageTransform.GetComponent<Image>();
                            if (itemImage != null)
                            {
                                itemImage.sprite = itemComponent.itemSprite;
                                itemImage.enabled = true;
                            }
                        }
                    }

                    inventoryItems[i] = newItem;

                    Debug.Log("Objeto con ID " + itemID + " añadido al inventario en el slot " + (i + 1));
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
   // Mostrar la descripción del ítem seleccionado
    void UpdateItemDescription()
    {
        if (inventoryItems.ContainsKey(currentSlotIndex))
        {
            GameObject item = inventoryItems[currentSlotIndex];
            InventoryItem itemComponent = item.GetComponent<InventoryItem>();

            if (itemComponent != null)
            {
                descriptionText.text = itemComponent.itemDescription;
            }
        }
        else
        {
            descriptionText.text = "Selecciona un ítem para ver su descripción.";
        }
    }

    // Usar el objeto seleccionado actualmente
    void UseCurrentItem()
    {
        if (inventoryItems.ContainsKey(currentSlotIndex))
        {
            GameObject item = inventoryItems[currentSlotIndex];
            InventoryItem itemComponent = item.GetComponent<InventoryItem>();

            if (itemComponent != null)
            {
                itemComponent.UseItem();
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
            Destroy(inventoryItems[slotIndex]);
            inventoryItems.Remove(slotIndex);

            Debug.Log("Objeto eliminado del slot " + slotIndex);
        }
    }
}   