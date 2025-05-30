using UnityEngine;
using System.Collections.Generic;

public class PlayerInteractor : MonoBehaviour
{
    public float distancia = 1.5f;
    public LayerMask capaInteractuables;

    public Transform puntoRaycast;      // Objeto hijo que apunta en la dirección actual
    public float offset = 1f;           // Distancia desde el jugador hacia donde se moverá el punto

    private Vector2 direccion = Vector2.right; // Dirección lógica del raycast (predeterminada)

    private List<IInteractuable> interactuablesCercanos = new List<IInteractuable>();
    public KeyCode teclaInteraccion = KeyCode.E;

    void Update()
    {
        ActualizarDireccion();

        bool interactuar = Input.GetKeyDown(teclaInteraccion) || Input.GetKeyDown(KeyCode.JoystickButton1);

        if (interactuar && interactuablesCercanos.Count > 0)
        {
            IInteractuable masCercano = null;
            float distanciaMin = float.MaxValue;
            Vector3 posJugador = transform.position;

            foreach (var interactuable in interactuablesCercanos)
            {
                if (interactuable == null) continue;
                float dist = Vector3.Distance(posJugador, ((MonoBehaviour)interactuable).transform.position);
                if (dist < distanciaMin)
                {
                    distanciaMin = dist;
                    masCercano = interactuable;
                }
            }

            if (masCercano != null)
                masCercano.Interactuar();
        }
    }

    void ActualizarDireccion()
    {
        if (Input.GetKeyDown(KeyCode.W))
            direccion = Vector2.up;
        else if (Input.GetKeyDown(KeyCode.S))
            direccion = Vector2.down;
        else if (Input.GetKeyDown(KeyCode.A))
            direccion = Vector2.left;
        else if (Input.GetKeyDown(KeyCode.D))
            direccion = Vector2.right;

        // Mover el puntoRaycast en esa dirección
        if (puntoRaycast != null)
            puntoRaycast.localPosition = direccion * offset;
    }

    private void OnDrawGizmosSelected()
    {
        if (puntoRaycast == null) return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(puntoRaycast.position, puntoRaycast.position + (Vector3)direccion * distancia);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var interactuable = other.GetComponent<IInteractuable>();
        if (interactuable != null && !interactuablesCercanos.Contains(interactuable))
            interactuablesCercanos.Add(interactuable);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        var interactuable = other.GetComponent<IInteractuable>();
        if (interactuable != null)
            interactuablesCercanos.Remove(interactuable);
    }
}


