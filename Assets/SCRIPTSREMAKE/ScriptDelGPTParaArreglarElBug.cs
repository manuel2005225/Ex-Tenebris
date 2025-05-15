using UnityEngine;

public class OrdenRenderizado : MonoBehaviour
{
    public Transform baseReferencia;  // Transform en la base del sprite (pies)

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (baseReferencia == null)
            baseReferencia = transform;  // Por defecto usa el transform principal
    }

    void LateUpdate()
    {
        spriteRenderer.sortingOrder = Mathf.RoundToInt(-baseReferencia.position.y * 100);
    }
}
