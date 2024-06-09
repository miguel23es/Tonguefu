using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public float spinSpeed = 2f;
    public Transform tree;
    private string sceneName = "Level1"; // Name of the scene to load

    public void Update()
    {
        tree.Rotate(0f, spinSpeed * Time.deltaTime, 0f);
    }
    public void StartGame()
    {
        SceneManager.LoadScene(sceneName);
    }

    //This only works in standalone builds, doesn't work when testing in unity editor
    public void QuitGame()
    {
        Application.Quit();
    }

    public void HowToPlay()
    {

    }

}
