using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class TextManager : MonoBehaviour
{
    public static TextManager Instance;

    public GameObject panelTexto;
    public TMP_Text texto;

    private List<string> paginas = new List<string>();
    private int indicePaginaActual = 0;
    private bool estaMostrandoDialogo = false;
    private bool inputBloqueado = false;

    private PlayerMovement jugador;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        panelTexto.SetActive(false);

        jugador = FindObjectOfType<PlayerMovement>(); // busca el script de movimiento
    }

    private void Update()
    {
        if (estaMostrandoDialogo && Input.GetKeyDown(KeyCode.E) && !inputBloqueado)
        {
            AvanzarPagina();
        }
    }

    /// <summary>
    /// Muestra un solo mensaje sin paginación.
    /// </summary>
    public void MostrarMensaje(string mensaje, float duracion = 2f)
    {
        StopAllCoroutines();

        panelTexto.SetActive(true);
        texto.text = mensaje;
        estaMostrandoDialogo = false;

        BloquearInput(true);
        jugador?.BloquearMovimiento(true); // 🚫 Bloqueo

        Invoke(nameof(OcultarMensaje), duracion);
    }

    /// <summary>
    /// Muestra un mensaje luego de una pausa (como uno corto, pero retrasado).
    /// </summary>
    public void MostrarDialogoPausado(string mensaje, float retraso, float duracion = 2f)
    {
        StopAllCoroutines();
        StartCoroutine(MostrarConRetraso(mensaje, retraso, duracion));
    }

    private IEnumerator MostrarConRetraso(string mensaje, float retraso, float duracion)
    {
        yield return new WaitForSeconds(retraso);
        MostrarMensaje(mensaje, duracion);
    }

    /// <summary>
    /// Muestra un diálogo con varias páginas.
    /// </summary>
    public void MostrarDialogo(List<string> paginasDialogo)
    {
        if (paginasDialogo == null || paginasDialogo.Count == 0) return;

        paginas = paginasDialogo;
        indicePaginaActual = 0;
        panelTexto.SetActive(true);
        texto.text = paginas[indicePaginaActual];
        estaMostrandoDialogo = true;

        BloquearInput(false); // Permitimos avanzar páginas
        jugador?.BloquearMovimiento(true); // 🚫 Bloqueo mientras hay diálogo
    }

    private void AvanzarPagina()
    {
        indicePaginaActual++;

        if (indicePaginaActual >= paginas.Count)
        {
            OcultarMensaje();
        }
        else
        {
            texto.text = paginas[indicePaginaActual];
        }
    }

    public void OcultarMensaje()
    {
        panelTexto.SetActive(false);
        estaMostrandoDialogo = false;
        paginas.Clear();

        jugador?.BloquearMovimiento(false); // ✅ Desbloqueo
    }

    public void BloquearInput(bool estado)
    {
        inputBloqueado = estado;
    }

    public bool EstaMostrando()
{
    return estaMostrandoDialogo;
}

}

