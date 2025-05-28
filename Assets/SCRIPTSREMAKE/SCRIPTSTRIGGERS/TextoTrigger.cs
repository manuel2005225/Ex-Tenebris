using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class TextoTrigger : MonoBehaviour
{

    public bool valortriggerfinal = false;
    public string textoTrigger = "¡Has encontrado un nuevo texto!";
    public PantallaFade pantallaFade;
    public VideoPlayer reproductorVideo;
    public GameObject contenedorVideo;
    public Transform destinoTeleporte;

    private Transform jugadorParaTeleporte;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (valortriggerfinal)
            {
                TextManager.Instance.MostrarMensaje(textoTrigger);
                pantallaFade.duracion = 0.5f;
                pantallaFade.FadeIn(() =>
                {
                    contenedorVideo.SetActive(true);
                    reproductorVideo.gameObject.SetActive(true);

                    reproductorVideo.loopPointReached -= OnVideoTerminado;
                    reproductorVideo.loopPointReached += OnVideoTerminado;

                    reproductorVideo.Stop();
                    reproductorVideo.Play();
                });

                // Guardar referencia al jugador para teletransporte
                jugadorParaTeleporte = other.transform;
            }
            else
            {
                TextManager.Instance.MostrarMensaje(textoTrigger);
            }
        }
        gameObject.SetActive(false);
    }

    // Este método es llamado cuando el video termina
    private void OnVideoTerminado(VideoPlayer vp)
    {
        reproductorVideo.loopPointReached -= OnVideoTerminado;
        reproductorVideo.Stop();

        pantallaFade.duracion = 0.5f;
        pantallaFade.FadeOut();

        contenedorVideo.SetActive(false);
        reproductorVideo.gameObject.SetActive(false);

        // Teletransporta al jugador
        if (jugadorParaTeleporte != null && destinoTeleporte != null)
            jugadorParaTeleporte.position = destinoTeleporte.position;
    }
}
