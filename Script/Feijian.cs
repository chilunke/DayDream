using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Feijian : MonoBehaviour
{
    public GameObject[] prefabsToSpawn; // Ҫ���ɵ�Ԥ��������
    public Transform spawnPoint; // Ԥ�������ɵĹ̶�λ��
    public float moveSpeed = 5f; // Ԥ�����ƶ����ٶ�
    public float spawnInterval = 2f; // Ԥ�������ɵļ��ʱ�䣨�룩
    public float detectionRadius = 5f; // ��ⷶΧ�İ뾶

    private List<GameObject> spawnedObjects = new List<GameObject>(); // �洢�������ɵ�Ԥ����
    private float nextSpawnTime; // ��һ������Ԥ�����ʱ��

    public AudioClip feijian;
    private AudioSource playfeijian;

    void Start()
    {
        // ��ȡ AudioSource ���
        playfeijian = GetComponent<AudioSource>();
    }

    void Update()
    {
        // ��ⷶΧ���Ƿ��д���Player��ǩ����Ϸ����
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Player"))
            {
                // ����д���Player��ǩ����Ϸ�����ڼ�ⷶΧ���Ҵﵽ����ʱ�䣬������Ԥ����
                if (Time.time >= nextSpawnTime)
                {
                    SpawnPrefab();
                    nextSpawnTime = Time.time + spawnInterval; // ������һ������Ԥ�����ʱ��
                }
                break; // �ҵ�Player��ǩ����Ϸ������ټ������
            }
        }

        // �����������ɵ�Ԥ���岢�������ǵ�λ��
        for (int i = spawnedObjects.Count - 1; i >= 0; i--)
        {
            if (spawnedObjects[i] != null)
            {
                // �����ƶ�Ԥ����
                spawnedObjects[i].transform.position += Vector3.left * moveSpeed * Time.deltaTime;
            }
            else
            {
                // ���Ԥ�����Ѿ������٣�����б����Ƴ�
                spawnedObjects.RemoveAt(i);
            }
        }
    
}

    void SpawnPrefab()
    {
        // ��Ԥ�������������ѡ��һ��Ԥ����������
        int prefabIndex = Random.Range(0, prefabsToSpawn.Length);
        GameObject prefabToSpawn = prefabsToSpawn[prefabIndex];

        // ����Ԥ����
        GameObject spawnedObject = Instantiate(prefabToSpawn, spawnPoint.position, Quaternion.identity);
        // ���Ź�����Ч
        if (feijian != null)
        {
            playfeijian.PlayOneShot(feijian);
        }
        spawnedObjects.Add(spawnedObject); // �����ɵ�Ԥ������ӵ��б���
    }

    // ���Ƽ�ⷶΧ�Ŀ��ӻ�������
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
