using UnityEngine;

public class InteraccionCrowbar : MonoBehaviour, IInteractuable
{
    public GameObject pared;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Interactuar(){

        TextManager.Instance.BloquearInput(true);
        TextManager.Instance.MostrarMensaje("<color=#e0aa3e>(Una palanca....)</color>", 2f);
        TextManager.Instance.MostrarMensaje("<color=#e0aa3e>(Parece oxidada)</color>", 2f);

        Destroy(gameObject);
        pared.SetActive(false);
    }
}
