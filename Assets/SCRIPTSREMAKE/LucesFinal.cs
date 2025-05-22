using System.Collections;
using UnityEngine;

public class EncenderLucesFinal : MonoBehaviour
{
    private Light[] luces;

    private void OnEnable()
    {
        luces = GetComponentsInChildren<Light>();

        foreach (var luz in luces)
        {
            luz.intensity = 0f; // Empezamos desde cero
        }

        StartCoroutine(AumentarIntensidad());
    }

    IEnumerator AumentarIntensidad()
    {
        float duracion = 2f;
        float tiempo = 0f;

        while (tiempo < duracion)
        {
            float intensidad = Mathf.Lerp(0f, 1f, tiempo / duracion);

            foreach (var luz in luces)
            {
                luz.intensity = intensidad;
            }

            tiempo += Time.deltaTime;
            yield return null;
        }

        foreach (var luz in luces)
        {
            luz.intensity = 1f;
        }
    }
}

