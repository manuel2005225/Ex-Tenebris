using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody2D body;
    private Vector2 movement;

    private bool puedeMoverse = true;
    private bool estaBloqueado = false;
    private Animator animator;

    private float lastMoveDir = 1f; // Valor por defecto (abajo)

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

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        movement = new Vector2(horizontal, vertical);

        if (movement.magnitude > 1f)
            movement.Normalize();

        if (movement.magnitude < 0.1f)
        {
            animator.SetFloat("Move_Dir", 0f);
            animator.SetFloat("Direction_Idle", lastMoveDir);
        }
        else
        {
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

        float threshold = 0.3f;

        bool up = y > threshold;
        bool down = y < -threshold;
        bool right = x > threshold;
        bool left = x < -threshold;

        if (up)
        {
            if (right) return 7f;
            else if (left) return 6f;
            else return 2f;
        }
        else if (down)
        {
            if (right) return 5f;
            else if (left) return 8f;
            else return 1f;
        }
        else
        {
            if (right) return 3f;
            else if (left) return 4f;
            else return 0f;
        }
    }
}




