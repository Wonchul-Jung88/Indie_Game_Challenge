using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public float rotateSpeed = 50f; // 回転スピードを設定します（変更可能）

    void Update()
    {
        // Y軸を中心に回転させます
        transform.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime);
    }
}
