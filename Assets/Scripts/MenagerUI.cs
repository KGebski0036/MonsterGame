using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenagerUI : MonoBehaviour
{
    [SerializeField] string testScene;
    [SerializeField] string gameScene;

    public void LoadTestScene()
    {
        SceneManager.LoadScene(testScene);

    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene(gameScene);
    }
}
