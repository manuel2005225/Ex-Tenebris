using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractableObjectWithDependency : MonoBehaviour
{
    [Header("Interacción")]
    public string objectName = "Objeto";
    [TextArea(3, 10)]
    public string objectDescription = "Descripción del objeto";
    public int wordsPerPage = 20;

    [Header("UI References")]
    public GameObject descriptionPanel;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI continueText;

    [Header("Dependencia")]
    public InteractableObjectWithDependency requiredInteraction; // Objeto que debe ser interactuado antes
    public string lockedMessage = "Este objeto requiere investigar otro objeto primero.";
    public bool hasBeenInteracted = false; // Si este objeto ya ha sido interactuado

    private bool playerInRange = false;
    private bool isShowing = false;
    private string[] descriptionPages;
    private int currentPage = 0;
    private float originalTimeScale;
    private float timeOpened = -1f;
    private float inputDelay = 0.2f; // Tiempo en segundos para evitar doble input

    void Start()
    {
        if (descriptionPanel != null)
        {
            descriptionPanel.SetActive(false);
        }

        SplitDescriptionIntoPages();
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E) && !isShowing)
        {
            AttemptInteraction();
        }

        // Solo aceptar input si pasó un tiempo desde que se abrió
        if (isShowing && Time.unscaledTime - timeOpened > inputDelay && Input.GetKeyDown(KeyCode.Space))
        {
            NextPage();
        }
    }
    
    

    void AttemptInteraction()
    {
        // Verificar si podemos interactuar con este objeto
        if (CanInteract())
        {
            ShowDescription();
            hasBeenInteracted = true;
        }
        else
        {
            // Mostrar mensaje de que está bloqueado
            if (NotificationManager.instance != null)
            {
                NotificationManager.instance.ShowNotification(lockedMessage);
            }
            else
            {
                Debug.Log(lockedMessage);
            }
        }
    }

    bool CanInteract()
    {
        // Si no hay dependencia, siempre se puede interactuar
        if (requiredInteraction == null)
        {
            return true;
        }

        // Si hay dependencia, verificar si ese objeto ya ha sido interactuado
        return requiredInteraction.hasBeenInteracted;
    }

    void SplitDescriptionIntoPages()
    {
        string[] words = objectDescription.Split(' ');
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

        descriptionPages = pages.ToArray();
    }

    void ShowDescription()
    {
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
            continueText.text = currentPage < descriptionPages.Length - 1
                ? "Presiona ESPACIO para continuar..."
                : "Presiona ESPACIO para cerrar";
        }
    }

    void NextPage()
    {
        currentPage++;

        if (currentPage >= descriptionPages.Length)
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
            if (NotificationManager.instance != null)
            {
                NotificationManager.instance.ShowNotification("Presiona E para interactuar con " + objectName);
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;

            // Ocultar la notificación al salir del trigger
            if (NotificationManager.instance != null)
            {
                NotificationManager.instance.HideNotification();
            }
        }
    }


    public void ForceCloseDescription()
    {
        if (isShowing)
        {
            CloseDescription();
        }
    }

    // Método público para verificar si este objeto ha sido interactuado
    public bool HasBeenInteracted()
    {
        return hasBeenInteracted;
    }

    // Método público para restablecer el estado de interacción (útil para reiniciar el juego)
    public void ResetInteraction()
    {
        hasBeenInteracted = false;
    }
}