using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StarGame : MonoBehaviour
{

    // Ҫ���صĳ�������
    public string sceneToLoad;

    // �������Լ���ָ���ĳ���
    public void LoadScene()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
