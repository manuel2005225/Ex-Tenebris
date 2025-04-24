using UnityEngine;

public class VolverAlInicio : MonoBehaviour
{

    public GameObject BotonGuia;
    public GameObject BotonInicio;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            BotonGuia.SetActive(true);
            BotonInicio.SetActive(true);
            gameObject.SetActive(false);

        }
    }
}
