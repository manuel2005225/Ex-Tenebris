using System.Collections;
using UnityEngine;
using TMPro;
using NUnit.Framework.Constraints;
using UnityEngine.UI;

[System.Serializable]
public class PlayerResponse
{
    public string responseText;
    public int nextLineIndex;
}

[System.Serializable]
public class DialogueLine
{
    [TextArea(4, 6)] public string npcText;
    public PlayerResponse[] responses;
}
public class Dialogo : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject dialogoMark;
    [SerializeField] private GameObject dialogoPanel;
    [SerializeField] private TMP_Text dialogoText;
    [SerializeField] private GameObject respuestaPanel;
    [SerializeField] private Button respuestaPrefab;

    [Header("Dialogo")]
    [SerializeField] private DialogueLine[] dialogueLines;
    private float typingTime = 0.05f;

    private bool isPlayerInRange;
    private bool didDialogueStart;
    private int lineIndex;

    private void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.C))
        {
            if (!didDialogueStart)
            {
                StartDialogue();
            }
            else if (dialogoText.text == dialogueLines[lineIndex].npcText)
            {
                ShowResponses();
            }
            else
            {
                StopAllCoroutines();
                dialogoText.text = dialogueLines[lineIndex].npcText;
            }
        }
    }

    private void StartDialogue()
    {
        didDialogueStart = true;
        dialogoPanel.SetActive(true);
        dialogoMark.SetActive(false);
        lineIndex = 0;
        Time.timeScale = 0f;
        StartCoroutine(ShowLine(dialogueLines[lineIndex].npcText));
    }

    private void ShowResponses()
    {
        respuestaPanel.SetActive(true);
        ClearPreviousResponses();

        foreach (var respuesta in dialogueLines[lineIndex].responses)
        {
            var newButton = Instantiate(respuestaPrefab, respuestaPanel.transform);
            newButton.GetComponentInChildren<TMP_Text>().text = respuesta.responseText;
            newButton.onClick.AddListener(() => OnResponseSelected(respuesta.nextLineIndex));
        }
    }

    private void OnResponseSelected(int nextIndex)
    {
        respuestaPanel.SetActive(false);
        lineIndex = nextIndex;

        if (lineIndex >= dialogueLines.Length)
        {
            EndDialogue();
            return;
        }

        StartCoroutine(ShowLine(dialogueLines[lineIndex].npcText));
    }

    private void EndDialogue()
    {
        didDialogueStart = false;
        dialogoPanel.SetActive(false);
        respuestaPanel.SetActive(false);
        dialogoMark.SetActive(true);
        Time.timeScale = 1f;
    }

    private IEnumerator ShowLine(string line)
    {
        dialogoText.text = string.Empty;

        foreach (char ch in line)
        {
            dialogoText.text += ch;
            yield return new WaitForSecondsRealtime(typingTime);
        }

        if (dialogueLines[lineIndex].responses.Length > 0)
        {
            ShowResponses();
        }
    }

    private void ClearPreviousResponses()
    {
        foreach (Transform child in respuestaPanel.transform)
        {
            Destroy(child.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = true;
            dialogoMark.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = false;
            dialogoMark.SetActive(false);
        }
    }
}
