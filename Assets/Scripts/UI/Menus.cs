using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menus : MonoBehaviour
{

    public void LoadTheScene(int sceneNo)
    {
        SceneManager.LoadScene(sceneNo);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
