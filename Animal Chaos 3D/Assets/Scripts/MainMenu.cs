using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Button playButton, exitButton;
    private void OnEnable()
    {
        playButton.onClick.AddListener(Play);
        exitButton.onClick.AddListener(Exit);
    }

    private void OnDisable()
    {
        playButton.onClick.RemoveListener(Play);
        exitButton.onClick.RemoveListener(Exit);
    }

    public void Play()
    {
        SceneManager.LoadScene("Gameplay");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
