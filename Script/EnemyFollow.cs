using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemyFollow : MonoBehaviour
{
    public float moveSpeed = 8f; // ���˵��ƶ��ٶ�
    public float detectionRadius = 20f; // ��ⷶΧ�뾶

    private Transform player; // ��ɫ��Transform���
    private bool isFollowing = false; // ��־�Ƿ�����ɫ

    public int maxHealth = 100; // ���˵����Ѫ��
    private int currentHealth; // ��ǰѪ��

    public AudioClip dieSound; // ������Ч
    private AudioSource audioSource; // ����������

    // ���˵�Sprite Renderer����
    private SpriteRenderer spriteRenderer;

    // ��˸��ɫ��ԭʼ��ɫ
    private Color originalColor;
    public Color flashColor = Color.red;

    // ��˸����ʱ��ͼ�ʱ��
    public float flashDuration = 0.1f;
    private float flashTimer;

    // �Ƿ�������˸
    private bool isFlashing = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // ���Ҵ��� "Player" ��ǩ�Ľ�ɫ����

        currentHealth = maxHealth; // ��ʼ����ǰѪ��Ϊ���Ѫ��

        // ��ȡ AudioSource ���
        audioSource = GetComponent<AudioSource>();

        // ��ʼ����ƶ�
        StartCoroutine(RandomMovement());
    }

    void Awake()
    {
        // ��ȡSprite Renderer���
        spriteRenderer = GetComponent<SpriteRenderer>();
        // ����ԭʼ��ɫ
        originalColor = spriteRenderer.color;
    }



    // �����ܵ��˺�ʱ���õķ���
    public void TakeDamage(int damageAmount)
    {

        currentHealth -= damageAmount; // ����Ѫ��

        // �����⵽����������Flash����
        Flash();

        // ���Ѫ��С�ڵ����㣬���������߼�
        if (currentHealth <= 0)
        {
            // ����die��Ч
            if (dieSound != null)
            {
                audioSource.PlayOneShot(dieSound);
            }
            Die(); // ���������߼�
        }
    }

    // ��������ʱ���õķ���
    void Die()
    {
        // ��������Բ�������������������Ч��

       
        // Ȼ�����ٵ��˶���
        Destroy(gameObject);
    }

    void Update()
    {
        // �����ɫ�ڼ�ⷶΧ���ҵ���û�п�ʼ�����ɫ
        if (!isFollowing && player != null && Vector2.Distance(transform.position, player.position) <= detectionRadius)
        {
            isFollowing = true; // ��ʼ�����ɫ
        }

        // ����Ѿ���ʼ�����ɫ
        if (isFollowing && player != null)
        {
            // ������˳����ɫ�ķ���
            Vector2 direction = player.position - transform.position;
            direction.Normalize();

            // ���ݽ�ɫ���ƶ�����������˵ĳ���
            if (direction.x < 0)
            {
                transform.localScale = new Vector3(1, 1, 1); // �����ƶ�ʱ����ת���˵ĳ���Ϊ��
            }
            else if (direction.x > 0)
            {
                transform.localScale = new Vector3(-1, 1, 1); // �����ƶ�ʱ����ת���˵ĳ���Ϊ��
            }

            // �ƶ����˳����ɫ
            transform.Translate(direction * moveSpeed * Time.deltaTime);
            Debug.Log("���ϰ�����");
        }
        // ���������˸�����¼�ʱ��
        if (isFlashing)
        {
            // ���¼�ʱ��
            flashTimer -= Time.deltaTime;
            if (flashTimer <= 0)
            {
                // ֹͣ��˸���ָ�ԭʼ��ɫ
                isFlashing = false;
                spriteRenderer.color = originalColor;
            }
        }

    }

    // �������������ʹ������˸
    public void Flash()
    {
        // ������˸��ɫ
        spriteRenderer.color = flashColor;
        // ���ü�ʱ��
        flashTimer = flashDuration;
        // ��ʼ��˸
        isFlashing = true;
    }

    IEnumerator RandomMovement()
    {
        while (true)
        {
            // ���ѡ������λ�������ƶ�
            Vector3 targetPosition1 = new Vector3(Random.Range(-100, 101), Random.Range(-100, 101), 0);
            Vector3 targetPosition2 = new Vector3(Random.Range(-100, 101), Random.Range(-100, 101), 0);


            // �ƶ�����һ��Ŀ��λ��
            yield return MoveToTarget(targetPosition1);

            // �ƶ����ڶ���Ŀ��λ��
            yield return MoveToTarget(targetPosition2);
            if (isFollowing && player != null)
            {
                Debug.Log("û������ƶ��˰ɣ�����");
                break;
            }
        }
    }

    IEnumerator MoveToTarget(Vector3 targetPosition)
    {
        while (Vector3.Distance(transform.position, targetPosition) > 0.1f) // ʹ��һ�����ݲ����ж��Ƿ񵽴�Ŀ��λ��
        {
            // �����ƶ�����
            Vector3 direction = (targetPosition - transform.position).normalized;

            // �ƶ�����
            transform.Translate(direction * moveSpeed * Time.deltaTime);

            yield return null; // �ȴ�һ֡
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // �����ײ�����Ƿ���TilemapCollider2D���
        if (collision.collider is TilemapCollider2D)
        {
            // ���ٵ�ǰ��Ԥ����
            Destroy(gameObject);
        }
    }

    // ���Ƽ�ⷶΧ�Ŀ��ӻ�������
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
