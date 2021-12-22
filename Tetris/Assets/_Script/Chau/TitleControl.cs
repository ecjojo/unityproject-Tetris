using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleControl : MonoBehaviour
{
    public static TitleControl instance;

    public Text P1BestScore, P2BestScore, TMBestScore;
    public GameObject textpanel_How,textpanel_Record,textpanel_Credits;

    void Start()
    {
        instance = this;
        P1BestScore.text = "" + PlayerPrefs.GetInt("P2BestScore");
        P2BestScore.text = "" + PlayerPrefs.GetInt("P2BestScore");
        TMBestScore.text = "" + PlayerPrefs.GetString("TMBestScore");
    }
    //----------Clean Score
    public void _Button_Clear_Score()
    {
        PlayerPrefs.SetInt("P2BestScore",0);
        PlayerPrefs.SetInt("P2BestScore", 0);
        PlayerPrefs.SetString("TMBestScore", "00:00");
        SceneManager.LoadScene("TitleScene");
    }

    //----------Button
    public void _Button_Howtoplay()
    {
        textpanel_How.SetActive(true);
    }

    public void _Button_HighestRecord()
    {
        textpanel_Record.SetActive(true);
    }

    public void _Button_Credits()
    {
        textpanel_Credits.SetActive(true);
    }

    public void _Button_Closepanel()
    {
        textpanel_How.SetActive(false);
        textpanel_Record.SetActive(false);
        textpanel_Credits.SetActive(false);
    }

    //--------------------------------LoadScene Button
    public void _Button_GM_one()
    {
        SceneManager.LoadScene("GameScene_onePlayer(ma)");
    }

    public void _Button_GM_two()
    {
        SceneManager.LoadScene("GameScene_twoPlayer(ma)");
    }

    public void _Button_GM_Score()
    {
        SceneManager.LoadScene("soloplayer(scores)");
    }
    //-------
    public void _Button_Exitgame()
    {
        //Application.Quit();
        Debug.Log("Game Quit");
    }
}
