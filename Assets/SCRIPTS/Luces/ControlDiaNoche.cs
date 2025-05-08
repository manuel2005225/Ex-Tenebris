using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ControlDiaNoche : MonoBehaviour
{
    public bool ValorDiayNoche = true;
    public int DiaActual;
    public int Puede_Dormir = 0; // 1 para permitir dormir, 0 para bloquear

    [Header("Cooldown en segundos para alternar día/noche")]
    public float cooldown = 10f;
    private float nextToggleTime = 0f;

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if (other.gameObject.CompareTag("Player") && Time.time >= nextToggleTime)
        {
            if (Puede_Dormir == 1)
            {
                // Buscar todos los objetos con el tag "LucesMundo"
                GameObject[] luces = GameObject.FindGameObjectsWithTag("LucesMundo");

                bool esDeNoche = false;

                if (luces.Length > 0 && luces[0].TryGetComponent(out Light2D luzEjemplo))
                {
                    esDeNoche = luzEjemplo.intensity > 0;
                }

                foreach (GameObject luzGO in luces)
                {
                    Light2D luz = luzGO.GetComponent<Light2D>();
                    if (luz != null)
                    {
                        luz.intensity = esDeNoche ? 0 : 1;
                    }
                }

                ValorDiayNoche = !esDeNoche;

                if (!esDeNoche)
                {
                    DiaActual += 1;
                }

                nextToggleTime = Time.time + cooldown;
            }
            else
            {
                Debug.Log("No puedes dormir todavía.");
                // Aquí puedes invocar un sistema de UI para mostrar el texto en pantalla
                // Ejemplo: UIManager.Instance.MostrarMensaje("No puedes dormir todavía");
            }
        }
    }
}
