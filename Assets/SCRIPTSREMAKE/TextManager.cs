using UnityEngine;
using TMPro;

public class TextManager : MonoBehaviour
{
    public static TextManager Instance;

    public GameObject panelTexto;
    public TMP_Text texto;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        panelTexto.SetActive(false);
    }

    public void MostrarMensaje(string mensaje, float duracion = 2f)
    {
        StopAllCoroutines();
        texto.text = mensaje;
        panelTexto.SetActive(true);
        Invoke(nameof(OcultarMensaje), duracion);
    }

    public void OcultarMensaje()
    {
        panelTexto.SetActive(false);
    }
}

