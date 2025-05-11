using UnityEngine;
using Pathfinding;

public class NPCMovimiento : MonoBehaviour
{
    [Header("Patrullaje")]
    public Transform[] puntosPatrulla;
    private int nodoActual = 0;

    private AIPath aiPath;
    public bool esperando = false;

    void Start()
    {
        aiPath = GetComponent<AIPath>();
        if (puntosPatrulla.Length > 0)
        {
            aiPath.destination = puntosPatrulla[nodoActual].position;
            aiPath.canMove = true;
        }
    }

    void Update()
    {
        if (!esperando && puntosPatrulla.Length > 0 && aiPath.reachedDestination)
        {
            nodoActual = (nodoActual + 1) % puntosPatrulla.Length;
            aiPath.destination = puntosPatrulla[nodoActual].position;
        }
    }

    public void IrADestino(Transform destino, bool quedarse)
    {
        esperando = quedarse;
        aiPath.destination = destino.position;
        aiPath.canMove = true;
    }

    public void ReanudarPatrullaje()
    {
        esperando = false;
        if (puntosPatrulla.Length > 0)
        {
            aiPath.destination = puntosPatrulla[nodoActual].position;
            aiPath.canMove = true;
        }
    }
}




