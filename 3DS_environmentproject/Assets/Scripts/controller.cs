using UnityEngine;

public class controller : MonoBehaviour
{
    public float moveSpeed = 5f;  // Velocidad de movimiento
    public float rotationSpeed = 10f;  // Velocidad de rotación

    private Rigidbody rb;
    private Vector3 movementDirection;
    private CharacterAnimationController animationController; // Referencia a CharacterAnimationController

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animationController = GetComponentInChildren<CharacterAnimationController>(); // Buscar el script de animación

        if (animationController == null)
        {
            Debug.LogError("❌ CharacterAnimationController no encontrado.");
        }
    }

    private void Update()
    {
        // Obtener entradas de movimiento
        float moveX = Input.GetAxis("Horizontal");  // A/D, flechas izquierda/derecha
        float moveZ = Input.GetAxis("Vertical");    // W/S, flechas arriba/abajo

        movementDirection = new Vector3(moveX, 0, moveZ).normalized;

        // Si el personaje se mueve, rota en esa dirección
        if (movementDirection.magnitude >= 0.1f)
        {
            RotateCharacter();
            animationController.UpdateAnimation(true); // Llamar a la animación de caminar
        }
        else
        {
            animationController.UpdateAnimation(false); // Llamar a la animación de inactividad
        }

        // Mover el personaje
        MoveCharacter();
    }

    private void MoveCharacter()
    {
        rb.MovePosition(transform.position + movementDirection * moveSpeed * Time.deltaTime);
    }

    private void RotateCharacter()
    {
        Quaternion targetRotation = Quaternion.LookRotation(movementDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
