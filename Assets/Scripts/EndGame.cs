using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    [SerializeField] PlayerController playerController;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerController.canTeleport = false;
            Invoke(nameof(LoadMainMenu), 1.2f);
        }
    }
    private void LoadMainMenu()
    {
        PlayerController.haveKey = false;
        SceneManager.LoadScene(0);
    }
}
