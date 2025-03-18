using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ControlDiaNoche : MonoBehaviour
{
    public Light2D luz;
    public bool ValorDiayNoche = true;

    public int DiaActual;

    [Header("Cooldown en segundos para alternar día/noche")]
    public float cooldown = 10f;
    private float nextToggleTime = 0f;

    private void OnCollisionEnter2D(Collision2D other) 
    {
        // Verifica si el objeto es Player y si ya pasó el cooldown
        if (other.gameObject.CompareTag("Player") && Time.time >= nextToggleTime)
        {
            if (luz.intensity == 0)
            {
                luz.intensity = 1;  
                ValorDiayNoche = true; // Día
                DiaActual += 1; 
            }
            else
            {
                luz.intensity = 0;
                ValorDiayNoche = false; // Noche
            }

            // Asigna el próximo momento en que se puede alternar la luz
            nextToggleTime = Time.time + cooldown;
        }
    }
}