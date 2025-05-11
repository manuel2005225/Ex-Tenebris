using System;
using UnityEngine;

public class PantallaFade : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public float duracion = 1f;

    private void Start()
    {
        // Asegurarse de que empieza invisible
        if (canvasGroup != null)
            canvasGroup.alpha = 0f;
    }

    /// <summary>
    /// Hace un fade a negro (pantalla opaca) y luego ejecuta una acción (como teletransportar).
    /// </summary>
    public void FadeIn(Action alFinal)
    {
        StartCoroutine(Fade(0f, 1f, alFinal));
    }

    /// <summary>
    /// Hace un fade de negro a transparente.
    /// </summary>
    public void FadeOut()
    {
        StartCoroutine(Fade(1f, 0f, null));
    }

    private System.Collections.IEnumerator Fade(float desde, float hasta, Action alFinal)
    {
        float tiempo = 0f;

        while (tiempo < duracion)
        {
            tiempo += Time.deltaTime;
            float alphaActual = Mathf.Lerp(desde, hasta, tiempo / duracion);
            canvasGroup.alpha = alphaActual;
            yield return null;
        }

        canvasGroup.alpha = hasta;
        alFinal?.Invoke();
    }
}

