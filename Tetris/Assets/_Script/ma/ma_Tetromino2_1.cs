using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ma_Tetromino2_1 : MonoBehaviour
{
    float fall2 = 0;
    public float fallSpeed2 = 1;
    public bool allowRotation2;
    public bool limitRotation2;
    public string prefabName2;

    private float continuousVerticalSpeed2 = 0.05f;

    private float buttonDownWaitMax2 = 0.2f;

    private float verticalTimer2 = 0;

    private float buttonDownWaitTimer2 = 0;



    public int individualScore2 = 100;

    private float individualScoreTime2;

    void Update()
    {
        CheckUserInput2();

        UpdateindividualScore2();
    }

    void UpdateindividualScore2()
    {
        if (individualScoreTime2 < 1)
        {
            individualScoreTime2 += Time.deltaTime;
        }
        else
        {
            individualScoreTime2 = 0;
            individualScore2 = Mathf.Max(individualScore2 - 10, 0);
        }
    }

    void CheckUserInput2()
    {


        if (Input.GetKeyDown(KeyCode.D))
        {
            transform.position += new Vector3(1, 0, 0);

            if (CheckIsValidPosition2())
            {
                FindObjectOfType<ma_Game2_1>().UpdateGrid(this);
            }
            else
            {
                transform.position += new Vector3(-1, 0, 0);
            }
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            transform.position += new Vector3(-1, 0, 0);

            if (CheckIsValidPosition2())
            {
                FindObjectOfType<ma_Game2_1>().UpdateGrid(this);
            }
            else
            {
                transform.position += new Vector3(1, 0, 0);
            }
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            if (allowRotation2)
            {
                if (limitRotation2)
                {
                    if (transform.rotation.eulerAngles.z >= 90)
                    {
                        transform.Rotate(0, 0, -90);
                    }
                    else
                    {
                        transform.Rotate(0, 0, 90);
                    }
                }
                else
                {
                    transform.Rotate(0, 0, 90);
                }

                if (CheckIsValidPosition2())
                {
                    FindObjectOfType<ma_Game2_1>().UpdateGrid(this);
                }
                else
                {
                    if (transform.rotation.eulerAngles.z >= 90)
                    {
                        transform.Rotate(0, 0, -90);
                    }
                    else
                    {
                        transform.Rotate(0, 0, 90);
                    }
                }

            }
        }


        else if (Input.GetKey(KeyCode.S) || Time.time - fall2 >= fallSpeed2)
        {

            if (buttonDownWaitTimer2 < buttonDownWaitMax2)
            {
                buttonDownWaitTimer2 += Time.deltaTime;
                return;
            }

            if (verticalTimer2 < continuousVerticalSpeed2)
            {
                verticalTimer2 += Time.deltaTime;
                return;
            }


            verticalTimer2 = 0;

            transform.position += new Vector3(0, -1, 0);

            if (CheckIsValidPosition2())
            {
                FindObjectOfType<ma_Game2_1>().UpdateGrid(this);
            }
            else
            {
                transform.position += new Vector3(0, 1, 0);
                FindObjectOfType<ma_Game2_1>().DeleteRow();

                if (FindObjectOfType<ma_Game2_1>().CheckIsAboveGrid(this))
                {
                    FindObjectOfType<ma_Game2_1>().GameOver();
                }

                FindObjectOfType<ma_Game2_1>().SpawnNextTetromino();

                GameObject.Find("Grid").GetComponent<ma_Game2_1>().currentScore += individualScore2;

                enabled = false;
            }

            fall2 = Time.time;
        }
    }

    bool CheckIsValidPosition2()
    {
        foreach (Transform mino in transform)
        {
            Vector2 pos = FindObjectOfType<ma_Game2_1>().Round(mino.position);
            if (FindObjectOfType<ma_Game2_1>().CheckIsInsideGrid(pos) == false)
            {
                return false;
            }
            if (FindObjectOfType<ma_Game2_1>().GetTransformAtGridPosition(pos) != null && FindObjectOfType<ma_Game2_1>().GetTransformAtGridPosition(pos).parent != transform)
            {
                return false;
            }
        }
        return true;
    }
}
