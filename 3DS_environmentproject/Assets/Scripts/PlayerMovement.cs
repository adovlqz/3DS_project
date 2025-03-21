using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 15f;  // Velocidad del personaje

    void Update()
    {
        // Captura las entradas de las teclas de dirección o WASD
        float moveHorizontal = Input.GetAxis("Horizontal")*1;
        float moveVertical = Input.GetAxis("Vertical")*-1;

        // Crea un vector de movimiento en base a las entradas
        Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0) * moveSpeed * Time.deltaTime;

        // Aplica el movimiento al personaje
        transform.Translate(movement);
    }
}