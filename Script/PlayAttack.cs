using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAttack : MonoBehaviour
{

    public int damageAmount = 10; // 攻击造成的伤害
    public AudioClip attackSound; // 攻击音效
    private AudioSource audioSource; // 声音播放器
    private int attackCounter = 0; // 攻击次数计数器
    public Animator otherAnimator; // 其他对象的Animator组件
    public float animationDuration = 2f; // 动画播放持续时间

    void Start()
    {
        // 获取 AudioSource 组件
        audioSource = GetComponent<AudioSource>();
    }

    // 当角色与敌人发生碰撞时调用
    void OnTriggerEnter2D(Collider2D other)
    {
        // 如果碰撞的对象是敌人
        if (other.CompareTag("Enemy"))
        {

            // 播放攻击音效
            if (attackSound != null)
            {
                audioSource.PlayOneShot(attackSound);
            }

            // 调用敌人的受伤逻辑，造成伤害
            other.GetComponent<EnemyFollow>().TakeDamage(damageAmount);
            // 增加攻击次数计数器
            attackCounter++;

            // 检查攻击次数是否达到20次
            if (attackCounter >= 40)
            {
                // 如果有其他对象的Animator组件被赋值
                if (otherAnimator != null)
                {
                    // 播放其他对象的动画
                    otherAnimator.SetBool("isBigAttack", true);
                    // 开始处理动画播放后的逻辑
                    StartCoroutine(HandleAnimationEnd());
                }
             
            }
            else
            {
                // 如果有其他对象的Animator组件被赋值
                if (otherAnimator != null)
                {
                    Debug.Log("为什么没有播放？？？");
                    // 播放其他对象的动画
                    otherAnimator.SetBool("isBigAttack", false); // 假设动画触发器名为"PlayAnimation"
                }
            }

            Debug.Log("Attack count: " + attackCounter);
        }
    }
    IEnumerator HandleAnimationEnd()
    {
        // 等待动画播放持续时间
        yield return new WaitForSeconds(animationDuration);

        // 动画播放完毕后，重置相关参数
        if (otherAnimator != null)
        {
            otherAnimator.SetBool("isBigAttack", false);
        }

        // 重置攻击次数计数器
        attackCounter = 0;
    }
}
