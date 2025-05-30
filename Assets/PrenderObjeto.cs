using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PrenderObjeto : MonoBehaviour, IInteractuable
{
    public bool PrimeraVez = true;
    public bool tendradialogo = false;
    private Light2D luz;

    private void Start()
    {
        luz = GetComponent<Light2D>();
    }

    public void Interactuar()
    {
        if (PrimeraVez && tendradialogo)
        {
            PrimeraVez = false;
            luz.enabled = !luz.enabled;
            TextManager.Instance.MostrarMensajeAvanzable("Parece prenderse y apagarse segun lo toco...");
        }
        else
        {
            luz.enabled = !luz.enabled;
        }
        
        

    }
}
