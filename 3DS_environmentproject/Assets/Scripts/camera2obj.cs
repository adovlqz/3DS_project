using UnityEngine;

public class camera2obj : MonoBehaviour
{
    public Transform target; // Referencia al cubo
    public Vector3 offset = new Vector3(0, 5, -10); // Ajusta la posición de la cámara
    public float smoothSpeed = 0.125f; // Velocidad de suavizado del movimiento de la cámara

    void Start()
    {
        // Si no se asignó un target, lo asignamos automáticamente al cubo en la escena
        if (target == null)
        {
            target = GameObject.Find("MODEL_character").transform; 
        }
    }


    void LateUpdate()
    {
        if (target != null)
        {
            // Calcula la posición deseada de la cámara con el offset
            Vector3 desiredPosition = target.position + offset;

            // Suaviza el movimiento de la cámara
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            // Actualiza la posición de la cámara
            transform.position = smoothedPosition;

            // Opcional: Para que la cámara siempre mire al cubo
            transform.LookAt(target);
        }
    }
}