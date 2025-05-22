using UnityEngine;

public class TriggerVictoria : MonoBehaviour
{
    public PantallaFade pantallaFade;

    private bool yaActivado = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (yaActivado) return;

        if (other.CompareTag("Player")) // Asegúrate de que el jugador tenga el tag "Player"
        {
            yaActivado = true;

            // Congela al jugador
            var jugador = other.GetComponent<PlayerMovement>();
            jugador?.BloquearMovimiento(true);

            // Hacer el fade y luego mostrar mensaje
            pantallaFade.FadeIn(() =>
            {
                TextManager.Instance.MostrarMensaje("¡Has ganado!", 3f);
            });
        }
    }
}
