using UnityEngine;

public class InventoryItem : MonoBehaviour
{
    public int itemID;
    public string itemName;
    public string itemDescription;
    public Sprite itemSprite;  

    public void UseItem()
    {
        Debug.Log("Usando el objeto: " + itemName + " (ID: " + itemID + ")");
        
    }
}

