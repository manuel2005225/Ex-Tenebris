using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class IntroNarrativa : MonoBehaviour
{
    [Header("Textos aleatorios")]
    [TextArea(2, 5)]
    public string[] frases;
    public float typingSpeed = 0.05f;

    [Header("Referencias UI")]
    public TextMeshProUGUI introText;
    public Image fadeImage;
    public float fadeSpeed = 1.5f;

    private string fraseElegida = "";
    private string colorTagStart = "";
    private string colorTagEnd = "</color>";
    private bool isTyping = false;
    private bool readyToFade = false;

    void Start()
    {
        fadeImage.color = new Color(0, 0, 0, 1);
        introText.text = "";

        string raw = frases[Random.Range(0, frases.Length)];
        PrepararFrase(raw);
        StartCoroutine(TypeTextWithColor(fraseElegida));
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isTyping)
            {
                StopAllCoroutines();
                introText.text = $"{colorTagStart}{fraseElegida}{colorTagEnd}";
                isTyping = false;
            }
            else if (!readyToFade)
            {
                StartCoroutine(FadeToScene());
                readyToFade = true;
            }
        }
    }

    IEnumerator TypeTextWithColor(string text)
    {
        isTyping = true;
        string visibleText = "";

        for (int i = 0; i < text.Length; i++)
        {
            visibleText += text[i];
            introText.text = $"{colorTagStart}{visibleText}{colorTagEnd}";
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
    }

    IEnumerator FadeToScene()
    {
        introText.text = "";

        float alpha = 1;
        Color fadeColor = fadeImage.color;

        while (alpha > 0)
        {
            alpha -= Time.deltaTime * fadeSpeed;
            fadeColor.a = Mathf.Clamp01(alpha);
            fadeImage.color = fadeColor;
            yield return null;
        }

        gameObject.SetActive(false);
    }

    void PrepararFrase(string raw)
    {
        string content = raw.TrimStart('*', '-', '_', '\'').Trim();

        Color color;
        if (raw.StartsWith("*"))
            color = new Color32(194, 164, 0, 255); // Amarillo oscuro
        else if (raw.StartsWith("-"))
            color = new Color32(78, 177, 255, 255); // Azul
        else if (raw.StartsWith("_"))
            color = new Color32(165, 0, 0, 255); // Rojo sangre
        else
            color = new Color32(136, 136, 136, 255); // Gris onírico

        string hex = ColorUtility.ToHtmlStringRGB(color);

        fraseElegida = content;
        colorTagStart = $"<color=#{hex}>";
    }
}


