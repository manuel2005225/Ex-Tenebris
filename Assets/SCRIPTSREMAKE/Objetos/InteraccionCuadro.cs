using System.Collections;
using UnityEngine;

public class InteraccionCuadro : MonoBehaviour, IInteractuable
{
    [Header("Componentes")]
    public GameObject contenedorVideo;         // Guardado para referencia futura
    // public PantallaFade pantallaFade;       // Guardado para referencia futura
    // public VideoPlayer reproductorVideo;    // Guardado para referencia futura
    public GameObject Bloqueo;

    [Header("Configuración del clip")]
    // public VideoClip clipHermanaClara;       // Guardado para referencia futura

    [Header("Trigger a activar al terminar el diálogo")]
    public GameObject triggerAlFinalDialogo;     // El trigger que se activará

    public void Interactuar()
    {
        TextManager.Instance.BloquearInput(true);
        TextManager.Instance.MostrarMensajeAvanzable("<color=#e0aa3e>La Hermana clara...</color>", () =>
        {
            // Esto se ejecuta cuando el jugador cierra el mensaje con E o B
            if (triggerAlFinalDialogo != null)
            {
                triggerAlFinalDialogo.SetActive(true);
                Debug.Log("Trigger activado tras el diálogo.");
            }
            else
            {
                Debug.LogWarning("No se asignó ningún trigger para activar.");
            }

            TextManager.Instance.BloquearInput(false);
            Bloqueo.SetActive(false);
            Destroy(gameObject); // Destruye el objeto actual
        });
    }

    

    /*
    // Código original de reproducción de video para futura adaptación

    private IEnumerator EsperarYReproducirVideo(float delay)
    {
        yield return new WaitForSeconds(delay);

        pantallaFade.FadeIn(() =>
        {
            LimpiarRenderTexture();

            reproductorVideo.clip = clipHermanaClara;
            reproductorVideo.gameObject.SetActive(true);
            contenedorVideo.SetActive(true);
            reproductorVideo.loopPointReached += OnVideoTerminado;

            reproductorVideo.Play();
            Debug.Log("Clip asignado: " + (clipHermanaClara != null ? clipHermanaClara.name : "NULO"));
            Debug.Log("🎥 Video comenzando correctamente");
        });
    }

    private void OnVideoTerminado(VideoPlayer vp)
    {
        Debug.Log("🛑 Video Terminado");

        reproductorVideo.loopPointReached -= OnVideoTerminado;
        reproductorVideo.Stop();

        LimpiarRenderTexture();

        reproductorVideo.gameObject.SetActive(false);
        contenedorVideo.SetActive(false);

        pantallaFade.FadeOut();

        TextManager.Instance.BloquearInput(false);
        Bloqueo.SetActive(false);
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
    */
}





