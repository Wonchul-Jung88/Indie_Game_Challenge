using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public float rotateSpeed = 50f; // ��]�X�s�[�h��ݒ肵�܂��i�ύX�\�j

    void Update()
    {
        // Y���𒆐S�ɉ�]�����܂�
        transform.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime);
    }
}
