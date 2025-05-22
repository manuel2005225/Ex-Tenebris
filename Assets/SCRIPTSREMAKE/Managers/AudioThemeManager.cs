using UnityEngine;

public class AudioThemeManager : MonoBehaviour
{
    [Header("Audio Sources")]
    public AudioSource audioDia;
    public AudioSource audioNoche;

    // Estado actual, true = día, false = noche
    private bool esDia = true;

    private void Start()
    {
        audioDia.Play();
        // Inicializar reproducción acorde al estado
        ActualizarAudio();
    }

    // Método público para cambiar el tema
    public void CambiarTema(bool valorDiaNoche)
    {
        esDia = valorDiaNoche;
        ActualizarAudio();
    }

    private void ActualizarAudio()
    {
        if (esDia)
        {
            if (!audioDia.isPlaying) audioDia.Play();
            if (audioNoche.isPlaying) audioNoche.Stop();
        }
        else
        {
            if (!audioNoche.isPlaying) audioNoche.Play();
            if (audioDia.isPlaying) audioDia.Stop();
        }
    }
}

