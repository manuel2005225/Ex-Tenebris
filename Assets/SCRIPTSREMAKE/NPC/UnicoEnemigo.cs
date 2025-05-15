using Pathfinding;
using UnityEngine;

public class EnemyFollowPlayer : MonoBehaviour
{
    public Transform player;                  // Referencia al jugador
    private AIPath aiPath;                    // Componente AIPath que controla el movimiento

    public PantallaFade pantallaFade;        // Referencia al script de fade

    void Start()
    {
        aiPath = GetComponent<AIPath>();
        if (aiPath == null) Debug.LogError("Falta componente AIPath");

        if (pantallaFade == null)
            Debug.LogWarning("No se asignó PantallaFade en EnemyFollowPlayer");
    }

    void Update()
    {
        if (player != null && aiPath != null)
        {
            aiPath.destination = player.position;
            aiPath.SearchPath();
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Colisión con el jugador detectada.");

            if (pantallaFade != null)
            {
                pantallaFade.FadeIn();  // Solo fade in, sin fade out
                TextManager.Instance.MostrarMensaje("La ira no fue lo que consumio a Alessandro. Fue la Violencia que esta trae...", 5f);
                TextManager.Instance.MostrarMensaje("Una violencia que es mas grande que el peso que pueden soportar tus hombros...", 5f);
                TextManager.Instance.MostrarMensaje("Lo que te presiona no es la violencia de Alessandro hacia otros, si no su violencia ante Dios.", 5f);
            }
        }
    }
}


