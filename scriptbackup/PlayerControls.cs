using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public Transform cameraTransform;  // Referencia a la cámara
    public float moveSpeed = 5f;       // Velocidad de movimiento del jugador

    private bool isInTrigger = false;  // Si el jugador está dentro del trigger
    private Vector3 moveDirection;     // Dirección de movimiento del jugador

    void Update()
    {
        float horizontal = 0;
        float vertical = 0;

        if (isInTrigger)
        {
            // Dentro del trigger: Controles modificados
            Debug.Log("Dentro del trigger: Controles modificados.");

            // D mueve en Z positivo, A en Z negativo
            vertical = Input.GetKey(KeyCode.D) ? 1f : Input.GetKey(KeyCode.A) ? -1f : 0f;

            // W mueve en X negativo, S en X positivo
            horizontal = Input.GetKey(KeyCode.W) ? -1f : Input.GetKey(KeyCode.S) ? 1f : 0f;
        }
        else
        {
            // Fuera del trigger: Movimiento normal
            Debug.Log("Fuera del trigger: Controles normales.");

            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");
        }

        // Definir la dirección de movimiento con base en el mundo, no en la cámara
        moveDirection = cameraTransform.right * horizontal + cameraTransform.forward * vertical;
        moveDirection.y = 0; // No queremos que el jugador se mueva hacia arriba o abajo.

        // Mover al jugador
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);

        // Debug para verificar la dirección del movimiento
        Debug.Log("Movimiento - H: " + horizontal + ", V: " + vertical);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("TriggerZone")) // Asegúrate de que el tag sea correcto
        {
            isInTrigger = true;
            Debug.Log("Jugador ha entrado en el trigger.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("TriggerZone"))
        {
            isInTrigger = false;
            Debug.Log("Jugador ha salido del trigger.");
        }
    }
}
