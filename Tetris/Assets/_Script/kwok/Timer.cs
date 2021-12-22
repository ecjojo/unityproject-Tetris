using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float timeRemaining = 180;
    public bool Timesup;
    public GameObject TimeUP;
    public GameObject GameTimer;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timeRemaining -= Time.deltaTime;
    }
    private void OnGUI()
    {
        if (timeRemaining > 0)
        {
            // GUI.Label(new Rect(400, 10, 200, 100), "Time Remaining :" + timeRemaining);
            GameObject.Find("GameTime").GetComponent<Text>().text = timeRemaining.ToString("f0");

        }
        else
        {
            //GUI.Label(new Rect(400, 10, 200, 100), "Time's up");
            TimeUP.SetActive(true);
            GameTimer.SetActive(false);
            Timesup = true;
        }
    }
}
