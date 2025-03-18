using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class ControlSchemeSwitcher : MonoBehaviour
{
    private PlayerInput playerInput;

    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    void OnEnable()
    {
        playerInput.onControlsChanged += OnControlsChanged;
    }

    void OnDisable()
    {
        playerInput.onControlsChanged -= OnControlsChanged;
    }

    private void OnControlsChanged(PlayerInput input)
    {
        // Leemos qué esquema de control está activo
        string currentScheme = input.currentControlScheme;

        // Si el esquema es Gamepad...
        if (currentScheme == "Gamepad")
        {
            // Ocultamos el cursor y, opcionalmente, bloqueamos el mouse
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            // Si además quisieras ignorar totalmente la entrada del mouse,
            // podrías deshabilitar la acción de delta pointer, etc.
        }
        // Si el esquema es Teclado+Ratón...
        else if (currentScheme == "Keyboard&Mouse")
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}