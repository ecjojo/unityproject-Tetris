using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ma_Game2 : MonoBehaviour
{
    public static int gridWidth2 = 10;
    public static int gridHeight2 = 20;

    public static Transform[,] grid2 = new Transform[gridWidth2, gridHeight2];

    public int ScoreOneLine2 = 50;
    public int ScoreTwoLine2 = 150;
    public int ScoreThreeLine2 = 300;
    public int ScoreFourLine2 = 1000;

    public Text hud_score2;

    public int currentScore2;

    private int numberOfRowsThisTurn2 = 0;

    private GameObject previewTetromino2;
    private GameObject nextTetromino2;

    private bool gameStarted2 = false;

    private Vector2 previewMinoPosition2 = new Vector2(28.5f, 17);


    void Start()
    {
        SpawnNextTetromino2();
    }

    void Update()
    {
        UpdateScore2();
        UpdateUI2();
    }

    public void UpdateUI2()
    {
        hud_score2.text = currentScore2.ToString();
    }

    public void UpdateScore2()
    {
        if (numberOfRowsThisTurn2 > 0)
        {
            if (numberOfRowsThisTurn2 == 1)
            {
                ClearedOneLine2();
            }
            else if (numberOfRowsThisTurn2 == 2)
            {
                ClearedTwoLine2();
            }
            else if (numberOfRowsThisTurn2 == 3)
            {
                ClearedThreeLine2();
            }
            else if (numberOfRowsThisTurn2 == 4)
            {
                ClearedFourLine2();
            }

            numberOfRowsThisTurn2 = 0;
        }
    }

    public void ClearedOneLine2()
    {
        currentScore2 += ScoreOneLine2;
    }

    public void ClearedTwoLine2()
    {
        currentScore2 += ScoreTwoLine2;
    }
    public void ClearedThreeLine2()
    {
        currentScore2 += ScoreThreeLine2;
    }
    public void ClearedFourLine2()
    {
        currentScore2 += ScoreFourLine2;
    }

    public bool CheckIsAboveGrid2(ma_Tetromino2 tetromino)
    {
        for (int x = 0; x < gridWidth2; ++x)
        {
            foreach (Transform mino in tetromino.transform)
            {
                Vector2 pos = Round(mino.position);
                if (pos.y > gridHeight2 - 2)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public bool IsFullRow2(int y)
    {
        for (int x = 0; x < gridWidth2; ++x)
        {
            if (grid2[x, y] == null)
            {
                return false;
            }
        }

        numberOfRowsThisTurn2++;

        return true;
    }

    public void DeleteMino2(int y)
    {
        for (int x = 0; x < gridWidth2; ++x)
        {
            Destroy(grid2[x, y].gameObject);
            grid2[x, y] = null;
        }
    }

    public void MoveRowDown2(int y)
    {
        for (int x = 0; x < gridWidth2; ++x)
        {
            if (grid2[x, y] != null)
            {
                grid2[x, y - 1] = grid2[x, y];
                grid2[x, y] = null;
                grid2[x, y - 1].position += new Vector3(0, -1, 0);
            }
        }
    }

    public void MoveAllRowsDown2(int y)
    {
        for (int i = y; i < gridHeight2; ++i)
        {
            MoveRowDown2(i);
        }
    }

    public void DeleteRow2()
    {
        for (int y = 0; y < gridHeight2; ++y)
        {
            if (IsFullRow2(y))
            {
                DeleteMino2(y);

                MoveAllRowsDown2(y + 1);
                --y;
            }
        }
    }

    public void UpdateGrid2(ma_Tetromino2 tetromino2)
    {
        for (int y = 0; y < gridHeight2; ++y)
        {
            for (int x = 0; x < gridWidth2; ++x)
            {
                if (grid2[x, y] != null)
                {
                    if (grid2[x, y].parent == tetromino2.transform)
                    {
                        grid2[x, y] = null;
                    }
                }
            }
        }

        foreach (Transform mino in tetromino2.transform)
        {
            Vector2 pos = Round(mino.position);

            if (pos.y < gridHeight2)
            {
                grid2[(int)pos.x, (int)pos.y] = mino;
            }
        }
    }

    public Transform GetTransformAtGridPosition2(Vector2 pos)
    {
        if (pos.y > gridHeight2 - 1)
        {
            return null;
        }
        else
        {
            return grid2[(int)pos.x, (int)pos.y];
        }
    }

    public void SpawnNextTetromino2()
    {
        if (!gameStarted2)
        {
            gameStarted2 = true;

            nextTetromino2 = (GameObject)Instantiate(Resources.Load(GetRandomTetromino(), typeof(GameObject)), new Vector2(5.0f, 20.0f), Quaternion.identity);
            previewTetromino2 = (GameObject)Instantiate(Resources.Load(GetRandomTetromino(), typeof(GameObject)), previewMinoPosition2, Quaternion.identity);
            previewTetromino2.GetComponent<ma_Tetromino2>().enabled = false;

        }
        else
        {
            previewTetromino2.transform.localPosition = new Vector2(5.0f, 20.0f);
            nextTetromino2 = previewTetromino2;
            nextTetromino2.GetComponent<ma_Tetromino2>().enabled = true;

            previewTetromino2 = (GameObject)Instantiate(Resources.Load(GetRandomTetromino(), typeof(GameObject)), previewMinoPosition2, Quaternion.identity);
            previewTetromino2.GetComponent<ma_Tetromino2>().enabled = false;
        }


    }

    public bool CheckIsInsideGrid(Vector2 pos)
    {
        return ((int)pos.x >= 0 && (int)pos.x < gridWidth2 && (int)pos.y >= 0);
    }

    public Vector2 Round(Vector2 pos)
    {
        return new Vector2(Mathf.Round(pos.x), Mathf.Round(pos.y));
    }

    string GetRandomTetromino()
    {
        int randomTetromino = Random.Range(1, 8);

        string randomTetrominoName = "Prefabs/Tetromino2_1";

        switch (randomTetromino)
        {
            case 1:
                randomTetrominoName = "Prefabs/Tetromino2_1";
                break;
            case 2:
                randomTetrominoName = "Prefabs/Tetromino2_2";
                break;
            case 3:
                randomTetrominoName = "Prefabs/Tetromino2_3";
                break;
            case 4:
                randomTetrominoName = "Prefabs/Tetromino2_4";
                break;
            case 5:
                randomTetrominoName = "Prefabs/Tetromino2_5";
                break;
            case 6:
                randomTetrominoName = "Prefabs/Tetromino2_6";
                break;
            case 7:
                randomTetrominoName = "Prefabs/Tetromino2_7";
                break;
        }
        return randomTetrominoName;
    }

    public void GameOver2()
    {
        SceneManager.LoadScene("P1win");
    }
}
