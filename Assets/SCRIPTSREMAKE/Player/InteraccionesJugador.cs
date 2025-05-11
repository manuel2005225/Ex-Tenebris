using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    public float distancia = 1.5f;
    public LayerMask capaInteractuables;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, distancia, capaInteractuables);

            if (hit.collider != null)
            {
                IInteractuable objeto = hit.collider.GetComponent<IInteractuable>();
                if (objeto != null)
                {
                    objeto.Interactuar(); // Llama a la función del NPC
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.right * transform.localScale.x * distancia);
    }
}

