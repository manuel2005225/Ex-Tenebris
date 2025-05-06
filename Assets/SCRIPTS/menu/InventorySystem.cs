using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySystem : MonoBehaviour
{
    // Panel principal del inventario
    public GameObject inventoryPanel;

    // Arreglo de slots en el inventario (para objetos recogidos)
    public GameObject[] itemSlots;
    // Arreglo de slots para interactuar con NPCs
    public GameObject[] npcSlots;

    // Slots activos actualmente (apunta a itemSlots o npcSlots)
    private GameObject[] activeSlots;
    // Índice del slot seleccionado actualmente dentro de activeSlots
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
    public GameObject mapainve;   // Mini mapa que aparece solo con el inventario

    // Control de sección activa: true = objetos, false = NPCs
    private bool usingItemSlots = true;

    void Start()
    {
        // Ocultar el inventario al inicio
        inventoryPanel.SetActive(false);

        // Por defecto navegamos en los itemSlots
        activeSlots = itemSlots;
        UpdateSelectorPosition();
    }

    void Update()
    {
        // Alternar inventario objetos <-> NPCs con Tab
        if (inventoryPanel.activeSelf && Input.GetKeyDown(KeyCode.Tab))
        {
            usingItemSlots = !usingItemSlots;
            activeSlots = usingItemSlots ? itemSlots : npcSlots;
            currentSlotIndex = 0;
            UpdateSelectorPosition();
            UpdateItemDescription();
            Debug.Log("Modo inventario: " + (usingItemSlots ? "Objetos" : "Interacción NPC"));
        }

        // Mostrar/ocultar inventario con la tecla I
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventory();
        }

        // Detener rotación del jugador si el juego está pausado
        if (!isPaused)
        {
            player.Rotate(Vector3.up * 50f * Time.deltaTime); 
        }
                    
        // Solo procesar entradas si el inventario está activo
        if (inventoryPanel.activeSelf)
        {
            // Navegación entre slots con WASD
            if (Input.GetKeyDown(KeyCode.A))
                MoveSelector(-1, 0);
            else if (Input.GetKeyDown(KeyCode.D))
                MoveSelector(1, 0);
            else if (Input.GetKeyDown(KeyCode.W))
                MoveSelector(0, -1);
            else if (Input.GetKeyDown(KeyCode.S))
                MoveSelector(0, 1);

            // Usar objeto o interacción con NPC con la tecla E
            if (Input.GetKeyDown(KeyCode.E))
                UseCurrentItem();
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
            if (descriptionText != null)
                descriptionText.text = "Selecciona un ítem para ver su descripción.";
            if (mapainve != null)
                mapainve.SetActive(true);
        }
        else
        {
            Time.timeScale = 1f;
            if (descriptionText != null)
                descriptionText.text = "";
            if (mapainve != null)
                mapainve.SetActive(false);
        }
    }

    void OnDisable()
    {
        Time.timeScale = 1f;
    }

    // Mover el selector entre los activeSlots
    void MoveSelector(int horizontalMove, int verticalMove)
    {
        int slotsPerRow = 2; // ajusta según tu layout
        int newIndex = currentSlotIndex;

        newIndex += horizontalMove;
        newIndex += verticalMove * slotsPerRow;

        if (newIndex >= 0 && newIndex < activeSlots.Length)
        {
            currentSlotIndex = newIndex;
            UpdateSelectorPosition();
            UpdateItemDescription();
        }
    }

    // Actualizar la posición visual del selector
    void UpdateSelectorPosition()
    {
        slotSelector.transform.position = activeSlots[currentSlotIndex].transform.position;
    }

    // Añadir un objeto al inventario por su ID (solo en itemSlots)
    public void AddItem(int itemID)
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (!inventoryItems.ContainsKey(i))
            {
                if (itemID >= 0 && itemID < itemPrefabs.Length)
                {
                    GameObject newItem = Instantiate(itemPrefabs[itemID], itemSlots[i].transform);
                    newItem.transform.localPosition = Vector3.zero;
                    newItem.transform.localScale = Vector3.one;

                    InventoryItem itemComponent = newItem.GetComponent<InventoryItem>();
                    if (itemComponent != null)
                    {
                        Transform imgT = itemSlots[i].transform.Find("ItemImage");
                        if (imgT != null)
                        {
                            Image img = imgT.GetComponent<Image>();
                            img.sprite = itemComponent.itemSprite;
                            img.enabled = true;
                        }
                    }

                    inventoryItems[i] = newItem;
                    Debug.Log("Añadido ID " + itemID + " en slot " + (i+1));
                    return;
                }
                else
                {
                    Debug.LogError("ID de objeto no válido: " + itemID);
                    return;
                }
            }
        }
        Debug.Log("Inventario lleno.");
    }

    // Mostrar la descripción del ítem o interacción seleccionada
    void UpdateItemDescription()
    {
        if (usingItemSlots)
        {
            if (inventoryItems.ContainsKey(currentSlotIndex))
            {
                var item = inventoryItems[currentSlotIndex];
                var comp = item.GetComponent<InventoryItem>();
                if (comp != null)
                    descriptionText.text = comp.itemDescription;
            }
            else
            {
                descriptionText.text = "Selecciona un ítem para ver su descripción.";
            }
        }
        else
        {
            // Aquí podrías mostrar la descripción de la acción NPC
            descriptionText.text = "Selecciona acción para interactuar con el NPC.";
        }
    }

    // Usar el objeto seleccionado o acción NPC
    void UseCurrentItem()
    {
        if (usingItemSlots)
        {
            if (inventoryItems.ContainsKey(currentSlotIndex))
            {
                var item = inventoryItems[currentSlotIndex];
                var comp = item.GetComponent<InventoryItem>();
                if (comp != null) comp.UseItem();
            }
            else
            {
                Debug.Log("Slot vacío (objeto).");
            }
        }
        else
        {
            Debug.Log("Interactuando con NPC en slot " + currentSlotIndex);
            // Añade aquí la lógica de interacción correspondiente
        }
    }

    // Eliminar un objeto del inventario
    public void RemoveItem(int slotIndex)
    {
        if (inventoryItems.ContainsKey(slotIndex))
        {
            Destroy(inventoryItems[slotIndex]);
            inventoryItems.Remove(slotIndex);
            Debug.Log("Eliminado slot " + slotIndex);
        }
    }
}   