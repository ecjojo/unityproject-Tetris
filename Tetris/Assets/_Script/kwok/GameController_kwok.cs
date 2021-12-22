using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController_kwok : MonoBehaviour
{
    public AudioSource DelLine;
    public AudioSource changeBlock;
    public AudioSource falldown;

    public int Scores = 0;
    // Start is called before the first frame update

    void Update()
    {
        if (Scores >= 200)
        {
            FindObjectOfType<Tetris_block>().fallTime = 0.6f;
        }
        if (Scores >= 500)
        {
            FindObjectOfType<Tetris_block>().fallTime = 0.4f;
        }
        if (Scores >= 800)
        {
            FindObjectOfType<Tetris_block>().fallTime = 0.2f;
        }
        if (Scores >= 1000)
        {
            SceneManager.LoadScene("Win");
        }
        if (FindObjectOfType<Timer>().Timesup == true)
        {
            SceneManager.LoadScene("GameOver");
        }
    }

    public void DelLineAudio()
    {
        DelLine.Play();
    }

    public void changeBlockAudio()
    {
        changeBlock.Play();
    }

    public void falldownAudio()
    {
        falldown.Play();
    }
}
