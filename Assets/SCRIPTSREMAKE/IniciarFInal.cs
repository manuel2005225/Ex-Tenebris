using System.Collections;
using UnityEngine;

public class IniciarFinal : MonoBehaviour
{
    public GameObject objetoAActivar;           // Objeto a activar al final
    public AudioSource audioSourceExterno;      // AudioSource que está en otro objeto
    public AudioClip audioFinal;                 // Clip a reproducir

    public GameObject LucesFinal;

    public GameObject bloqueoLaberinto; // Referencia al objeto que se va a activar
    public bool enemigoActivado = false;

    void OnTriggerEnter2D(Collider2D collision)
    {
        
        enemigoActivado = true;

        if (collision.gameObject.CompareTag("Player"))
        {
            transform.position = new Vector3(122323f, 0f, 0f);
            StartCoroutine(MostrarDialogos());
            InicioEscape();
        }
    }

    private IEnumerator MostrarDialogos()
    {
        TextManager.Instance.BloquearInput(false);

        yield return MostrarMensajeYEsperar("<color=#D3D3D3>(Sientes repulsion al entrar a la habitacion, algo profano habita aqui)</color>", 4f);
        yield return MostrarMensajeYEsperar("<color=#D3D3D3>(Una mezcla de emociones te invade, mientras tu cuerpo solo te pide huir)</color>", 4f);
        yield return MostrarMensajeYEsperar("<color=#DC143C>(No hay fe en este lugar...)</color>", 2f);
        yield return MostrarMensajeYEsperar("<color=#DC143C>(Lo profano te acecha, y deberias de haberlo evitado cuando podias.)</color>", 4f);
        yield return MostrarMensajeYEsperar("<color=#D3D3D3>(Escapa.)</color>", 2f);

        StartCoroutine(ActivarObjetoConAudio());
    }

    private IEnumerator MostrarMensajeYEsperar(string mensaje, float duracion)
    {
        TextManager.Instance.MostrarMensaje(mensaje, duracion);
        yield return new WaitForSeconds(duracion);
    }

    private IEnumerator ActivarObjetoConAudio()
    {
        if (objetoAActivar != null)
        {
            objetoAActivar.SetActive(true);
        }

        if (audioSourceExterno != null && audioFinal != null)
        {
            audioSourceExterno.PlayOneShot(audioFinal);
            yield return new WaitForSeconds(audioFinal.length);
        }
        else
        {
            Debug.LogWarning("AudioSource externo o clip de audio no asignados.");
        }
        gameObject.SetActive(false);
        // Desactiva este script para evitar reactivaciones
        Debug.Log("Objeto activado y audio reproducido después de los diálogos.");
    }


        private void InicioEscape()
    {
        bloqueoLaberinto.SetActive(false);
        LucesFinal.SetActive(true);
    }
}


