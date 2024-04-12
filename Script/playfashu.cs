using UnityEngine;

public class playfashu : MonoBehaviour
{
    public GameObject[] prefabsToSpawn; // Ҫ���ɵ�Ԥ��������
    public float spawnInterval = 2f; // Ԥ�������ɵļ��ʱ�䣨�룩
    public GameObject targetObject; // ָ����Ŀ�����
    public float moveSpeed = 5f; // Ԥ�����ƶ����ٶ�

    private float nextSpawnTime; // ��һ������Ԥ�����ʱ��

    void Update()
    {
        // ����Ƿ񵽴�����ʱ��
        if (Time.time >= nextSpawnTime)
        {
            SpawnPrefab();
            nextSpawnTime = Time.time + spawnInterval; // ������һ������Ԥ�����ʱ��
        }
    }

    void SpawnPrefab()
    {
        // ��Ԥ�������������ѡ��һ��Ԥ����������
        int prefabIndex = Random.Range(0, prefabsToSpawn.Length);
        GameObject prefabToSpawn = prefabsToSpawn[prefabIndex];

        // ����Ԥ����
        GameObject spawnedObject = Instantiate(prefabToSpawn, transform.position, Quaternion.identity);

        // �����ƶ�����Ϊ�����ɵ�Ԥ����ָ��Ŀ�����
        Vector3 moveDirection = (targetObject.transform.position - spawnedObject.transform.position).normalized;
        // ������ɵ�Ԥ�����Ƿ�Ϊ��
        if (spawnedObject != null)
        {
            // �����ɵ�Ԥ��������ƶ���Ϊ
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
        // �ƶ�Ԥ����
        transform.position += (Vector3)(moveDirection * moveSpeed * Time.deltaTime);
    }
}
