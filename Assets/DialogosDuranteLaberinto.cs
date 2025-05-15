using UnityEngine;
using UnityEngine.UI;

public class DialogosDuranteLaberinto : MonoBehaviour
{
    public string mensaje;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnTriggerEnter2D(Collider2D collision)
    {
        TextManager.Instance.BloquearInput(false);
        TextManager.Instance.MostrarMensaje("<color=#D3D3D3>(Mientras caminas escuchas una voz a lo lejos.)</color>", 2f);

        TextManager.Instance.MostrarMensaje("<color=#DC143C>¡¡MALDITO HIJO DE PERRA!! ¡¡AHORA ME TOMARAS ENSERIO!!</color>", 2f);

        TextManager.Instance.MostrarMensaje("<color=#D3D3D3>(¿Es un susurro? ¿O es tu mente?)</color>", 2f);
        

    }
}
