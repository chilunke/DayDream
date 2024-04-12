using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StarGame : MonoBehaviour
{

    // 要加载的场景名称
    public string sceneToLoad;

    // 被调用以加载指定的场景
    public void LoadScene()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
