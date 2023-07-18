using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSkill : MonoBehaviour
{
    public GameObject ballPrefab;
    public float orbitRadius = 2.0f;
    public float cooldownTime = 5.0f;
    public float orbitSpeed = 50f;
    public int maxBallsBase = 5;
    public int level = 1;

    private CharacterController controller;
    private List<GameObject> balls = new List<GameObject>();
    private float lastSkillTime;

    void Start()
    {
        lastSkillTime = -cooldownTime;
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        
        int maxBalls = maxBallsBase + level;

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (Time.time - lastSkillTime >= cooldownTime)
            {
                if (balls.Count < maxBalls)
                {
                    Vector3 spawnPosition = transform.position + new Vector3(0, controller.center.y, 0);
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

       
        for (int i = 0; i < balls.Count; i++)
        {
            if (balls[i] != null)
            {
                float angle = (360f / balls.Count * i) + (Time.time * orbitSpeed);
                Vector3 pos = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), 0, Mathf.Sin(angle * Mathf.Deg2Rad)) * orbitRadius;
                pos.y = controller.center.y;
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
