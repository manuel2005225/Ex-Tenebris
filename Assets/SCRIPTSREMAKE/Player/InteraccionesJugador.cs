using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    public float distancia = 1.5f;
    public LayerMask capaInteractuables;

    public Transform puntoRaycast;      // Objeto hijo que apunta en la dirección actual
    public float offset = 1f;           // Distancia desde el jugador hacia donde se moverá el punto

    private Vector2 direccion = Vector2.right; // Dirección lógica del raycast (predeterminada)

    void Update()
    {
        ActualizarDireccion();

        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit2D hit = Physics2D.Raycast(puntoRaycast.position, direccion, distancia, capaInteractuables);

            if (hit.collider != null)
            {
                IInteractuable interactuable = hit.collider.GetComponent<IInteractuable>();
                if (interactuable != null)
                {
                    interactuable.Interactuar();
                }
            }
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
}


