using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSkill : MonoBehaviour
{
    public GameObject ballPrefab; // �{�[���̃v���n�u���C���X�y�N�^����ݒ�
    public float orbitRadius = 2.0f; // �q���O���̔��a
    public float cooldownTime = 5.0f; // �X�L���̃N�[���_�E�����ԁi�b�j
    public float orbitSpeed = 50f; // �{�[���̋O�����x
    public int maxBallsBase = 5; // �x�[�X�ƂȂ�{�[���̍ő吔
    public int level = 1; // ���x���ɂ��{�[���̍ő吔��������d�g�݂̂��߂̃��x���ϐ�

    private CharacterController controller; // Character Controller�R���|�[�l���g
    private List<GameObject> balls = new List<GameObject>(); // �������ꂽ�{�[�����i�[���郊�X�g
    private float lastSkillTime; // �Ō�ɃX�L�����g�p��������

    void Start()
    {
        lastSkillTime = -cooldownTime; // ������Ԃł͒����ɃX�L�����g�p�ł���悤�ɂ���
        controller = GetComponent<CharacterController>(); // Character Controller�R���|�[�l���g���擾
    }

    void Update()
    {
        // �{�[���̍ő吔���v�Z�i���x���ɉ����đ�������z��j
        int maxBalls = maxBallsBase + level;

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (Time.time - lastSkillTime >= cooldownTime)
            {
                if (balls.Count < maxBalls)
                {
                    Vector3 spawnPosition = transform.position + new Vector3(0, controller.center.y, 0); // Center��Y�̒l���l�������X�|�[���ʒu
                    GameObject ball = Instantiate(ballPrefab, spawnPosition, Quaternion.identity);
                    balls.Add(ball);
                    lastSkillTime = Time.time;

                    //Debug.Log("Ball created. Current ball count: " + balls.Count);
                }
                else
                {
                    //Debug.Log("Reached the maximum number of balls. Current max: " + maxBalls);
                }
            }
            else
            {
                //Debug.Log("Skill is cooling down. Remaining time: " + (cooldownTime - (Time.time - lastSkillTime)) + " seconds");
            }
        }

        // �{�[�����L�����N�^�[�̎���ɓ��Ԋu�ɔz�u���A�������]������
        for (int i = 0; i < balls.Count; i++)
        {
            if (balls[i] != null)
            {
                float angle = (360f / balls.Count * i) + (Time.time * orbitSpeed); // �������ꂽ�e�{�[���̊p�x�i���Ԋu�ɂȂ�悤�ɕ���������Ŏ��Ԍo�߂ɂ���]�����Z�j
                Vector3 pos = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), 0, Mathf.Sin(angle * Mathf.Deg2Rad)) * orbitRadius; // �V�����{�[���̈ʒu
                pos.y = controller.center.y; // ����������CharacterController��Center��Y�̒l�ɂ���
                balls[i].transform.position = transform.position + pos;
            }
        }
    }

    public void NotifyBallDestroyed(GameObject ball)
    {
        if (balls.Contains(ball))
        {
            balls.Remove(ball);
            //Debug.Log("Ball destroyed. Remaining ball count: " + balls.Count);
        }
    }

}
