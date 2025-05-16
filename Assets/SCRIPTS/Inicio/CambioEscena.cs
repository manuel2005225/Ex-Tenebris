using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class CambioEscena : MonoBehaviour
{
    public Image fadeImage; // Asigna el objeto FadeImage desde el Inspector
    public float fadeDuration = 1f;

    public void IrAJuego()
    {
        StartCoroutine(FadeOutAndLoadScene("EscenaNuevoEntorno"));
    }

    IEnumerator FadeOutAndLoadScene(string sceneName)
    {
        // Asegúrate de que la imagen esté encima y activa
        fadeImage.gameObject.SetActive(true);

        Color color = fadeImage.color;
        float t = 0f;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float alpha = Mathf.Clamp01(t / fadeDuration);
            fadeImage.color = new Color(color.r, color.g, color.b, alpha);
            yield return null;
        }

        // Cargar escena después del fade
        SceneManager.LoadScene(sceneName);
    }
}



