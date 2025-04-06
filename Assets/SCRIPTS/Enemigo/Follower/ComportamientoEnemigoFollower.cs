using UnityEngine;

public class SimpleEnemy : MonoBehaviour
{
    public Transform player;         // Referencia al jugador
    public float moveSpeed = 3f;       // Velocidad de movimiento
    public float checkRadius = 0.5f;   // Radio para comprobar si el jugador está iluminado

    void Update()
    {
        // Verifica si hay algún objeto con el tag "PlayerLight" alrededor del jugador
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

        // Si el jugador NO está iluminado, el enemigo se mueve hacia él
        if (!isIlluminated)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        }
    }

    // (Opcional) Para visualizar el área de comprobación en la escena
    private void OnDrawGizmosSelected()
    {
        if (player != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(player.position, checkRadius);
        }
    }
}
