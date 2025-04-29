using UnityEngine;
// usando GamePad está comentado porque aún no lo tienes habilitado
// using UnityEngine.N3DS;

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

    private Vector3 direction;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        rb.useGravity = false;
        rb.isKinematic = false;

        if (noFrictionMaterial != null && col != null)
        {
            col.sharedMaterial = noFrictionMaterial;
        }
    }

    void Update()
    {
        // Simulación del Circle Pad (3DS) usando teclas del teclado
        float rawX = 0f;
        float rawZ = 0f;

        // Soporte para teclado numérico (como en la 3DS)
        if (Input.GetKey(KeyCode.Keypad4)) rawX = -1f;
        if (Input.GetKey(KeyCode.Keypad6)) rawX = 1f;
        if (Input.GetKey(KeyCode.Keypad8)) rawZ = 1f;
        if (Input.GetKey(KeyCode.Keypad2)) rawZ = -1f;

        // Alternativa para testear en PC con WASD
        if (Input.GetKey(KeyCode.A)) rawX = -1f;
        if (Input.GetKey(KeyCode.D)) rawX = 1f;
        if (Input.GetKey(KeyCode.W)) rawZ = 1f;
        if (Input.GetKey(KeyCode.S)) rawZ = -1f;

        // Umbral para ignorar entradas pequeñas
        if (Mathf.Abs(rawX) < 0.1f) rawX = 0f;
        if (Mathf.Abs(rawZ) < 0.1f) rawZ = 0f;

        Vector3 inputDirection = new Vector3(rawX, 0, rawZ).normalized;

        // Dirección según cámara
        Camera playerCamera = Camera.main;
        if (playerCamera != null)
        {
            Vector3 cameraForward = playerCamera.transform.forward;
            cameraForward.y = 0f;
            cameraForward.Normalize();

            Vector3 cameraRight = playerCamera.transform.right;
            cameraRight.y = 0f;
            cameraRight.Normalize();

            Vector3 movementDirection = cameraForward * rawZ + cameraRight * rawX;
            movementDirection.Normalize();

            direction = movementDirection;
        }
        else
        {
            direction = inputDirection;
        }

        if (anim != null)
            anim.SetFloat("Speed", direction.magnitude);
    }

    void FixedUpdate()
    {
        if (direction.magnitude >= moveThreshold)
        {
            Vector3 velocity = direction * velocitymov;

            if (velocity.magnitude > maxSpeed)
            {
                velocity = velocity.normalized * maxSpeed;
            }

            float targetVelX = Mathf.MoveTowards(rb.velocity.x, velocity.x, decelerationX * Time.fixedDeltaTime);
            float targetVelZ = Mathf.MoveTowards(rb.velocity.z, velocity.z, decelerationZ * Time.fixedDeltaTime);

            rb.velocity = new Vector3(targetVelX, rb.velocity.y, targetVelZ);

            if (direction.magnitude >= 0.1f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
            }
        }
        else
        {
            rb.velocity = new Vector3(
                Mathf.MoveTowards(rb.velocity.x, 0, decelerationX * Time.fixedDeltaTime),
                rb.velocity.y,
                Mathf.MoveTowards(rb.velocity.z, 0, decelerationZ * Time.fixedDeltaTime)
            );
        }

        // Ajuste de altura con Raycast
        RaycastHit hit;
        Vector3 rayOrigin = transform.position + Vector3.up * 0.1f;
        if (Physics.Raycast(rayOrigin, Vector3.down, out hit, 1f))
        {
            Vector3 pos = transform.position;
            pos.y = hit.point.y;
            transform.position = pos;
        }
    }
}
