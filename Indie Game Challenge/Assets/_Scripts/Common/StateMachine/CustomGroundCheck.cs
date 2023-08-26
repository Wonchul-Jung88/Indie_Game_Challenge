using UnityEngine;

public class CustomGroundCheck : MonoBehaviour
{
    public float groundCheckDistance = 0.1f;  // 地面判定のRayの距離
    public float defaultTolerance = 0.05f;  // デフォルトの許容量（Tolerance）
    public float jumpTolerance = 0.0f;  // ジャンプ時の許容量

    private CharacterController characterController;
    private bool isGrounded;
    private float tolerance;
    private float lastHeight;
    public bool IsGrounded { get { return isGrounded; } }

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        tolerance = defaultTolerance;  // 初期許容量をセット
        lastHeight = transform.position.y;
    }

    void FixedUpdate()
    {
        CheckIfGrounded();
    }

    void CheckIfGrounded()
    {
        // 現在の高さをチェック
        float currentHeight = transform.position.y;

        // ジャンプ（高度が急激に変更）を検出
        if (Mathf.Abs(currentHeight - lastHeight) > tolerance)
        {
            tolerance = jumpTolerance;
        }
        else if (isGrounded)
        {
            // 地面に接触していれば、許容量を元に戻す
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

        // 現在の高さを保存
        lastHeight = currentHeight;
    }
}
