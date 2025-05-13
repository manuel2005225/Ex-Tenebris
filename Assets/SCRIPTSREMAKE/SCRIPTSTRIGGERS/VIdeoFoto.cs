using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VIdeoFoto : MonoBehaviour, IInteractuable
{
    public GameObject contenedorVideo;
    public PantallaFade pantallaFade;
    public VideoPlayer reproductorVideo;

    public void Interactuar()
    {
        // Implement the interaction logic here
        TextManager.Instance.BloquearInput(true);
        TextManager.Instance.MostrarMensaje("<color=#e0aa3e>La Hermana clara...</color>", 2f);
        
        StartCoroutine(ActivarEnUnosSegundos(2f));
        pantallaFade.duracion = 0.5f;
        pantallaFade.FadeIn(() =>
                {
                    

                    contenedorVideo.SetActive(true);
                    reproductorVideo.Play();   
                    Debug.Log("Video Reproduciendose");

                    
                });
        
    }

    private IEnumerator ActivarEnUnosSegundos(float delay)
    {
        yield return new WaitForSeconds(delay);
        contenedorVideo.SetActive(true);
        reproductorVideo.loopPointReached += OnVideoTerminado;
        Debug.Log("Video cargando wahahha");
    }

    private void OnVideoTerminado(VideoPlayer vp)
    {
        reproductorVideo.loopPointReached -= OnVideoTerminado;

        contenedorVideo.SetActive(false);

        pantallaFade.duracion = 0.5f;
        pantallaFade.FadeOut();

        TextManager.Instance.BloquearInput(false);
        Debug.Log("Video Terminado");

        if (reproductorVideo.targetTexture != null)
        {
            RenderTexture.active = reproductorVideo.targetTexture;
            GL.Clear(true, true, Color.black);
            RenderTexture.active = null;
        }
    }
}
   

