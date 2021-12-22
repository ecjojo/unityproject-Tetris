using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ma_Game2_1 : MonoBehaviour
{
    public static int gridWidth = 10;
    public static int gridHeight = 20;

    public static Transform[,] grid = new Transform[gridWidth, gridHeight];

    public int ScoreOneLine = 50;
    public int ScoreTwoLine = 150;
    public int ScoreThreeLine = 300;
    public int ScoreFourLine = 1000;

    public Text hud_score;

    public int currentScore;

    private int numberOfRowsThisTurn = 0;

    private GameObject previewTetromino;
    private GameObject nextTetromino;

    private bool gameStarted = false;

    private Vector2 previewMinoPosition = new Vector2(-4.11f, 17);

    public AudioSource DelLine;
    public AudioSource changeBlock;
    public AudioSource falldown;


    void Start()
    {
        SpawnNextTetromino();
    }

    void Update()
    {
        UpdateScore();
        UpdateUI();
    }

    public void UpdateUI()
    {
        hud_score.text = currentScore.ToString();
    }

    public void UpdateScore()
    {
        if (numberOfRowsThisTurn > 0)
        {
            if (numberOfRowsThisTurn == 1)
            {
                ClearedOneLine();
            }
            else if (numberOfRowsThisTurn == 2)
            {
                ClearedTwoLine();
            }
            else if (numberOfRowsThisTurn == 3)
            {
                ClearedThreeLine();
            }
            else if (numberOfRowsThisTurn == 4)
            {
                ClearedFourLine();
            }

            numberOfRowsThisTurn = 0;
        }
    }

    public void ClearedOneLine()
    {
        currentScore += ScoreOneLine;
    }

    public void ClearedTwoLine()
    {
        currentScore += ScoreTwoLine;
    }
    public void ClearedThreeLine()
    {
        currentScore += ScoreThreeLine;
    }
    public void ClearedFourLine()
    {
        currentScore += ScoreFourLine;
    }

    public bool CheckIsAboveGrid(ma_Tetromino2_1 tetromino)
    {
        for (int x = 0; x < gridWidth; ++x)
        {
            foreach (Transform mino in tetromino.transform)
            {
                Vector2 pos = Round(mino.position);
                if (pos.y > gridHeight - 2)
                {
                    return true;
                }
            }
        }
        return false;
        
    }

    public bool IsFullRow(int y)
    {
        for (int x = 0; x < gridWidth; ++x)
        {
            if (grid[x, y] == null)
            {
                return false;
            }
        }

        numberOfRowsThisTurn++;

        return true;
    }

    public void DeleteMino(int y)
    {
        for (int x = 0; x < gridWidth; ++x)
        {
            Destroy(grid[x, y].gameObject);
            grid[x, y] = null;
        }
    }

    public void MoveRowDown(int y)
    {
        for (int x = 0; x < gridWidth; ++x)
        {
            if (grid[x, y] != null)
            {
                grid[x, y - 1] = grid[x, y];
                grid[x, y] = null;
                grid[x, y - 1].position += new Vector3(0, -1, 0);
            }
        }
    }

    public void MoveAllRowsDown(int y)
    {
        for (int i = y; i < gridHeight; ++i)
        {
            MoveRowDown(i);
        }
    }

    public void DeleteRow()
    {
        for (int y = 0; y < gridHeight; ++y)
        {
            if (IsFullRow(y))
            {
                DeleteMino(y);

                MoveAllRowsDown(y + 1);
                --y;
                DelLineAudio();
            }
        }
    }

    public void UpdateGrid(ma_Tetromino2_1 tetromino)
    {
        for (int y = 0; y < gridHeight; ++y)
        {
            for (int x = 0; x < gridWidth; ++x)
            {
                if (grid[x, y] != null)
                {
                    if (grid[x, y].parent == tetromino.transform)
                    {
                        grid[x, y] = null;
                    }
                }
            }
        }

        foreach (Transform mino in tetromino.transform)
        {
            Vector2 pos = Round(mino.position);

            if (pos.y < gridHeight)
            {
                grid[(int)pos.x, (int)pos.y] = mino;
            }
        }
    }

    public Transform GetTransformAtGridPosition(Vector2 pos)
    {
        if (pos.y > gridHeight - 1)
        {
            return null;
        }
        else
        {
            return grid[(int)pos.x, (int)pos.y];
        }
    }

    public void SpawnNextTetromino()
    {
        if (!gameStarted)
        {
            gameStarted = true;

            nextTetromino = (GameObject)Instantiate(Resources.Load(GetRandomTetromino(), typeof(GameObject)), new Vector2(5.0f, 20.0f), Quaternion.identity);
            previewTetromino = (GameObject)Instantiate(Resources.Load(GetRandomTetromino(), typeof(GameObject)), previewMinoPosition, Quaternion.identity);
            previewTetromino.GetComponent<ma_Tetromino2_1>().enabled = false;

        }
        else
        {
            previewTetromino.transform.localPosition = new Vector2(5.0f, 20.0f);
            nextTetromino = previewTetromino;
            nextTetromino.GetComponent<ma_Tetromino2_1>().enabled = true;

            previewTetromino = (GameObject)Instantiate(Resources.Load(GetRandomTetromino(), typeof(GameObject)), previewMinoPosition, Quaternion.identity);
            previewTetromino.GetComponent<ma_Tetromino2_1>().enabled = false;
        }


    }

    public bool CheckIsInsideGrid(Vector2 pos)
    {
        return ((int)pos.x >= 0 && (int)pos.x < gridWidth && (int)pos.y >= 0);
    }

    public Vector2 Round(Vector2 pos)
    {
        return new Vector2(Mathf.Round(pos.x), Mathf.Round(pos.y));
    }

    string GetRandomTetromino()
    {
        int randomTetromino = Random.Range(1, 8);

        string randomTetrominoName = "Prefabs/Tetromino3_1";

        switch (randomTetromino)
        {
            case 1:
                randomTetrominoName = "Prefabs/Tetromino3_1";
                break;
            case 2:
                randomTetrominoName = "Prefabs/Tetromino3_2";
                break;
            case 3:
                randomTetrominoName = "Prefabs/Tetromino3_3";
                break;
            case 4:
                randomTetrominoName = "Prefabs/Tetromino3_4";
                break;
            case 5:
                randomTetrominoName = "Prefabs/Tetromino3_5";
                break;
            case 6:
                randomTetrominoName = "Prefabs/Tetromino3_6";
                break;
            case 7:
                randomTetrominoName = "Prefabs/Tetromino3_7";
                break;
        }
        return randomTetrominoName;
    }

    public void GameOver()
    {
        SceneManager.LoadScene("P2win");
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
