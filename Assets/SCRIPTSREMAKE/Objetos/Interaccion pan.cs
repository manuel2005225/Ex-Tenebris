using UnityEngine;
using System.Collections;

public class Interaccionpan : MonoBehaviour, IInteractuable
{
    public GameObject pared;

    public void Interactuar()
    {
        pared.SetActive(false);
        Destroy(gameObject);

        TextManager.Instance.BloquearInput(true);
        MostrarMensajes();
    }

    private void MostrarMensajes()
    {
        Debug.Log("Inicio MostrarMensajes");
        TextManager.Instance.MostrarMensajeAvanzable(
            "<color=#e0aa3e>(Un pan mohoso.... esta lleno de larvas)</color>",
            () => {
                Debug.Log("Mostrando segundo mensaje");
                TextManager.Instance.MostrarMensajeAvanzable(
                    "<color=#DC143C>¿PAN PARA EL ALMA? ¿O GUSANOS PARA EL VIENTRE…?</color>",
                    () => {
                        Debug.Log("Fin MostrarMensajes");
                        TextManager.Instance.BloquearInput(false);
                    }
                );
            }
        );
    }
}

