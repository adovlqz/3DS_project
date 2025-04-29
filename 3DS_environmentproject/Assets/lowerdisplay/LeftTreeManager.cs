using UnityEngine;

public class LeftTreeManager : MonoBehaviour
{
    public GameObject canvasLeft;    // Asignar el Canvas del árbol izquierdo
    public GameObject canvasCloudBg; // Asignar el Canvas de fondo (nubes)
    private TreeSlide leftTreeSlide;

    void Start()
    {
        // Obtener la referencia al script TreeSlide
        leftTreeSlide = canvasLeft.GetComponent<TreeSlide>();
    }

    // Función que regresa al fondo de las nubes
    public void ReturnToCloudBg()
    {
        canvasLeft.SetActive(false);     // Desactivar el Canvas del árbol izquierdo
        canvasCloudBg.SetActive(true);   // Activar el Canvas de fondo
    }

    // Función que hace que el árbol izquierdo se deslice fuera de la pantalla
    public void SlideLeftTreeOut()
    {
        leftTreeSlide.StartSlidingOut(); // Iniciar la animación para salir
    }
}
