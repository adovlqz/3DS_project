using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;  // El personaje a seguir
    public float distance = 10.0f;
    public float heightOffset = 0.0f;
    public float characterOriginY = 0.0f;
    public float tiltAngle = -60.0f;

    private float currentRotationY = 0f;
    private float targetRotationY = 0f;
    public float rotationSpeed = 360f;  // Grados por segundo

    private Rigidbody targetRigidbody;  // Rigidbody del personaje

    private void Start()
    {
        if (target == null)
        {
            Debug.LogError("No se ha asignado un target a la cámara.");
        }

        targetRigidbody = target.GetComponent<Rigidbody>();  // Obtener el Rigidbody del personaje
    }

    private void LateUpdate()
    {
        if (target != null)
        {
            HandleInput();
            UpdateCameraPosition();
            //UpdatePlayerMovement();
        }
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.JoystickButton4)) // L
        {
            targetRotationY -= 90f;
        }
        if (Input.GetKeyDown(KeyCode.JoystickButton5)) // R
        {
            targetRotationY += 90f;
        }
    }

    private void UpdateCameraPosition()
    {
        // Ajuste del origen del personaje en Y
        Vector3 characterAdjustedPosition = target.position + new Vector3(0, characterOriginY, 0);

        // Interpolamos la rotación actual hacia la rotación objetivo (gira suavemente)
        currentRotationY = Mathf.MoveTowardsAngle(currentRotationY, targetRotationY, rotationSpeed * Time.deltaTime);

        // Aplicamos la rotación en Y
        Quaternion rotation = Quaternion.Euler(tiltAngle, currentRotationY, 0);
        Vector3 offset = rotation * new Vector3(0, heightOffset, -distance);

        Vector3 desiredPosition = characterAdjustedPosition + offset;

        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * 15f);

        // Hacer que la cámara mire al personaje
        transform.LookAt(characterAdjustedPosition);
    }

    // Actualizamos el movimiento del jugador en función de la rotación de la cámara
    /* private void UpdatePlayerMovement()
     {
         // Obtenemos el input del joystick
         float horizontal = Input.GetAxis("Horizontal");
         float vertical = Input.GetAxis("Vertical");

         // Convertimos ese input a un movimiento relativo a la cámara
         Vector3 moveDirection = new Vector3(horizontal, 0, vertical).normalized;

         // Si hay dirección de movimiento, movemos el personaje
         if (moveDirection.magnitude >= 0.1f)
         {
             // Alineamos el movimiento con la rotación de la cámara.
             Quaternion cameraRotation = Quaternion.Euler(0, currentRotationY, 0);
             moveDirection = cameraRotation * moveDirection;

             // Hacemos que el personaje rote para que su "frente" apunte hacia la dirección del movimiento
             target.rotation = Quaternion.LookRotation(moveDirection);

             // Aplicamos el movimiento al personaje usando Rigidbody (físico)
             targetRigidbody.MovePosition(target.position + moveDirection * Time.deltaTime * 5f);
         }
         else
         {
             // Si no hay movimiento, frenamos el personaje (esto depende del drag)
             targetRigidbody.velocity = Vector3.zero;
         }
     }*/
}