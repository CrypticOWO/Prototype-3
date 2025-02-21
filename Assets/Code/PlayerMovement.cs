using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControls : MonoBehaviour
{
    public Rigidbody RB;
    public Transform Player;
    public CapsuleCollider playerCollider;

    public static float Speed = 5f;
    public float JumpForce = 10f;

    public bool isGrounded;
    public int jumpCount = 0;

    public float gravityStrength = -1.5f;

    void Update()
    {
        NormalControls();

        Vector3 gravity = new Vector3(0, gravityStrength, 0);
        RB.AddForce(gravity, ForceMode.Acceleration);
    }

    void NormalControls()
    {
        RB.isKinematic = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, 0, verticalInput).normalized;

        if (direction.magnitude >= 0.1f)
        {
            Vector3 forward = Camera.main.transform.TransformDirection(Vector3.forward);
            forward.y = 0;
            Vector3 right = Camera.main.transform.TransformDirection(Vector3.right);

            Vector3 moveDirection = (forward * direction.z + right * direction.x).normalized;

            RB.velocity = new Vector3(moveDirection.x * Speed, RB.velocity.y, moveDirection.z * Speed);
        }
        else
        {
            RB.velocity = new Vector3(0, RB.velocity.y, 0);
        }
        
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            RB.velocity = new Vector3(RB.velocity.x, 0, RB.velocity.z);
            RB.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            isGrounded = true;
            jumpCount = 0;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            isGrounded = false;
            jumpCount = 1;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("DeadZone"))
        {
            SceneManager.LoadScene("SampleScene");
        }
    }
}