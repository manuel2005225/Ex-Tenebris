using UnityEngine;

public class PanEstereoPorOidos : MonoBehaviour
{
    public Transform oidos; // Empty object con la orientación del jugador
    public AudioSource audioSource;

    void Start()
    {
        audioSource.spatialBlend = 0.9f; // Mezcla entre 2D y 3D
    }

    void Update()
    {
        if (!audioSource.isPlaying) return;

        Vector2 dirSonido = (transform.position - oidos.position).normalized;
        Vector2 direccionOidos = oidos.right.normalized;

        float angulo = Vector2.SignedAngle(direccionOidos, dirSonido);
        float pan = Mathf.Clamp(angulo / 90f, -1f, 1f);

        Debug.Log("Pan calculado: " + pan);
        audioSource.panStereo = pan;
    }
}
