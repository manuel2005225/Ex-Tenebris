using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("Referencias")]
    public Transform player;  // Referencia al jugador

    [Header("Movimiento")]
    public float moveSpeed = 3f;
    public float roamRadius = 8f;             // Radio de patrullaje alrededor del jugador
    public float minDistanceFromPlayer = 3f;   // Distancia mínima respecto al jugador
    public float zPosition = -2f;              // Posición en Z fija para que el enemigo quede en frente del escenario

    [Header("Detección")]
    public float detectionTime = 1f; // Tiempo que debe estar iluminado para comenzar a perseguir
    public float waitTime = 2f;      // Tiempo sin luz para volver a patrullar

    private bool isChasing = false;
    private float lightTimer = 0f;
    private float idleTimer = 0f;
    private Vector3 targetPosition;
    private float timeToChangeDirection = 0f;
    
    // Contador para saber cuántos colliders con el tag "PlayerLight" están solapando al enemigo.
    private int lightCounter = 0;

    void Start()
    {
        SetNewTargetPosition();
    }

    void Update()
    {
        // Detección basada en si hay alguna luz (cualquiera de las linternas) superpuesta.
        if (isChasing)
        {
            // Si ya está persiguiendo pero ya no hay ninguna luz...
            if (lightCounter <= 0)
            {
                idleTimer += Time.deltaTime;
                if (idleTimer >= waitTime)
                {
                    // Después de 2 segundos sin luz, vuelve a patrullar.
                    isChasing = false;
                    lightTimer = 0f;
                    idleTimer = 0f;
                    SetNewTargetPosition();
                }
            }
            else
            {
                idleTimer = 0f;
            }
        }
        else
        {
            // Si no está persiguiendo y hay alguna luz, acumula tiempo de iluminación.
            if (lightCounter > 0)
            {
                lightTimer += Time.deltaTime;
                if (lightTimer >= detectionTime)
                {
                    isChasing = true;
                    idleTimer = 0f;
                }
            }
            else
            {
                lightTimer = 0f;
            }
        }

        // Movimiento del enemigo
        if (isChasing)
            ChasePlayer();
        else
            RoamAroundPlayer();
    }

    private void RoamAroundPlayer()
    {
        // Si el enemigo está cerca de su destino o se cumplió el tiempo para cambiar de dirección, genera una nueva posición
        if (Vector3.Distance(new Vector3(transform.position.x, transform.position.y, 0), targetPosition) < 0.5f ||
            Time.time >= timeToChangeDirection)
        {
            SetNewTargetPosition();
        }

        Vector3 currentPos = new Vector3(transform.position.x, transform.position.y, zPosition);
        Vector3 destination = new Vector3(targetPosition.x, targetPosition.y, zPosition);
        transform.position = Vector3.MoveTowards(currentPos, destination, moveSpeed * Time.deltaTime);
    }

    private void SetNewTargetPosition()
    {
        // Genera una posición aleatoria en un círculo alrededor del jugador
        float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
        Vector3 newTarget = new Vector3(player.position.x, player.position.y, 0) +
                            new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * roamRadius;

        // Asegura que el destino no esté demasiado cerca del jugador
        if (Vector3.Distance(newTarget, player.position) < minDistanceFromPlayer)
        {
            newTarget = new Vector3(player.position.x, player.position.y, 0) +
                        new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * (minDistanceFromPlayer + 1f);
        }

        targetPosition = newTarget;
        timeToChangeDirection = Time.time + Random.Range(3f, 6f); // Cambia dirección cada 3-6 segundos
    }

    private void ChasePlayer()
    {
        // Persigue al jugador manteniendo la posición en Z
        Vector3 currentPos = new Vector3(transform.position.x, transform.position.y, zPosition);
        Vector3 destination = new Vector3(player.position.x, player.position.y, zPosition);
        transform.position = Vector3.MoveTowards(currentPos, destination, moveSpeed * Time.deltaTime);
    }

    // Cuando un collider con el tag "PlayerLight" entra, se incrementa el contador.
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerLight"))
        {
            lightCounter++;
        }
    }

    // Al salir, se decrementa el contador (asegurándose de no bajar de 0).
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("PlayerLight"))
        {
            lightCounter = Mathf.Max(0, lightCounter - 1);
        }
    }
}

