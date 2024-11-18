using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public void startGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void Cutscene1_2()
    {
        SceneManager.LoadScene("Cutscene1_2");
    }

    public void Cutscene3_4()
    {
        SceneManager.LoadScene("Cutscene3_4");
    }


    public void quit()
    {
        Application.Quit();
    }
}
