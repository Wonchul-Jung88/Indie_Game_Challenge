using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSkill : MonoBehaviour
{
    public GameObject ballPrefab; // ボールのプレハブをインスペクタから設定
    public float orbitRadius = 2.0f; // 衛星軌道の半径
    public float cooldownTime = 5.0f; // スキルのクールダウン時間（秒）
    public float orbitSpeed = 50f; // ボールの軌道速度
    public int maxBallsBase = 5; // ベースとなるボールの最大数
    public int level = 1; // レベルによりボールの最大数が増える仕組みのためのレベル変数

    private CharacterController controller; // Character Controllerコンポーネント
    private List<GameObject> balls = new List<GameObject>(); // 生成されたボールを格納するリスト
    private float lastSkillTime; // 最後にスキルを使用した時間

    void Start()
    {
        lastSkillTime = -cooldownTime; // 初期状態では直ちにスキルを使用できるようにする
        controller = GetComponent<CharacterController>(); // Character Controllerコンポーネントを取得
    }

    void Update()
    {
        // ボールの最大数を計算（レベルに応じて増加する想定）
        int maxBalls = maxBallsBase + level;

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (Time.time - lastSkillTime >= cooldownTime)
            {
                if (balls.Count < maxBalls)
                {
                    Vector3 spawnPosition = transform.position + new Vector3(0, controller.center.y, 0); // CenterのYの値を考慮したスポーン位置
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

        // ボールをキャラクターの周りに等間隔に配置し、それを回転させる
        for (int i = 0; i < balls.Count; i++)
        {
            if (balls[i] != null)
            {
                float angle = (360f / balls.Count * i) + (Time.time * orbitSpeed); // 生成された各ボールの角度（等間隔になるように分割した上で時間経過による回転を加算）
                Vector3 pos = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), 0, Mathf.Sin(angle * Mathf.Deg2Rad)) * orbitRadius; // 新しいボールの位置
                pos.y = controller.center.y; // 生成高さをCharacterControllerのCenterのYの値にする
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
