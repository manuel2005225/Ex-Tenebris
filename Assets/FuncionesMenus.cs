using UnityEngine;
using UnityEngine.SceneManagement;
public class FuncionesMenus : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void BotonReiniciarEscena(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Debug.Log("ola");
    }
}
