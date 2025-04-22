using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class movemetlogic : MonoBehaviour
{
    public float velocitymov = 2.4f;
    public float rotationSpeed = 5.0f;
    public float moveThreshold = 0.1f;
    public float maxSpeed = 5.0f;
    public float decelerationX = 5.0f;
    public float decelerationZ = 5.0f;
    public PhysicMaterial noFrictionMaterial;

    private Animator anim;
    private Vector2 currentInput;
    private Rigidbody rb;
    private Collider col;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        rb.useGravity = false;
        rb.isKinematic = false;
        rb.constraints = RigidbodyConstraints.FreezeRotation;

        if (noFrictionMaterial != null && col != null)
        {
            col.sharedMaterial = noFrictionMaterial;
        }
    }

    void Update()
    {
        float rawX = Input.GetAxisRaw("Horizontal");
        float rawZ = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(rawX, 0, rawZ).normalized;

        if (direction.magnitude >= moveThreshold)
        {
            Vector3 velocity = direction * velocitymov;

            if (velocity.magnitude > maxSpeed)
            {
                velocity = velocity.normalized * maxSpeed;
            }

            float targetVelX = Mathf.MoveTowards(rb.velocity.x, velocity.x, decelerationX * Time.deltaTime);
            float targetVelZ = Mathf.MoveTowards(rb.velocity.z, velocity.z, decelerationZ * Time.deltaTime);

            rb.velocity = new Vector3(targetVelX, rb.velocity.y, targetVelZ);

            if (direction.magnitude >= 0.1f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
        }
        else
        {
            rb.velocity = new Vector3(
                Mathf.MoveTowards(rb.velocity.x, 0, decelerationX * Time.deltaTime),
                rb.velocity.y,
                Mathf.MoveTowards(rb.velocity.z, 0, decelerationZ * Time.deltaTime)
            );
        }

        // ===== Ajuste de altura con Raycast para evitar que el personaje se hunda o flote =====
        RaycastHit hit;
        Vector3 rayOrigin = transform.position + Vector3.up * 0.1f;
        if (Physics.Raycast(rayOrigin, Vector3.down, out hit, 1f))
        {
            Vector3 pos = transform.position;
            pos.y = hit.point.y;
            transform.position = pos;
        }

        // Parámetro de velocidad para el Animator
        anim.SetFloat("Speed", direction.magnitude);

        // Debug
        Debug.Log("Movimiento X: " + rawX + " | Z: " + rawZ + " | Magnitud: " + direction.magnitude);
    }

    // Eliminamos la restricción artificial en Y, ya que ahora usamos raycast
    void OnCollisionStay(Collision collision)
    {
        Debug.Log("El personaje sigue en contacto con: " + collision.gameObject.name);
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Colisión detectada con: " + collision.gameObject.name);
    }

    void OnCollisionExit(Collision collision)
    {
        Debug.Log("El personaje ha dejado de colisionar con: " + collision.gameObject.name);
    }
}
