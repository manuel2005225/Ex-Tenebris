using UnityEngine;
using UnityEngine.SceneManagement;
public class FuncionesMenus : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void BotonReiniciarEscena(){
        NPCDialogueHandler[] npcs = FindObjectsOfType<NPCDialogueHandler>();
        foreach (var npc in npcs)
        {
            npc.DialogoCompletado = 0;
        }
        
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Debug.Log("ola");
    }
}
