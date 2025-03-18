using JetBrains.Annotations;
using UnityEngine;

public class MovimientoJugador : MonoBehaviour
{
    public float movementSpeed = 2f;
    private Rigidbody2D rb;

    private float rotacion;

 

    private Vector2 VectorMovimiento;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();  
        rotacion = transform.eulerAngles.z;        
    }

    // Update is called once per frame
    void Update()
    {

        if(rotacion >= 180 || rotacion < 0 ){

        float movementHor = Input.GetAxisRaw("Horizontal");
        float movementVer = Input.GetAxisRaw("Vertical");

        Vector2 input = new Vector2(movementHor,movementVer).normalized;

        VectorMovimiento = (transform.up * input.x) + (transform.right * input.y);
        }else{
            

        float movementHor = Input.GetAxisRaw("Horizontal");
        float movementVer = Input.GetAxisRaw("Vertical");

        Vector2 input = new Vector2(movementHor*-1,movementVer).normalized;

        VectorMovimiento = (transform.up * input.x) + (transform.right * input.y);
        }
        

    }


    void FixedUpdate()
    {
        rb.MovePosition(rb.position + VectorMovimiento * movementSpeed * Time.fixedDeltaTime);
        
    }
}
