using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneNavigator : MonoBehaviour
{
    public void NavHighScores()
    {
        SceneManager.LoadScene("Highscores");
    }

    public void NavPlay()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void NavMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
