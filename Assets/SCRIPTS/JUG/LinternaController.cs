using UnityEngine;
using UnityEngine.Rendering.Universal;


public class LinternaController : MonoBehaviour
{
    public GameObject LuzArea;
    public GameObject LuzLinterna;

    public float aceite = 100f;

    void Update()
    {
        // Cambio de luces al presionar F
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (LuzArea.activeSelf)
            {
                LuzArea.SetActive(false);
                LuzLinterna.SetActive(true);
            }
            else if (LuzLinterna.activeSelf)
            {
                LuzLinterna.SetActive(false);
            }
            else
            {
                LuzArea.SetActive(true);
            }
        }

        // Consumo de aceite basado en la luz activa
        if (LuzArea.activeSelf)
        {
            aceite -= 0.5f * Time.deltaTime; // Consumo constante por segundo
            if (aceite <= 0)
            {
                aceite = 0;
                LuzArea.SetActive(false); // Apagar luz si se queda sin aceite
            }
        }
        else if (LuzLinterna.activeSelf)
        {
            aceite -= 1.0f * Time.deltaTime; // Consumo constante por segundo
            if (aceite <= 0)
            {
                aceite = 0;
                LuzLinterna.SetActive(false); // Apagar luz si se queda sin aceite
            }
        }
    }
}
