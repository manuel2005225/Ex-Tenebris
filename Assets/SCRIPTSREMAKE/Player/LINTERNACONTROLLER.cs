using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LINTERNACONTROLLER : MonoBehaviour
{


    public Light2D linterna;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        //Activador y desactivador de la linterna wahahaha
        if(Input.GetKeyDown(KeyCode.F))
        {

            LinternaUsar();
        }
    }

    void LinternaUsar()
    {
        if (linterna.enabled == true)
        {
            linterna.enabled = false;
            
        }
        else
        {
            linterna.enabled = true;
        }
    }
}
