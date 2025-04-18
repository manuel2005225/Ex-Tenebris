using UnityEngine;
using Pathfinding;

public class EnemigoConLuz : MonoBehaviour
{
    public Transform jugador;
    public Transform[] puntosPatrulla;
    private int indiceActual = 0;
    private bool enLuz = false;

    private AIPath aiPath;
    private AIDestinationSetter destino;

    void Start()
    {
        aiPath = GetComponent<AIPath>();
        destino = GetComponent<AIDestinationSetter>();
        destino.target = puntosPatrulla[indiceActual];
    }

    void Update()
    {
        if (enLuz)
        {
            destino.target = jugador;
        }
        else
        {
            // Si no está en la luz, patrulla
            if (!aiPath.pathPending && aiPath.remainingDistance < 0.5f)
            {
                indiceActual = (indiceActual + 1) % puntosPatrulla.Length;
                destino.target = puntosPatrulla[indiceActual];
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerLight"))
        {
            enLuz = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("PlayerLight"))
        {
            enLuz = false;

            // Opcional: reinicia patrulla al último punto
            destino.target = puntosPatrulla[indiceActual];
        }
    }
}