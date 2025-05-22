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
    private bool mostrandoMensajeSimple = false;
    private bool inputBloqueado = false;

    private PlayerMovement jugador;

    private Queue<(string mensaje, float duracion)> colaMensajesSimples = new Queue<(string mensaje, float duracion)>();

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        panelTexto.SetActive(false);
        jugador = FindObjectOfType<PlayerMovement>();
    }

    private void Update()
    {
        if (estaMostrandoDialogo && Input.GetKeyDown(KeyCode.E) && !inputBloqueado)
        {
            AvanzarPagina();
        }
    }

    public void MostrarMensaje(string mensaje, float duracion = 2f)
    {
        colaMensajesSimples.Enqueue((mensaje, duracion));
        TryMostrarMensajes();
    }

    private void TryMostrarMensajes()
    {
        if (!estaMostrandoDialogo && !mostrandoMensajeSimple && colaMensajesSimples.Count > 0)
        {
            StartCoroutine(ProximoMensajeSimple());
        }
    }

    private IEnumerator ProximoMensajeSimple()
    {
        mostrandoMensajeSimple = true;

        while (colaMensajesSimples.Count > 0)
        {
            var (msg, dur) = colaMensajesSimples.Dequeue();

            panelTexto.SetActive(true);
            texto.text = msg;

            
            jugador?.BloquearMovimiento(true);

            yield return new WaitForSeconds(dur);

            panelTexto.SetActive(false);

            BloquearInput(false);
            jugador?.BloquearMovimiento(false);
        }

        mostrandoMensajeSimple = false;
    }

    public void MostrarDialogo(List<string> paginasDialogo)
    {
        if (paginasDialogo == null || paginasDialogo.Count == 0) return;

        // Limpiamos cola de mensajes simples si queremos evitar solapamientos
        // colaMensajesSimples.Clear(); // opcional

        paginas = paginasDialogo;
        indicePaginaActual = 0;
        panelTexto.SetActive(true);
        texto.text = paginas[indicePaginaActual];
        estaMostrandoDialogo = true;

        BloquearInput(false);
        jugador?.BloquearMovimiento(true);
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

        jugador?.BloquearMovimiento(false);

        // Al terminar diálogo, revisa si hay mensajes pendientes
        TryMostrarMensajes();
    }

    public void MostrarDialogoPausado(string mensaje, float retraso, float duracion = 2f)
    {
        StartCoroutine(MostrarConRetraso(mensaje, retraso, duracion));
    }

    private IEnumerator MostrarConRetraso(string mensaje, float retraso, float duracion)
    {
        yield return new WaitForSeconds(retraso);
        MostrarMensaje(mensaje, duracion);
    }

    public void BloquearInput(bool estado)
    {
        inputBloqueado = estado;
    }

    public bool EstaMostrando()
    {
        return estaMostrandoDialogo || mostrandoMensajeSimple;
    }
}



