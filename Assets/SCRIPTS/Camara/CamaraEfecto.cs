using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class FilmGrainController : MonoBehaviour
{
    public Volume volume;
    private FilmGrain filmGrain;
    public float ValorFilmGrain;

    void Start()
    {
        if (volume.profile.TryGet(out filmGrain))
        {
            Debug.Log("Film Grain found!");
        }
    }

    void Update()
    {
        if (filmGrain != null)
        {
            // Cambiar intensidad (de 0 a 1)
            filmGrain.intensity.value = ValorFilmGrain;
            // Cambiar el tipo de grano (Opcional)
            filmGrain.type.value = FilmGrainLookup.Large01; // Cambia el tipo de grano
            // Ajustar respuesta
            
        }
    }
}
    
