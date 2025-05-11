using System.Collections.Generic;
using UnityEngine;

public class NPCDialogo : MonoBehaviour, IInteractuable
{
    [Header("Fases del NPC")]
    public List<FaseDialogoSO> fases;
    private int faseActual = 0;

    private NPCMovimiento movimiento;

    void Start()
    {
        movimiento = GetComponent<NPCMovimiento>();
    }

    public void Interactuar()
    {
        if (faseActual >= fases.Count) return;

        var fase = fases[faseActual];
        if (fase.requiereCondicion && !CondicionCumplida(fase.idCondicion))
            return;

        List<string> paginas = new();
        for (int i = 0; i < fase.lineas.Count; i++)
        {
            string texto = fase.lineas[i];
            if (i < fase.pistas.Count && fase.pistas[i])
                texto = $"<color=red>{texto}</color>";

            paginas.Add(texto);
        }

        TextManager.Instance.MostrarDialogo(paginas);

    if (!string.IsNullOrEmpty(fase.nombreDestinoPostDialogo) && movimiento != null)
    {
        GameObject destinoGO = GameObject.Find(fase.nombreDestinoPostDialogo);

        if (destinoGO != null)
        {
            movimiento.IrADestino(destinoGO.transform, fase.quedarseEnDestino);
        }
        else
        {
            Debug.LogWarning($"[NPCDialogo] No se encontró el destino '{fase.nombreDestinoPostDialogo}' en la escena.");
        }
    }

    }

    public void AvanzarFase()
    {
        if (faseActual + 1 < fases.Count)
        {
            faseActual++;
            if (!fases[faseActual].quedarseEnDestino && movimiento != null)
                movimiento.ReanudarPatrullaje();
        }
    }

    private bool CondicionCumplida(string id)
    {
        return true;
    }
}



