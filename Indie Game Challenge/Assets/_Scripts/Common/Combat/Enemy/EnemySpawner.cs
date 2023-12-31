using JojikYT;
using System;
using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Serializable]
    public class Range
    {
        public int Start;
        public int End;

        public Range(int v1, int v2)
        {
            Start = v1;
            End = v2;
        }
    }

    public GameObject enemyPrefab;
    int xPos;
    int zPos;
    Range enemyCountRange = new Range(3,10);
    int enemyCount;
    int maxEnemyCount;

    // Start is called before the first frame update
    void Start()
    {
        enemyCount = 0;
        maxEnemyCount = UnityEngine.Random.Range(enemyCountRange.Start, enemyCountRange.End + 1); // +1を追加して最大値も含める
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (enemyCount < maxEnemyCount)
        {
            xPos = UnityEngine.Random.Range(-10, 10);
            zPos = UnityEngine.Random.Range(-10, 10);

            // Instantiate the enemy as a child of this object
            GameObject enemy = Instantiate(enemyPrefab, transform);
            enemy.transform.localPosition = new Vector3(xPos, 0, zPos); // Set local position

            enemy.GetComponent<EnemyAITutorial>().spawner = this; // 生成した敵にこのEnemySpawnerを参照として渡す
            yield return new WaitForSeconds(5);
            enemyCount += 1;
        }
    }


    public void DecrementEnemyCount() // 敵が破壊された時にこの関数を呼ぶ
    {
        enemyCount -= 1;
    }
}
