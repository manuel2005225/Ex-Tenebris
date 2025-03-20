using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem : MonoBehaviour
{
    // ID del objeto
    public int itemID;
    
    // Nombre del objeto
    public string itemName;
    
    // Descripción del objeto
    public string itemDescription;
    
    // Método para usar el objeto
    public void UseItem()
    {
        Debug.Log("Usando el objeto: " + itemName + " (ID: " + itemID + ")");
        // Aquí puedes añadir efectos específicos según el objeto
    }
}