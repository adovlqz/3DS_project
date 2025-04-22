using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;  // El objeto que la cámara debe seguir (MODEL_character)
    public float height = 20.0f;  // Altura de la cámara sobre el objetivo
    public float tiltAngle = -60.0f;  // Ángulo de inclinación (tilt) de la cámara
    public bool isRotating = false;  // Para habilitar o deshabilitar la rotación automática

    private void Start()
    {
        if (target == null)
        {
            Debug.LogError("No se ha asignado un target a la cámara.");
            return;
        }

        // Inicializamos la cámara en una posición correcta al lado del objetivo
        UpdateCameraPosition();
    }

    void LateUpdate()
    {
        if (target == null)
        {
            return;
        }

        // Si la cámara está rotando (por TriggerRotation), no actualices la posición
        if (isRotating)
        {
            return;  // No hacer nada, dejar que TriggerRotation controle la cámara
        }

        // Si no estamos en el trigger, seguimos la posición del personaje
        UpdateCameraPosition();
    }


    private void UpdateCameraPosition()
    {
        // Obtenemos la posición del objetivo
        Vector3 targetPosition = target.position;

        // Calculamos la posición de la cámara detrás del personaje usando su dirección hacia adelante
        Vector3 desiredPosition = targetPosition - target.TransformDirection(Vector3.forward) * 10.0f;

        // Ajustamos la altura de la cámara (en el eje Y)
        desiredPosition.y = height;  // La altura está fija en el valor de "height"

        // Aplicamos la inclinación (tilt) en el eje X
        Quaternion tiltRotation = Quaternion.Euler(tiltAngle, 0, 0);
        desiredPosition = tiltRotation * (desiredPosition - target.position) + target.position;

        transform.position = desiredPosition;  // Asignamos la nueva posición de la cámara

        // La cámara siempre mira al objetivo
        transform.LookAt(target);  // Asegura que la cámara siempre mire al objetivo
    }
}
