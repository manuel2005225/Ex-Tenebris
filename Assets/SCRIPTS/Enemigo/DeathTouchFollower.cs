using UnityEngine;

public class DeathTouchFollower : MonoBehaviour
{

    
    public GameObject MenuInventario;
    

    public GameObject MenuMuerte;
    

    // Update is called once per frame
    void Update()
    {
        
        
       
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        
            Debug.Log("wahahaha");
            MenuMuerte.SetActive(true);
            Time.timeScale = 0f;
            MenuInventario.SetActive(false);

            
        
    }
}