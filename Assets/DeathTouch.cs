using UnityEngine;

public class DeathTouch : MonoBehaviour
{

    private bool VerificacionAlumbrado;
    public EnemigoConLuz enemigoConLuz;
    public GameObject MenuInventario;
    

    public GameObject MenuMuerte;
    

    // Update is called once per frame
    void Update()
    {
        
        
       VerificacionAlumbrado = enemigoConLuz.enLuz;
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if(VerificacionAlumbrado == true){
            Debug.Log("wahahaha");
            MenuMuerte.SetActive(true);
            Time.timeScale = 0f;
            MenuInventario.SetActive(false);

            
        }
    }
}
