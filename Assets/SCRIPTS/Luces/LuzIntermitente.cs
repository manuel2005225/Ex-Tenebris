using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FlickerLight : MonoBehaviour
{
    [Header("Rango de intensidad")]
    public float minIntensity = 0.5f;
    public float maxIntensity = 2f;

    [Header("Velocidad de parpadeo")]
    public float flickerSpeed = 1.0f;

    private Light2D targetLight;

    void Awake()
    {
        // Obtiene la componente Light2D del mismo GameObject
        targetLight = GetComponent<Light2D>();
    }

    void Update()
    {
        // Generamos un valor pseudoaleatorio entre 0 y 1 usando Perlin Noise
        float noise = Mathf.PerlinNoise(Time.time * flickerSpeed, 0f);

        // Interpolamos la intensidad de la luz entre los valores mínimo y máximo
        float newIntensity = Mathf.Lerp(minIntensity, maxIntensity, noise);

        // Asignamos la nueva intensidad
        targetLight.intensity = newIntensity;
    }
}
