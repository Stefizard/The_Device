using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool paused = false;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject UI;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused)
            {
                ResumeButton();
            }
            else
            {
                Pause();
            }
        }
    }

    void Pause()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        pauseMenu.SetActive(true);
        UI.SetActive(false);
        Time.timeScale = 0f;
        paused = true;
    }
    public void ResumeButton()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        pauseMenu.SetActive(false);
        UI.SetActive(true);
        Time.timeScale = 1f;
        paused = false;
    }
    public void RestartButton()
    {
        Time.timeScale = 1f;
        paused = false;
        if (SceneManager.GetActiveScene().name == "Level 2")
        {
            PlayerController.haveKey = false;
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenuButton()
    {
        Time.timeScale = 1f;
        paused = false;
        PlayerController.haveKey = false;
        SceneManager.LoadScene(0);
    }

    public void ExitButton()
    {
        Application.Quit();
    }
}
