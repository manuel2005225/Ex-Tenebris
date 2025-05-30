using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class UISelector : MonoBehaviour
{
    public GameObject firstButton;

    void Start()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstButton);
    }

    void Update()
    {
        if (Gamepad.current != null && Gamepad.current.dpad.up.wasPressedThisFrame)
        {
            Debug.Log("Gamepad detectado: D-PAD ↑ presionado");
        }
    }
}
