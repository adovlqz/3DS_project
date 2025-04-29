using UnityEngine;

public class RightTreeManager : MonoBehaviour
{
    public GameObject canvasRight;   // Asignar el Canvas del árbol derecho
    public GameObject canvasCloudBg; // Asignar el Canvas de fondo (nubes)
    private TreeSlide rightTreeSlide;

    void Start()
    {
        // Obtener la referencia al script TreeSlide
        rightTreeSlide = canvasRight.GetComponent<TreeSlide>();
    }

    // Función que regresa al fondo de las nubes
    public void ReturnToCloudBg()
    {
        canvasRight.SetActive(false);     // Desactivar el Canvas del árbol derecho
        canvasCloudBg.SetActive(true);    // Activar el Canvas de fondo
    }

    // Función que hace que el árbol derecho se deslice fuera de la pantalla
    public void SlideRightTreeOut()
    {
        rightTreeSlide.StartSlidingOut(); // Iniciar la animación para salir
    }
}
