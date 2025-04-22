using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;       // Velocidad de movimiento del jugador
    private Vector3 moveDirection;     // Dirección de movimiento del jugador

    void Update()
    {
        float horizontal = 0;
        float vertical = 0;

        // Usamos Input.GetAxisRaw para una detección instantánea de las teclas.
        // Detectamos las teclas A, D (horizontal) y W, S (vertical)
        horizontal = Input.GetAxisRaw("Horizontal"); // Esto captura A/D o las flechas izquierda/derecha.
        vertical = Input.GetAxisRaw("Vertical");     // Esto captura W/S o las flechas arriba/abajo.

        // Verificamos si la dirección está calculada correctamente
        Debug.Log("Dirección calculada - H: " + horizontal + ", V: " + vertical);

        // Mover al jugador solo en el plano X-Z (sin movimiento en Y)
        moveDirection = new Vector3(horizontal, 0, vertical).normalized;

        // Aplicamos el movimiento al jugador
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);

        // Debug para verificar el movimiento
        Debug.Log("Movimiento aplicado al jugador.");
    }
}
