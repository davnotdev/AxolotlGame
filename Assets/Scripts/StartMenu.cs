using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public GameObject nextScreen;
    public TMP_Text dialogueBox;
    public string nextScene;
    public int initialStepIdx;

    private string[][] dialogue = {
        new string[] {
            "Papa: Louie, my dearest. I have raised you all that I could after your mother left us. It is time.",
            "Papa: You must know about my prized possession.",
            "Papa: My honor! My bike! Mon velo!",
            "Papa: I am too old for this year’s Tour de Axolotl. And too drunk.",
            "Papa: It is yours. Keep it, and remember me."
        },
        new string[] {
            "Louie: Papa? Where are you going?",
            "Papa: Away, Louie. I must make haste. (I’ve lost my stash...)",
            "Louie: Papa!",
            "Papa: Goodbye, Louie."
        },
        new string[] {
            "Louie: I can finally bike on my own now. It’s time to find Papa.",
            "Louie: The only way I can find him is through the Tour de Axolotl. It was the last thing he talked about…"
        }
    };

    private int stepIdx;
    private int dialogueIdx = 0;

    public void Awake() 
    {
        stepIdx = initialStepIdx;
    }

    public void Next() 
    {
        if (dialogueIdx >= dialogue[stepIdx].Length) {
            dialogueBox.text = "";
            if (stepIdx == 0) 
            {
                nextScreen.SetActive(true);
                stepIdx++;
                dialogueIdx = 0;
            } 
            else 
            {
                SceneManager.LoadScene(nextScene);
            }
        } 
        else 
        {
            dialogueBox.text = dialogue[stepIdx][dialogueIdx];
            dialogueIdx++;
        }
    }

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
