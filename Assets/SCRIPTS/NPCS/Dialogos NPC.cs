using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogosNPC : MonoBehaviour
{
    [Header("NPC Configuración")]
    public string npcName = "NPC";
    
    [Header("Objetos Requeridos")]
    public InteractableObjectWithDependency[] linkedObjects; // Objetos que el NPC verificará
    
    [Header("Diálogos")]
    [TextArea(3, 5)]
    public string dialogueNoInteractions = "Parece que no has explorado mucho por aquí...";
    
    [TextArea(3, 5)]
    public string dialogueOneInteraction = "Veo que has encontrado algo interesante. Sigue explorando.";
    
    [TextArea(3, 5)]
    public string dialogueTwoInteractions = "Bastante bien, has descubierto algunas cosas. Pero hay más.";
    
    [TextArea(3, 5)]
    public string dialogueAllInteractions = "¡Impresionante! Has descubierto todos los secretos que buscaba.";
    
    [Header("UI References")]
    public GameObject dialoguePanel;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI continueText;
    public int wordsPerPage = 30;
    
    private bool playerInRange = false;
    private bool isShowingDialogue = false;
    private string[] dialoguePages;
    private int currentPage = 0;
    private float originalTimeScale;
    private float timeOpened = -1f;
    private float inputDelay = 0.2f;
    
    void Start()
    {
        if (dialoguePanel != null)
        {
            dialoguePanel.SetActive(false);
        }
    }
    
    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E) && !isShowingDialogue)
        {
            ShowNPCDialogue();
        }
        
        if (isShowingDialogue && Time.unscaledTime - timeOpened > inputDelay && Input.GetKeyDown(KeyCode.Space))
        {
            NextPage();
        }
    }
    
    void ShowNPCDialogue()
    {
        // Determinar qué diálogo mostrar basado en las interacciones
        string selectedDialogue = GetAppropriateDialogue();
        
        // Dividir el diálogo en páginas
        SplitDialogueIntoPages(selectedDialogue);
        
        // Pausar el juego
        originalTimeScale = Time.timeScale;
        Time.timeScale = 0f;
        
        // Mostrar panel de diálogo
        dialoguePanel.SetActive(true);
        currentPage = 0;
        isShowingDialogue = true;
        
        // Actualizar texto
        UpdateDialogueText();
        
    }
    
    string GetAppropriateDialogue()
    {
        // Contar cuántos objetos han sido interactuados
        int interactedCount = 0;
        
        if (linkedObjects != null && linkedObjects.Length > 0)
        {
            foreach (var obj in linkedObjects)
            {
                if (obj != null && obj.hasBeenInteracted)
                {
                    interactedCount++;
                }
            }
        }
        
        // Seleccionar el diálogo apropiado basado en el conteo
        if (linkedObjects == null || linkedObjects.Length == 0)
        {
            // Si no hay objetos vinculados, mostrar diálogo por defecto
            return dialogueNoInteractions;
        }
        else if (interactedCount == 0)
        {
            return dialogueNoInteractions;
        }
        else if (interactedCount == 1 || (linkedObjects.Length == 2 && interactedCount == 1))
        {
            return dialogueOneInteraction;
        }
        else if (interactedCount == 2 || (linkedObjects.Length == 2 && interactedCount == 2))
        {
            return dialogueTwoInteractions;
        }
        else if (interactedCount >= 3 || interactedCount == linkedObjects.Length)
        {
            return dialogueAllInteractions;
        }
        
        // Por defecto
        return dialogueNoInteractions;
    }
    
    void SplitDialogueIntoPages(string fullDialogue)
    {
        string[] words = fullDialogue.Split(' ');
        List<string> pages = new List<string>();
        string currentPage = "";
        int wordCount = 0;
        
        foreach (string word in words)
        {
            currentPage += word + " ";
            wordCount++;
            
            if (wordCount >= wordsPerPage)
            {
                pages.Add(currentPage.Trim());
                currentPage = "";
                wordCount = 0;
            }
        }
        
        if (currentPage.Trim().Length > 0)
        {
            pages.Add(currentPage.Trim());
        }
        
        if (pages.Count == 0)
        {
            pages.Add("");
        }
        
        dialoguePages = pages.ToArray();
    }
    
    void UpdateDialogueText()
    {
        if (nameText != null)
        {
            nameText.text = npcName;
            timeOpened = Time.unscaledTime;
        }
        
        if (dialogueText != null)
        {
            dialogueText.text = dialoguePages[currentPage];
        }
        
        if (continueText != null)
        {
            continueText.text = currentPage < dialoguePages.Length - 1
                ? "Presiona ESPACIO para continuar..."
                : "Presiona ESPACIO para cerrar";
        }
    }
    
    void NextPage()
    {
        currentPage++;
        
        if (currentPage >= dialoguePages.Length)
        {
            CloseDialogue();
            return;
        }
        
        UpdateDialogueText();
    }
    
    void CloseDialogue()
    {
        dialoguePanel.SetActive(false);
        Time.timeScale = originalTimeScale;
        isShowingDialogue = false;
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            if (NotificationManager.instance != null)
            {
                NotificationManager.instance.ShowNotification("Presiona E para hablar con " + npcName);
            }
        }
    }
    
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
    
    public void ForceCloseDialogue()
    {
        if (isShowingDialogue)
        {
            CloseDialogue();
        }
    }
    
    // Para depuración: verificar cuántos objetos han sido interactuados
    public int GetInteractedObjectsCount()
    {
        int count = 0;
        
        if (linkedObjects != null)
        {
            foreach (var obj in linkedObjects)
            {
                if (obj != null && obj.hasBeenInteracted)
                {
                    count++;
                }
            }
        }
        
        return count;
    }
}