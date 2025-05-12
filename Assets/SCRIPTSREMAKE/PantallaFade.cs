using System;
using System.Collections;
using UnityEngine;

public class PantallaFade : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public float duracion = 1f;

    private void Start()
    {
        if (canvasGroup != null)
            canvasGroup.alpha = 0f; // Inicia transparente
    }

    public void FadeIn(Action alFinal = null)
    {
        Debug.Log(">>> Ejecutando FADE IN");
        StartCoroutine(FadeCoroutine(0f, 1f, alFinal));
    }

    public void FadeOut(Action alFinal = null)
    {
        Debug.Log(">>> Ejecutando FADE OUT");
        StartCoroutine(FadeCoroutine(1f, 0f, alFinal));
    }

    private IEnumerator FadeCoroutine(float desde, float hasta, Action alFinal)
    {
        if (canvasGroup == null)
        {
            Debug.LogError("CanvasGroup no asignado en PantallaFade");
            yield break;
        }

        float tiempo = 0f;
        canvasGroup.blocksRaycasts = true; // Bloquea interacciones

        while (tiempo < duracion)
        {
            tiempo += Time.deltaTime;
            float t = Mathf.Clamp01(tiempo / duracion);
            canvasGroup.alpha = Mathf.Lerp(desde, hasta, t);
            yield return null;
        }

        canvasGroup.alpha = hasta;
        canvasGroup.blocksRaycasts = hasta > 0f;
        alFinal?.Invoke();
    }
}


