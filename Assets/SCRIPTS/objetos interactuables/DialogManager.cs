using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class DialogManager : MonoBehaviour
{
    public static DialogManager instance;
    
    // El panel que contiene todos los elementos del diálogo
    public GameObject dialogPanel;
    
    // El texto del diálogo
    public Text dialogText;
    
    // Opcional: imagen del personaje que habla
    public Image speakerImage;
    
    // Opcional: nombre del personaje que habla
    public Text speakerNameText;
    
    // Indicador visual de que hay más texto (por ejemplo, un ícono parpadeante)
    public GameObject continueIndicator;
    
    // Velocidad de escritura del texto (caracteres por segundo)
    public float typingSpeed = 30f;
    
    // Sonido de tipeo (opcional)
    public AudioClip typingSound;
    public float typingSoundInterval = 0.1f;
    
    private Queue<string> sentences = new Queue<string>();
    private bool isTyping = false;
    private bool dialogActive = false;
    
    private void Awake()
    {
        // Patrón Singleton
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        
        // Asegurarse de que el panel esté desactivado al inicio
        if (dialogPanel != null)
        {
            dialogPanel.SetActive(false);
        }
    }
    
    // Iniciar una conversación con múltiples frases
    public void StartDialog(string[] sentences, Sprite speakerSprite = null, string speakerName = "")
    {
        dialogActive = true;
        dialogPanel.SetActive(true);
        
        // Limpiar cualquier diálogo anterior
        this.sentences.Clear();
        
        // Configurar la imagen y nombre del hablante si se proporcionan
        if (speakerImage != null && speakerSprite != null)
        {
            speakerImage.sprite = speakerSprite;
            speakerImage.gameObject.SetActive(true);
        }
        else if (speakerImage != null)
        {
            speakerImage.gameObject.SetActive(false);
        }
        
        if (speakerNameText != null)
        {
            speakerNameText.text = speakerName;
            speakerNameText.gameObject.SetActive(!string.IsNullOrEmpty(speakerName));
        }
        
        // Encolar todas las frases
        foreach (string sentence in sentences)
        {
            this.sentences.Enqueue(sentence);
        }
        
        // Mostrar la primera frase
        DisplayNextSentence();
    }
    
    // Sobrecarga para una sola frase (más simple)
    public void StartDialog(string sentence, Sprite speakerSprite = null, string speakerName = "")
    {
        StartDialog(new string[] { sentence }, speakerSprite, speakerName);
    }
    
    // Mostrar la siguiente frase de la conversación
    public void DisplayNextSentence()
    {
        // Si estamos escribiendo, mostrar toda la frase de inmediato
        if (isTyping)
        {
            StopAllCoroutines();
            dialogText.text = sentences.Peek();
            if (continueIndicator != null) continueIndicator.SetActive(true);
            isTyping = false;
            return;
        }
        
        // Si no hay más frases, terminar el diálogo
        if (sentences.Count == 0)
        {
            EndDialog();
            return;
        }
        
        // Obtener la siguiente frase y mostrarla gradualmente
        string sentence = sentences.Dequeue();
        if (continueIndicator != null) continueIndicator.SetActive(false);
        StartCoroutine(TypeSentence(sentence));
    }
    
    // Mostrar el texto gradualmente, letra por letra
    IEnumerator TypeSentence(string sentence)
    {
        isTyping = true;
        dialogText.text = "";
        
        float timeSinceLastSound = 0f;
        
        foreach (char letter in sentence.ToCharArray())
        {
            dialogText.text += letter;
            
            // Reproducir sonido de tipeo a intervalos
            if (typingSound != null)
            {
                timeSinceLastSound += Time.deltaTime;
                if (timeSinceLastSound >= typingSoundInterval)
                {
                    AudioSource.PlayClipAtPoint(typingSound, Camera.main.transform.position, 0.5f);
                    timeSinceLastSound = 0f;
                }
            }
            
            yield return new WaitForSeconds(1f / typingSpeed);
        }
        
        isTyping = false;
        if (continueIndicator != null) continueIndicator.SetActive(true);
    }
    
    // Terminar el diálogo y ocultar el panel
    void EndDialog()
    {
        dialogActive = false;
        dialogPanel.SetActive(false);
    }
    
    // Para uso externo - verificar si hay un diálogo activo
    public bool IsDialogActive()
    {
        return dialogActive;
    }
    
    // Para uso externo - avanzar el diálogo o cerrarlo
    public void AdvanceDialog()
    {
        if (dialogActive)
        {
            DisplayNextSentence();
        }
    }
    
    // Se puede llamar desde Update de otro script para permitir al jugador avanzar con una tecla
    void Update()
    {
        if (dialogActive && Input.GetKeyDown(KeyCode.Space))
        {
            AdvanceDialog();
        }
    }
}