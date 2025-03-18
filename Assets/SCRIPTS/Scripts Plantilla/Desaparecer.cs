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
            // Si ValorDiayNoche es false, desactiva el objeto. Si es true, lo activa.
            this.gameObject.SetActive(Camascript.ValorDiayNoche);
        }
    }
}
