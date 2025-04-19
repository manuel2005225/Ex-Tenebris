using UnityEngine;
using UnityEngine.Rendering.Universal;

public class RoamerReDo : MonoBehaviour
{

    public ComportamientoPersonaje RoamerConfirmation;
    public GameObject RoamerSelect;
    public Light2D luzMundo;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(luzMundo.intensity<= 0 && RoamerConfirmation.DialogoCompletado == 1){
            RoamerSelect.SetActive(true);
        }else{
            RoamerSelect.SetActive(false);
        }
    }
}
