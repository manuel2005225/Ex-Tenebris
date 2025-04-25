using UnityEngine;

public class MuerteManager : MonoBehaviour
{
    public static MuerteManager instancia;

    [TextArea(2, 4)]
    public string mensajeDeMuerteActual = "Has muerto.";

    void Awake()
    {
        if (instancia != null && instancia != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instancia = this;
            DontDestroyOnLoad(gameObject); // Si necesitas que sobreviva entre escenas
        }
    }

    public void SetMensajeMuerte(string nuevoMensaje)
    {
        mensajeDeMuerteActual = nuevoMensaje;
    }
}

