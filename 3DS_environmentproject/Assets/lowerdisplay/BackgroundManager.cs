using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    public GameObject canvasLeft;    // Asignar el Canvas del árbol izquierdo
    public GameObject canvasRight;   // Asignar el Canvas del árbol derecho
    public GameObject canvasCloudBg; // Asignar el Canvas de fondo (nubes)

    private TreeSlide leftTreeSlide;
    private TreeSlide rightTreeSlide;

    void Start()
    {
        // Obtener las referencias al script TreeSlide de los árboles
        leftTreeSlide = canvasLeft.GetComponent<TreeSlide>();
        rightTreeSlide = canvasRight.GetComponent<TreeSlide>();
    }

    // Función que activa el árbol izquierdo y desactiva el fondo
    public void ShowLeftTree()
    {
        canvasLeft.SetActive(true);       // Activar el Canvas del árbol izquierdo
        canvasCloudBg.SetActive(true);    // Asegurarnos de que el fondo siga activo
        leftTreeSlide.StartSliding();     // Iniciar la animación de deslizamiento
    }

    // Función que activa el árbol derecho y desactiva el fondo
    public void ShowRightTree()
    {
        canvasRight.SetActive(true);      // Activar el Canvas del árbol derecho
        canvasCloudBg.SetActive(true);    // Asegurarnos de que el fondo siga activo
        rightTreeSlide.StartSliding();    // Iniciar la animación de deslizamiento
    }

    // Función para regresar al fondo de las nubes
    public void ReturnToCloudBg()
    {
        canvasLeft.SetActive(false);     // Desactivar los árboles
        canvasRight.SetActive(false);
        canvasCloudBg.SetActive(true);   // Asegurarnos de que el fondo esté activo
    }
}
