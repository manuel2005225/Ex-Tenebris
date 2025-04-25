using UnityEngine;
using TMPro;

public class DeathTouch : MonoBehaviour
{
    [TextArea(2, 4)]
    public string mensajeConsejo = "Consejo genérico de muerte.";

    private bool VerificacionAlumbrado;
    public EnemigoConLuz enemigoConLuz;
    public GameObject MenuInventario;
    

    public GameObject MenuMuerte;
    [Header("Pantalla de Muerte")]
public TextMeshProUGUI textoConsejoUI;
    

    // Update is called once per frame
    void Update()
    {
        
        
       VerificacionAlumbrado = enemigoConLuz.enLuz;
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        

        if(collision.CompareTag("Player") && VerificacionAlumbrado == true){
            Debug.Log("wahahaha");
            MenuMuerte.SetActive(true);
            Time.timeScale = 0f;
            MenuInventario.SetActive(false);
            if (textoConsejoUI != null)
        {
        textoConsejoUI.text = mensajeConsejo;
        }


            
        }
    }
}
