using UnityEngine;

public class TreeSlide : MonoBehaviour
{
    public Vector2 startPosition;  // Posición inicial fuera de la pantalla
    public Vector2 endPosition;    // Posición final dentro de la pantalla
    public float speed = 400f;     // Velocidad de deslizamiento

    private RectTransform rt;
    private bool isSliding = false;
    private bool slideIn = true;   // Controla si la animación es para entrar o salir

    void Start()
    {
        rt = GetComponent<RectTransform>();
        rt.anchoredPosition = startPosition;
    }

    // Función que inicia la animación de deslizamiento (entrando)
    public void StartSliding()
    {
        isSliding = true;
        slideIn = true;
    }

    // Función que inicia la animación de deslizamiento (saliendo)
    public void StartSlidingOut()
    {
        isSliding = true;
        slideIn = false;
    }

    void Update()
    {
        if (!isSliding) return;

        // Deslizar hacia adentro o hacia afuera dependiendo de la dirección
        if (slideIn)
        {
            rt.anchoredPosition = Vector2.MoveTowards(rt.anchoredPosition, endPosition, speed * Time.deltaTime);
        }
        else
        {
            rt.anchoredPosition = Vector2.MoveTowards(rt.anchoredPosition, startPosition, speed * Time.deltaTime);
        }

        // Si ha llegado a la posición objetivo, detener la animación
        if (Vector2.Distance(rt.anchoredPosition, (slideIn ? endPosition : startPosition)) < 1f)
        {
            isSliding = false;
        }
    }
}
