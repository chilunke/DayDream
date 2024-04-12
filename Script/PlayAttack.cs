using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAttack : MonoBehaviour
{

    public int damageAmount = 10; // ������ɵ��˺�
    public AudioClip attackSound; // ������Ч
    private AudioSource audioSource; // ����������
    private int attackCounter = 0; // ��������������
    public Animator otherAnimator; // ���������Animator���
    public float animationDuration = 2f; // �������ų���ʱ��

    void Start()
    {
        // ��ȡ AudioSource ���
        audioSource = GetComponent<AudioSource>();
    }

    // ����ɫ����˷�����ײʱ����
    void OnTriggerEnter2D(Collider2D other)
    {
        // �����ײ�Ķ����ǵ���
        if (other.CompareTag("Enemy"))
        {

            // ���Ź�����Ч
            if (attackSound != null)
            {
                audioSource.PlayOneShot(attackSound);
            }

            // ���õ��˵������߼�������˺�
            other.GetComponent<EnemyFollow>().TakeDamage(damageAmount);
            // ���ӹ�������������
            attackCounter++;

            // ��鹥�������Ƿ�ﵽ20��
            if (attackCounter >= 40)
            {
                // ��������������Animator�������ֵ
                if (otherAnimator != null)
                {
                    // ������������Ķ���
                    otherAnimator.SetBool("isBigAttack", true);
                    // ��ʼ���������ź���߼�
                    StartCoroutine(HandleAnimationEnd());
                }
             
            }
            else
            {
                // ��������������Animator�������ֵ
                if (otherAnimator != null)
                {
                    Debug.Log("Ϊʲôû�в��ţ�����");
                    // ������������Ķ���
                    otherAnimator.SetBool("isBigAttack", false); // ���趯����������Ϊ"PlayAnimation"
                }
            }

            Debug.Log("Attack count: " + attackCounter);
        }
    }
    IEnumerator HandleAnimationEnd()
    {
        // �ȴ��������ų���ʱ��
        yield return new WaitForSeconds(animationDuration);

        // ����������Ϻ�������ز���
        if (otherAnimator != null)
        {
            otherAnimator.SetBool("isBigAttack", false);
        }

        // ���ù�������������
        attackCounter = 0;
    }
}
