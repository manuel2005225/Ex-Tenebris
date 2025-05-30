using UnityEngine;

public class InteraccionCrowbar : MonoBehaviour, IInteractuable
{
    public GameObject pared;
    public bool ultimoobjeto = false;

    public void Interactuar()
    {
        pared.SetActive(false);
        TextManager.Instance.BloquearInput(true);
        Destroy(gameObject);
        MostrarMensajes();
        ultimoobjeto = true;
    }

    private void MostrarMensajes()
    {
        Debug.Log("Inicio MostrarMensajes Crowbar");
        TextManager.Instance.MostrarMensajeAvanzable(
            "<color=#e0aa3e>(Una palanca....)</color>",
            () => {
                Debug.Log("Mostrando segundo mensaje Crowbar");
                TextManager.Instance.MostrarMensajeAvanzable(
                    "<color=#e0aa3e>(Parece oxidada)</color>",
                    () => {
                        Debug.Log("Fin MostrarMensajes Crowbar");
                        
                        TextManager.Instance.BloquearInput(false);
                        
                    }
                );
            }
        );
    }
}
