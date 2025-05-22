using UnityEngine;
using UnityEngine.Video;

public class TriggerReproducirVideo : MonoBehaviour
{
    [Header("Componentes")]
    public GameObject contenedorVideo;
    public PantallaFade pantallaFade;
    public VideoPlayer reproductorVideo;

    [Header("Configuración del clip")]
    public VideoClip clipVideo;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip sonidoAntesDelFade;

    private bool videoReproduciendose = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("[Trigger] OnTriggerEnter2D");

        if (videoReproduciendose) return;

        if (other.CompareTag("Player"))
        {
            Debug.Log("[Trigger] Jugador entró al trigger");
            videoReproduciendose = true;

            TextManager.Instance.BloquearInput(true);
            var jugador = other.GetComponent<PlayerMovement>();
            jugador?.BloquearMovimiento(true);

            float delay = 0f;

            if (sonidoAntesDelFade != null && audioSource != null)
            {
                Debug.Log("[Trigger] Reproduciendo sonido de entrada");
                audioSource.PlayOneShot(sonidoAntesDelFade);
                delay = sonidoAntesDelFade.length;
            }

            Debug.Log($"[Trigger] Esperando {delay + 0.1f} segundos antes de iniciar FadeIn");
            Invoke(nameof(IniciarFadeYVideo), delay + 0.1f);
        }
    }

    private void IniciarFadeYVideo()
    {
        Debug.Log("[Fade] Iniciando FadeIn");

        pantallaFade.duracion = 0.5f;

        pantallaFade.FadeIn(() =>
        {
            Debug.Log("[Fade] Callback de FadeIn alcanzado");

            LimpiarRenderTexture();

            if (clipVideo == null)
            {
                Debug.LogError("[Video] No hay clip de video asignado.");
                return;
            }

            reproductorVideo.clip = clipVideo;
            reproductorVideo.gameObject.SetActive(true);
            contenedorVideo.SetActive(true);

            reproductorVideo.loopPointReached -= OnVideoTerminado;
            reproductorVideo.loopPointReached += OnVideoTerminado;

            reproductorVideo.Stop();
            reproductorVideo.Play();

            Debug.Log("[Video] Reproducción del video iniciada");
        });
    }

    private void OnVideoTerminado(VideoPlayer vp)
    {
        Debug.Log("[Video] Video finalizado");

        reproductorVideo.loopPointReached -= OnVideoTerminado;
        reproductorVideo.Stop();

        LimpiarRenderTexture();

        reproductorVideo.gameObject.SetActive(false);
        contenedorVideo.SetActive(false);

        pantallaFade.duracion = 0.5f;
        pantallaFade.FadeOut();

        TextManager.Instance.BloquearInput(false);

        GameObject jugador = GameObject.FindGameObjectWithTag("Player");
        jugador?.GetComponent<PlayerMovement>()?.BloquearMovimiento(false);

        videoReproduciendose = false;

        Debug.Log("[Video] Movimiento y control desbloqueados");

        Destroy(gameObject); // ✅ ¡Ahora sí lo destruimos al final, cuando todo ha terminado!
    }

    private void LimpiarRenderTexture()
    {
        Debug.Log("[RenderTexture] Limpiando RenderTexture");

        if (reproductorVideo.targetTexture != null)
        {
            RenderTexture.active = reproductorVideo.targetTexture;
            GL.Clear(true, true, Color.black);
            RenderTexture.active = null;
        }
    }
}







