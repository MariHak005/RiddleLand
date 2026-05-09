using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveForce = 50f;
    public float maxSpeed = 6f;
    public float jumpForce = 8f;

    private Rigidbody rb;
    private bool isGrounded;
    private Vector3 moveVector;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 4f);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        moveVector = transform.forward * z + transform.right * x;
    }

    void FixedUpdate()
    {
        float speed = 5f;

        Vector3 v = rb.linearVelocity;

        rb.linearVelocity = new Vector3(
            moveVector.x * speed,
            v.y,
            moveVector.z * speed
        );
    }
}