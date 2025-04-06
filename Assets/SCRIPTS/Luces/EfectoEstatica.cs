using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class URPStaticAndDistortionController : MonoBehaviour
{
    [Header("Referencias")]
    public Transform enemy;  // Referencia al enemigo
    public Transform player; // Referencia al jugador

    [Header("Parámetros de Efecto")]
    public float maxEffectDistance = 10f;    // Distancia a partir de la cual los efectos desaparecen
    public float minEffectDistance = 2f;     // Distancia a la que los efectos están al máximo
    public float maxFilmGrainIntensity = 1f; // Intensidad máxima para Film Grain (estática)
    public float maxLensDistortionIntensity = -0.5f; // Intensidad máxima para Lens Distortion (valores negativos producen efecto barrel)

    private Volume volume;
    private FilmGrain filmGrain;
    private LensDistortion lensDistortion;

    void Start()
    {
        volume = GetComponent<Volume>();
        if (volume != null)
        {
            if (!volume.profile.TryGet<FilmGrain>(out filmGrain))
            {
                Debug.LogWarning("No se encontró el override Film Grain en el Volume Profile. Agrégalo en el Volume.");
            }
            if (!volume.profile.TryGet<LensDistortion>(out lensDistortion))
            {
                Debug.LogWarning("No se encontró el override Lens Distortion en el Volume Profile. Agrégalo en el Volume.");
            }
        }
        else
        {
            Debug.LogWarning("No se encontró un componente Volume en este GameObject.");
        }
    }

    void Update()
    {
        if (filmGrain == null && lensDistortion == null)
            return;

        // Calcula la distancia entre el jugador y el enemigo
        float distance = Vector3.Distance(player.position, enemy.position);

        // Calcula un factor de intensidad entre 0 y 1 basado en la distancia
        float intensityFactor = 0f;
        if (distance <= minEffectDistance)
        {
            intensityFactor = 1f;
        }
        else if (distance < maxEffectDistance)
        {
            intensityFactor = 1f - Mathf.Clamp01((distance - minEffectDistance) / (maxEffectDistance - minEffectDistance));
        }
        else
        {
            intensityFactor = 0f;
        }

        // Aplica la intensidad al Film Grain (estática)
        if (filmGrain != null)
        {
            filmGrain.intensity.value = intensityFactor * maxFilmGrainIntensity;
        }
        // Aplica la intensidad al Lens Distortion (distorsión)
        if (lensDistortion != null)
        {
            lensDistortion.intensity.value = intensityFactor * maxLensDistortionIntensity;
        }
    }
}