using UnityEngine;

public class ActivadorGuia : MonoBehaviour
{


    public GameObject MenuGuia;
    public GameObject BotonIniciar;
    public GameObject BotonGuia;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void ActivarGuia(){

        MenuGuia.SetActive(true);
        BotonIniciar.SetActive(false);
        BotonGuia.SetActive(false); 
    }

    // Update is called once per frame
    
}
