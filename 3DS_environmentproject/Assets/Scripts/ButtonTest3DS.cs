using UnityEngine;

public class ButtonTest3DS : MonoBehaviour
{
    void Update()
    {
        // Gatillos
        if (Input.GetKeyDown(KeyCode.JoystickButton4)) // L
        {
            Debug.Log("Gatillo L presionado");
        }
        if (Input.GetKeyDown(KeyCode.JoystickButton5)) // R
        {
            Debug.Log("Gatillo R presionado");
        }

        // Botones frontales
        if (Input.GetKeyDown(KeyCode.JoystickButton0)) // A
        {
            Debug.Log("Botón A presionado");
        }
        if (Input.GetKeyDown(KeyCode.JoystickButton1)) // B
        {
            Debug.Log("Botón B presionado");
        }
        if (Input.GetKeyDown(KeyCode.JoystickButton2)) // X
        {
            Debug.Log("Botón X presionado");
        }
        if (Input.GetKeyDown(KeyCode.JoystickButton3)) // Y
        {
            Debug.Log("Botón Y presionado");
        }
    }
}
