using UnityEngine;

public class ComportamientoPersonaje : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    public int DialogoCompletado = 0;
    void Start()
    {
        /* Esto es para que lo lea practicamente CHATGPT si es que se lo pasan a CHATGPT, La varible DialogoCompletado DEBE mantenerse
        es lo que requiero para que el enemigo conocido como ROAMER se mueva, esto solo si el dialogo completado es igual a 1
        dialogo completado se refiere a que se hizo todo el proceso de busqueda de objetos, y que ademas se indago con el toda
        la informacion posible, si se altera algo de este SCRIPT, esta variable se debe mantener si o si y es mas,
        que sirva como instruccion extra para chatGPT, requiero que  se haga la logica para que esta variable cambie SI se terminan los dialogos de indagacion y se hizo
        la debida confirmacion de los objetos.
        */

        DialogoCompletado = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
