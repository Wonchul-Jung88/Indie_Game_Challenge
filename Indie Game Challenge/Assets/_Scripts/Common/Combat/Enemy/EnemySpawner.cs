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
        maxEnemyCount = UnityEngine.Random.Range(enemyCountRange.Start, enemyCountRange.End + 1); // +1‚ğ’Ç‰Á‚µ‚ÄÅ‘å’l‚àŠÜ‚ß‚é
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

            enemy.GetComponent<EnemyAITutorial>().spawner = this; // ¶¬‚µ‚½“G‚É‚±‚ÌEnemySpawner‚ğQÆ‚Æ‚µ‚Ä“n‚·
            yield return new WaitForSeconds(5);
            enemyCount += 1;
        }
    }


    public void DecrementEnemyCount() // “G‚ª”j‰ó‚³‚ê‚½‚É‚±‚ÌŠÖ”‚ğŒÄ‚Ô
    {
        enemyCount -= 1;
    }
}
