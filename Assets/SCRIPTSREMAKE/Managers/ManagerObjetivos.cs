using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Scir : MonoBehaviour
{


    public TMP_Text TextoObjetivo;
    public ControlDiaNocheR Cama;
    public InteraccionCrowbar InteraccionCrowbar;

    public ActivadorObjeto ActivadorObjeto;
    public IniciarFinal IniciarFinal;

    public bool IntCrowbar;
    private int AvanzadorObjetivos = 0;

    private int ContNPCS = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    // Update is called once per frame
    void Update()
    {
        ContNPCS = Cama.ContadorNPCs;
        IntCrowbar = InteraccionCrowbar.ultimoobjeto;

        switch (AvanzadorObjetivos)
        {
            case 0:
                TextoObjetivo.text = "Habla con los pecadores";

                // Código para el objetivo 0
                if (ContNPCS == 3)
                {
                    AvanzadorObjetivos = 1;



                }


                break;
            case 1:
                // Código para el objetivo 1
                TextoObjetivo.text = "Ve a la cama";
                if (Cama.ValorDiayNoche == false)
                {
                    AvanzadorObjetivos = 2;

                }

                break;
            case 2:
                TextoObjetivo.text = "Investiga alrededor";

                if (IntCrowbar == true)
                {

                    AvanzadorObjetivos = 3;
                }




                // Código para el objetivo 2
                break;
            case 3:
                TextoObjetivo.color = new Color(1f, 0f, 0f);
                TextoObjetivo.text = "Entra.";

                if (ActivadorObjeto.ActivadoBloqueo == true)
                {
                    AvanzadorObjetivos = 4;
                }


                break;
            case 4:

                TextoObjetivo.text = "Explora el laberinto";
                // Código para el objetivo 3
                if (IniciarFinal.enemigoActivado == true)
                {
                    AvanzadorObjetivos = 5;
                }
                break;
            case 5:
                TextoObjetivo.text = "Escapa del laberinto";
                // Código para el objetivo 4


                break;
            default:
                // Código para otros casos
                break;
        }
    }
}
