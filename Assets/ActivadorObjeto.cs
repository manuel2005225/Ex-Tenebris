using UnityEngine;

public class ActivadorObjeto : MonoBehaviour
{   
    public GameObject objetoActivar;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            objetoActivar.SetActive(true);
        }
    }
}
