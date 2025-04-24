using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NPCDialogueHandler : MonoBehaviour
{

    [Header("Estado del Diálogo")]
    public int DialogoCompletado = 0;

    private int currentDialogueIndex = 0;
    private int lastUsedLevel = -1;
    private DialogueLevel currentLevel = null;

    [Header("NPC Configuración")]
    public string npcName = "NPC";

    [Header("Objetos Requeridos")]
    public InteractableObjectWithDependency[] linkedObjects;

    [System.Serializable]
    public class DialogueLevel
    {
        public int requiredInteractions;
        [TextArea(3, 5)]
        public List<string> dialogueOptions;
    }

    [Header("Diálogos por Nivel de Interacción")]
    public List<DialogueLevel> dialogueLevels;

    [Header("UI")]
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

    if (isShowingDialogue && Time.unscaledTime - timeOpened > inputDelay && Input.GetKeyDown(KeyCode.E))
    {
        NextPage();
    }
}

    void ShowNPCDialogue()
{
    string selectedDialogue = GetAppropriateDialogue();

    if (string.IsNullOrEmpty(selectedDialogue))
        return; // No mostrar nada si no hay más diálogos por ahora

    selectedDialogue = ApplyDialogueColors(selectedDialogue);
    SplitDialogueIntoPages(selectedDialogue);

    originalTimeScale = Time.timeScale;
    Time.timeScale = 0f;

    dialoguePanel.SetActive(true);
    currentPage = 0;
    isShowingDialogue = true;

    UpdateDialogueText();
}

    string GetAppropriateDialogue()
{
    int interactedCount = GetInteractedObjectsCount();
    DialogueLevel newLevel = null;

    foreach (var level in dialogueLevels)
    {
        if (interactedCount >= level.requiredInteractions)
        {
            newLevel = level;
        }
    }

    // Si ya mostramos todos los diálogos del nivel actual, no hacer nada más
    if (newLevel == null || newLevel.dialogueOptions.Count == 0)
        return "No hay diálogos disponibles.";

    // Si estamos en un nuevo nivel, reiniciar el índice
    if (newLevel.requiredInteractions > lastUsedLevel)
    {
        currentLevel = newLevel;
        lastUsedLevel = newLevel.requiredInteractions;
        currentDialogueIndex = 0;
    }

    // Si ya mostramos todo el diálogo de este nivel, no permitir interactuar
    if (currentDialogueIndex >= currentLevel.dialogueOptions.Count)
    {
        return null; // Esto evitará mostrar el diálogo
    }

    return currentLevel.dialogueOptions[currentDialogueIndex];
}

    string ApplyDialogueColors(string rawDialogue)
{
    string[] lines = rawDialogue.Split('\n');

    for (int i = 0; i < lines.Length; i++)
    {
        string line = lines[i].Trim();

        if (line.StartsWith("*"))
        {
            line = line.Substring(1).Trim(); // quitar el * inicial
            lines[i] = $"<color=#C2A400>{line}</color>"; // Amarillo oscuro
        }
        else
        {
            lines[i] = $"<color=#000000>{line}</color>"; // Negro (NPC)
        }
    }

    return string.Join("\n", lines);
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
    if (dialoguePages == null || dialoguePages.Length == 0)
        return;

    timeOpened = Time.unscaledTime;

    string currentLine = dialoguePages[currentPage];

    if (nameText != null)
    {
        // Detectar si es diálogo del jugador por el color amarillo oscuro
        if (currentLine.StartsWith("<color=#C2A400>"))
        {
            nameText.text = "Gabriel Fortea";
        }
        else
        {
            nameText.text = npcName;
        }
    }

    if (dialogueText != null)
    {
        dialogueText.text = currentLine;
    }

    if (continueText != null)
    {
        continueText.text = currentPage < dialoguePages.Length - 1
            ? "Presiona E para continuar..."
            : "Presiona E para cerrar";
    }
}

    void NextPage()
{
    currentPage++;

    if (currentPage >= dialoguePages.Length)
    {
        // Avanzar al siguiente diálogo del nivel actual
        currentDialogueIndex++;

        // Si se terminó el nivel actual
        if (currentLevel == null || currentDialogueIndex >= currentLevel.dialogueOptions.Count)
        {
            // Si es el nivel 3 (interacciones requeridas == 3)
            if (currentLevel != null && currentLevel.requiredInteractions == 3)
            {
                DialogoCompletado = 1;
            }

            CloseDialogue();
            return;
        }

        // Mostrar siguiente diálogo del nivel
        string nextDialogue = ApplyDialogueColors(currentLevel.dialogueOptions[currentDialogueIndex]);
        SplitDialogueIntoPages(nextDialogue);
        currentPage = 0;
    }

    UpdateDialogueText();
}

    void CloseDialogue()
{
    dialoguePanel.SetActive(false);
    Time.timeScale = originalTimeScale;
    isShowingDialogue = false;

    currentDialogueIndex++; // Avanzar al siguiente diálogo solo al cerrar
}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            NotificationManager.instance?.ShowNotification("Presiona E para hablar con " + npcName);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            NotificationManager.instance?.HideNotification();
        }
    }

    public void ForceCloseDialogue()
    {
        if (isShowingDialogue)
        {
            CloseDialogue();
        }
    }

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
