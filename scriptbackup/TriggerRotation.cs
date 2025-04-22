using UnityEngine;

public class TriggerRotation : MonoBehaviour
{
    public Transform cameraTransform;  // El transform de la cámara que será rotada
    public Transform target;  // El objeto que la cámara debe seguir (MODEL_character)
    public float rotationAngle = 90f; // Ángulo de rotación
    public float rotationSpeed = 2f; // Velocidad de la rotación (no se usará más para la rotación)
    private CameraFollow cameraFollowScript;  // Referencia al script CameraFollow del objeto cámara

    private bool isRotating = false; // Para evitar activar la rotación varias veces
    private Vector3 initialCameraPosition; // Posición inicial de la cámara relativa al personaje

    public float zOffset = 0f; // Parámetro para ajustar la distancia de la cámara en el eje Z (ajustable)
    public float cameraOffsetX = 15f; // Parámetro para alejar la cámara en el eje X (ajustable)
    public float cameraRotationX = 35f; // Parámetro para controlar la rotación de la cámara en el eje X (ajustable)
    public float cameraHeight = 10f; // Parámetro para controlar la altura de la cámara sobre el objetivo (ajustable)

    private void Start()
    {
        // Obtener el script CameraFollow del objeto que contiene la cámara
        cameraFollowScript = cameraTransform.GetComponent<CameraFollow>();

        // Inicializar CameraFollow para que la cámara empiece a seguir al personaje desde el inicio
        cameraFollowScript.target = target;  // Aseguramos que la cámara esté siguiendo al target
        cameraFollowScript.isRotating = false;  // Aseguramos que la rotación automática esté habilitada

        // Guardamos la posición inicial de la cámara relativa al personaje
        initialCameraPosition = cameraTransform.position - target.position;

        // Ajustamos la distancia de la cámara en el eje Z
        initialCameraPosition.z = zOffset; // Aplicamos el offset Z que puede ser ajustado
    }

    private void OnTriggerEnter(Collider other)
    {
        // Depuración: Mostrar el nombre del objeto que entra en el trigger
        Debug.Log("OnTriggerEnter activado. Objeto que entra: " + other.gameObject.name);

        // Comprobamos si es el Player
        if (other.CompareTag("Player") && !isRotating)
        {
            isRotating = true;
            cameraFollowScript.isRotating = true;  // Deshabilitar la rotación automática
            Debug.Log("Trigger activado: La cámara comienza a rotar.");
            // Salta directamente a la nueva rotación
            RotateCameraInstantly();
        }
        else
        {
            Debug.Log("El objeto que entra no es el Player o ya está rotando.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Depuración: Mostrar el nombre del objeto que sale del trigger
        Debug.Log("OnTriggerExit activado. Objeto que sale: " + other.gameObject.name);

        if (other.CompareTag("Player"))
        {
            isRotating = false;
            cameraFollowScript.isRotating = false;  // Habilitar la rotación automática
            Debug.Log("Trigger desactivado: La cámara deja de rotar.");
        }
    }

    void RotateCameraInstantly()
    {
        // Rotación instantánea a la nueva orientación
        Quaternion targetRotation = cameraTransform.rotation * Quaternion.Euler(0, rotationAngle, 0);
        cameraTransform.rotation = targetRotation;

        // Depuración: Ver la rotación final
        Debug.Log("Rotación final: " + cameraTransform.rotation.eulerAngles);
    }

    void Update()
    {
        // Si la cámara está dentro del trigger, mantenemos su posición relativa
        if (isRotating)
        {
            // Aseguramos que la cámara mantenga su posición relativa al personaje
            cameraTransform.position = target.position + initialCameraPosition;

            // Ajustamos la cámara con el offset X
            cameraTransform.position += target.right * cameraOffsetX; // Desplazamos la cámara en el eje X

            // Aseguramos que la cámara esté completamente horizontal en el eje Y
            cameraTransform.rotation = Quaternion.Euler(cameraRotationX, cameraTransform.rotation.eulerAngles.y, 0);

            // Ajustamos la altura de la cámara (eje Y)
            cameraTransform.position = new Vector3(cameraTransform.position.x, cameraHeight, cameraTransform.position.z);
        }
    }
}
