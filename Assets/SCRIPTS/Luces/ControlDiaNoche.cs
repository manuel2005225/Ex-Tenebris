    using UnityEngine;
    using UnityEngine.Rendering.Universal;

    public class ControlDiaNoche : MonoBehaviour
    {
        public bool ValorDiayNoche = true;
        public int DiaActual;

        [Header("Cooldown en segundos para alternar día/noche")]
        public float cooldown = 10f;
        private float nextToggleTime = 0f;

        private void OnCollisionEnter2D(Collision2D other) 
        {
            if (other.gameObject.CompareTag("Player") && Time.time >= nextToggleTime)
            {
                // Buscar todos los objetos con el tag "LucesMundo"
                GameObject[] luces = GameObject.FindGameObjectsWithTag("LucesMundo");

                bool esDeNoche = false;

                // Si al menos una luz está encendida, se apagan todas (modo noche)
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
                    DiaActual += 1; // Solo avanza el día cuando pasa de noche a día
                }

                nextToggleTime = Time.time + cooldown;
            }
        }
    }