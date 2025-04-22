using UnityEngine;

public class CharacterAnimationController : MonoBehaviour
{
    private Animator CharacterAnimator;

    private void Start()
    {
        CharacterAnimator = GetComponent<Animator>();

        if (CharacterAnimator == null)
        {
            Debug.LogError("❌ Animator no encontrado en " + gameObject.name);
        }
        else
        {
            Debug.Log("✅ Animator asignado correctamente.");
        }
    }

    public void UpdateAnimation(bool isWalking)
    {
        if (CharacterAnimator != null)
        {
            // Usar índice de capa 0 si no hay capas adicionales
            //int layerIndex = 0;

            // Establecer el parámetro 'IsWalking' en el Animator
            CharacterAnimator.SetBool("IsWalking", isWalking);

            // Obtener el valor del parámetro 'Speed' basado en si el personaje está caminando o no
            float speed = isWalking ? 1f : 0f;  // Si camina, speed = 1, si no, speed = 0
            CharacterAnimator.SetFloat("Speed", speed);

            // Agregar Debug.Log para mostrar el valor del parámetro 'Speed'
            Debug.Log("Valor del parámetro 'Speed': " + speed);

            // Controlar la animación según el estado
            if (isWalking)
            {
                CharacterAnimator.CrossFade("walkanim", 0.1f); // Cambiar animación de caminar
            }
            else
            {
                CharacterAnimator.CrossFade("idleanim", 0.1f); // Cambiar animación de inactividad
            }
        }
    }
}
