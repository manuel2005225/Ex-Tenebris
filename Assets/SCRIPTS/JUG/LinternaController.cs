using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;

public class LinternaController : MonoBehaviour
{
    public GameObject LuzArea;
    public GameObject LuzLinterna;

    public float aceite = 100f;
    public float aceiteMax = 100f;

    public Image barraAceite; // Arrástralo desde el Inspector (Barra > fill)

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

        // Consumo de aceite
        if (LuzArea.activeSelf)
        {
            aceite -= 0.5f * Time.deltaTime;
        }
        else if (LuzLinterna.activeSelf)
        {
            aceite -= 1.0f * Time.deltaTime;
        }

        if (aceite <= 0)
        {
            aceite = 0;
            LuzArea.SetActive(false);
            LuzLinterna.SetActive(false);
        }

        // Actualiza la barra
        if (barraAceite != null)
        {
            barraAceite.fillAmount = aceite / aceiteMax;
        }
    }
}
