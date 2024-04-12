using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs; // ��ͬ����ĵ���Ԥ�Ƽ�����
    public Transform[] spawnPoints; // ���ɵ��˵�λ��
    public float spawnInterval = 5f; // ���ɵ��˵ļ��ʱ��

    void Start()
    {
        // ����Э�����������ɵ���
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            // ���ĸ�ָ����λ�����ɲ�ͬ����ĵ���
            for (int i = 0; i < spawnPoints.Length; i++)
            {
                // ���ѡ��һ������Ԥ�Ƽ�
                GameObject enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

                // �ڵ�ǰλ�����ɵ���
                GameObject newEnemy = Instantiate(enemyPrefab, spawnPoints[i].position, Quaternion.identity);

              
            }

            // �ȴ�һ��ʱ����ٴ����ɵ���
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
