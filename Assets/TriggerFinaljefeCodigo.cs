using UnityEngine;

public class SalidaJefeTrigger : MonoBehaviour
{
    public EnemigoObservadorController controlador;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && controlador.enemigoActivo)
        {
            controlador.ForzarSalida();
            gameObject.SetActive(false);
        }
    }
}

