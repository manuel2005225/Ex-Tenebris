using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody2D body;
    private Vector2 movement;

    private bool puedeMoverse = true;
    private bool estaBloqueado = false;
    private Animator animator;

    private float lastMoveDir = 0f; // Valor por defecto (idle)

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!puedeMoverse)
        {
            movement = Vector2.zero;
            animator.SetFloat("Move_Dir", 0f);
            return;
        }

        // Detectar el movimiento horizontal y vertical con teclas
        float horizontal = 0f;
        float vertical = 0f;

        if (Input.GetKey(KeyCode.D)) horizontal = 1f; // Mover a la derecha
        if (Input.GetKey(KeyCode.A)) horizontal = -1f; // Mover a la izquierda
        if (Input.GetKey(KeyCode.W)) vertical = 1f; // Mover hacia arriba
        if (Input.GetKey(KeyCode.S)) vertical = -1f; // Mover hacia abajo

        movement = new Vector2(horizontal, vertical);

        // Normalizamos el vector si la magnitud es mayor que 1 (para las diagonales)
        if (movement.magnitude > 1f)
            movement.Normalize();

        // Detectar si el movimiento es diagonal y actualizar la animación
        if (movement.magnitude < 0.1f)
        {
            animator.SetFloat("Move_Dir", 0f); // Parado
            animator.SetFloat("Direction_Idle", lastMoveDir);
        }
        else
        {
            // Calculamos la dirección de movimiento en función de los ejes X y Y
            float moveDir = GetMoveDirection(movement);
            animator.SetFloat("Move_Dir", moveDir);
            animator.SetFloat("Direction_Idle", moveDir);

            lastMoveDir = moveDir;
        }
    }

    void FixedUpdate()
    {
        body.MovePosition(body.position + movement * speed * Time.fixedDeltaTime);
    }

    public void BloquearMovimiento(bool estado)
    {
        if (estaBloqueado == estado) return; // evita llamadas repetidas
        estaBloqueado = estado;
        puedeMoverse = !estado;
    }

    private float GetMoveDirection(Vector2 dir)
    {
        float x = dir.x;
        float y = dir.y;

        // Umbral para detectar movimiento
        float threshold = 0.3f;

        // Condiciones para las direcciones
        bool up = y > threshold;
        bool down = y < -threshold;
        bool right = x > threshold;
        bool left = x < -threshold;

        // Asignar valores específicos para las diagonales y direcciones cardinales
        if (up)
        {
            if (right) return 7f;  // Arriba derecha
            if (left) return 6f;   // Arriba izquierda
            return 2f;             // Arriba
        }
        else if (down)
        {
            if (right) return 5f;  // Abajo derecha
            if (left) return 8f;   // Abajo izquierda
            return 1f;             // Abajo
        }
        else
        {
            if (right) return 3f;  // Derecha
            if (left) return 4f;   // Izquierda
            return 0f;             // Parado
        }
    }
}
