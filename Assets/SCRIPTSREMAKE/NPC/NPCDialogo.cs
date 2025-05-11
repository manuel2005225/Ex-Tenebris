using System.Collections.Generic;
using UnityEngine;

public class NPCDialogo : MonoBehaviour, IInteractuable
{
    [Header("Líneas del NPC (paginadas)")]
    [TextArea(2, 4)]
    public List<string> lineasNPC;

    [Header("Respuestas del jugador (opcional)")]
    [TextArea(2, 4)]
    public List<string> respuestasJugador;

    [Header("Interacción inicial especial")]
    public bool primeraInteraccion = true;

    [Header("Referencias para teletransporte")]
    public Transform destinoJugador;
    public Transform destinoNPC;
    public Transform jugador;

    public PantallaFade pantallaFade; // Opcional si tienes un sistema de fade

    public void Interactuar()
    {
        if (primeraInteraccion)
        {
            primeraInteraccion = false;
            IniciarPrimeraSecuencia();
        }
        else
        {
            IniciarDialogo();
        }
    }

    private void IniciarPrimeraSecuencia()
    {
        TextManager.Instance.MostrarMensaje("Acompáñame al confesionario, hijo", 2f);
        Invoke(nameof(TeletransportarAmbos), 2.1f);
    }

    private void TeletransportarAmbos()
    {
        if (pantallaFade != null)
        {
            pantallaFade.FadeIn(() =>
            {
                jugador.position = destinoJugador.position;
                transform.position = destinoNPC.position;

                IniciarDialogo();
                pantallaFade.FadeOut();
            });
        }
        else
        {
            jugador.position = destinoJugador.position;
            transform.position = destinoNPC.position;

            IniciarDialogo();
        }
    }

    private void IniciarDialogo()
    {
        List<string> dialogoCompleto = new List<string>();

        int total = Mathf.Max(lineasNPC.Count, respuestasJugador.Count);

        for (int i = 0; i < total; i++)
        {
            if (i < lineasNPC.Count)
                dialogoCompleto.Add(lineasNPC[i]);

            if (i < respuestasJugador.Count)
                dialogoCompleto.Add(respuestasJugador[i]);
        }

        TextManager.Instance.MostrarDialogo(dialogoCompleto);
    }
}


