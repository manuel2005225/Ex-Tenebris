using UnityEngine;

public class TriggerVictoria : MonoBehaviour
{
    public PantallaFade pantallaFade;
    public GameObject enemigo;
    private bool yaActivado = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (yaActivado) return;

        if (other.CompareTag("Player"))
        {
            yaActivado = true;
            Destroy(enemigo);
            // Congela al jugador
            var jugador = other.GetComponent<PlayerMovement>();
            jugador?.BloquearMovimiento(true);

            // Mostrar mensaje antes del fade
            TextManager.Instance.MostrarMensajeAvanzable("<color=#FFD700>¡Fin del juego!</color>");

            // Hacer el fade después de un pequeño retraso si quieres (opcional)
            pantallaFade.duracion = 2f;
            pantallaFade.FadeIn(() => { });
            
        }
    }
}
