using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool paused = false;
    [SerializeField] GameObject pauseMenu;
    
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
        Time.timeScale = 0f;
        paused = true;
    }
    public void ResumeButton()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        paused = false;
    }

    public void MainMenuButton()
    {
        Time.timeScale = 1f;
        paused = false;
        SceneManager.LoadScene(0);
    }

    public void SettingsButton()
    {

    }

    public void ExitButton()
    {
        Application.Quit();
    }
}
