using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour
{
    [SerializeField] float delay = 0.5f;
    [SerializeField] TranslateAnimation button;
    [SerializeField] GameObject panel;

    public void PlayButton()
    {
        button.Play();
        Invoke(nameof(StartGame), delay);
    }

    public void CreditsButton()
    {
        panel.SetActive(true);
    }

    public void ExitButton()
    {
        Application.Quit();
    }
    private void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            panel.SetActive(false);
        }
    }
}
