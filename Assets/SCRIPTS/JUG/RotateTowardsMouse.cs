using UnityEngine;

public class RotateTowardsMouse2D : MonoBehaviour
{
    void Update()
    {
        // Obtiene la posición del mouse en píxeles
        Vector3 mousePos = Input.mousePosition;

        // Convierte esa posición a coordenadas de mundo
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);

        // Calcula la dirección del objeto hacia el mouse
        Vector3 direccion = mousePos - transform.position;

        // Calcula el ángulo en grados con Atan2 (eje Y primero, eje X después)
        float angulo = Mathf.Atan2(direccion.y, direccion.x) * Mathf.Rad2Deg;

        // Aplica el ángulo como una rotación en Z (2D usa el Z para girar)
        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angulo));
    }
}