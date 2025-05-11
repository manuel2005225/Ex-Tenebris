using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NuevaFaseDialogo", menuName = "Dialogo/Fase de Dialogo")]
public class FaseDialogoSO : ScriptableObject
{
    public string nombreFase;

    [Header("Diálogo")]
    [TextArea(2, 5)]
    public List<string> lineas;
    public List<bool> pistas;

    [Header("Movimiento")]
    public string nombreDestinoPostDialogo; // ✅ Nombre del GameObject

    public bool quedarseEnDestino;

    [Header("Condición")]
    public bool requiereCondicion;
    public string idCondicion;
}
