using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs; // 不同种类的敌人预制件数组
    public Transform[] spawnPoints; // 生成敌人的位置
    public float spawnInterval = 5f; // 生成敌人的间隔时间

    void Start()
    {
        // 启动协程来定期生成敌人
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            // 在四个指定的位置生成不同种类的敌人
            for (int i = 0; i < spawnPoints.Length; i++)
            {
                // 随机选择一个敌人预制件
                GameObject enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

                // 在当前位置生成敌人
                GameObject newEnemy = Instantiate(enemyPrefab, spawnPoints[i].position, Quaternion.identity);

              
            }

            // 等待一段时间后再次生成敌人
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
