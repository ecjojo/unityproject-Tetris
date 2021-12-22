using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ma_Tetromino : MonoBehaviour
{
    float fall = 0;
    public float fallSpeed = 1;
    public bool allowRotation ;
    public bool limitRotation ;
    public string prefabName;

    private float continuousVerticalSpeed = 0.05f;

    private float buttonDownWaitMax = 0.2f;

    private float verticalTimer = 0;

    private float buttonDownWaitTimer = 0;

 

    public int individualScore = 100;

    private float individualScoreTime;

    void Update()
    {
        CheckUserInput();

        UpdateindividualScore();
    }

    void UpdateindividualScore()
    {
        if (individualScoreTime < 1)
        {
            individualScoreTime += Time.deltaTime;
        }
        else
        {
            individualScoreTime = 0;
            individualScore = Mathf.Max(individualScore - 10, 0);
        }
    }

    void CheckUserInput()
    {
        

        if (Input.GetKeyDown(KeyCode.D))
        {
            transform.position += new Vector3(1, 0, 0);

            if (CheckIsValidPosition())
            {
                FindObjectOfType<ma_Game>().UpdateGrid(this);
            }
            else
            {
                transform.position += new Vector3(-1, 0, 0);
            }
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {         
            transform.position += new Vector3(-1, 0, 0);

            if (CheckIsValidPosition())
            {
                FindObjectOfType<ma_Game>().UpdateGrid(this);
            }
            else
            {
                transform.position += new Vector3(1, 0, 0);
            }
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            if (allowRotation) {
                if (limitRotation)
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

                 if (CheckIsValidPosition())
                 {
                    FindObjectOfType<ma_Game>().UpdateGrid(this);
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
            GameObject.Find("Grid").GetComponent<ma_Game>().changeBlockAudio();
        }
          
        
        else if (Input.GetKey(KeyCode.S) || Time.time-fall>=fallSpeed)
        {
           
                if (buttonDownWaitTimer < buttonDownWaitMax)
            {
                buttonDownWaitTimer += Time.deltaTime;
                return;
            }

            if (verticalTimer < continuousVerticalSpeed)
            {
                verticalTimer += Time.deltaTime;
                return;
            }
  
         
            verticalTimer = 0;

            transform.position += new Vector3(0, -1, 0);

            if (CheckIsValidPosition())
            {
                FindObjectOfType<ma_Game>().UpdateGrid(this);
            }
            else
            {
                transform.position += new Vector3(0, 1, 0);
                FindObjectOfType<ma_Game>().DeleteRow();

                if (FindObjectOfType<ma_Game>().CheckIsAboveGrid(this))
                {
                    FindObjectOfType<ma_Game>().GameOver();
                }
                GameObject.Find("Grid").GetComponent<ma_Game>().falldownAudio();
                FindObjectOfType<ma_Game>().SpawnNextTetromino();

                ma_Game.currentScore += individualScore;

                enabled = false;
            }

            fall = Time.time;
        }
    }

    bool CheckIsValidPosition()
    {
        foreach(Transform mino in transform)
        {
            Vector2 pos = FindObjectOfType<ma_Game>().Round(mino.position);
            if (FindObjectOfType<ma_Game>().CheckIsInsideGrid(pos) == false)
            {
               return false;
            }
            if (FindObjectOfType<ma_Game>().GetTransformAtGridPosition(pos)!=null&& FindObjectOfType<ma_Game>().GetTransformAtGridPosition(pos).parent != transform)
            {
                return false;
            }
        }
        return true;
    }
}
