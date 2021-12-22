using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Tetris_block : MonoBehaviour
{
    public Vector3 rotationPoint;
    private float previousTime;
    public float fallTime=0.8f;
    public static int height = 21;
    public static int widht = 10;
    private static Transform[,] grid = new Transform[widht, height];

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            transform.position += new Vector3(-1, 0, 0);
            if (!VailMove())
            {
                transform.position -= new Vector3(-1, 0, 0);
            }
        }

        else if (Input.GetKeyDown(KeyCode.D))
        {
            transform.position += new Vector3(1, 0, 0);
            if (!VailMove())
            {
                transform.position -= new Vector3(1, 0, 0);
            }
        }

        else if (Input.GetKeyDown(KeyCode.W))
        {
            transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 90);
            GameObject.Find("GameController").GetComponent<GameController_kwok>().changeBlockAudio();
            if (!VailMove())
            {
                transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), -90);
                GameObject.Find("GameController").GetComponent<GameController_kwok>().changeBlockAudio();
            }



        }

        if (Time.time - previousTime > (Input.GetKey(KeyCode.S) ? fallTime / 10 : fallTime))
        {
            transform.position += new Vector3(0, -1, 0);
            if (!VailMove())
            {
                transform.position -= new Vector3(0, -1, 0);
                AddToGrid();
                CheckForLines();

                this.enabled = false;
                FindObjectOfType<SpawnTetromino>().NewTetromino();
            }
            previousTime = Time.time;
            GameObject.Find("ScoresText").GetComponent<Text>().text = FindObjectOfType<GameController_kwok>().Scores.ToString();
        }

       
    }


    void CheckForLines()
    {
        for (int i = height - 1; i >= 0; i--)
        {
            if (HasLine(i))
            {
                DeleteLine(i);
                RowDown(i); 
            }
            
        }

    }

    bool HasLine(int i)
    {
        for (int j = 0; j < widht; j++)
        {
            if (grid[j, i] == null)
                return false;
        }

        return true;
    }

    void DeleteLine(int i)
    {
        for (int j = 0; j < widht; j++)
        {
            Destroy(grid[j, i].gameObject); 
            grid[j, i] = null;
            GameObject.Find("GameController").GetComponent<GameController_kwok>().DelLineAudio();
            
        }
    }

    void RowDown(int i)
    {
        for (int y = i; y < height; y++)
        {
            for (int j = 0; j < widht; j++)
            {
                if (grid[j, y] != null)
                {
                    grid[j, y - 1] = grid[j, y];
                    grid[j, y] = null;
                    grid[j, y - 1].transform.position -= new Vector3(0, 1, 0);
                }
            }
        }
        FindObjectOfType<GameController_kwok>().Scores += 50;
    }

    void AddToGrid()
    {
        foreach (Transform children in transform)
        {
            int roundedX = Mathf.RoundToInt(children.transform.position.x);
            int roundedY = Mathf.RoundToInt(children.transform.position.y);

            if (roundedX<widht && roundedY < height)
            {
            grid[roundedX, roundedY] = children;
            GameObject.Find("GameController").GetComponent<GameController_kwok>().falldownAudio();
            }
            else
            {
                SceneManager.LoadScene("GameOver");
            }

        }
    }

    bool VailMove()
    {
        foreach(Transform children in transform)
        {
            int roundedX = Mathf.RoundToInt(children.transform.position.x);
            int roundedY = Mathf.RoundToInt(children.transform.position.y);

            if (roundedX<0 || roundedX >=widht || roundedY < 0 || roundedY >= height)
            {
                return false;
            }
            if (grid[roundedX, roundedY] != null)
                return false;
        }
        return true;
    }
}
