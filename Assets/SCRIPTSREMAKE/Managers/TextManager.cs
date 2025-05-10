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

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        panelTexto.SetActive(false);
    }

    private void Update()
    {
        if (estaMostrandoDialogo && Input.GetKeyDown(KeyCode.E))
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
    }
}

