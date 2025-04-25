using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractableObjectWithDependency : MonoBehaviour
{
    [Header("Interacción")]
    public string objectName = "Objeto";
    public List<string> descriptionPages = new List<string>(); // NUEVO: páginas editables en el inspector
    public int wordsPerPage = 20; // Ya no se usa

    [Header("UI References")]
    public GameObject descriptionPanel;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI continueText;

    [Header("Dependencia")]
    public InteractableObjectWithDependency requiredInteraction;
    public string lockedMessage = "Este objeto requiere investigar otro objeto primero.";
    public bool hasBeenInteracted = false;

    private bool playerInRange = false;
    private bool isShowing = false;
    private int currentPage = 0;
    private float originalTimeScale;
    private float timeOpened = -1f;
    private float inputDelay = 0.2f;

    void Start()
    {
        if (descriptionPanel != null)
        {
            descriptionPanel.SetActive(false);
        }
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E) && !isShowing)
        {
            AttemptInteraction();
        }

        if (isShowing && Time.unscaledTime - timeOpened > inputDelay && Input.GetKeyDown(KeyCode.E))
        {
            NextPage();
        }
    }

    void AttemptInteraction()
    {
        if (CanInteract())
        {
            ShowDescription();
            hasBeenInteracted = true;
        }
        else
        {
            NotificationManager.instance?.ShowNotification(lockedMessage);
        }
    }

    bool CanInteract()
    {
        if (requiredInteraction == null) return true;
        return requiredInteraction.hasBeenInteracted;
    }

    void ShowDescription()
    {
        if (descriptionPages == null || descriptionPages.Count == 0)
        {
            Debug.LogWarning("No hay páginas definidas para " + objectName);
            return;
        }

        originalTimeScale = Time.timeScale;
        Time.timeScale = 0f;

        descriptionPanel.SetActive(true);
        currentPage = 0;
        isShowing = true;

        UpdateDescriptionText();
    }

    void UpdateDescriptionText()
    {
        if (titleText != null)
        {
            titleText.text = objectName;
            timeOpened = Time.unscaledTime;
        }

        if (descriptionText != null)
        {
            descriptionText.text = descriptionPages[currentPage];
        }

        if (continueText != null)
        {
            continueText.text = currentPage < descriptionPages.Count - 1
                ? "Presiona E para continuar..."
                : "Presiona E para cerrar";
        }
    }

    void NextPage()
    {
        currentPage++;

        if (currentPage >= descriptionPages.Count)
        {
            CloseDescription();
            return;
        }

        UpdateDescriptionText();
    }

    void CloseDescription()
    {
        descriptionPanel.SetActive(false);
        Time.timeScale = originalTimeScale;
        isShowing = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            NotificationManager.instance?.ShowNotification("Presiona E para interactuar con " + objectName);
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

    public void ForceCloseDescription()
    {
        if (isShowing)
        {
            CloseDescription();
        }
    }

    public bool HasBeenInteracted()
    {
        return hasBeenInteracted;
    }

    public void ResetInteraction()
    {
        hasBeenInteracted = false;
    }
}
