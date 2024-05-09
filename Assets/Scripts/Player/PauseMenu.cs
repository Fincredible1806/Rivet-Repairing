using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    private KeyCode pauseCode = KeyCode.Escape;
    public GameObject PauseCanvas;
    public GameObject otherCanvas;
    private bool isPaused = false;
    [SerializeField] GameObject playerRoot;
    [SerializeField] GameObject cam01;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        isPaused = false;
        playerRoot.SetActive(true);
    }
    private void Update()
    {
        if(Input.GetKeyDown(pauseCode))
        {
            if(isPaused)
            {
                PlayGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void ReturnToMenu(int sceneNo)
    {
        SceneManager.LoadScene(sceneNo);
    }

    public void PauseGame()
    {
        playerRoot.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        isPaused = true;
        PauseCanvas.SetActive(true);
        otherCanvas.SetActive(false);
        Time.timeScale = 0f;
        cam01.SetActive(true);
    }

    public void PlayGame()
    {
        playerRoot.SetActive(true);
        isPaused = false;
        PauseCanvas.SetActive(false);
        otherCanvas.SetActive(true);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        cam01.SetActive(false);
    }

    public void GameQuitter()
    {
        Application.Quit();
    }

}
