using UnityEngine;

public class CameraFollow2D : MonoBehaviour
{
    // Referencia al Transform del jugador o de un objeto "foco" cercano a él.
    [SerializeField] private Transform player;

    // Ajusta cuán rápido se mueve la cámara al seguir al jugador.
    [SerializeField] private float followSpeed = 2.0f;

    // Offset para separar la cámara de la posición exacta del jugador
    // (por ejemplo, si quieres que la cámara esté un poco más arriba o a un costado).
    [SerializeField] private Vector3 offset;

    private void LateUpdate()
    {
        if (player == null) return; 

        // Calcula la posición deseada (posición del jugador + offset).
        Vector3 desiredPosition = player.position + offset;

        // Interpola suavemente entre la posición actual de la cámara y la deseada.
        Vector3 smoothedPosition = Vector3.Lerp(
            transform.position,
            desiredPosition,
            followSpeed * Time.deltaTime
        );

        // Actualiza la posición de la cámara. Mantén la Z si es un juego 2D.
        transform.position = new Vector3(
            smoothedPosition.x,
            smoothedPosition.y,
            transform.position.z
        );
    }
}