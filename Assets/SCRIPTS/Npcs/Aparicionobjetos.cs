using UnityEngine;

public class NPCObjetoActivator : MonoBehaviour
{
    [Header("Referencias")]
    public GameObject npcObject;                     // GameObject del NPC
    public NPCDialogueHandler npcDialogo;            // Script del NPC con DialogoCompletado
    public ControlDiaNoche verificadorDiaNoche;      // Control día/noche
    public GameObject objetoActivable;               // Objeto que debe aparecer

    private bool yaActivado = false;
    private int valorGuardado = 0;                   // Último valor de DialogoCompletado
    private bool npcOcultado = false;

    void Update()
    {
        // 🔄 1. Si el NPC está activo, actualizamos su valor de diálogo
        if (npcObject != null && npcObject.activeSelf)
        {
            valorGuardado = npcDialogo.DialogoCompletado;
        }

        // 🌒 2. Si es de noche y el diálogo fue completado, activamos el objeto (solo una vez)
        if (!yaActivado && valorGuardado == 1 && !verificadorDiaNoche.ValorDiayNoche)
        {
            objetoActivable.SetActive(true);
            yaActivado = true;
        }

        // 🌚 3. Si es de noche y el NPC aún no ha sido ocultado, lo ocultamos
        if (!npcOcultado && !verificadorDiaNoche.ValorDiayNoche)
        {
            if (npcObject != null)
            {
                npcObject.SetActive(false);
                npcOcultado = true;
            }
        }

        // 🌞 4. Si es de día y el NPC estaba oculto, lo reactivamos
        if (npcOcultado && verificadorDiaNoche.ValorDiayNoche)
        {
            if (npcObject != null)
            {
                npcObject.SetActive(true);
                npcOcultado = false;
            }
        }
    }
}



