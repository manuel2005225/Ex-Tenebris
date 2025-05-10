using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody2D rb;
    private Vector2 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Captura la entrada del teclado (WASD o flechas)
        movement.x = Input.GetAxisRaw("Horizontal"); // A/D o Flechas izquierda/derecha
        movement.y = Input.GetAxisRaw("Vertical");   // W/S o Flechas arriba/abajo
    }

    void FixedUpdate()
    {
        // Mueve el Rigidbody2D
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }
}

