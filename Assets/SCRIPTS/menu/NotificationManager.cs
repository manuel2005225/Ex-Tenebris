using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class NotificationManager : MonoBehaviour
{
    public static NotificationManager instance;
    
    // Panel de notificación
    public GameObject notificationPanel;
    
    // Texto de la notificación
    public Text notificationText;
    
    // Duración de la notificación en pantalla
    public float displayTime = 2.5f;
    
    
    private void Awake()
    {
        // Singleton pattern
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        
        // Asegurarse de que el panel esté desactivado al inicio
        if (notificationPanel != null)
        {
            notificationPanel.SetActive(false);
        }
    }
    
    // Método para mostrar una notificación
    public void ShowNotification(string message)
    {
        if (notificationPanel != null && notificationText != null)
        {
            // Detener cualquier corrutina previa
            StopAllCoroutines();
            
            // Establecer el texto
            notificationText.text = message;
            
            // Mostrar el panel
            notificationPanel.SetActive(true);
            
            
            
            // Iniciar corrutina para ocultar la notificación
            StartCoroutine(HideNotificationAfterDelay());
        }
    }
    
    private IEnumerator HideNotificationAfterDelay()
    {
        yield return new WaitForSeconds(displayTime);
        
        
        
        // Ocultar el panel
        notificationPanel.SetActive(false);
    }
}