using UnityEngine;
using UnityEngine.Video;

public class TriggerReproducirVideo : MonoBehaviour
{
    [Header("Componentes")]
    public GameObject contenedorVideo;         // Canvas o visual que contiene el RawImage
    public PantallaFade pantallaFade;          // Sistema de fade in/out
    public VideoPlayer reproductorVideo;       // Componente VideoPlayer
    

    [Header("Configuración del clip")]
    public VideoClip clipVideo;                 // Clip a reproducir

    private bool videoReproduciendose = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (videoReproduciendose) return;       // Evita reactivaciones múltiples
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject); // Destruye el trigger al entrar
            videoReproduciendose = true;
         // Asegúrate de que el contenedor esté activado al inicio

            TextManager.Instance.BloquearInput(true);
            StartCoroutine(PrepararYReproducirVideo());
        }
    }

    private System.Collections.IEnumerator PrepararYReproducirVideo()
{
    // Inicia el fade in y continúa todo dentro del callback
    pantallaFade.duracion = 0.5f;
    pantallaFade.FadeIn(() =>
    {
        LimpiarRenderTexture();

        reproductorVideo.clip = clipVideo;
        reproductorVideo.gameObject.SetActive(true);
        contenedorVideo.SetActive(true);

        reproductorVideo.loopPointReached -= OnVideoTerminado;
        reproductorVideo.loopPointReached += OnVideoTerminado;

        reproductorVideo.Stop();
        reproductorVideo.Play();

        Debug.Log("🎥 Video comenzando correctamente: " + (clipVideo != null ? clipVideo.name : "NULO"));
    });

    yield break; // No esperamos nada más aquí
}

    private void OnVideoTerminado(VideoPlayer vp)
    {
        Debug.Log("🛑 Video Terminado");

        reproductorVideo.loopPointReached -= OnVideoTerminado;
        reproductorVideo.Stop();

        LimpiarRenderTexture();

        reproductorVideo.gameObject.SetActive(false);
        contenedorVideo.SetActive(false);

        pantallaFade.duracion = 0.5f;
        pantallaFade.FadeOut();

        TextManager.Instance.BloquearInput(false);

        videoReproduciendose = false;
    }

    private void LimpiarRenderTexture()
    {
        if (reproductorVideo.targetTexture != null)
        {
            RenderTexture.active = reproductorVideo.targetTexture;
            GL.Clear(true, true, Color.black);
            RenderTexture.active = null;
        }
    }
}


