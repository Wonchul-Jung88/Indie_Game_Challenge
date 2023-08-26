using UnityEngine;

public class CustomGroundCheck : MonoBehaviour
{
    public float groundCheckDistance = 0.1f;  // �n�ʔ����Ray�̋���
    public float defaultTolerance = 0.05f;  // �f�t�H���g�̋��e�ʁiTolerance�j
    public float jumpTolerance = 0.0f;  // �W�����v���̋��e��

    private CharacterController characterController;
    private bool isGrounded;
    private float tolerance;
    private float lastHeight;
    public bool IsGrounded { get { return isGrounded; } }

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        tolerance = defaultTolerance;  // �������e�ʂ��Z�b�g
        lastHeight = transform.position.y;
    }

    void FixedUpdate()
    {
        CheckIfGrounded();
    }

    void CheckIfGrounded()
    {
        // ���݂̍������`�F�b�N
        float currentHeight = transform.position.y;

        // �W�����v�i���x���}���ɕύX�j�����o
        if (Mathf.Abs(currentHeight - lastHeight) > tolerance)
        {
            tolerance = jumpTolerance;
        }
        else if (isGrounded)
        {
            // �n�ʂɐڐG���Ă���΁A���e�ʂ����ɖ߂�
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

        // ���݂̍�����ۑ�
        lastHeight = currentHeight;
    }
}
