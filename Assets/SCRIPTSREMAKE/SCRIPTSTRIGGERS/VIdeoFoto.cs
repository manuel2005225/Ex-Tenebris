using System.Collections;
using UnityEngine;
using UnityEngine.Video;

public class VIdeoFoto : MonoBehaviour, IInteractuable
{
    [Header("Componentes")]
    public GameObject contenedorVideo;         // El canvas o visual que contiene el RawImage
    public PantallaFade pantallaFade;          // Tu sistema de fade in/out
    public VideoPlayer reproductorVideo;       // El componente VideoPlayer
    public GameObject Bloqueo;

    [Header("Configuración del clip")]
    public VideoClip clipHermanaClara;         // Asigna el video desde el inspector

    public void Interactuar()
    {
        TextManager.Instance.BloquearInput(true);
        TextManager.Instance.MostrarMensaje("<color=#e0aa3e>La Hermana clara...</color>", 2f);

        pantallaFade.duracion = 0.5f;
        StartCoroutine(EsperarYReproducirVideo(2f));
    }

    private IEnumerator EsperarYReproducirVideo(float delay)
    {
        yield return new WaitForSeconds(delay);

        pantallaFade.FadeIn(() =>
        {
            // 🔒 Limpieza previa
            LimpiarRenderTexture();

            // Asignar el clip
            reproductorVideo.clip = clipHermanaClara;

            // Activar y preparar
            reproductorVideo.gameObject.SetActive(true);
            contenedorVideo.SetActive(true);
            reproductorVideo.loopPointReached += OnVideoTerminado;

            reproductorVideo.Play();
            Debug.Log("🎥 Video comenzando correctamente");
        });
    }

    private void OnVideoTerminado(VideoPlayer vp)
    {
        Debug.Log("🛑 Video Terminado");

        reproductorVideo.loopPointReached -= OnVideoTerminado;
        reproductorVideo.Stop();

        // 🔒 Limpieza post reproducción
        LimpiarRenderTexture();

        reproductorVideo.gameObject.SetActive(false);
        contenedorVideo.SetActive(false);

        pantallaFade.duracion = 0.5f;
        pantallaFade.FadeOut();

        TextManager.Instance.BloquearInput(false);
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




