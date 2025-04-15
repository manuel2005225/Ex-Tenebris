using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ImprovedInteractiveObject : MonoBehaviour
{
    private bool playerInRange = false;
    
    // Referencias al panel de diálogo y su texto
    public GameObject dialogPanel;
    public Text dialogText;
    
    // Referencia al componente InventoryItem para obtener la descripción
    private InventoryItem item;
    
    // Tiempo que permanecerá visible el diálogo
    public float displayTime = 4.0f;
    
    // Margen adicional para el panel (espacio extra alrededor del texto)
    public float panelMargin = 20f;
    
    // Opcional: referencias para ajuste automático del panel
    public RectTransform dialogTextRectTransform;
    public RectTransform dialogPanelRectTransform;
    
    // Opcional: sonido que se reproduce al interactuar
    public AudioClip interactionSound;
    
    // Indicador visual de interacción
    public GameObject interactionIndicator;
    
    void Start()
    {
        // Obtener la referencia al componente InventoryItem
        item = GetComponent<InventoryItem>();
        
        // Si no se asignaron en el inspector, intentar encontrarlos automáticamente
        if (dialogTextRectTransform == null && dialogText != null)
        {
            dialogTextRectTransform = dialogText.GetComponent<RectTransform>();
        }
        
        if (dialogPanelRectTransform == null && dialogPanel != null)
        {
            dialogPanelRectTransform = dialogPanel.GetComponent<RectTransform>();
        }
        
        // Ocultar el panel de diálogo al inicio
        if (dialogPanel != null)
        {
            dialogPanel.SetActive(false);
        }
        
        // Ocultar el indicador de interacción al inicio
        if (interactionIndicator != null)
        {
            interactionIndicator.SetActive(false);
        }
    }
    
    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            ShowDialog();
        }
    }
    
    void ShowDialog()
    {
        // Verificar que tenemos las referencias necesarias
        if (dialogPanel != null && dialogText != null)
        {
            // Detener cualquier corrutina previa para evitar conflictos
            StopAllCoroutines();
            
            // Obtener el texto de descripción del objeto
            string textToShow = "";
            if (item != null)
            {
                textToShow = item.itemDescription;
            }
            else
            {
                textToShow = "Este objeto no tiene descripción.";
                Debug.LogWarning("El objeto " + gameObject.name + " no tiene un componente InventoryItem.");
            }
            
            // Establecer el texto del diálogo
            dialogText.text = textToShow;
            
            // Asegurarnos de que el panel está activo para poder calcular tamaños correctamente
            dialogPanel.SetActive(true);
            
            // Forzar actualización del layout para que el texto se ajuste
            Canvas.ForceUpdateCanvases();
            
            // Ajustar el tamaño del panel basado en el tamaño del texto (si tenemos las referencias)
            if (dialogTextRectTransform != null && dialogPanelRectTransform != null)
            {
                // Esperar un frame para que el layout se actualice correctamente
                StartCoroutine(AdjustPanelSize());
            }
            
            // Reproducir sonido si existe
            if (interactionSound != null)
            {
                AudioSource.PlayClipAtPoint(interactionSound, transform.position);
            }
            
            // Iniciar la corrutina para ocultar el diálogo después de un tiempo
            StartCoroutine(HideDialogAfterDelay());
        }
        else
        {
            Debug.LogError("Referencias de UI faltantes en " + gameObject.name);
        }
    }
    
    private IEnumerator AdjustPanelSize()
    {
        // Esperar un frame para asegurar que el texto se ha renderizado correctamente
        yield return null;
        
        // Obtener el tamaño preferido del texto
        Vector2 textSize = dialogTextRectTransform.sizeDelta;
        
        // Calcular el nuevo tamaño del panel con margen
        Vector2 newPanelSize = new Vector2(
            textSize.x + panelMargin * 2,
            textSize.y + panelMargin * 2
        );
        
        // Aplicar el nuevo tamaño
        dialogPanelRectTransform.sizeDelta = newPanelSize;
    }
    
    private IEnumerator HideDialogAfterDelay()
    {
        yield return new WaitForSeconds(displayTime);
        dialogPanel.SetActive(false);
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            
            // Mostrar indicador de interacción
            if (interactionIndicator != null)
            {
                interactionIndicator.SetActive(true);
            }
            
            Debug.Log("Presiona E para interactuar con " + gameObject.name);
        }
    }
    
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            
            // Ocultar indicador de interacción
            if (interactionIndicator != null)
            {
                interactionIndicator.SetActive(false);
            }
        }
    }
}