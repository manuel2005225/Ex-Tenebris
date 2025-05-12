using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialogo : MonoBehaviour, IInteractuable
{
    [Header("Líneas del Padre (inicia el diálogo)")]
    [TextArea(2, 4)]
    public List<string> lineasPadre;

    [Header("Líneas del NPC (responde al padre)")]
    [TextArea(2, 4)]
    public List<string> lineasNPC;

    [Header("Interacción inicial especial")]
    public bool primeraInteraccion = true;

    [Header("Referencias para teletransporte")]
    public Transform destinoJugador;
    public Transform destinoNPC;
    public Transform jugador;

    [Header("Control de transición visual")]
    public PantallaFade pantallaFade;

    [Header("Sistema de sueño y día/noche")]
    public ControlDiaNocheR controlDiaNoche;

    private bool bloqueadoTemporalmente = false;

    public void Interactuar()
    {
        if (bloqueadoTemporalmente) return;

        if (primeraInteraccion)
        {
            primeraInteraccion = false;
            bloqueadoTemporalmente = true;
            IniciarPrimeraSecuencia();
        }
        else
        {
            IniciarDialogo();
        }
    }

    private void IniciarPrimeraSecuencia()
    {
        TextManager.Instance.BloquearInput(true);
        TextManager.Instance.MostrarMensaje("<color=#e0aa3e>Acompáñame al confesionario, hijo</color>", 2f);

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

                pantallaFade.FadeOut();

                Invoke(nameof(DesbloquearEIniciarDialogo), 1f);
            });
        }
        else
        {
            jugador.position = destinoJugador.position;
            transform.position = destinoNPC.position;
            Invoke(nameof(DesbloquearEIniciarDialogo), 0.5f);
        }
    }

    private void DesbloquearEIniciarDialogo()
    {
        bloqueadoTemporalmente = false;
        TextManager.Instance.BloquearInput(false);
        IniciarDialogo();
    }

    private void IniciarDialogo()
    {
        List<string> dialogoCompleto = new List<string>();
        int total = Mathf.Max(lineasPadre.Count, lineasNPC.Count);

        for (int i = 0; i < total; i++)
        {
            if (i < lineasPadre.Count)
                dialogoCompleto.Add($"<color=#e0aa3e>{lineasPadre[i]}</color>");

            if (i < lineasNPC.Count)
                dialogoCompleto.Add(lineasNPC[i]);
        }

        TextManager.Instance.MostrarDialogo(dialogoCompleto);

        StartCoroutine(FinalizarDialogoDespues());
    }

    private IEnumerator FinalizarDialogoDespues()
    {
        while (TextManager.Instance.EstaMostrando())
            yield return null;

        pantallaFade?.FadeIn(() =>
        {
            if (controlDiaNoche != null)
            {
                controlDiaNoche.ContadorNPCs += 1;
            }

            gameObject.SetActive(false);
            pantallaFade.FadeOut();
        });
    }
}






