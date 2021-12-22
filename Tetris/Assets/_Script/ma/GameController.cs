using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("Grid").GetComponent<ma_Game2_1>().currentScore > GameObject.Find("Grid2").GetComponent<ma_Game2>().currentScore2)
        {
            SceneManager.LoadScene("P1win");
        }
        else if (GameObject.Find("Grid").GetComponent<ma_Game2_1>().currentScore < GameObject.Find("Grid2").GetComponent<ma_Game2>().currentScore2)
        {
            SceneManager.LoadScene("P2win");
        }

    }
}
