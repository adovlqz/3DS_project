using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;  // El personaje a seguir
    public float distance = 10.0f;  // Distancia detrás del personaje
    public float heightOffset = 0.0f; // Ajuste de altura en Y
    public float characterOriginY = 0.0f; // Corrige el origen del personaje en Y
    public float tiltAngle = -60.0f;  // Ángulo de inclinación en X

    private void Start()
    {
        if (target == null)
        {
            Debug.LogError("No se ha asignado un target a la cámara.");
            return;
        }
    }

    private void LateUpdate()
    {
        if (target != null)
        {
            UpdateCameraPosition();
        }
    }

    private void UpdateCameraPosition()
    {
        // Ajuste del origen del personaje en Y
        Vector3 characterAdjustedPosition = target.position + new Vector3(0, characterOriginY, 0);

        // Calcular la nueva posición relativa al personaje
        Vector3 offset = new Vector3(0, heightOffset, -distance);
        Vector3 desiredPosition = characterAdjustedPosition + Quaternion.Euler(tiltAngle, 0, 0) * offset;

        // Mover la cámara suavemente hacia la posición deseada
        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * 5f);

        // Hacer que la cámara mire al personaje
        transform.LookAt(characterAdjustedPosition);
    }
}
