using UnityEngine;
using TMPro;

public class DeathTouchFollower : MonoBehaviour
{
    [TextArea(2, 4)]
    public string mensajeConsejo = "Consejo genérico de muerte.";
    
    public GameObject MenuInventario;
    

    public GameObject MenuMuerte;

    [Header("Pantalla de Muerte")]
public TextMeshProUGUI textoConsejoUI;
    

    // Update is called once per frame
    void Update()
    {
        
        
       
    }


    void OnTriggerEnter2D(Collider2D collision)
    {

         if(collision.CompareTag("Player")){
            if (textoConsejoUI != null)
    {
    textoConsejoUI.text = mensajeConsejo;
    }

        
            Debug.Log("wahahaha");
            MenuMuerte.SetActive(true);
            Time.timeScale = 0f;
            MenuInventario.SetActive(false);

            Destroy(gameObject);

            
        
    }
         }
        
}