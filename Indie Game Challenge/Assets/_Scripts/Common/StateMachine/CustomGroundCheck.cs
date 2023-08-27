using UnityEngine;

public class CustomGroundCheck : MonoBehaviour
{
    public float groundCheckDistance = 0.1f;
    public float defaultTolerance = 0.05f;
    public float jumpTolerance = 0.0f;

    private CharacterController characterController;
    private bool isGrounded;
    private float tolerance;
    private float lastHeight;
    public bool IsGrounded { get { return isGrounded; } }

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        tolerance = defaultTolerance;
        lastHeight = transform.position.y;
    }

    void FixedUpdate()
    {
        CheckIfGrounded();
    }

    void CheckIfGrounded()
    {
        float currentHeight = transform.position.y;

        if (Mathf.Abs(currentHeight - lastHeight) > tolerance)
        {
            tolerance = jumpTolerance;
        }
        else if (isGrounded)
        {
            tolerance = defaultTolerance;
        }

        Vector3 rayOrigin = transform.position + characterController.center;
        if (Physics.Raycast(rayOrigin, Vector3.down, groundCheckDistance + tolerance))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
        lastHeight = currentHeight;
    }
}
