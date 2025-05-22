using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ControlObjetivos : MonoBehaviour
{
    public RawImage rawImage; // Asigna tu RawImage desde el inspector

    public float slideDistance = 200f; // Distancia a deslizar (en píxeles)
    public float slideDuration = 0.2f; // Duración del deslizamiento (en segundos)

    private bool estaAbajo = false;
    private Coroutine corutinaActual = null;
    private Vector2 posicionArriba;
    private Vector2 posicionAbajo;

    // Start is called before the first frame update
    void Start()
    {
        posicionArriba = rawImage.rectTransform.anchoredPosition;
        posicionAbajo = posicionArriba + Vector2.down * slideDistance;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (corutinaActual != null)
                StopCoroutine(corutinaActual);

            if (!estaAbajo)
                corutinaActual = StartCoroutine(Deslizar(posicionAbajo));
            else
                corutinaActual = StartCoroutine(Deslizar(posicionArriba));

            estaAbajo = !estaAbajo;
        }
    }

    private IEnumerator Deslizar(Vector2 destino)
    {
        RectTransform rt = rawImage.rectTransform;
        Vector2 startPos = rt.anchoredPosition;
        float elapsed = 0f;

        while (elapsed < slideDuration)
        {
            rt.anchoredPosition = Vector2.Lerp(startPos, destino, elapsed / slideDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        rt.anchoredPosition = destino;
    }
}
