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
        StartCoroutine(MostrarMensajes());
    }

    private IEnumerator MostrarMensajes()
{
    Debug.Log("Inicio MostrarMensajes");
    TextManager.Instance.MostrarMensaje("<color=#e0aa3e>(Un pan mohoso.... esta lleno de larvas)</color>", 2f);
    

    Debug.Log("Mostrando segundo mensaje");
    TextManager.Instance.MostrarDialogoPausado("<color=#DC143C>¿PAN PARA EL ALMA? ¿O GUSANOS PARA EL VIENTRE…?</color>", 2f);
    yield return new WaitForSeconds(2f);

    Debug.Log("Fin MostrarMensajes");
    TextManager.Instance.BloquearInput(false);
    
}

}

