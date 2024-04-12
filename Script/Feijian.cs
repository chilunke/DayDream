using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Feijian : MonoBehaviour
{
    public GameObject[] prefabsToSpawn; // 要生成的预制体数组
    public Transform spawnPoint; // 预制体生成的固定位置
    public float moveSpeed = 5f; // 预制体移动的速度
    public float spawnInterval = 2f; // 预制体生成的间隔时间（秒）
    public float detectionRadius = 5f; // 检测范围的半径

    private List<GameObject> spawnedObjects = new List<GameObject>(); // 存储所有生成的预制体
    private float nextSpawnTime; // 下一次生成预制体的时间

    public AudioClip feijian;
    private AudioSource playfeijian;

    void Start()
    {
        // 获取 AudioSource 组件
        playfeijian = GetComponent<AudioSource>();
    }

    void Update()
    {
        // 检测范围内是否有带有Player标签的游戏对象
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Player"))
            {
                // 如果有带有Player标签的游戏对象在检测范围内且达到生成时间，则生成预制体
                if (Time.time >= nextSpawnTime)
                {
                    SpawnPrefab();
                    nextSpawnTime = Time.time + spawnInterval; // 更新下一次生成预制体的时间
                }
                break; // 找到Player标签的游戏对象后不再继续检查
            }
        }

        // 遍历所有生成的预制体并更新它们的位置
        for (int i = spawnedObjects.Count - 1; i >= 0; i--)
        {
            if (spawnedObjects[i] != null)
            {
                // 向左移动预制体
                spawnedObjects[i].transform.position += Vector3.left * moveSpeed * Time.deltaTime;
            }
            else
            {
                // 如果预制体已经被销毁，则从列表中移除
                spawnedObjects.RemoveAt(i);
            }
        }
    
}

    void SpawnPrefab()
    {
        // 从预制体数组中随机选择一个预制体来生成
        int prefabIndex = Random.Range(0, prefabsToSpawn.Length);
        GameObject prefabToSpawn = prefabsToSpawn[prefabIndex];

        // 生成预制体
        GameObject spawnedObject = Instantiate(prefabToSpawn, spawnPoint.position, Quaternion.identity);
        // 播放攻击音效
        if (feijian != null)
        {
            playfeijian.PlayOneShot(feijian);
        }
        spawnedObjects.Add(spawnedObject); // 将生成的预制体添加到列表中
    }

    // 绘制检测范围的可视化辅助线
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
