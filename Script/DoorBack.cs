using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorBack : MonoBehaviour
{
    public string targetScene; // Ŀ�곡��������
    public Vector3 targetDoorPosition; // Ŀ�곡���е���λ��

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // ȷ����ײ�Ķ��������
        {
            DontDestroyOnLoad(other.gameObject); // ȷ����Ҷ����ڳ�������ʱ��������
            SceneManager.LoadScene(targetScene); // ����Ŀ�곡��
            SceneManager.sceneLoaded += OnSceneLoaded; // ע�᳡��������ɺ�Ļص�
        }
    }

    // ����������ɺ�Ļص�����
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == targetScene)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player"); // �ҵ���Ҷ���
            player.transform.position = targetDoorPosition; // �������λ��ΪĿ���ŵ�λ��
            SceneManager.sceneLoaded -= OnSceneLoaded; // ȡ��ע��ص��������ظ�����
        }
    }


}
