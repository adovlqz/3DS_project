using UnityEngine;

public class shadow : MonoBehaviour
{
    public Transform target;  // El personaje al que seguirá la sombra
    public float offsetY = 0.1f; // Altura de la sombra para evitar clipping con el suelo

    void Update()
    {
        if (target == null)
            return;

        RaycastHit hit;
        // Lanza el rayo hacia abajo para detectar el suelo
        if (Physics.Raycast(target.position, Vector3.down, out hit, Mathf.Infinity))
        {
            // Visualiza el rayo para depurar
            Debug.DrawRay(target.position, Vector3.down * 10, Color.red);

            // Mueve la sombra a la posición del suelo
            transform.position = hit.point + Vector3.up * offsetY;
        }
    }
}