using UnityEngine;
using UnityEngine.InputSystem;

public class JoystickAimNewInput : MonoBehaviour
{
    // Vincular la acción “Look” en el Inspector
    public InputActionReference lookAction; 

    void OnEnable()
    {
        lookAction.action.Enable();
    }

    void OnDisable()
    {
        lookAction.action.Disable();
    }

    void Update()
    {
        // Lee el valor como Vector2
        Vector2 lookDirection = lookAction.action.ReadValue<Vector2>();

        if (lookDirection.sqrMagnitude > 0.01f)
        {
            float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }
    }
}
