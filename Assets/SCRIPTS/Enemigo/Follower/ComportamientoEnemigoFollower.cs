using UnityEngine;
using Pathfinding;

public class LuzEvadeEnemy : MonoBehaviour
{
    public Transform player;         // Referencia al jugador
    public float checkRadius = 0.5f; // Radio para comprobar si está iluminado

    private AIPath aiPath;
    private AIDestinationSetter destino;

    void Start()
    {
        aiPath = GetComponent<AIPath>();
        destino = GetComponent<AIDestinationSetter>();

        // Inicialmente no lo sigue
        destino.target = null;
    }

    void Update()
    {
        // Verifica si hay luz cerca del jugador
        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.position, checkRadius);
        bool isIlluminated = false;

        foreach (Collider2D col in colliders)
        {
            if (col.CompareTag("PlayerLight"))
            {
                isIlluminated = true;
                break;
            }
        }

        if (!isIlluminated)
        {
            // Comienza a perseguir
            destino.target = player;
            aiPath.canMove = true;
        }
        else
        {
            // Deja de moverse
            destino.target = null;
            aiPath.canMove = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (player != null)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(player.position, checkRadius);
        }
    }
}