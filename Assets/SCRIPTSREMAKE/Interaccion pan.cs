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
        TextManager.Instance.MostrarMensaje("<color=#e0aa3e></color>", 2f);
        yield return new WaitForSeconds(2f); // espera a que termine el primer mensaje

        TextManager.Instance.MostrarMensaje("<color=#DC143C>¿PAN PARA EL ALMA? ¿O GUSANOS PARA EL VIENTRE…?</color>", 2f);
        yield return new WaitForSeconds(2f);

        TextManager.Instance.BloquearInput(false); // desbloquea después de todo
    }
}

