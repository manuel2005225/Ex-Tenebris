using UnityEngine;

public class InteractiveObjectWithDialog : MonoBehaviour
{
    private bool playerInRange = false;
    
    // Textos para mostrar en el diálogo
    [TextArea(3, 10)]
    public string[] dialogTexts;
    
    // Opcional: nombre del objeto o personaje que "habla"
    public string objectName = "";
    
    // Opcional: sprite para mostrar junto al diálogo
    public Sprite objectSprite;
    
    // Opcional: sonido al interactuar
    public AudioClip interactionSound;
    
    // Indicador visual que aparece cuando el jugador está cerca (opcional)
    public GameObject interactionIndicator;
    
    void Start()
    {
        // Ocultar el indicador de interacción al inicio
        if (interactionIndicator != null)
        {
            interactionIndicator.SetActive(false);
        }
    }
    
    void Update()
    {
        // Si el jugador está en rango y presiona E, mostrar el diálogo
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            // Verificar si ya hay un diálogo activo
            if (DialogManager.instance != null && !DialogManager.instance.IsDialogActive())
            {
                Interact();
            }
        }
    }
    
    void Interact()
    {
        // Reproducir sonido si existe
        if (interactionSound != null)
        {
            AudioSource.PlayClipAtPoint(interactionSound, transform.position);
        }
        
        // Mostrar el diálogo
        if (DialogManager.instance != null)
        {
            DialogManager.instance.StartDialog(dialogTexts, objectSprite, objectName);
        }
        else
        {
            Debug.LogError("No se encontró el DialogManager en la escena.");
        }
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