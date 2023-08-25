using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] float delay = 0.5f;
    [SerializeField] TranslateAnimation button;

    public void PlayButton()
    {
        button.Play();
        Invoke(nameof(StartGame), delay);
    }

    public void SettingsButton()
    {

    }

    public void ExitButton()
    {
        Application.Quit();
    }
    private void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
