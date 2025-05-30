using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody2D body;
    private Vector2 movement;

    private bool puedeMoverse = true;
    private bool estaBloqueado = false;
    private Animator animator;

    private float lastMoveDir = 1f; // Valor inicial por defecto (abajo)

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float horizontal = 0f;
        float vertical = 0f;

        // Captura de input solo si puede moverse
        if (puedeMoverse)
        {
            horizontal = Input.GetAxisRaw("Horizontal");
            vertical = Input.GetAxisRaw("Vertical");
        }

        movement = new Vector2(horizontal, vertical);

        // Normaliza diagonales para que no sean más rápidas
        if (movement.magnitude > 1f)
            movement.Normalize();

        // Histéresis para evitar rebotes de idle
        bool estaRealmenteMoviendose = movement.magnitude > 0.05f;

        if (!estaRealmenteMoviendose)
        {
            animator.SetBool("IsMoving", false);
            animator.SetFloat("Move_Dir", 0f); // Esto evita que el Blend Tree de caminar reaccione
            animator.SetFloat("Direction_Idle", lastMoveDir);
        }
        else
        {
            float moveDir = GetMoveDirection(movement);
            animator.SetBool("IsMoving", true);
            animator.SetFloat("Move_Dir", moveDir);
            animator.SetFloat("Direction_Idle", moveDir);
            lastMoveDir = moveDir;
        }

        // Debug para verificar valores en tiempo real
        Debug.Log($"[Animator] IsMoving: {animator.GetBool("IsMoving")} | Move_Dir: {animator.GetFloat("Move_Dir"):F1} | Direction_Idle: {animator.GetFloat("Direction_Idle"):F1} | Input: ({movement.x}, {movement.y}) | Mag: {movement.magnitude:F2}");
    }

    void FixedUpdate()
    {
        body.MovePosition(body.position + movement * speed * Time.fixedDeltaTime);
    }

    public void BloquearMovimiento(bool estado)
    {
        if (estaBloqueado == estado) return;
        estaBloqueado = estado;
        puedeMoverse = !estado;
    }

    private float GetMoveDirection(Vector2 dir)
    {
        float x = dir.x;
        float y = dir.y;
        float threshold = 0.1f;

        bool up = y > threshold;
        bool down = y < -threshold;
        bool right = x > threshold;
        bool left = x < -threshold;

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
            return lastMoveDir;   // Mantener dirección anterior si no hay input
        }
    }
}



