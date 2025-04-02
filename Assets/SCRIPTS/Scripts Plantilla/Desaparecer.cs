using UnityEngine;

public class Desaparecer : MonoBehaviour
{
    public GameObject cama;
    private ControlDiaNoche Camascript;

    void Start()
    {
        // Obtener la referencia al script ControlDiaNoche
        Camascript = cama.GetComponent<ControlDiaNoche>();

        if (Camascript == null)
        {
            Debug.LogError("¡El objeto cama no tiene el script ControlDiaNoche adjunto!");
        }
    }

    void Update()
    {
        // Verifica que la referencia sea válida antes de usarla
        if (Camascript != null)
        {
            // Si ValorDiayNoche es false, desactiva los hijos. Si es true, los activa.
            bool shouldActivate = !Camascript.ValorDiayNoche;
            
            // Recorrer todos los hijos y activar/desactivar
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(shouldActivate);
            }
        }
    }
}
