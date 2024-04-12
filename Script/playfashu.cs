using UnityEngine;

public class playfashu : MonoBehaviour
{
    public GameObject[] prefabsToSpawn; // 要生成的预制体数组
    public float spawnInterval = 2f; // 预制体生成的间隔时间（秒）
    public GameObject targetObject; // 指定的目标对象
    public float moveSpeed = 5f; // 预制体移动的速度

    private float nextSpawnTime; // 下一次生成预制体的时间

    void Update()
    {
        // 检查是否到达生成时间
        if (Time.time >= nextSpawnTime)
        {
            SpawnPrefab();
            nextSpawnTime = Time.time + spawnInterval; // 更新下一次生成预制体的时间
        }
    }

    void SpawnPrefab()
    {
        // 从预制体数组中随机选择一个预制体来生成
        int prefabIndex = Random.Range(0, prefabsToSpawn.Length);
        GameObject prefabToSpawn = prefabsToSpawn[prefabIndex];

        // 生成预制体
        GameObject spawnedObject = Instantiate(prefabToSpawn, transform.position, Quaternion.identity);

        // 计算移动方向为从生成的预制体指向目标对象
        Vector3 moveDirection = (targetObject.transform.position - spawnedObject.transform.position).normalized;
        // 检查生成的预制体是否为空
        if (spawnedObject != null)
        {
            // 给生成的预制体添加移动行为
            spawnedObject.AddComponent<Mover>().Initialize(moveDirection, moveSpeed);
        }
    }
}

public class Mover : MonoBehaviour
{
    private Vector2 moveDirection;
    private float moveSpeed;

    public void Initialize(Vector2 direction, float speed)
    {
        moveDirection = direction;
        moveSpeed = speed;
    }

    void Update()
    {
        // 移动预制体
        transform.position += (Vector3)(moveDirection * moveSpeed * Time.deltaTime);
    }
}
