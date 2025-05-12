using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Video;

public class ControlDiaNocheR : MonoBehaviour
{
    public bool ValorDiayNoche = true;
    public int DiaActual;
    public int Puede_Dormir = 0;

    public PantallaFade pantallaFade;

    [Header("Cooldown en segundos para alternar día/noche")]
    public float cooldown = 10f;
    private float nextToggleTime = 0f;

    [Header("Puntos de teletransporte")]
    public Transform posicionDia;
    public Transform posicionNoche;

    public Light2D LuzGlobal;

    public int ContadorNPCs = 0;

    [Header("Reproducción de video")]
    public VideoPlayer reproductorVideo;
    public GameObject contenedorVideo;

    private void Start()
    {
        if (reproductorVideo.targetTexture != null)
        {
            RenderTexture.active = reproductorVideo.targetTexture;
            GL.Clear(true, true, Color.black);
            RenderTexture.active = null;
        }

        contenedorVideo.SetActive(false);
    }
    private void Update()
    {
        if (ContadorNPCs == 3)
        {
            Puede_Dormir = 1;
        }
        else
        {
            Puede_Dormir = 0;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") && Time.time >= nextToggleTime)
        {
            if (Puede_Dormir == 1)
            {
                // Teletransportar al jugador
                Transform destino = ValorDiayNoche ? posicionNoche : posicionDia;
                other.transform.position = destino.position;

                // Alternar estado día/noche
                ValorDiayNoche = !ValorDiayNoche;
                if (!ValorDiayNoche) DiaActual += 1;

                nextToggleTime = Time.time + cooldown;

                // FADE IN rápido y luego reproducir video
                pantallaFade.duracion = 0.5f;
                pantallaFade.FadeIn(() =>
                {
                    LuzGlobal.intensity = 0f;

                    contenedorVideo.SetActive(true);
                    reproductorVideo.Play();

                    reproductorVideo.loopPointReached += OnVideoTerminado;
                });
            }
            else
            {
                TextManager.Instance.MostrarMensaje("No puedes dormir todavía.");
            }
        }
    }

    private void OnVideoTerminado(VideoPlayer vp)
    {
        reproductorVideo.loopPointReached -= OnVideoTerminado;

        contenedorVideo.SetActive(false);

        pantallaFade.duracion = 0.5f;
        pantallaFade.FadeOut();

        TextManager.Instance.MostrarDialogoPausado("Presiona F para prender la linterna", 1f, 2f);
    }
}

