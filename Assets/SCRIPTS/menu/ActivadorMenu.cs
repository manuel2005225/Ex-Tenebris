using UnityEngine;

public class ActivateMenu : MonoBehaviour
{

    public GameObject menu;
    void Start()
    {
        // Busca el objeto llamado "menu" en la escena
        
        if (menu != null)
        {
            menu.SetActive(true);
        }
        else
        {
            Debug.LogWarning("No se encontró el objeto 'menu' en la escena.");
        }
    }
}
