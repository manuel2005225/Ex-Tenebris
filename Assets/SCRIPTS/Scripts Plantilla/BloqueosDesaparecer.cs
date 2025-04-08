using UnityEngine;

public class Aparecer : MonoBehaviour
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
        if (Camascript != null)
        {
            // Si ValorDiayNoche es true, activa los hijos. Si es false, los desactiva.
            bool shouldActivate = Camascript.ValorDiayNoche;

            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(shouldActivate);
            }
        }
    }
}