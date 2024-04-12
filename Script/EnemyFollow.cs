using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemyFollow : MonoBehaviour
{
    public float moveSpeed = 8f; // 敌人的移动速度
    public float detectionRadius = 20f; // 检测范围半径

    private Transform player; // 角色的Transform组件
    private bool isFollowing = false; // 标志是否跟随角色

    public int maxHealth = 100; // 敌人的最大血量
    private int currentHealth; // 当前血量

    public AudioClip dieSound; // 攻击音效
    private AudioSource audioSource; // 声音播放器

    // 敌人的Sprite Renderer引用
    private SpriteRenderer spriteRenderer;

    // 闪烁颜色和原始颜色
    private Color originalColor;
    public Color flashColor = Color.red;

    // 闪烁持续时间和计时器
    public float flashDuration = 0.1f;
    private float flashTimer;

    // 是否正在闪烁
    private bool isFlashing = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // 查找带有 "Player" 标签的角色对象

        currentHealth = maxHealth; // 初始化当前血量为最大血量

        // 获取 AudioSource 组件
        audioSource = GetComponent<AudioSource>();

        // 开始随机移动
        StartCoroutine(RandomMovement());
    }

    void Awake()
    {
        // 获取Sprite Renderer组件
        spriteRenderer = GetComponent<SpriteRenderer>();
        // 保存原始颜色
        originalColor = spriteRenderer.color;
    }



    // 敌人受到伤害时调用的方法
    public void TakeDamage(int damageAmount)
    {

        currentHealth -= damageAmount; // 减少血量

        // 如果检测到攻击，调用Flash方法
        Flash();

        // 如果血量小于等于零，触发死亡逻辑
        if (currentHealth <= 0)
        {
            // 播放die音效
            if (dieSound != null)
            {
                audioSource.PlayOneShot(dieSound);
            }
            Die(); // 触发死亡逻辑
        }
    }

    // 敌人死亡时调用的方法
    void Die()
    {
        // 在这里可以播放死亡动画、播放音效等

       
        // 然后销毁敌人对象
        Destroy(gameObject);
    }

    void Update()
    {
        // 如果角色在检测范围内且敌人没有开始跟随角色
        if (!isFollowing && player != null && Vector2.Distance(transform.position, player.position) <= detectionRadius)
        {
            isFollowing = true; // 开始跟随角色
        }

        // 如果已经开始跟随角色
        if (isFollowing && player != null)
        {
            // 计算敌人朝向角色的方向
            Vector2 direction = player.position - transform.position;
            direction.Normalize();

            // 根据角色的移动方向调整敌人的朝向
            if (direction.x < 0)
            {
                transform.localScale = new Vector3(1, 1, 1); // 向左移动时，翻转敌人的朝向为左
            }
            else if (direction.x > 0)
            {
                transform.localScale = new Vector3(-1, 1, 1); // 向右移动时，翻转敌人的朝向为右
            }

            // 移动敌人朝向角色
            transform.Translate(direction * moveSpeed * Time.deltaTime);
            Debug.Log("跟上啊？？");
        }
        // 如果正在闪烁，更新计时器
        if (isFlashing)
        {
            // 更新计时器
            flashTimer -= Time.deltaTime;
            if (flashTimer <= 0)
            {
                // 停止闪烁并恢复原始颜色
                isFlashing = false;
                spriteRenderer.color = originalColor;
            }
        }

    }

    // 调用这个方法来使敌人闪烁
    public void Flash()
    {
        // 设置闪烁颜色
        spriteRenderer.color = flashColor;
        // 重置计时器
        flashTimer = flashDuration;
        // 开始闪烁
        isFlashing = true;
    }

    IEnumerator RandomMovement()
    {
        while (true)
        {
            // 随机选择两个位置来回移动
            Vector3 targetPosition1 = new Vector3(Random.Range(-100, 101), Random.Range(-100, 101), 0);
            Vector3 targetPosition2 = new Vector3(Random.Range(-100, 101), Random.Range(-100, 101), 0);


            // 移动到第一个目标位置
            yield return MoveToTarget(targetPosition1);

            // 移动到第二个目标位置
            yield return MoveToTarget(targetPosition2);
            if (isFollowing && player != null)
            {
                Debug.Log("没有随机移动了吧？？？");
                break;
            }
        }
    }

    IEnumerator MoveToTarget(Vector3 targetPosition)
    {
        while (Vector3.Distance(transform.position, targetPosition) > 0.1f) // 使用一定的容差来判断是否到达目标位置
        {
            // 计算移动方向
            Vector3 direction = (targetPosition - transform.position).normalized;

            // 移动敌人
            transform.Translate(direction * moveSpeed * Time.deltaTime);

            yield return null; // 等待一帧
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // 检查碰撞对象是否有TilemapCollider2D组件
        if (collision.collider is TilemapCollider2D)
        {
            // 销毁当前的预制体
            Destroy(gameObject);
        }
    }

    // 绘制检测范围的可视化辅助线
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
