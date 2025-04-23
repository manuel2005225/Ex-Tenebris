using System.Collections;
using UnityEngine;
using TMPro; // Importante: usar TextMeshPro

public class NotificationManager : MonoBehaviour
{
    public static NotificationManager instance;

    // Panel de notificación
    public GameObject notificationPanel;

    // Texto de la notificación (TextMeshPro)
    public TextMeshProUGUI notificationText;

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
            StopAllCoroutines();

            // Establecer el texto
            notificationText.text = message;

            // Mostrar el panel
            notificationPanel.SetActive(true);

            // Iniciar corrutina para ocultar la notificación
            StartCoroutine(HideNotificationAfterDelay());
        }
    }
    // Mostrar una notificación con duración personalizada
    public void ShowNotification(string message, float customDuration)
    {
        if (notificationPanel != null && notificationText != null)
        {
            StopAllCoroutines();
            notificationText.text = message;
            notificationPanel.SetActive(true);
            StartCoroutine(HideNotificationAfterDelay(customDuration));
        }
    }

    private IEnumerator HideNotificationAfterDelay(float duration)
    {
        yield return new WaitForSecondsRealtime(duration);
        notificationPanel.SetActive(false);
    }

    public void HideNotification()
    {
        if (notificationPanel != null)
        {
            notificationPanel.SetActive(false);
        }
    }


    private IEnumerator HideNotificationAfterDelay()
    {
        yield return new WaitForSeconds(displayTime);

        // Ocultar el panel
        notificationPanel.SetActive(false);
    }
}
