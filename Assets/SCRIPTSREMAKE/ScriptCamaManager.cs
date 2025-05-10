using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ControlDiaNocheR : MonoBehaviour
{
    public bool ValorDiayNoche = true;
    public int DiaActual;
    public int Puede_Dormir = 0;

    [Header("Cooldown en segundos para alternar día/noche")]
    public float cooldown = 10f;
    private float nextToggleTime = 0f;

    [Header("Puntos de teletransporte")]
    public Transform posicionDia;
    public Transform posicionNoche;

    public Light2D LuzGlobal;

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

                LuzGlobal.intensity = 0f;
                TextManager.Instance.MostrarDialogoPausado("Presiona F para prender la linterna", 1f, 2f);
            }
            else
            {
                // Usar TextManager para mostrar advertencia
                TextManager.Instance.MostrarMensaje("No puedes dormir todavía.");
            }
        }
    }
}
