using TMPro;
using UnityEngine;

public class Scir : MonoBehaviour
{


    public TextMeshPro TextoObjetivo;
    public ControlDiaNocheR ContadorNPCS;

    private int AvanzadorObjetivos = 0;

    private int ContNPCS = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    // Update is called once per frame
    void Update()
    {
        ContadorNPCS.ContadorNPCs = ContNPCS;

        switch (AvanzadorObjetivos)
        {
            case 0:
                // Código para el objetivo 0
                if (ContNPCS == 3)
                {
                    AvanzadorObjetivos = 1;
                    ContNPCS = 0;

                    TextoObjetivo.text = "Habla con los pecadores";

                }


                break;
            case 1:
                // Código para el objetivo 1
                break;
            case 2:
                // Código para el objetivo 2
                break;
            default:
                // Código para otros casos
                break;
        }
    }
}
