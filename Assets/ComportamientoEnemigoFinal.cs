using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class EnemigoObservadorController : MonoBehaviour
{
    [Header("Condiciones de activación")]
    public GameObject[] objetosInactivos;
    public ControlDiaNoche verificadorDiaNoche;

    [Header("Referencias visuales")]
    public GameObject enemigoVisual;
    public SpriteRenderer backgroundRenderer;
    public Color backgroundNormal = Color.green;
    public DistorsionJugador distorsionJugador;

    [Header("Luz roja y jugador")]
    public Light2D luzRojaJugador;
    public Transform jugador;

    [Header("Timers y comportamiento")]
    public float tiempoAntesDeVerificar = 3f;
    public float tiempoObservacion = 1f;
    public Vector2 intervaloObservacion = new Vector2(3f, 6f);
    public float velocidadOscurecer = 1f;

    [HideInInspector]
    public bool enemigoActivo = false;

    private bool observando = false;

    void Start()
    {
        luzRojaJugador.intensity = 0;
        backgroundRenderer.color = backgroundNormal;

        StartCoroutine(LoopVerificacionActivacion());
    }

    IEnumerator LoopVerificacionActivacion()
    {
        yield return new WaitForSeconds(tiempoAntesDeVerificar);

        while (!enemigoActivo)
        {
            if (TodosDestruidosOInactivos() && !verificadorDiaNoche.ValorDiayNoche)
            {
                StartCoroutine(ActivarEnemigoVisual());
                yield break;
            }

            yield return new WaitForSeconds(0.5f);
        }
    }

    bool TodosDestruidosOInactivos()
    {
        foreach (GameObject obj in objetosInactivos)
        {
            if (obj != null)
                return false;
        }
        return true;
    }

    IEnumerator ActivarEnemigoVisual()
    {
        enemigoActivo = true;

        float t = 0;
        Color originalColor = backgroundRenderer.color;
        Color targetColor = new Color(0, 0, 0, 0);

        while (t < 1f)
        {
            t += Time.deltaTime * velocidadOscurecer;
            backgroundRenderer.color = Color.Lerp(originalColor, targetColor, t);
            yield return null;
        }

        backgroundRenderer.enabled = false;

        yield return new WaitForSeconds(0.5f);

        enemigoVisual.SetActive(true);
        StartCoroutine(LoopObservacion());
    }

    IEnumerator LoopObservacion()
    {
        while (enemigoActivo)
        {
            float tiempo = Random.Range(intervaloObservacion.x, intervaloObservacion.y);
            yield return new WaitForSeconds(tiempo);
            StartCoroutine(ObservarJugador());
        }
    }

    IEnumerator ObservarJugador()
    {
        observando = true;
        distorsionJugador.ActivarDistorsion();

        Vector3 posicionInicial = jugador.position;
        float t = 0;

        while (t < 1f)
        {
            t += Time.deltaTime / 0.2f;
            luzRojaJugador.intensity = Mathf.Lerp(0, 1, t);
            yield return null;
        }

        float tiempoObs = 0f;
        while (tiempoObs < tiempoObservacion)
        {
            if (Vector3.Distance(posicionInicial, jugador.position) > 0.05f)
            {
                Debug.LogWarning("Jugador se movió... ¡Muerte!");
                // Lógica de muerte aquí
            }

            tiempoObs += Time.deltaTime;
            yield return null;
        }

        t = 0;
        while (t < 1f)
        {
            t += Time.deltaTime / 0.3f;
            luzRojaJugador.intensity = Mathf.Lerp(1, 0, t);
            yield return null;
        }

        distorsionJugador.DesactivarDistorsion();
        observando = false;
    }

    public void ForzarSalida()
    {
        StopAllCoroutines();
        StartCoroutine(DesactivarEnemigo());
    }

    IEnumerator DesactivarEnemigo()
    {
        enemigoActivo = false;
        luzRojaJugador.intensity = 0;
        distorsionJugador.DesactivarDistorsion();
        backgroundRenderer.color = new Color(0, 0, 0, 1);
        backgroundRenderer.enabled = true;
        enemigoVisual.SetActive(false);
        yield return null;
    }
}




